
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WarehouseSystemTest.Infrastructure.Shared
{
    public static class Utils
    {
        private static readonly Dictionary<string, Delegate> _getters = new();

        public static string ToSnakeCase(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            // Replace spaces with underscores first
            str = str.Replace(" ", "_");

            // Convert PascalCase/camelCase to snake_case
            var result = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

            // Remove duplicate underscores and convert to lowercase
            while (result.Contains("__"))
            {
                result = result.Replace("__", "_");
            }

            return result.ToLower();
        }

        public static string RandStr(int length)
        {
            var random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string JsonSerialize(object json)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            return JsonConvert.SerializeObject(json);
        }

        public static T JsonDeserialize<T>(string json)
        {
            var responseJson = JsonConvert.DeserializeObject<T>(json);
            return responseJson;
        }

        public static T JsonDeserializeResponse<T>(HttpContent response)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            using var reader = new StreamReader(response.ReadAsStream());
            var responseBody = reader.ReadToEnd();
            var responseJson = JsonConvert.DeserializeObject<T>(responseBody.ToString());

            return responseJson;
        }

        public static void BackgroundProcessThreadAsync(Func<Task> func)
        {
            Thread thread = new(async () => { await func(); });
            thread.Start();
        }

        public static void BackgroundProcessThreadSync(Func<bool> func)
        {
            Thread thread = new(() => { func(); });
            thread.Start();
        }

        public static string MoveFileToTemp(IFormFile file, string folderPath)
        {
            string originalName = Path.GetFileName(file.FileName);
            var tempFilePath = "temp/" + folderPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + originalName;
            using (FileStream fs = File.Create(tempFilePath))
            {
                file.CopyTo(fs);
            }

            return tempFilePath;
        }

        public static bool IsSuccessStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode <= 299;
        }

        public static string MoveFileToStorage(IFormFile file, string storagePath, string folderPath)
        {
            var originalName = Path.GetFileName(file.FileName);
            var filePath = folderPath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + originalName;
            var storageFilePath = storagePath + "/" + filePath;
            using (FileStream fs = File.Create(storageFilePath))
            {
                file.CopyTo(fs);
            }

            return filePath;
        }

        public static List<string> MoveFilesToTemp(IFormFile[] files, string filePath)
        {
            var tempFilePaths = new List<string>();

            foreach (var file in files)
            {
                string originalName = Path.GetFileName(file.FileName);
                var tempFilePath = "temp/" + filePath + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + originalName;
                using (FileStream fs = File.Create(tempFilePath))
                {
                    file.CopyTo(fs);
                }

                tempFilePaths.Add(tempFilePath);
            }

            return tempFilePaths;
        }

        public static string GetFileExtension(IFormFile file)
        {
            string originalName = Path.GetFileName(file.FileName);
            return originalName.Split('.').Last();
        }

        public static string GetFileNameFromPath(string filePath)
        {
            return filePath.Split('/').Last();
        }

        public static byte[] ParseObjectToByte(Object obj)
        {
            BinaryFormatter bf = new();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T ParseByteToObject<T>(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return (T)obj;
            }
        }


        public static int CountPage(int totalData, int take)
        {
            var totalDataDec = Decimal.Parse(string.Concat(totalData));
            var takeDec = Decimal.Parse(string.Concat(take));


            return (int)Math.Ceiling(totalDataDec / take);
        }

        public static Guid GetUserLoggedId(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst("id")?.Value;
            if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

        public static Guid GetUserLoggedRoleId(IHttpContextAccessor httpContextAccessor)
        {
            var rolesString = httpContextAccessor.HttpContext!.User.FindFirst("role_ids")?.Value;
            List<Guid> roleIds = string.IsNullOrEmpty(rolesString)
                    ? [] : System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(rolesString);
            var roleId = roleIds.FirstOrDefault();

            return roleId;
        }

        public static void ValidateRequiredProperties<T>(
            List<T> rows,
            List<string> errors,
            string domain,
            ILogger logger)
        {
            Type type = typeof(T);
            var props = type.GetProperties();

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                var errPerFields = new Dictionary<string, object>();
                var logRequiredFiedls = new List<string>();

                foreach (var prop in props)
                {
                    if (Attribute.IsDefined(prop, typeof(RequiredAttribute)))
                    {
                        var getter = (Func<T, object>)GetOrCreateGetter<T>(prop);

                        var value = getter(row);
                        logRequiredFiedls.Add($"Validate Required Property: {prop.Name} = {value}");
                        if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                        {
                            errPerFields.Add(prop.Name, value);
                        }
                    }
                }

                logger.LogInformation("{messages}", string.Join("\n", logRequiredFiedls));

                if (errPerFields.Count > 0)
                {
                    string keys = string.Join(",", errPerFields.Keys);
                    errors.Add($"Required fields of {domain} for this row: {i + 2} is invalid. Value for [{keys}] must not empty");
                }
            }
        }

        private static Delegate GetOrCreateGetter<T>(PropertyInfo prop)
        {
            string key = $"{typeof(T).FullName}.{prop.Name}.getter";
            if (!_getters.TryGetValue(key, out Delegate getter))
            {
                var method = prop.GetGetMethod();

                if (method != null)
                {
                    var paramObj = Expression.Parameter(typeof(T), "obj");
                    var callMethod = Expression.Call(paramObj, method);
                    var convert = Expression.Convert(callMethod, typeof(object));
                    var lambda = Expression.Lambda<Func<T, object>>(convert, paramObj);
                    getter = lambda.Compile();
                }

                _getters[key] = getter;
            }

            return getter;
        }

        public static List<string> GetClassPublicPropertyNames<T>()
        {
            return typeof(T)
                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Select(p => p.Name)
                .ToList();
        }

        public static void ThrowIfHaveErrors(List<string> errors)
        {
            if (errors.Count > 0)
            {
                throw new UnprocessableEntityException(string.Join("\n", errors));
            }
        }

        public static List<string> ValidateHeaders<T>(List<string> rawHeaders)
        {
            var headers = rawHeaders
                .Where(x => !string.IsNullOrEmpty(x) || !string.IsNullOrWhiteSpace(x))
                .ToList();
            List<string> dtoProps = GetClassPublicPropertyNames<T>()
                .Order()
                .ToList();

            if (headers.Count != dtoProps.Count || !dtoProps.SequenceEqual(headers.Order()))
            {
                throw new UnprocessableEntityException("The uploaded template is not suitable");
            }

            return headers;
        }

        public static bool ParseBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var normalizedValue = value.Trim().ToLower();
            return normalizedValue == "1" || normalizedValue.Equals("true") || normalizedValue.Equals("yes");
        }

        public static Guid GetUserId(this IHttpContextAccessor ctx)
        {
            _ = Guid.TryParse(ctx.HttpContext.User.FindFirst("id")?.Value, out Guid userId);
            return userId;
        }

        public static string GetUserName(this IHttpContextAccessor ctx)
        {
            return ctx.HttpContext.User.FindFirst("Name")?.Value;
        }

        public static List<string> GetUserPermissions(this IHttpContextAccessor ctx)
        {
            var permissionsString = ctx.HttpContext?.User.FindFirst("permissions")?.Value;
            if (string.IsNullOrEmpty(permissionsString)) return [];

            return JsonDeserialize<List<string>>(permissionsString);
        }

        public static List<Guid> GetUserRoleIds(this IHttpContextAccessor httpContextAccessor)
        {
            var rolesString = httpContextAccessor.HttpContext.User.FindFirst("role_ids")?.Value;
            if (string.IsNullOrEmpty(rolesString))
            {
                return [];
            }

            return JsonDeserialize<List<Guid>>(rolesString);
        }
    }
}

namespace WarehouseSystemTest.Domain.Warehouse.Dto
{
    public class WarehouseResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public WarehouseResultDto(Models.Warehouse warehouse)
        {
            Id = warehouse?.Id ?? Guid.Empty;
            Name = warehouse.Name;
            City = warehouse.City;
            Address = warehouse.Address;
        }

        public static List<WarehouseResultDto> MapRepo(List<Models.Warehouse> data)
        {
            return data?.Select(data => new WarehouseResultDto(data)).ToList() ?? [];
        }
    }
}

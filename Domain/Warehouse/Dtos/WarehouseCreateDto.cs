namespace WarehouseSystemTest.Domain.Warehouse.Dto
{
    public class WarehouseCreateDto
    {

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public static WarehouseSystemTest.Models.Warehouse ToModel(WarehouseCreateDto data)
        {
            var result = new WarehouseSystemTest.Models.Warehouse
            {
                Name = data.Name,
                City = data.City,
                Address = data.Address
            };
            return result;
        }
    }

}

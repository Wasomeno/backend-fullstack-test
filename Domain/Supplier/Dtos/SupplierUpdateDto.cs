namespace WarehouseSystemTest.Domain.Supplier.Dto
{
    public class SupplierUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public static Models.Supplier ToModel(SupplierUpdateDto data) =>
            new Models.Supplier
            {
                Name = data.Name,
                Address = data.Address,
                PhoneNumber = data.PhoneNumber,
                Email = data.Email,
            };
    }
}

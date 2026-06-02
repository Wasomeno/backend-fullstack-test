namespace WarehouseSystemTest.Domain.Supplier.Dto
{
    public class SupplierResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public SupplierResultDto(Models.Supplier supplier)
        {
            Id = supplier.Id;
            Name = supplier.Name;
            Address = supplier.Address;
            PhoneNumber = supplier.PhoneNumber;
            Email = supplier.Email;
        }

        public static List<SupplierResultDto> MapRepo(List<Models.Supplier> data) =>
            data?.Select(d => new SupplierResultDto(d)).ToList() ?? [];
    }
}

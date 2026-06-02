using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystemTest.Models
{
    [Table("supplier")]
    public class Supplier
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        [Column("address")]
        [MaxLength(256)]
        [Required]
        public string Address { get; set; }

        [Column("phone_number")]
        [MaxLength(12)]
        [Required]
        public string PhoneNumber { get; set; }

        [Column("email")]
        [MaxLength(256)]
        [Required]
        public string Email { get; set; }

    }
}

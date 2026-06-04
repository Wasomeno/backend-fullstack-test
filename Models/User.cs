using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystemTest.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("email")]
        [MaxLength(256)]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("name")]
        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("password")]
        [MaxLength(256)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Column("role")]
        [MaxLength(50)]
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}

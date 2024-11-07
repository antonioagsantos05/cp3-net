using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_")]
    public class BarcoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] 
        public string Nome { get; set; }

        [Required]
        [StringLength(100)] 
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public double Tamanho { get; set; }
    }
}

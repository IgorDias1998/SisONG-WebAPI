using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SisONGp2.Model
{
    public class Doador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoadorId { get; set; }
        public string NomeDoador { get; set; }
        public string Username { get; set; }
        public string EmailDoador { get; set; }
        public string Senha { get; set; }
    }
}

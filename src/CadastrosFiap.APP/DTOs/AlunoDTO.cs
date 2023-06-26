using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CadastrosFiap.APP.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; } 

        //[NotMapped]
        //[JsonIgnore]
        //public bool? SenhaIsValid { get; set; }

    }
}

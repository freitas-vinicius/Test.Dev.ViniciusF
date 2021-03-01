using System.ComponentModel.DataAnnotations;

namespace Test.Dev.ViniciusF.Models
{
    public class Studant : EntityBase
    {
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string LastName { get; set; }

    }
}

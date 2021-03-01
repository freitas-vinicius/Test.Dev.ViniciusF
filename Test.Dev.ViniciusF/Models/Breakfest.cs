using System.ComponentModel.DataAnnotations;

namespace Test.Dev.ViniciusF.Models
{
    public class Breakfest : EntityBase
    {
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public int Capacity { get; set; }
    }
}

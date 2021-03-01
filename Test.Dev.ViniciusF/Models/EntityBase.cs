using System.ComponentModel.DataAnnotations;

namespace Test.Dev.ViniciusF.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}

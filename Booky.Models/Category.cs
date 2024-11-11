using System.ComponentModel.DataAnnotations;

namespace Booky.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [Range(1, 100)]
        public int DisplayOrder {  get; set; }

    }
}

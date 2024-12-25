using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR30323.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public required string Description { get; set; } 
        public string? Image { get; set; } 

        public string Avtor { get; set; } 
      
        public int PublicationDate { get; set; } 
        public double Price { get; set; } 



        //навигационные поля
        public int CategoryId { get; set; }
        //[JsonIgnore] //игнорирование при сериализации
        public Category? Category { get; set; }

    }
}

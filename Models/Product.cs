using Simulation7.Models.Base;

namespace Simulation7.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }

    }
}

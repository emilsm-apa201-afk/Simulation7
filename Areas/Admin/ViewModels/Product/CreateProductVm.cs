namespace Simulation7.Areas.Admin.ViewModels.Product
{
    public class CreateProductVm
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}

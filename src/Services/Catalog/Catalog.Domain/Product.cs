namespace Catalog.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //Propiedades de navegación
        public ProductInStock Stock { get; set; }
    }
}
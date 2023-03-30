namespace StoreApp.Service.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public double Arrivalprice { get; set; }

        public double Price { get; set; }

        public string Barcode { get; set; }
        public long CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public long SubCategoryId { get; set; }

        public string SubCategoryName { get; set;}


    }
}

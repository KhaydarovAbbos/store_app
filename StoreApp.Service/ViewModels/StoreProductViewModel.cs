namespace StoreApp.Service.ViewModels
{
    public class StoreProductViewModel
    {
        public long StoreId { get; set; }
        public string Storename { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public long SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }

        public double ArrivalPrice { get; set; }

        public double Price { get; set; }
        public string Barcode { get; set; } 

        public double Quantity { get; set; }
    }
}

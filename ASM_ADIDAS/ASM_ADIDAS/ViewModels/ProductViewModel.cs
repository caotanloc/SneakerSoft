using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_ADIDAS.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(int productID, int quantity, string name, decimal price, string image)
        {
            ProductID = productID;
            Quantity = quantity;
            Name = name;
            Price = price;
            Image = image;
        }
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public decimal Total
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}

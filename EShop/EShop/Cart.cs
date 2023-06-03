using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    internal class Cart
    {
        public int Id { get; set; }
        Dictionary<Product, int> quentity;
        public int CustomerId { get; set; }
        List<string> products = new List<string>();

        public void AddProduct()
        {


        }
        public void RemoveProduct()
        {

        }
        public void CalculateProduct()
        {

        }
    }
}

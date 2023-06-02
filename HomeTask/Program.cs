using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson04
{
    internal class Program
    {
        static List<Product> products = new List<Product>
        {
            new Product(1, "Coca-Cola", 10000),
            new Product(2, "Fanta", 5000),
            new Product(3, "Sprite", 2000),
            new Product(4, "Pepsi", 15000),
        };
        static User currentUser;

        static void Main(string[] args)
        {
            User user = new User(1);
            user.CartItems = new List<CartItem>();
            currentUser = user;

            bool exitRequested = false;
            while (!exitRequested)
            {
                SelectModule();
                Console.ReadLine();

                Console.WriteLine("Do you want to exit? (Y/N)");
                string input = Console.ReadLine();
                exitRequested = input.Equals("Y", StringComparison.OrdinalIgnoreCase);
            }
        }

        static void SelectModule()
        {
            Console.WriteLine("Select module");
            Console.WriteLine("1. Catalog         2. Cart        3. History");

            int moduleChoice;
            if (!int.TryParse(Console.ReadLine(), out moduleChoice))
            {
                Console.WriteLine("Invalid input. Please select a valid module.");
                Console.ReadKey();
                SelectModule();
                return;
            }

            switch (moduleChoice)
            {
                case 1:
                    CatalogModule();
                    break;
                case 2:
                    CartModule();
                    break;
                case 3:
                    HistoryModule();
                    break;
                default:
                    Console.WriteLine("Select a correct module.");
                    Console.ReadKey();
                    SelectModule();
                    break;
            }
        }

        static void CatalogModule()
        {
            Console.Clear();
            Console.WriteLine("---- Catalog ----");

            if (products.Count == 0)
            {
                Console.WriteLine("Catalog is empty.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }

            Console.WriteLine("Do you want to add a product to the cart? (Y/N)");
            string input = Console.ReadLine();

            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                AddProduct();
            }
            else
            {
                SelectModule();
            }
        }

        static void AddProduct()
        {
            Console.WriteLine("Enter the ID of the product you want to add or press 0 to go back.");
            int productId;
            if (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                AddProduct();
                return;
            }

            if (productId == 0)
            {
                SelectModule();
                return;
            }

            var product = products.Find(p => p.Id == productId);
            if (product != null)
            {
                currentUser.AddProduct(product);
                Console.WriteLine($"{product} is added to the cart.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine("Do you want to add another product? (Y/N)");
            string input = Console.ReadLine();

            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                AddProduct();
            }
            else
            {
                CalculateTotalPrice();
            }
        }

        static void CartModule()
        {
            if (currentUser.CartItems.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                Console.ReadKey();
                SelectModule();
                return;
            }

            Console.WriteLine("---- Cart ----");

            currentUser.DisplayCart();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            SelectModule();
        }

        static void CalculateTotalPrice()
        {
            decimal totalPrice = currentUser.CalculateTotalPrice();
            Console.WriteLine($"Total price: {totalPrice}");

            Console.WriteLine("Do you want to proceed to checkout? (Y/N)");
            string input = Console.ReadLine();

            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Checkout();
            }
            else
            {
                SelectModule();
            }
        }

        static void Checkout()
        {
            Console.WriteLine("---- Checkout ----");



            Console.WriteLine("Checkout completed.");
            Console.ReadKey();
            SelectModule();
        }

        static void HistoryModule()
        {
            Console.Clear();
            Console.WriteLine("---- Purchase History ----");

            if (currentUser.PurchaseHistory.Count == 0)
            {
                Console.WriteLine("No purchase history found.");
            }
            else
            {
                foreach (var purchase in currentUser.PurchaseHistory)
                {
                    Console.WriteLine($"Purchase Date: {purchase.PurchaseDate}");
                    foreach (var item in purchase.Items)
                    {
                        Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}, Price: {item.Product.Price}");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            SelectModule();
        }
    }

    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price}";
        }
    }

    internal class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }

    internal class Purchase
    {
        public DateTime PurchaseDate { get; set; }
        public List<CartItem> Items { get; set; }

        public Purchase(List<CartItem> items)
        {
            PurchaseDate = DateTime.Now;
            Items = items;
        }
    }

    internal class User
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Purchase> PurchaseHistory { get; set; }

        public User(int id)
        {
            Id = id;
            CartItems = new List<CartItem>();
            PurchaseHistory = new List<Purchase>();
        }

        public void AddProduct(Product product)
        {
            Console.WriteLine("Enter the quantity:");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Invalid input. Please enter a valid quantity.");
                AddProduct(product);
                return;
            }

            CartItem existingItem = CartItems.Find(item => item.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                CartItem newItem = new CartItem(product, quantity);
                CartItems.Add(newItem);
            }
        }

        public void DisplayCart()
        {
            foreach (var item in CartItems)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}, Price: {item.Product.Price}");
            }
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in CartItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            return totalPrice;
        }

        public void Checkout()
        {
            Purchase purchase = new Purchase(CartItems);
            PurchaseHistory.Add(purchase);
            CartItems.Clear();
        }
    }
}
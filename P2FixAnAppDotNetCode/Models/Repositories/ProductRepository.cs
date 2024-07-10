using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    /// <summary>
    /// The class that manages product data
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        // Create an unique instance of ProductRepository wich will be instancied on time on the start of the program
        //(singleton pattern implementation)
        private static readonly ProductRepository _instance = new ProductRepository();
        private static List<Product> _products;

        /// <summary>
        ///Private constructor to avoid extern instanciation
        /// </summary>
        private ProductRepository()
        {
            _products = new List<Product>();
            GenerateProductData();
        }

        /// <summary>
        /// Public property to access to the unique instance
        /// </summary>
        public static ProductRepository Instance
        {
            get { return _instance; }
        }

        public static ProductRepository GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Generate the default list of products
        /// </summary>
        public void GenerateProductData()
        {
            int id = 0;
            _products.Add(new Product(++id, 10, 92.50, "Echo Dot", "(2nd Generation) - Black"));
            _products.Add(new Product(++id, 20, 9.99, "Anker 3ft / 0.9m Nylon Braided", "Tangle-Free Micro USB Cable"));
            _products.Add(new Product(++id, 30, 69.99, "JVC HAFX8R Headphone", "Riptidz, In-Ear"));
            _products.Add(new Product(++id, 40, 32.50, "VTech CS6114 DECT 6.0", "Cordless Phone"));
            _products.Add(new Product(++id, 50, 895.00, "NOKIA OEM BL-5J", "Cell Phone "));
        }

        /// <summary>
        /// Get all products from the inventory
        /// </summary>
        public List<Product> GetAllProducts()
        {
            List<Product> list = _products.Where(p => p.Stock > 0).OrderBy(p => p.Name).ToList();
            return list;
        }

        /// <summary>
        /// Update the stock of a product in the inventory by its id
        /// </summary>
        public void UpdateProductStocks(int productId, int quantityToRemove)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                product.Stock = product.Stock - quantityToRemove;
                if (product.Stock <= 0)
                {
                    _products.Remove(product);
                }
            }
            
        }

        public Product GetProductById(int id)
        {
            foreach (Product product in _products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void ClearProducts()
        {
            _products.Clear();
        }
    }
}

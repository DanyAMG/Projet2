using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.Repositories
{
    public interface IProductRepository
    {
        void GenerateProductData();
        List<Product> GetAllProducts();

       Product GetProductById(int id);

        void UpdateProductStocks(int productId, int quantityToRemove);

        void AddProduct(Product product);

        void ClearProducts();
    }
}

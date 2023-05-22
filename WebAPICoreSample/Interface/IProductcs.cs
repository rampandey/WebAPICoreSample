using WebAPICoreSample.Models;

namespace WebAPICoreSample.Interface
{
    public interface IProduct
    {
        public List<Product> GetProductDetails();
        public Product GetProductDetails(int id);
        public void AddProduct(Product employee);
        public void UpdateProduct(Product employee);
        public Product DeleteProduct(int id);
        public bool CheckEmployee(int id);
    }
}

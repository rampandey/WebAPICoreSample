using Microsoft.EntityFrameworkCore;
using WebAPICoreSample.Interface;
using WebAPICoreSample.Models;

namespace WebAPICoreSample.Repository
{
    public class ProductRepository: IProduct
    {
        readonly DatabaseContext _dbContext = new();
        public ProductRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProductDetails()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Product GetProductDetails(int id)
        {
            try
            {
                Product? product = _dbContext.Products.Find(id);
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Product DeleteProduct(int id)
        {
            try
            {
                Product? product = _dbContext.Products.Find(id);

                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();
                    return product;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckEmployee(int id)
        {
            return _dbContext.Products.Any(e => e.ProductId == id);
        }
    }
}

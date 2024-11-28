using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectApp.DAL;


namespace ProjectApp.BLL
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return products.Select(ProductMapper.MapToDTO).ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            return ProductMapper.MapToDTO(product);
        }
        public void AddProduct(ProductDTO productDTO)
            {
            var product = ProductMapper.MapToEntity(productDTO);
            _productRepository.AddProduct(product);
        }
        public void UpdateProduct(ProductDTO productDTO)
        {
            var product = ProductMapper.MapToEntity(productDTO);
            _productRepository.UpdateProduct(product);

        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }

        internal void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
 
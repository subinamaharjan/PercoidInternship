using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApp.DAL
{
    public class ProductMapper
    {
        public static ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity


            };

        }


        public static Product MapToEntity(ProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity
            };
        }
    }
}

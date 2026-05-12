using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.ProductDtos;

namespace WoodyCornerApp.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResult<GetProductDto>> GetProductByIdAsync(int id);

        public Task<ServiceResult<GetProductDto>> DeleteProductAsync(int id);
        
        public Task<ServiceResult<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto);

        public Task<IEnumerable<GetProductDto>> GetAllProductsAsync();

        public Task<ServiceResult<UpdateProductDto>> UpdateProduct(UpdateProductDto updateProductDto);
    }
}

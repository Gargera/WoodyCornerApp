using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL;
using WoodyCornerApp.DAL.Interfaces;
using WoodyCornerApp.BLL.DTOs;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Services
{
    public class ProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _unitOfWork.Products.AddEntityAsync(createProductDto.EntityToProduct());
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);

            if (product != null)
                await _unitOfWork.Products.DeleteEntityAsync(id);
            else
                throw new Exception($"There's no Product with id = {id}");
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllEntitiesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.Products.GetEntityByIdAsync(id);
        }

        public void UpdateProduct(UpdateProductDto updateProductDto)
        {
            _unitOfWork.Products.UpdateEntity(updateProductDto.EntityToProduct());
        }
    }
}

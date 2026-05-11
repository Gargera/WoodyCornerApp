using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL;
using WoodyCornerApp.DAL.Interfaces;
using WoodyCornerApp.BLL.DTOs;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.Validation;

namespace WoodyCornerApp.BLL.Services
{
    public class ProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetEntityByIdAsync(id);
            ServiceResult<GetProductDto> result = new ServiceResult<GetProductDto>();

            if (product != null)
            {
                result.Success = true;
                result.Message = "Product Found";
                result.Data = product.EntityToGetProductDto();
            }
            else
            {
                result.Success = false;
                result.Message = "Product NotFound!";
            }

            return result;
        }

        public async Task<ServiceResult<GetProductDto>> DeleteProductAsync(int id)
        {
            var result = await GetProductByIdAsync(id);
            
            if (result.Success)
            {
                await _unitOfWork.Products.DeleteEntityAsync(id);
                result.Message = "Product Deleted Successfully";
            }

            return result;
        }

        public async Task<ServiceResult<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var findAny = await _unitOfWork.Products.AnyAsync(p => p.ImagePath == createProductDto.ImagePath);

            var result = new ServiceResult<CreateProductDto>();
            if(findAny)
            {
                result.Success = false;
                result.Message = "A product with the same image path already exists";
            }
            else
            {
                var validResult = createProductDto.EntityToProduct().Valid();
                if (validResult.valid)
                {
                    await _unitOfWork.Products.AddEntityAsync(createProductDto.EntityToProduct());
                    result.Success = true;
                    result.Message = "Product Created Successfully";
                }
                else
                {
                    result.Success = false;
                    result.Message = validResult.message;
                }
            }

            result.Data = createProductDto;
            return result;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllEntitiesAsync();
            return products.Select(p => p.EntityToGetProductDto()).ToList();
        }

        public async Task<ServiceResult<UpdateProductDto>> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = await _unitOfWork.Products.GetEntityByIdAsync(updateProductDto.Id);
            ServiceResult<UpdateProductDto> result = new ServiceResult<UpdateProductDto>();

            if (product == null)
            {
                result.Success = false;
                result.Message = "Product NotFound!";
            }
            else
            {
                var validResult = updateProductDto.EntityToProduct().Valid();
                if (validResult.valid)
                {
                    _unitOfWork.Products.UpdateEntity(updateProductDto.EntityToProduct());
                    result.Message = "Product Updated Successfully";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = validResult.message;
                }
                result.Data = product.EntityToUpdateProductDto();
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL;
using WoodyCornerApp.DAL.Interfaces;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.Validation;
using WoodyCornerApp.BLL.DTOs.ProductDtos;
using Microsoft.EntityFrameworkCore;

namespace WoodyCornerApp.BLL.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetEntityById(id)
                                                    .Include(p => p.CartItems)
                                                    .Include(p => p.OrderItems)
                                                    .Include(p => p.Room)
                                                    .FirstOrDefaultAsync();

            var result = new ServiceResult<GetProductDto>();

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
                await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
            var products = await _unitOfWork.Products.GetAllEntities()
                                               .Include(p => p.CartItems)
                                               .Include(p => p.OrderItems)
                                               .Include(p => p.Room)
                                               .ToListAsync();

            return products.Select(p => p.EntityToGetProductDto());
        }

        public async Task<ServiceResult<UpdateProductDto>> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var GetProduct = await GetProductByIdAsync(updateProductDto.Id);
            var result = new ServiceResult<UpdateProductDto>();

            if (!GetProduct.Success)
            {
                result.Success = false;
                result.Message = "Product NotFound!";
            }
            else
            {
                var findAny = await _unitOfWork.Products.AnyAsync(p => p.ImagePath == updateProductDto.ImagePath);

                if (findAny)
                {
                    result.Success = false;
                    result.Message = "A product with the same image path already exists";
                }
                else
                {
                    var validResult = updateProductDto.EntityToProduct().Valid();
                    if (validResult.valid)
                    {
                        _unitOfWork.Products.UpdateEntity(updateProductDto.EntityToProduct());
                        await _unitOfWork.SaveChangesAsync();
                        result.Message = "Product Updated Successfully";
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = validResult.message;
                    }
                }
            }

            result.Data = updateProductDto;
            return result;
        }
    }
}
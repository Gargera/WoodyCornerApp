using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.ProductDtos;
using WoodyCornerApp.BLL.DTOs.RoomDtos;
using WoodyCornerApp.BLL.Mapping;
using WoodyCornerApp.BLL.Validation;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.DAL.Interfaces;

namespace WoodyCornerApp.BLL.Services
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<GetRoomDto>> GetRoomByIdAsync(int id)
        {
            var room = await _unitOfWork.Rooms.GetEntityById(id)
                                              .Include(r => r.Products)
                                              .FirstOrDefaultAsync();

            var result = new ServiceResult<GetRoomDto>();

            if (room != null)
            {
                result.Success = true;
                result.Message = "Room Found";
                result.Data = room.EntityToGetRoomDto();
            }
            else
            {
                result.Success = false;
                result.Message = "Room NotFound!";
            }

            return result;
        }

        public async Task<ServiceResult<GetRoomDto>> DeleteRoomAsync(int id)
        {
            var result = await GetRoomByIdAsync(id);

            if (result.Success)
            {
                await _unitOfWork.Rooms.DeleteEntityAsync(id);
                result.Message = "Room Deleted Successfully";
            }

            return result;
        }

        public async Task<ServiceResult<CreateRoomDto>> CreateRoomAsync(CreateRoomDto createRoomDto)
        {
            var findAny = await _unitOfWork.Rooms.AnyAsync(p => p.ImagePath == createRoomDto.ImagePath);

            var result = new ServiceResult<CreateRoomDto>();
            if (findAny)
            {
                result.Success = false;
                result.Message = "A Room with the same image path already exists";
            }
            else
            {
                var validResult = createRoomDto.EntityToRoom().Valid();
                if (validResult.valid)
                {
                    await _unitOfWork.Rooms.AddEntityAsync(createRoomDto.EntityToRoom());
                    result.Success = true;
                    result.Message = "Room Created Successfully";
                }
                else
                {
                    result.Success = false;
                    result.Message = validResult.message;
                }
            }

            result.Data = createRoomDto;
            return result;
        }

        public async Task<IEnumerable<GetRoomDto>> GetAllRoomsAsync()
        {
            var rooms = await _unitOfWork.Rooms.GetAllEntities()
                                               .Include(r => r.Products)
                                               .Select(r => r)
                                               .ToListAsync();

            return rooms.Select(r => r.EntityToGetRoomDto());
        }

        public async Task<ServiceResult<UpdateRoomDto>> UpdateRoom(UpdateRoomDto updateRoomDto)
        {
            var GetRoom = await GetRoomByIdAsync(updateRoomDto.Id);
            var result = new ServiceResult<UpdateRoomDto>();

            if (!GetRoom.Success)
            {
                result.Success = false;
                result.Message = "Room NotFound!";
            }
            else
            {
                var findAny = await _unitOfWork.Rooms.AnyAsync(p => p.ImagePath == updateRoomDto.ImagePath);

                if (findAny)
                {
                    result.Success = false;
                    result.Message = "A Room with the same image path already exists";
                }
                else
                {
                    var validResult = updateRoomDto.EntityToRoom().Valid();
                    if (validResult.valid)
                    {
                        _unitOfWork.Rooms.UpdateEntity(updateRoomDto.EntityToRoom());
                        result.Message = "Room Updated Successfully";
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = validResult.message;
                    }
                }
            }

            result.Data = updateRoomDto;
            return result;
        }
    }
}

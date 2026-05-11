using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;
using WoodyCornerApp.BLL.DTOs.RoomDtos;

namespace WoodyCornerApp.BLL.Mapping
{
    public static class RoomMapping
    {
        public static Room EntityToRoom (this UpdateRoomDto updateRoomDto)
        {
            return new Room
            {
                Id = updateRoomDto.Id,
                Name = updateRoomDto.Name,
                Description = updateRoomDto.Description,
                ImagePath = updateRoomDto.ImagePath,
            };
        }
        public static UpdateRoomDto EntityToUpdateRoomDto (this Room room)
        {
            return new UpdateRoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                ImagePath= room.ImagePath
            };
        }

        public static Room EntityToRoom(this CreateRoomDto createRoomDto)
        {
            return new Room
            {
                Name = createRoomDto.Name,
                Description = createRoomDto.Description,
                ImagePath = createRoomDto.ImagePath
            };
        }
        public static CreateRoomDto EntityToCreateRoomDto(this Room room)
        {
            return new CreateRoomDto
            {
                Name = room.Name,
                Description = room.Description,
                ImagePath = room.ImagePath
            };
        }

        public static Room EntityToRoom(this GetRoomDto getRoomDto)
        {
            return new Room
            {
                Id = getRoomDto.Id,
                Name = getRoomDto.Name,
                Description = getRoomDto.Description,
                ImagePath = getRoomDto.ImagePath
            };
        }
        public static GetRoomDto EntityToGetRoomDto(this Room room)
        {
            return new GetRoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                ImagePath = room.ImagePath,
                Products = room.Products.Select(p => p.EntityToGetProductDto()).ToList()
            };
        }
    }
}

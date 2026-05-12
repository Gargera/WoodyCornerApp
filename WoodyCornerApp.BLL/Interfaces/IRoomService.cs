using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.BLL.Common;
using WoodyCornerApp.BLL.DTOs.RoomDtos;

namespace WoodyCornerApp.BLL.Interfaces
{
    public interface IRoomService
    {
        public Task<ServiceResult<GetRoomDto>> GetRoomByIdAsync(int id);

        public Task<ServiceResult<GetRoomDto>> DeleteRoomAsync(int id);

        public Task<ServiceResult<CreateRoomDto>> CreateRoomAsync(CreateRoomDto createRoomDto);

        public Task<IEnumerable<GetRoomDto>> GetAllRoomsAsync();

        public Task<ServiceResult<UpdateRoomDto>> UpdateRoom(UpdateRoomDto updateRoomDto);

    }
}

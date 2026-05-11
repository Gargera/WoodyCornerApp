using System;
using System.Collections.Generic;
using System.Text;
using WoodyCornerApp.DAL.Entities;

namespace WoodyCornerApp.BLL.Validation
{
    public static class RoomValidation
    {
        public static ValidResult Valid(this Room room)
        {
            var result = new ValidResult();
            result.valid = false;

            if (room == null) result.message = "Room not found";
            else if (room.Name == null) result.message = "Room Name is Required";
            else if (room.Name.Length < 2 || room.Name.Length > 100) result.message = "Room Name must be between 2 and 100";
            else if (room.Description == null) result.message = "Room Description is Required";
            else if (room.Description.Length < 10 || room.Description.Length > 50) result.message = "Room Description must be between 10 and 50";
            else if (room.ImagePath == null) result.message = "Room ImagePath is Required";
            else result.valid = true;

            return result;
        }
    }
}

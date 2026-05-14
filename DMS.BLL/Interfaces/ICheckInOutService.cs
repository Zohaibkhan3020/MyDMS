using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface ICheckInOutService
    {
        Task CheckOutAsync(
            CheckOutDto dto,
            int userId,
            string machineName,
            string ip);

        Task CheckInAsync(
            CheckInDto dto);
    }
}

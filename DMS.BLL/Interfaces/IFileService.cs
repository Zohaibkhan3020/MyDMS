using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IFileService
    {
        Task<int> UploadAsync(
            UploadFileDto dto,
            int userId);
    }
}

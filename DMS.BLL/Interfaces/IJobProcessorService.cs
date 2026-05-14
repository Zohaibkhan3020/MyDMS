using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IJobProcessorService
    {
        Task ProcessAsync(
            BackgroundJob job);
    }
}

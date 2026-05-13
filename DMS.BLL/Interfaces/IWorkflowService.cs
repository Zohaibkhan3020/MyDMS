using DMS.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Interfaces
{
    public interface IWorkflowService
    {
        Task<int>
            CreateWorkflowAsync(
                CreateWorkflowDto dto);

        Task<int>
            CreateStateAsync(
                CreateStateDto dto);

        Task<int>
            CreateTransitionAsync(
                CreateTransitionDto dto);

        Task StartWorkflowAsync(
            StartWorkflowDto dto,
            int userId);

        Task MoveNextAsync(
            WorkflowActionDto dto,
            int userId);
    }
}

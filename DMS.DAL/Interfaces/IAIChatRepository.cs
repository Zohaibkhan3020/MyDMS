using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IAIChatRepository
    {
        Task<int> CreateSessionAsync(
            AIChatSession model);

        Task SaveMessageAsync(
            AIChatMessage model);

        Task<List<AIChatMessage>>
            GetMessagesAsync(
                int sessionId);

        Task SaveMemoryAsync(
            AIMemory model);

        Task<List<AIMemory>>
            GetMemoryAsync(
                int userId);
    }
}

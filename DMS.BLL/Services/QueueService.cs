using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;

namespace DMS.BLL.Services
{
    public class QueueService
    : IQueueService
    {
        private readonly
            IBackgroundJobRepository
                _repository;

        public QueueService(
            IBackgroundJobRepository repository)
        {
            _repository =
                repository;
        }

        public async Task<int>
            EnqueueAsync(
                QueueJobDto dto)
        {
            return await _repository
                .CreateAsync(
                    new BackgroundJob
                    {
                        JobType =
                            dto.JobType,

                        Payload =
                            dto.Payload
                    });
        }
    }
}

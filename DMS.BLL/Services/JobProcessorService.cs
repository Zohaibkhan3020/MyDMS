using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class JobProcessorService
    : IJobProcessorService
    {
        private readonly
            IBackgroundJobRepository
                _repository;

        public JobProcessorService(
            IBackgroundJobRepository repository)
        {
            _repository =
                repository;
        }

        public async Task ProcessAsync(
            BackgroundJob job)
        {
            try
            {
                switch (job.JobType)
                {
                    case "EMAIL":

                        // send email

                        break;

                    case "OCR":

                        // OCR process

                        break;

                    case "AI_METADATA":

                        // AI process

                        break;

                    case "INDEXING":

                        // indexing

                        break;
                }

                await _repository
                    .UpdateStatusAsync(
                        job.JobID,
                        "Completed");
            }
            catch (Exception ex)
            {
                await _repository
                    .UpdateStatusAsync(
                        job.JobID,
                        "Failed",
                        ex.Message);
            }
        }
    }
}

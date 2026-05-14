using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    using DMS.BLL.DTOs;
    using DMS.BLL.Interfaces;
    using DMS.DAL.Interfaces;
    using DMS.Domain.Entities;
    using Microsoft.Extensions.Configuration;
    using OpenAI.Chat;

    public class AIChatService
        : IAIChatService
    {
        private readonly
            IConfiguration
                _configuration;

        private readonly
            IAIChatRepository
                _repository;

        public AIChatService(
            IConfiguration configuration,

            IAIChatRepository repository)
        {
            _configuration =
                configuration;

            _repository =
                repository;
        }

        public async Task<AIChatResponseDto>
            AskAsync(
                AIChatRequestDto dto)
        {
            var apiKey =
                _configuration["OpenAI:ApiKey"];

            var client =
                new ChatClient(
                    model: "gpt-4o-mini",
                    apiKey: apiKey);

            if (dto.SessionID == 0)
            {
                dto.SessionID =
                    await _repository
                        .CreateSessionAsync(
                            new AIChatSession
                            {
                                UserID =
                                    dto.UserID
                            });
            }

            await _repository
                .SaveMessageAsync(
                    new AIChatMessage
                    {
                        SessionID =
                            dto.SessionID,

                        SenderType =
                            "USER",

                        MessageText =
                            dto.Message
                    });

            var history =
                await _repository
                    .GetMessagesAsync(
                        dto.SessionID);

            var prompt =
                string.Join(
                    "\n",
                    history.Select(x =>
                        $"{x.SenderType}: {x.MessageText}"));

            var completion =
                await client
                    .CompleteChatAsync(prompt);

            var response =
                completion.Value
                    .Content[0]
                    .Text;

            await _repository
                .SaveMessageAsync(
                    new AIChatMessage
                    {
                        SessionID =
                            dto.SessionID,

                        SenderType =
                            "AI",

                        MessageText =
                            response
                    });

            return new AIChatResponseDto
            {
                SessionID =
                    dto.SessionID,

                Response =
                    response
            };
        }
    }
}

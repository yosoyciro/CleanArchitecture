using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand, int>
    {
        private readonly IAsyncRepository<Streamer> repository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateStreamerCommandHandler> logger;

        public CreateStreamerCommandHandler(IStreamerRepository repository, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }
        public async Task<int> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = mapper.Map<Streamer>(request);
            var newStreamer = await repository.AddAsync(streamerEntity);
            logger.LogInformation($"Streamer {newStreamer.Id} fue creado exitosamente!");

            await SendEmail(newStreamer);

            return newStreamer.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "ciro.daniele@gmail.com",
                Subject = "Nuevo streamer",
                Body = $"Nombre: {streamer.Nombre}"
            };

            try
            {
                await emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                logger.LogError($"No se pudo enviar el correo de {streamer.Id}. Detalles:{ex.Message}.");
            }

        }
    }
}

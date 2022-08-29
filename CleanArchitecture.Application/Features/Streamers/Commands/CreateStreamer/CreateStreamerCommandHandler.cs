using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateStreamerCommandHandler> logger;

        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }
        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerEntity = mapper.Map<Streamer>(request);
            //var newStreamer = await repository.AddAsync(streamerEntity);

            //UoW
            unitOfWork.StreamerRepository.AddEntity(streamerEntity);
            var result = await unitOfWork.CommitAsync();
            if (result <= 0)
            {
                throw new Exception($"No se pudo grabar el Streamer");
            }

            logger.LogInformation($"Streamer {streamerEntity.Id} fue creado exitosamente!");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
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

using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            //var streamerToUpdate = await repository.GetByIdAsync(request.Id);
            var streamerToUpdate = await unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if (streamerToUpdate == null)
            {
                logger.LogError($"No se encontro el streamerid {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
            //await repository.UpdateAsync(streamerToUpdate);

            unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);
            var result = await unitOfWork.CommitAsync();
            if (result <= 0)
            {
                logger.LogError("No se pudo actualizar el Streamer");
                throw new Exception("No se pudo actualizar el Streamer");
            }

            logger.LogInformation($"El streamer {request.Id} se actualizo correctamente");

            return Unit.Value;
        }
    }
}

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
        private readonly IStreamerRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateStreamerCommand> logger;

        public UpdateStreamerCommandHandler(IStreamerRepository repository, IMapper mapper, ILogger<UpdateStreamerCommand> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await repository.GetByIdAsync(request.Id);
            if(streamerToUpdate == null)
            {
                logger.LogError($"No se encontro el streamerid {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
            await repository.UpdateAsync(streamerToUpdate);

            logger.LogInformation($"El streamer {request.Id} se actualizo correctamente");

            return Unit.Value;
        }
    }
}

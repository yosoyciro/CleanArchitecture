using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    internal class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IStreamerRepository repository;
        private readonly ILogger<DeleteStreamerCommand> logger;

        public DeleteStreamerCommandHandler(IStreamerRepository repository, ILogger<DeleteStreamerCommand> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await repository.GetByIdAsync(request.Id);
            if(streamerToDelete == null)
            {
                logger.LogError($"El streamerid {request.Id} no existe");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            await repository.DeleteAsync(streamerToDelete);
            logger.LogInformation($"El streamer {request.Id} fue eliminado con exito");

            return Unit.Value;
        }
    }
}

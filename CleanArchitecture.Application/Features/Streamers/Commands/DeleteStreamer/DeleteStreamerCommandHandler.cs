using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    internal class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DeleteStreamerCommand> logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteStreamerCommand> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if(streamerToDelete == null)
            {
                logger.LogError($"El streamerid {request.Id} no existe");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
            var result = await unitOfWork.CommitAsync();
            if (result <= 0)
            {
                throw new Exception("No se pudo eliminar el Streamer");
            }

            logger.LogInformation($"El streamer {request.Id} fue eliminado con exito");

            return Unit.Value;
        }
    }
}

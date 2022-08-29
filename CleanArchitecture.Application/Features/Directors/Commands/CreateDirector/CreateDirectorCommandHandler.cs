using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly ILogger<CreateDirectorCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateDirectorCommandHandler(ILogger<CreateDirectorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var entidad = mapper.Map<Director>(request);

            unitOfWork.Repository<Director>().AddEntity(entidad);
            var result = await unitOfWork.CommitAsync();

            if (result <= 0)
            {
                logger.LogError("No se insertó el registro de director");
                throw new Exception("No se pudo insertar Director");
            }

            return entidad.Id;
        }
    }
}

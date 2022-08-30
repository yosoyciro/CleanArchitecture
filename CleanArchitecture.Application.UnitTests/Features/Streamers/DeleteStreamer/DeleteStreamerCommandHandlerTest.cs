using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.DeleteStreamer
{
    public class DeleteStreamerCommandHandlerTest
    {
        private readonly IMapper mapper;
        private readonly Mock<UnitOfWork> unitOfWork;
        private readonly Mock<ILogger<DeleteStreamerCommandHandler>> logger;

        public DeleteStreamerCommandHandlerTest()
        {
            this.unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            this.mapper = mapperConfig.CreateMapper();
            this.logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task UpdateStreammerCommand_InputStreamerById_ReturnsUnit()
        {
            var streamerInput = new DeleteStreamerCommand
            {
                Id = 8001,
            };

            var handler = new DeleteStreamerCommandHandler(unitOfWork.Object, logger.Object);
            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}

using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.UpdateStreamer
{
    public class UpdateStreamerCommandHanlderTest
    {
        private readonly IMapper mapper;
        private readonly Mock<UnitOfWork> unitOfWork;
        private readonly Mock<IEmailService> emailService;
        private readonly Mock<ILogger<UpdateStreamerCommandHandler>> logger;

        public UpdateStreamerCommandHanlderTest()
        {
            this.unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            this.mapper = mapperConfig.CreateMapper();
            this.emailService = new Mock<IEmailService>();
            this.logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task UpdateStreamerCommand_InputStreamer_ReturnsUnit()
        {
            var streamerInput = new UpdateStreamerCommand
            {
                Id = 8001,
                Nombre = "Test1",
                Url = "empty"
            };

            var handler = new UpdateStreamerCommandHandler(unitOfWork.Object, mapper, logger.Object);
            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}

using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.CreateStreamer
{
    public class CreateStreamerCommandHandlerTest
    {
        private readonly IMapper mapper;
        private readonly Mock<UnitOfWork> unitOfWork;
        private readonly Mock<IEmailService> emailService;
        private readonly Mock<ILogger<CreateStreamerCommandHandler>> logger;

        public CreateStreamerCommandHandlerTest()
        {
            this.unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });            
            this.mapper = mapperConfig.CreateMapper();
            this.emailService = new Mock<IEmailService>();
            this.logger = new Mock<ILogger<CreateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber()
        {
            var streamerInput = new CreateStreamerCommand
            {
                Nombre = "Test1",
                Url = "empty"
            };

            var handler = new CreateStreamerCommandHandler(unitOfWork.Object, mapper, emailService.Object, logger.Object);
            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
            result.ShouldBeGreaterThan(0);
        }        
    }
}

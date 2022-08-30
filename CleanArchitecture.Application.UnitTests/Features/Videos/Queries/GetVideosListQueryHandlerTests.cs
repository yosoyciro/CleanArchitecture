using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Videos.Queries
{
    public class GetVideosListQueryHandlerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<UnitOfWork> unitOfWork;

        public GetVideosListQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            this.unitOfWork = MockUnitOfWork.GetUnitOfWork();
            this.mapper = mapperConfig.CreateMapper();

            MockVideoRepository.AddDataVideoRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosListQueryHandler(unitOfWork.Object, mapper);

            var request = new GetVideosListQuery("test");
            var result = await handler.Handle(request, CancellationToken.None);

            result.Count.ShouldBe(1);
            
        }
    }
}

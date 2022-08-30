using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public class MockVideoRepository
    {
        public static void AddDataVideoRepository(StreamerDbContext contextFake)
        {
            //Creo data aleatoria
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();

            //Data especifica
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "test")
                .Create()
            );

            contextFake.Videos!.AddRange(videos);
            contextFake.SaveChanges();
        }
    }
}

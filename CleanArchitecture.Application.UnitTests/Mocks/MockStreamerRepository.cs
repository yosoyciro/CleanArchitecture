using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockStreamerRepository
    {
        public static void AddDataStreamerRepository(StreamerDbContext contextFake)
        {
            //Creo data aleatoria
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var streamers = fixture.CreateMany<Streamer>().ToList();

            //Data especifica
            streamers.Add(fixture.Build<Streamer>()
                .With(tr => tr.Id, 8001)
                .Without(tr => tr.Videos)
                .Create()
            );

            contextFake.Streamers!.AddRange(streamers);
            contextFake.SaveChanges();
        }
    }
}

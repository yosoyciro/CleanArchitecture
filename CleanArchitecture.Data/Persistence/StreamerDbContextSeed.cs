﻿using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        { 
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();

                logger.LogInformation("Se generaron los records por GetPreconfiguredStreamer");
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer { Nombre = "Paramount+", Url = "url" },
                new Streamer { Nombre = "Star+", Url = "url" },
                new Streamer { Nombre = "Disney+", Url = "url" }
            };
        }
    }
}

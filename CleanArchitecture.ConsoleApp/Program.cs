using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.InteropServices;

StreamerDbContext dbContext = new();
await MultipleEntitiesQuery();

Console.WriteLine("Presione una tecla cualquiera.... Cual es cualquiera? Tengo Ctrl, Esc");
Console.ReadKey();

async Task MultipleEntitiesQuery()
{
    var videoWithActores = await dbContext.Videos!.Include(a => a.Actores).ToListAsync();
    //foreach (var video in videoWithActores)
    //{
    //    foreach (var actor in video.Actores)
    //    {
    //        Console.WriteLine($"{video.Id} - {actor.Nombre}");
    //    }
        
    //}
    //var actor = await dbContextoutl
}

async Task AddNewDirectorWithVideo()
{
    var director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Lamas",
        VideoId  = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}

async Task AddNewVideoWithActor()
{
    var actor = new Actor { Nombre = "Morgan", Apellido = "Freeman" };
    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor { ActorId = actor.Id, VideoId = 1 };
    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamrWithVideoId()
{    
    var peli = new Video
    {
        Nombre = "The Batman",
        StreamerId = 3
    };

    await dbContext.AddAsync(peli);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamrWithVideo()
{
    var pantalla = new Streamer
    {
        Nombre = "Pantalla"
    };

    var peli = new Video { 
        Nombre = "Back To The Future", 
        Streamer = pantalla 
    };

    await dbContext.AddAsync(peli);
    await dbContext.SaveChangesAsync();
}

async Task TrackingAndNotTracking()
{
    var streamerWithTracking = await dbContext.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
    var streamerWithNoTracking = await dbContext.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking!.Nombre = "Nuevo Netflix";
    streamerWithNoTracking!.Nombre = "Nuevo AmazonPlus";
    await dbContext.SaveChangesAsync();
}

async Task QueryLinq()
{    var streamers = await (from i in dbContext.Streamers
                           where i.Id == 1
                    select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethods()
{
    var streamContext = dbContext.Streamers!;

    var firstAsync = await streamContext.Where(x => x.Nombre!.Contains("e")).FirstAsync();
    var firstOrDefaultAsync = await streamContext.Where(x => x.Nombre!.Contains("y")).FirstOrDefaultAsync();
    var firstOrDefaultAsyncNoWhere = await streamContext.FirstOrDefaultAsync(x => x.Nombre!.Contains("y"));

    var singleAsync = await streamContext.Where(x => x.Id == 1).SingleAsync();
    var singleOrDefaultAsync = await streamContext.Where(x => x.Id == 1).SingleOrDefaultAsync();

    var resultado = await streamContext.FindAsync(1);
}

async Task QueryFilter()
{
	Console.WriteLine("Ingrese compañia:");
	var input = Console.ReadLine();
    var streamers = await dbContext.Streamers!.Where(x => x.Nombre!.Equals(input!.ToString())).ToListAsync();

	foreach (var streamer in streamers)
	{
		Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
	}

    //var streamerPartialResult = await dbContext.Streamers!.Where(x => x.Nombre!.Contains(input!.ToString())).ToListAsync();
    var streamerPartialResult = await dbContext.Streamers!.Where(x => EF.Functions.Like(x.Nombre!, $"%{input}%"!)).ToListAsync();
    foreach (var streamer in streamerPartialResult)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddRecords()
{
    var existeStreamer = await dbContext!.Streamers!.FindAsync(1);
    if (existeStreamer == null)
    {
        Streamer streamer = new()
        {
            Nombre = "Amazon Prime",
            Url = "http://www.amazonprime.com"
        };

        dbContext!.Streamers!.Add(streamer);
        await dbContext.SaveChangesAsync();
    }



    var movies = new List<Video>
    {
        new Video { Nombre = "The Shawshank Redemption", StreamerId = existeStreamer!.Id  },
        new Video { Nombre = "Dead Society Poets", StreamerId = existeStreamer!.Id  },
        new Video { Nombre = "Into The Wild", StreamerId = existeStreamer!.Id  }
    };

    await dbContext!.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}


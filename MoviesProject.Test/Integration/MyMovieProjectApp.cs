using Microsoft.AspNetCore.Mvc.Testing;
using MoviesProject.Application.Services.Impl;
using MoviesProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MoviesProject.Test.Integration
{
    internal class MyMovieProjectApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public TestDatabase Database { get; }

        public MyMovieProjectApp()
        {
            Database = new TestDatabase();
            Client = WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_ => Database.Context);
                    services.AddScoped<IMovieService, MovieService>();
                });
            }).CreateClient();
        }
    }
}

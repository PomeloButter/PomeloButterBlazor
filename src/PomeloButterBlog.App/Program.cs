using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PomeloButterBlog.App.HttpClientApi;

namespace PomeloButterBlog.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
          
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:32770") });
            builder.Services.AddScoped<BlogApi>();
            await builder.Build().RunAsync();
        }
    }
}

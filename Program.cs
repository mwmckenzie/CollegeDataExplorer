using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CollegeDataExplorer;
using CollegeDataExplorer.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddMudServices();
builder.Services.AddSingleton<DataStateService>();
builder.Services.AddSingleton<SchoolDataService>();
builder.Services.AddSingleton<ProgramDataService>();


await builder.Build().RunAsync();
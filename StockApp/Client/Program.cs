using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StockApp.Client;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("StockApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("StockApp.ServerAPI"));
// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("StockApp.ServerAPI"));

builder.Services.AddApiAuthorization();

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjYxMjEzQDMyMzAyZTMxMmUzME1nY21HK3dYWW53Wk8rRTl2QlF1bEhjQ0xHc3hHK3IrTUVwMGJRZi9oWWs9");


await builder.Build().RunAsync();

using Blazored.LocalStorage;
using FrontEnd;
using FrontEnd.Consumer;
using FrontEnd.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<AuthMessageHandler>();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
});
builder.Services.AddHttpClient<AuthConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>(); 
builder.Services.AddHttpClient<CartConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddHttpClient<ProductConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>(); 
builder.Services.AddHttpClient<WishListConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>(); 
builder.Services.AddScoped<AdminCategoryConsumer>();
builder.Services.AddScoped<AdminOrderConsumer>();
builder.Services.AddScoped<AdminProductConsumer>();
builder.Services.AddScoped<AdminProductVariantConsumer>();
builder.Services.AddScoped<CheckOutConsumer>();
builder.Services.AddScoped<OrderConsumer>();
builder.Services.AddScoped<FavoriteStateService>();
builder.Services.AddScoped<CartStateService>();
builder.Services.AddScoped<ProductVariantConsumer>();
builder.Services.AddScoped<CategoryConsumer>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<TokenStorage>();


await builder.Build().RunAsync();

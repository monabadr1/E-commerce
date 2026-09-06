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
builder.Services.AddHttpClient<CheckOutConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();
builder.Services.AddHttpClient<OrderConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddHttpClient<AdminCategoryConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddHttpClient<AdminOrderConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddHttpClient<AdminProductConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddHttpClient<AdminProductVariantConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();
builder.Services.AddHttpClient<CategoryConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();
builder.Services.AddHttpClient<DiscountConsumer>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7027/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddScoped<FavoriteStateService>();
builder.Services.AddScoped<CartStateService>();
builder.Services.AddScoped<ProductVariantConsumer>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<TokenStorage>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();

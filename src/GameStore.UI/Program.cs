using GameStore.UI.Clients;
using GameStore.UI.Components;


const string GAME_STORE_API_URL_KEY = "GamesStoreApiUrl";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// this registers default services
// ... like HttpClient and NavigationManager
builder.Services.AddRazorComponents()
	// see video at 3h55m
	// register interactive ServerSideRendering (SSR)
	.AddInteractiveServerComponents();

// register httpClient
var gameStoreApiUrl = builder.Configuration.GetSection(GAME_STORE_API_URL_KEY).Value
	?? throw new ArgumentNullException(GAME_STORE_API_URL_KEY);

builder.Services.AddHttpClient<GamesClient>(httpClient => httpClient.BaseAddress = new Uri(gameStoreApiUrl));
builder.Services.AddHttpClient<GenresClient>(httpClient => httpClient.BaseAddress = new Uri(gameStoreApiUrl));

// no need for these anymore
//builder.Services.AddSingleton<GamesClient>();
//builder.Services.AddSingleton<GenreClient>();


// --------------------------------------------------
// build Kestrel server and things
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// comment this out to prevent a warning in the console when starting the app.
//app.UseHttpsRedirection();

// leave these
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	// register interactive ServerSideRendering (SSR)
	.AddInteractiveServerRenderMode();

app.Run();

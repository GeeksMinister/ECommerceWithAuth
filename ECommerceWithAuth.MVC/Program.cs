var builder = WebApplication.CreateBuilder(args);

var apiLocation = builder.Configuration["ApiLocation"]!;

builder.Services.AddDbContext<ECommerceDbContext>();
builder.Services.AddDbContextPool<ECommerceWithAuthMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ECommerceWithAuthMVCContext>();

builder.Services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.AddRefitClient<IEmployeeClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IProductClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IOrderClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<ISalesReport>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddControllersWithViews();

builder.Services.AddResponseCompression(opts => opts.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.MapBlazorHub();

app.UseAuthentication();;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chathub");

app.Run();

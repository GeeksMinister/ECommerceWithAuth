var builder = WebApplication.CreateBuilder(args);

var apiLocation = builder.Configuration["ApiLocation"]!;

builder.Services.AddDbContext<ECommerceDbContext>();
builder.Services.AddDbContextPool<ECommerceWithAuthMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ECommerceWithAuthMVCContext>();

builder.Services.AddRefitClient<IProductClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddRefitClient<IEmployeeClientData>().ConfigureHttpClient(client => client.BaseAddress = new Uri(apiLocation));
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

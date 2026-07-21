using LibraryManagement.Data;
using LibraryManagement.MappingProfile;
using LibraryManagement.Model.Identity;
using LibraryManagement.Repositories.Implementation;
using LibraryManagement.Repositories.Interface;
using LibraryManagement.Services.Implementation;
using LibraryManagement.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<LibraryMappingProfile>();
});


//identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(option =>
{
    option.Password.RequiredLength = 3;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireDigit = true;
    option.Password.RequireUppercase = false;
    option.User.RequireUniqueEmail = true;
    option.Lockout.MaxFailedAccessAttempts = 2;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    option.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IBorrowRecordRepository, BorrowRecordRepository>();

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    await RoleSeeder.SeedAsync(roleManager);
}

// Configure HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
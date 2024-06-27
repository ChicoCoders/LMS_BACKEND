global using LMS.Models;
global using LMS.Data;
using Microsoft.EntityFrameworkCore;
using LMS.Repository;
using LMS.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Quartz;
using LMS.BackgroundJobs;
using Quartz.Impl.Matchers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDashboardService,DashboardService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<RefreshTokenService>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is a very secure key for me")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});



// Add Hangfire services.
builder.Services.AddHangfire((sp, config) =>
{
    var connectionstring = sp.GetRequiredService<IConfiguration>().GetConnectionString("Server=.;Database=HangFire;Integrated Security=True;TrustServerCertificate=true");
    config.UseSqlServerStorage("Server=.;Database=HangFire;Integrated Security=True;TrustServerCertificate=true");

});
builder.Services.AddHangfireServer();


// Add the processing server as IHostedService

builder.Services.AddAuthorization();
builder.Services.AddCors();

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));





FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serviceAccountKey.json")),
});

builder.Services.AddQuartz(q =>
{
    // Use a Scoped container to create jobs.
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Create a job
    var weelyjob = new JobKey("WeekJob");
    q.AddJob<WeeklyJob>(opts => opts.WithIdentity(weelyjob));

    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(weelyjob) // Link to the ExampleJob
        .WithIdentity("ExampleJob-trigger") // Give the trigger a unique name
        .WithCronSchedule("0 0 0 ? * MON")); // Every sunday 12 Am

    var dailyJobKey = new JobKey("DailyJob");

    // Register the daily job with the DI container
    q.AddJob<DailyJob>(opts => opts.WithIdentity(dailyJobKey));

    // Create a trigger for the daily job to run every day at 12:00 AM
    q.AddTrigger(opts => opts
        .ForJob(dailyJobKey) // Link to the DailyJob
        .WithIdentity("DailyJob-trigger") // Give the trigger a unique name
        .WithCronSchedule("0 0 0 * * ?"));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(policy => policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials());



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHangfireDashboard();
app.UseHttpsRedirection();

app.UseCors(options=>options
    .WithOrigins("http://localhost:3000/")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    );


app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard();

app.MapControllers();

app.Run();

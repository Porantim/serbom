var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:44403")
                // .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddTransient<Serbom.Domain.TokenService>();
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(new Serbom.Domain.TokenService().GetKey()),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseCookiePolicy();
app.UseRouting();
// app.UseRateLimiter();
// app.UseRequestLocalization();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
// app.UseSession();
// app.UseResponseCompression();
// app.UseResponseCaching();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=token}/{action=index}/{id?}");
app.MapFallbackToFile("index.html");

app.Run();

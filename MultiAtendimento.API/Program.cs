using MultiAtendimento.API.Hubs;
using MultiAtendimento.API.Services;
using MultiAtendimento.API.Repository;
using MultiAtendimento.API.Models.Interfaces;
using MultiAtendimento.API.Repository.BancoDeDados;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiAtendimento.API.Models.FilterActionPersonalizado;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddSignalR();

builder.Services.AddDbContext<ContextoDoBancoDeDados>();

builder.Services.AddControllers(opcoes =>
{
    opcoes.Filters.Add(typeof(VerificacaoPadraoDoModelo));
})
.ConfigureApiBehaviorOptions(opcoes =>
{
    opcoes.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato 'Bearer {seu-token}'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddSingleton<IHttpContextAccessor,  HttpContextAccessor>();

builder.Services.AddSingleton<ListaDeChatsTemporaria>();

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMensagemRepository, MensagemRepository>();


builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<SetorService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services
       .AddAuthentication(opcs =>
       {
           opcs.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           opcs.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
       .AddJwtBearer(opcs =>
       {
           opcs.RequireHttpsMetadata = true;
           opcs.SaveToken = true;
           opcs.TokenValidationParameters = TokenService.ObterParametrosDoToken();

           opcs.Events = new JwtBearerEvents
           {
               OnMessageReceived = context =>
               {
                   var accessToken = context.Request.Query["access_token"];

                   var path = context.HttpContext.Request.Path;
                   if (!string.IsNullOrEmpty(accessToken) &&
                       (path.StartsWithSegments("/chatHub")))
                   {
                       context.Token = accessToken;
                   }
                   return Task.CompletedTask;
               }
           };
       });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
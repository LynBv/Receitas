using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Receitas.Api.Context;
using Receitas.Api.Middlewares;
using Receitas.Api.Services;
using Receitas.Api.Services.Parse;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		builder.Services.Configure<JsonOptions>(
			option => option.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
			
		builder.Services.AddScoped<CustomExceptionHandlerMiddleware>();
		builder.Services.AddScoped<IngredienteService>();
		builder.Services.AddScoped<ReceitaService>();
		builder.Services.AddScoped<ReceitaIngredienteService>();
		builder.Services.AddScoped<ParseIngrediente>();
		builder.Services.AddScoped<ParseReceita>();
		builder.Services.AddScoped<ParseReceitaIngrediente>();
		
		builder.Services.AddControllers()
			.AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
			
		builder.Services.AddEndpointsApiExplorer();
		
		builder.Services.AddDbContext<ReceitasContext>(option => 
		{
			string connString = builder.Configuration.GetConnectionString("sqLite")
				?? throw new Exception("Ta faltando a connection string com o nome 'sqLite'");
				
			option.UseSqlite(connString);
		});
		
		builder.Services.AddSwaggerGen(options =>
		{
    		options.SwaggerDoc("v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "Receitas API",
				Description = "API para guardar suas receitas favoritas!."
			});
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		
		app.UseMiddleware<CustomExceptionHandlerMiddleware>();
		
		app.MapControllers();
		
		using (var scope = app.Services.CreateScope())
		{
			var context = scope.ServiceProvider.GetRequiredService<ReceitasContext>();
			context.Database.Migrate();
		}
		

		app.Run();
	}
}
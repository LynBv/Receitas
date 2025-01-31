using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddDbContext<ReceitasContext>(option => 
		{
			string connString = builder.Configuration.GetConnectionString("sqLite")
				?? throw new Exception("Ta faltando a connection string com o nome 'sqLite'");
				
			option.UseSqlite(connString);
		});
		
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.Run();
	}
}
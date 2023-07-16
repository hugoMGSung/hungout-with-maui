
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(); // CORS 해결

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

            //builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();
            //app.UseAuthorization();
            //app.MapControllers();

            app.MapGet("api/todo", async (AppDbContext context) =>
            {
                var items = await context.Todos.ToListAsync();
                return Results.Ok(items);
            });

            app.MapGet("api/todo/{id}", async (AppDbContext context, int id) =>
            {
                var item = await context.Todos.FirstOrDefaultAsync(obj => obj.Id == id);
                return Results.Ok(item);
            });

            app.MapPost("api/todo", async (AppDbContext context, Todo data) =>
            {
                await context.Todos.AddAsync(data);
                await context.SaveChangesAsync();
                return Results.Created($"api/todo/{data.Id}", data);
            });

            app.MapPut("api/todo/{id}", async (AppDbContext context, int id, Todo data) =>
            {
                var result = await context.Todos.FirstOrDefaultAsync(obj => obj.Id == id);

                if (result == null) return Results.NotFound();

                result.TodoName = data.TodoName;
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("api/todo/{id}", async (AppDbContext context, int id) =>
            {
                var result = await context.Todos.FirstOrDefaultAsync(obj => obj.Id == id);

                if (result == null) return Results.NotFound();

                context.Todos.Remove(result);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });

            // CORS 해결
            app.UseCors(options =>
               options.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );

            app.Run();
        }
    }
}
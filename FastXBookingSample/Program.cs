using AutoMapper;
using FastXBookingSample.Helper;
using FastXBookingSample.Models;
using FastXBookingSample.Repository;
using Microsoft.EntityFrameworkCore;
//Hey its Bharat this side editing the Project
namespace FastXBookingSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddTransient<IBusRepository,BusRepository>();
            builder.Services.AddTransient<IBoardingPointRepository,BoardingPointRepository>();
            builder.Services.AddTransient<IDroppingPointRepository,DroppingPointRepository>();
            builder.Services.AddTransient<IAmenityRepository,AmenityRepository>();
            builder.Services.AddTransient<IRouteRepository,RouteRepository>();
            builder.Services.AddTransient<IUserRepository,UserRepository>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<BookingContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:myconnection"]));
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

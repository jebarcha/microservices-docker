using Existance.Grpc.Protos;

namespace Basket.Api
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
            builder.Services.AddSwaggerGen();


            // configure Redis
            builder.Services.AddStackExchangeRedisCache()

            // configure our gRPC service
            builder.Services.AddGrpcClient<ExistanceService.ExistanceServiceClient>
                ( o => o.Address = new Uri(builder.Configuration["GrpcSettings:HostAddress"]) );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
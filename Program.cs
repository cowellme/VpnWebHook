
using System.IO.Pipes;

namespace VpnWebHook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


        public static async void SendNamedPipe(string log)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "payments", PipeDirection.InOut))
                    {
                        pipeClient.Connect(); // Подключаемся к серверу

                        using (StreamWriter writer = new StreamWriter(pipeClient)) // Отправка данных серверу
                        {
                            writer.WriteLine(log);
                            writer.Flush();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
    }
}

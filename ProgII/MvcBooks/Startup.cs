using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcBooks
{
    // Model - слой хранения данных и взаимодействия с ними
    // View - представление, пользовательский интерфейс
    // Controller - принимает запросы от пользователя, обрабатывает их
    // и посылает пользователю ответ

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Набор сервисов для обеспечения работы схемы Model-View-Controller
            IMvcBuilder mvcBuilder = services.AddMvc();

            // Нужно для работы app.UseMvcWithDefaultRoute();
            mvcBuilder.AddMvcOptions(x => x.EnableEndpointRouting = false);

            // Создаём службу-синглтон для базы данных
            // Для потребителей (контроллеры) будет доступна через интерфейс IBooksStorage
            services.AddSingleton<IBooksStorage, BooksDatabase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Разрешить передачу статических файлов из /wwwroot/
            // (CSS, картинки и т.д.)
            app.UseStaticFiles();

            // Включает режим MVC, и добавляет рутинг на дефолтный ресурс (хост/), который ведёт
            // на контроллер под названием Home
            app.UseMvcWithDefaultRoute();

            /*app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/hello", async context =>
                {
                    await context.Response.WriteAsync("Hello TA-19V!");
                });
            });*/
        }
    }
}

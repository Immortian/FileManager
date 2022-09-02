using FileManager.Data;
using FileManager.ViewModels;
using FileManager.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FileManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            //ensure that db exists
            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<historydbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception e) { }
            }
        }

        /// <summary>
        /// A kind of Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<historydbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Database=historydb;Username=filemanager;Password=filemanager");
            });
            
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}

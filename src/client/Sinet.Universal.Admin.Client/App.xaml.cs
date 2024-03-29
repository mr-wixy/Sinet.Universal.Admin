﻿using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Configuration;
using System.Data;
using System.Windows;
using Volo.Abp;

namespace Sinet.Universal.Admin.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IAbpApplicationWithInternalServiceProvider? _abpApplication;

        protected override async void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

            try
            {
                Log.Information("Starting WPF host.");

                _abpApplication = await AbpApplicationFactory.CreateAsync<ClientModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                });

                await _abpApplication.InitializeAsync();
                var serviceProvider = _abpApplication.Services.GetRequiredService<IServiceProvider>();
                Resources.Add("services", serviceProvider);
                _abpApplication.Services.GetRequiredService<MainWindow>()?.Show();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_abpApplication != null)
            {
                await _abpApplication.ShutdownAsync();
            }
            Log.CloseAndFlush();
        }
    }

}

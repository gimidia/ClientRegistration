using ClientRegistration.Services;
using ClientRegistration.ViewModels;
using ClientRegistration.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace ClientRegistration
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows => windows.OnWindowCreated((window) =>
                    {
                        var appWindow = window.AppWindow;
                        if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                        {
                            presenter.Maximize();
                        }
                    }));
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar serviços
            builder.Services.AddSingleton<IClientDatabase, ClientDatabase>();
            builder.Services.AddSingleton<IClientService, ClientService>();

            // Registrar ViewModels
            builder.Services.AddTransient<ClientListViewModel>();
            builder.Services.AddTransient<ClientDetailViewModel>();

            // Registrar Views
            builder.Services.AddTransient<ClientListPage>();
            builder.Services.AddTransient<ClientDetailPage>(); 

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

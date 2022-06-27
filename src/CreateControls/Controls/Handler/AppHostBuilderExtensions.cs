using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace CreateControls.Controls
{
    public static class AppHostBuilderExtensions
    {
        public static MauiAppBuilder ConfigureCustomControls(this MauiAppBuilder builder, bool useCompatibilityRenderers = false)
        {
            builder
                .ConfigureMauiHandlers(handlers =>
                {
                    if (useCompatibilityRenderers)
                        handlers.AddLibraryCompatibilityRenderers();
                    else
                        handlers.AddLibraryHandlers();
                });

            return builder;
        }

        public static IMauiHandlersCollection AddLibraryCompatibilityRenderers(this IMauiHandlersCollection handlers)
        {
#if __ANDROID__
            handlers.AddCompatibilityRenderer(typeof(CustomCheckBox), typeof(CustomCheckBoxRenderer));
#endif
            return handlers;
        }

        public static IMauiHandlersCollection AddLibraryHandlers(this IMauiHandlersCollection handlers)
        {
            handlers.AddTransient(typeof(CustomCheckBox), h => new CustomCheckBoxHandler());

            return handlers;
        }
    }
}
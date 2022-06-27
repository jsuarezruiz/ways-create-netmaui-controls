using CreateControls.Controls;

namespace CreateControls;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureCustomControls();

		return builder.Build();
	}
}
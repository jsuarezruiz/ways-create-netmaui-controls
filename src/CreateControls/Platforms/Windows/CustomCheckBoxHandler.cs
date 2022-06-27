using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<ICustomCheckBox, FrameworkElement>
    {
        protected override FrameworkElement CreatePlatformView() => throw new NotImplementedException();

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox) { }
    }
}
using Microsoft.Maui.Handlers;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<ICustomCheckBox, object>
    {
        protected override object CreatePlatformView() => throw new NotImplementedException();

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox) { }
    }
}
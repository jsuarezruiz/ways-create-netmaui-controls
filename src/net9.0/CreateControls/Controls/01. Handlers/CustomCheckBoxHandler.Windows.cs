using Microsoft.Maui.Handlers;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<CustomCheckBox, Microsoft.UI.Xaml.Controls.CheckBox>
    {
        protected override Microsoft.UI.Xaml.Controls.CheckBox CreatePlatformView()
        {
            throw new NotImplementedException();
        }

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox)
        {
            handler.PlatformView?.UpdateIsChecked(checkBox);
        }
    }
}

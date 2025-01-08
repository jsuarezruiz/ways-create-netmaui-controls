using Microsoft.Maui.Handlers;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<CustomCheckBox, UIKit.UIButton>
    {
        protected override UIKit.UIButton CreatePlatformView()
        {
            throw new NotImplementedException();
        }

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox)
        {
            handler.PlatformView?.UpdateIsChecked(checkBox);
        }
    }
}

using Microsoft.Maui.Handlers;
using UIKit;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<ICustomCheckBox, UIView>
    {
        protected override UIView CreatePlatformView() => throw new NotImplementedException();

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox) { }
    }
}
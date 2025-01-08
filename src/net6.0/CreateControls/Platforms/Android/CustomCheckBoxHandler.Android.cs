using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler : ViewHandler<ICustomCheckBox, AppCompatCheckBox>
    {
        protected override AppCompatCheckBox CreatePlatformView()
        {
            var platformCheckBox = new AppCompatCheckBox(Context)
            {
                SoundEffectsEnabled = false
            };

            return platformCheckBox;
        }

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox) 
        {
            handler.PlatformView?.UpdateIsChecked(checkBox);
        }
    }
}
using Microsoft.Maui.Handlers;

#if IOS || MACCATALYST
using PlatformView = UIKit.UIButton;
#elif ANDROID
using PlatformView = AndroidX.AppCompat.Widget.AppCompatCheckBox;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.GraphicsView.CheckBox;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.CheckBox;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
# endif

namespace CreateControls.Controls
{
    public partial class CustomCheckBoxHandler
    {
        public static IPropertyMapper<CustomCheckBox, CustomCheckBoxHandler> PropertyMapper = new PropertyMapper<CustomCheckBox, CustomCheckBoxHandler>(ViewHandler.ViewMapper)
        {
            [nameof(CustomCheckBox.IsChecked)] = MapIsChecked,
        };

        public static CommandMapper<CustomCheckBox, CustomCheckBoxHandler> CommandMapper = new(ViewCommandMapper);

        public CustomCheckBoxHandler() : base(PropertyMapper, CommandMapper)
        {
        }
    }
}

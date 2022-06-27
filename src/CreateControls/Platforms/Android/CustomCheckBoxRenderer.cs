using Android.Content;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;

namespace CreateControls.Controls
{
    public class CustomCheckBoxRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ViewRenderer<CheckBox, AppCompatCheckBox>
    {
        public CustomCheckBoxRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new AppCompatCheckBox(Context));
                }

                UpdateIsChecked();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(CustomCheckBox.IsCheckedProperty.PropertyName))
                UpdateIsChecked();
        }

        void UpdateIsChecked()
        {
            Control.Checked = Element.IsChecked;
        }
    }
}
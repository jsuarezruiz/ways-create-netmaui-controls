using AndroidX.AppCompat.Widget;

namespace CreateControls.Controls
{
    public static class CustomCheckBoxExtensions
    {
        public static void UpdateIsChecked(this AppCompatCheckBox platformCheckBox, ICustomCheckBox checkBox)
        {
            platformCheckBox.Checked = checkBox.IsChecked;
        }
    }
}
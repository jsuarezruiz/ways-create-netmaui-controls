namespace CreateControls.Controls
{
    public static class CustomCheckBoxExtensions
    {
        public static void UpdateIsChecked(this Microsoft.UI.Xaml.Controls.CheckBox platformCheckBox, ICustomCheckBox checkBox)
        {
            platformCheckBox.IsChecked = checkBox.IsChecked;
        }
    }
}
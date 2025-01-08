namespace CreateControls.Controls
{
    public static class CustomCheckBoxExtensions
    {
        public static void UpdateIsChecked(this object platformCheckBox, ICustomCheckBox checkBox)
        {
            throw new NotImplementedException();
        }

        public static void MapIsChecked(CustomCheckBoxHandler handler, ICustomCheckBox checkBox)
        {
            handler.PlatformView?.UpdateIsChecked(checkBox);
        }
    }
}
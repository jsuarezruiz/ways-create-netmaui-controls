namespace CreateControls.Controls
{
    public class CustomCheckBox : View, ICustomCheckBox
    {
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CustomCheckBox), false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CustomCheckBox)bindable).CheckedChanged?.Invoke(bindable, new CheckedChangedEventArgs((bool)newValue));
                }, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(CustomCheckBox), null);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;
    }
}
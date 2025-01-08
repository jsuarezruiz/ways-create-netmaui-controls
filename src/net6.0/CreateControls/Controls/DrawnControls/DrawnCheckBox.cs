namespace CreateControls.Controls
{
    public class DrawnCheckBox : GraphicsView
    {
        public DrawnCheckBox()
        {
            HorizontalOptions = LayoutOptions.Start;
            VerticalOptions = LayoutOptions.Start;

            HeightRequest = 24;
            WidthRequest = 24;

            Drawable = CheckBoxDrawable = new CheckBoxDrawable();

            StartInteraction += OnCheckBoxStartInteraction;
        }

        public CheckBoxDrawable CheckBoxDrawable { get; set; }

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(DrawnCheckBox), false,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is DrawnCheckBox checkBox)
                    {
                        checkBox.UpdateIsChecked();
                    }
                });

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty CheckedBrushProperty =
            BindableProperty.Create(nameof(Color), typeof(Brush), typeof(DrawnCheckBox), Brush.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is DrawnCheckBox checkBox)
                    {
                        checkBox.UpdateCheckedBrush();
                    }
                });

        public Brush CheckedBrush
        {
            get => (Brush)GetValue(CheckedBrushProperty);
            set => SetValue(CheckedBrushProperty, value);
        }

        public static readonly BindableProperty UncheckedBrushProperty =
            BindableProperty.Create(nameof(Color), typeof(Brush), typeof(DrawnCheckBox), Brush.Transparent,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is DrawnCheckBox checkBox)
                    {
                        checkBox.UpdateUncheckedBrush();
                    }
                });

        public Brush UncheckedBrush
        {
            get => (Brush)GetValue(UncheckedBrushProperty);
            set => SetValue(UncheckedBrushProperty, value);
        }

        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(DrawnCheckBox), Brush.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is DrawnCheckBox checkBox)
                    {
                        checkBox.UpdateStroke();
                    }
                });

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(DrawnCheckBox), 3.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is DrawnCheckBox checkBox)
                    {
                        checkBox.UpdateStrokeThickness();
                    }
                });

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateIsChecked();
                UpdateCheckedBrush();
                UpdateUncheckedBrush();
                UpdateStroke();
                UpdateStrokeThickness();
            }
        }

        void UpdateIsChecked()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.IsChecked = IsChecked;

            Invalidate();
        }

        void UpdateCheckedBrush()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.CheckedPaint = CheckedBrush;

            Invalidate();
        }

        void UpdateUncheckedBrush()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.UncheckedPaint = UncheckedBrush;

            Invalidate();
        }

        void UpdateStroke()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.StrokePaint = Stroke;

            Invalidate();
        }

        void UpdateStrokeThickness()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.StrokeThickness = StrokeThickness;

            Invalidate();
        }

        void OnCheckBoxStartInteraction(object sender, TouchEventArgs e)
        {
            IsChecked = !IsChecked;

            UpdateIsChecked();

            CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(IsChecked));
        }
    }
}
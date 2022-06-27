using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace CreateControls.Controls
{
    public class ContentViewCheckBox : ContentView, IDisposable
    {
        TapGestureRecognizer _tapGestureRecognizer;
        Shape _background;
        Shape _glyph;

        public ContentViewCheckBox()
        {
            Initialize();
        }

        void Initialize()
        {
            var container = new Grid
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                RowSpacing = 0,
                Margin = new Thickness(6)
            };

            _background = new Rectangle
            {
                Fill = Brush.Transparent,
                HeightRequest = 18,
                WidthRequest = 18,
                RadiusX = 2,
                RadiusY = 2,
                Stroke = Color,
                StrokeThickness = 2
            };

            _glyph = new Path
            {
                Aspect = Stretch.Uniform,
                Stroke = Brush.White,
                StrokeThickness = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 14,
                WidthRequest = 14,
                Opacity = 0,
                Margin = new Thickness(2)
            };

            var pathGeometryConverter = new PathGeometryConverter();

            if (_glyph is Path path && pathGeometryConverter.ConvertFromInvariantString("M30.561941,0L31.997,1.393004 10.467954,23.597999 0,15.350999 1.2379759,13.780992 10.287961,20.909952z") is Geometry data)
                path.Data = data;

            container.Children.Add(_background);
            container.Children.Add(_glyph);

            Content = container; 

            _tapGestureRecognizer = new TapGestureRecognizer();

            UpdateIsEnabled();
        }

        public void Dispose()
        {
            if (_tapGestureRecognizer != null)
                _tapGestureRecognizer.Tapped -= OnCheckBoxTapped;
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Brush), typeof(ContentViewCheckBox), Brush.DeepPink);

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ContentViewCheckBox), false,
                propertyChanged: OnIsCheckedChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        static async void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ContentViewCheckBox checkbox)) return;
            checkbox.CheckedChanged?.Invoke(checkbox, new CheckedChangedEventArgs((bool)newValue));
            await checkbox.AnimateCheckedChanged();
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsEnabledProperty.PropertyName)
                UpdateIsEnabled();
        }

        void UpdateIsEnabled()
        {
            if (IsEnabled)
            {
                _tapGestureRecognizer.Tapped += OnCheckBoxTapped;
                GestureRecognizers.Add(_tapGestureRecognizer);
            }
            else
            {
                _tapGestureRecognizer.Tapped -= OnCheckBoxTapped;
                GestureRecognizers.Remove(_tapGestureRecognizer);
            }
        }

        void OnCheckBoxTapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;

            if (IsChecked)
            {
                if (_background != null)
                    _background.Fill = Color;

                if (_glyph != null)
                    _glyph.Opacity = 1;
            }
            else
            {
                if (_background != null)
                    _background.Fill = Brush.Default;

                if (_glyph != null)
                    _glyph.Opacity = 0;
            }

            RaiseCheckedChanged();
        }

        async Task AnimateCheckedChanged()
        {
            if (_background.Parent is View parent)
            {
                await parent.ScaleTo(0.85, 100);
                await parent.ScaleTo(1, 100, Easing.BounceOut);
            }
        }

        void RaiseCheckedChanged()
        {
            var checkedChangedEventArgs = new CheckedChangedEventArgs(IsChecked);
            CheckedChanged?.Invoke(this, checkedChangedEventArgs);
        }
    }
}
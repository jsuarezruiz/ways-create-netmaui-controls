# Ways to create and customize .NET MAUI Controls

In this repository we collect all the ways to create or extend controls in .NET MAUI.

1. Using Custom Renderers (Xamarin.Forms Architecture).
2. Using Custom Handlers.
3. Using ContentView.
4. Using TemplatedView (Templated Controls).
5. Using GraphicsView (Drawn controls).

## 1. Using Custom Renderers (Xamarin.Forms Architecture)

To make the transition from Xamarin.Forms to .NET MAUI as smooth as possible, the Compatability package is added that adds Xamarin.Forms functionality allowing to reuse code such as Custom Renderers without require to make changes.

Xamarin.Forms user interfaces are rendered using the native controls of the target platform, allowing Xamarin.Forms applications to retain the appropriate look and feel for each platform. Custom Renderers let developers override this process to customize the appearance and behavior of Xamarin.Forms controls on each platform.

### Custom Renderer

![Custom Renderer](images/custom-renderer.png)

Let's see an example. We are going to create a custom Entry.

```
namespace Renderers
{
    public class CustomEntry : Entry
    {

    }
}
```

Android Implementation.

```
using Android.Content;
using Compatibility;
using Compatibility.Droid.Renderers;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Compatibility.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
            }
        }
    }
}
```

To use a custom Renderer the main changes are replace some namespace to use **Microsoft.Maui.Controls.Compatibility**.

In addition, instead of using assembly scanning, .NET MAUI uses the MauiProgram class to perform tasks like registering Handlers or Renderers.

```
appBuilder
    .UseMauiApp<App>()
    .ConfigureMauiHandlers(handlers =>
    {
#if __ANDROID__
        handlers.AddCompatibilityRenderer(typeof(CustomEntry), typeof(Droid.Renderers.CustomEntryRenderer));
#endif
    });
```

### Pros & Cons

**Pros**
* We get good performance (native control).
* We can allow extensions (Effects, Platform Specific, etc.) taking advantage of the capacity and possibilities of the native code.

**Cons**
* Requires more code.
* Requires native development skills on each platform.
* Extending also requires knowledge of native development.

## 2. Using Custom Handlers

Do you remember the ExportRenderer attribute that you use to register the Renderer? This tells Xamarin.Forms that at startup, making use of assembly scanning, it should search all the libraries referenced and using this attribute, and if it finds it, register the renderer. It's easy to use, but ... assembly scanning is slow and penalizes startup.

OnElementChanged method usually causes confusion. When to use OldElement?, and NewElement?, when do I create my default values or subscribe to events?, and when do I unsubscribe?. Cause confusion is a problem, but it is even bigger problem if not having an easy way to subscribe/unsubscribe causes to sometimes unsubscribe (for example) and... penalizes performance.

All those private methods to update properties of the native control are a big problem. You may need to make a small change and due to lack of access (again, private methods here and there!), you end up creating a Custom Renderer larger than necessary, etc.

### Creating the control

Handlers can be accessed through a control-specific interface provided derived from IView interface. This avoids the cross-platform control having to reference its handler, and the handler having to reference the cross-platform control. The mapping of the cross-platform control API to the platform API is provided by a mapper.

```
public interface ICustomEntry : IView
{
    public string Text { get; }
    public Color TextColor { get; }
    public string Placeholder { get; }
    public Color PlaceholderColor { get; }
    public double CharacterSpacing { get; }
    public TextAlignment HorizontalTextAlignment { get; }
    public TextAlignment VerticalTextAlignment { get; }

    void Completed();
}
```

### Creating the Custom Handler on each Platform

Create a subclass of the ViewHandler class that renders the native control.

```
public partial class CustomEntryHandler : ViewHandler<ICustomEntry, EditText>
{

}
```

Inheriting from ViewHandler, we have to implement the **CreateNativeView** method.

```
protected override EditText CreateNativeView()
{
    return new EditText(Context);
}
```

### The Mapper

The Mapper is a new concept introduced by Handlers. It is nothing more than a dictionary with the properties (and actions) defined in the interface of our control (remember, we use interfaces in the Handler). It replaces everything that was done in the OnElementPropertyChanged method in Xamarin.Forms.

```
public static PropertyMapper<ICustomEntry, CustomEntryHandler> CustomEntryMapper = new PropertyMapper<ICustomEntry, CustomEntryHandler>(ViewHandler.ViewMapper)
{
    [nameof(ICustomEntry.Text)] = MapText,
    [nameof(ICustomEntry.TextColor)] = MapTextColor,
    [nameof(ICustomEntry.Placeholder)] = MapPlaceholder,
    [nameof(ICustomEntry.PlaceholderColor)] = MapPlaceholderColor,
    [nameof(ICustomEntry.CharacterSpacing)] = MapCharacterSpacing,
    [nameof(ICustomEntry.HorizontalLayoutAlignment)] = MapHorizontalLayoutAlignment,
    [nameof(ICustomEntry.VerticalLayoutAlignment)] = MapVerticalLayoutAlignment
};
```

### Registering the handler

Unlike Xamarin.Forms using the ExportRenderer attribute which in turn made use of Assembly Scanning, in .NET MAUI the handler registration is slightly different. We make use of AppHostBuilder and the **AddHandler** method to register the Handler. 

```
appBuilder
    .UseMauiApp<App>()
    .ConfigureMauiHandlers(handlers =>
    {
#if __ANDROID__
        handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
#endif
    });
```

### Pros & Cons

**Pros**

* We get good performance (native control).
* We can allow extensions in an easy way using the Mapper and taking advantage of the capacity and possibilities of the native code.

**Cons**

* Requires more code.
* Requires native development skills on each platform.
* Extending also requires knowledge of native development.

## 3. Using ContentView

**ContentView** is a type of Layout that contains a single child element and is typically used to create custom, reusable controls.

The process for creating a custom control is:
1. Create a new class that inherits from ContentView.
2. Define properties (BindableProperty) or events.
3. Create the user interface.

### Rememer to consider

For the best possible performance:
* Have exhaustive control of the hierarchy to create with the control. The higher the hierarchy, the greater the negative impact on performance.
* Create controls that implement IDisposable and remember to free up resources.

### Pros & Cons

**Pros**
* Simple to create. No platform-specific knowledge required; everything is built using the .NET MAUI abstraction.
* Define the control only once for all platforms.

**Cons**
* If to define a control, we create it via composition using 5 .NET MAUI views, it is required to instantiate those 5 views with a performance impact.

## 4. Using TemplatedView

**Allow customize everything?**

With a good specification for a control, creating properties, events, etc. we can cover most requirements, but it would be impossible to cover all cases.

Let's take an example. In our CheckBox, we can add a property to customize the border color, but what if someone wants a dotted border? Okay, we can add another property, but what if someone needs…?

### ControlTemplate

The **ControlTemplate** allows you to define the visual structure of the control.

```
<ControlTemplate>
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>
        <Rectangle
            x:Name="PART_Background"
            Stroke="{TemplateBinding Color}"/>
        <Path
            x:Name="PART_Glyph"
            Data="M30.561941,0L31.997,1.393004 10.467954,23.597999 0,15.350999 1.2379759,13.780992 10.287961,20.909952z"
            Stroke="White"/>
        <ContentPresenter
            x:Name="PART_Content"
            Content="{TemplateBinding Content}"
            Grid.Column="1"
            HorizontalOptions="Center"
            VerticalOptions="Center"/>
    </Grid>
</ControlTemplate>
```

The element called **ContentPresenter**, often used within control templates, is an element that allows to insert content at runtime.

The **TemplateBinding** markup extension, on the other hand, binds a property of an element that is in a ControlTemplate class to a public property defined by the custom control.

After a control template has been instantiated, the **OnApplyTemplate** method is invoked. In this method we can use the **GetTemplateChild** method to access the views that compose the control.

### Rememer to consider

For the best possible performance:
1. Control template elements are typically accessed for generic operations related to assigning sizes, positions, or basic properties such as colors. Use base elements. This way, if the template is modified it has no impact.
2. Do not access elements of the template if you are not going to use them.
3. Create controls that implement IDisposable and remember to free up resources.

### Pros & Cons

**Pros**

* Simple to create. No platform-specific knowledge required; everything is built using the .NET MAUI abstraction.
* Define the control only once for all platforms.
* It allows not only to customize the control via properties, but also to access the template and modify anything!

**Cons**

* If to define a control, we create it via composition using 5 .NET MAUI views, it is required to instantiate those 5 views with a performance impact.

## 5. Using GraphicsView

### Microsoft.Maui.Graphics

Microsoft.Maui.Graphics is a cross-platform graphics library for iOS, Android, Windows, macOS, Tizen and Linux written completely in C#. With this library you can use a common API to target multiple abstractions allowing you to share your drawing code between platforms or mix and match graphics implementations within a singular application.

In .NET MAUI a new control is included, GraphicsView, which exposes a Canvas where we can draw using MAUI Graphics.

### Use GraphicsView: The Canvas

To start drawing, just call the drawable Draw method. 

```
public class CheckBoxDrawable : IDrawable
{
     public void Draw(ICanvas canvas, RectF dirtyRect)
     {

     }
}
```

### Use GraphicsView: Draw

To draw something, we can directly add a point, line, rectangle, circle, rounded rectangle, text or image to the canvas. 

```
canvas.DrawRoundedRectangle(0, 0, 24, 24, 6);
```

### Use GraphicsView : User Input

Controls typically receive some input from the user. To make our control interactive, we can use .NET MAUI gestures, native gestures, or GraphicsView Touch events.

```
StartInteraction += OnCheckBoxStartInteraction;

void OnCheckBoxStartInteraction(object sender, TouchEventArgs e)
{

}
```

We will mainly make use of a Canvas where we will draw most of the control. MAUI Graphics, as we have seen, also allows us to manage user interaction, etc. To allow customization we rely on the use of BindableProperties.

### Rememer to consider

For the best possible performance:
* Avoid using multiple Canvas and draw as much as possible on a single Canvas.
* When creating controls, you can use a GraphicsView as a base element instead of a ContentView if you don't need to compose (add other elements) besides the Canvas.
  
### Pros & Cons

**Pros**

* Drawing directly on a Canvas and even with the possibility of using GPU, very high performance.
* Define the control only once for all platforms.

**Cons**

* _“We are not using native elements; we are drawing on a canvas.”_
* Requires knowledge of Maui Graphics APIs.

using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
#if ANDROID
using Android.Graphics;
using AButton = Android.Widget.Button;
#endif

namespace Vapolia.Maui.Effects;

public static class ButtonEffects
{
    public static readonly BindableProperty IsUnderlinedProperty = BindableProperty.CreateAttached("IsUnderlined", typeof(bool?), typeof(ButtonEffects), null, propertyChanged: OnChanged);
    public static readonly BindableProperty IsCapitalizedProperty = BindableProperty.CreateAttached("IsCapitalized", typeof(bool?), typeof(ButtonEffects), null, propertyChanged: OnChanged);
    public static readonly BindableProperty RemovePaddingProperty = BindableProperty.CreateAttached("RemovePadding", typeof(bool?), typeof(ButtonEffects), null, propertyChanged: OnChanged);

    public static bool? GetIsUnderlined(BindableObject view) => (bool?)view.GetValue(IsUnderlinedProperty);
    public static bool? GetIsCapitalized(BindableObject view) => (bool?)view.GetValue(IsCapitalizedProperty);
    public static bool? GetRemovePadding(BindableObject view) => (bool?)view.GetValue(RemovePaddingProperty);

    private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is VisualElement visual && !visual.Effects.Any(e => e is Effect))
            visual.Effects.Add(new Effect());
    }

    private class Effect : RoutingEffect {}

    public static MauiAppBuilder AddButtonEffects(this MauiAppBuilder builder)
    {
        //In MAUI, an effect MUST be registered on all platforms, when used in an app. Otherwise MAUI crashes on the platforms where it's not implemented.
        builder.ConfigureEffects(effects => effects.Add<ButtonEffects.Effect, PlatformButtonEffect>());
        return builder;
    }
}


internal class PlatformButtonEffect : PlatformEffect
{
    protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
    {
#if ANDROID
        if (Control is AButton button)
        {
            if (args.PropertyName == string.Empty || args.PropertyName == ButtonEffects.IsUnderlinedProperty.PropertyName)
            {
                var isUnderlined = ButtonEffects.GetIsUnderlined(Element);
                if (isUnderlined.HasValue)
                {
                    if (isUnderlined == true)
                        button.PaintFlags |= PaintFlags.UnderlineText;
                    else
                        button.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }

            if (args.PropertyName == string.Empty || args.PropertyName == ButtonEffects.IsCapitalizedProperty.PropertyName)
            {
                var isCapitalized = ButtonEffects.GetIsCapitalized(Element);
                if (isCapitalized.HasValue)
                    button.SetAllCaps(isCapitalized.Value);
            }

            if (args.PropertyName == string.Empty || args.PropertyName == ButtonEffects.RemovePaddingProperty.PropertyName)
            {
                var removePadding = ButtonEffects.GetRemovePadding(Element);
                if (removePadding == true)
                {
                    button.SetMinHeight(-1);
                    button.SetMinimumHeight(-1);
                    button.SetMinWidth(-1);
                    button.SetMinimumWidth(-1);
                    button.SetPadding(0, 0, 0, 0);
                }
            }
        }
#endif
    }

    protected override void OnAttached()
    {
        OnElementPropertyChanged(new PropertyChangedEventArgs(string.Empty));
    }

    protected override void OnDetached()
    {
    }
}

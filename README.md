# Samples for the Enterprise SVG UI control for MAUI and native platforms

This repository contains samples and documentation for the SVG control on Windows, Android, iOS, and Mac.
[More infos](https://vapolia.eu)

[![NuGet][nuget-img]][nuget-link] ![Nuget](https://img.shields.io/nuget/dt/Vapolia.Svg)

# SvgImage and SvgImageSource

.net Maui Controls:
- `SvgImage` displays an svg
- `SvgImageSource` displays an svg inside buttons, tabs and more.

.net Native Controls (without maui):
- Android: `SvgImageView` and `SvgPictureDrawable`
- iOS and Mac:  `UISvgImageView` with support for xcode designer




# Quick start for MAUI

## In MauiProgram.cs, add `.UseEasySvg()` to the builder

```c#
builder
    .UseMauiApp<App>()
    .UseEasySvg();
```

## Add some SVG images

- Create a new folder named `VectorImages` in your project. Do not use the existing `Resources/Images` folder !
- place your SVG images there, ensuring they have the `.svg` extension.
- add the following lines to your `.csproj` file:

```xml
<ItemGroup>
  <None Remove="VectorImages\**\*.svg" />
  <EmbeddedResource Include="VectorImages\**\*.svg" />
</ItemGroup>
```

 That configures the svg images as `embedded resources`.


##  In a XAML page, use `SvgImage` or `SvgImageSource`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:svg="https://vapolia.eu/svg"
             x:Class="XamSvg.Demo.MainPage"
             IconImageSource="{svg:Svg myicon.svg,Height=60}">

  <ContentView>
    <StackLayout Orientation="Vertical" VerticalOptions="Start">

        <svg:SvgImage Source="logo.svg" HorizontalOptions="Start" HeighRequest="32" />

        <svg:SvgImage Source="logo.svg" HorizontalOptions="Start" HeighRequest="32"
                      ColorMapping="{Binding ColorMapping}" 
                      ColorMappingSelected="ffffff=>00ff00,000000=>0000FF" 
                      />
      
        <svg:SvgImage WidthRequest="100" Source="https://upload.wikimedia.org/wikipedia/commons/1/15/Svg.svg" />

        <svg:SvgImage WidthRequest="100" Source="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlb..." />

        <Image Source="{svg:Svg Source=no_image_available.svg, Width=40, ColorMapping='000=>666'}"
               WidthRequest="40" />
      
    </StackLayout>
  </ContentView>
</ContentPage>
```

Remarks:
* The `xmlns:svg` attribute is required on the `ContentPage`.
* `Source` is the name of an svg image. This image is searched in embedded resources across all loaded assemblies. This list is cached on first use.
* All properties are bindable.

You can also use `<SvgImageSource Svg="...." Height="50" />` as the ImageSource for tab icons, button icons, navigation bar icons, and more.  
For these elements, the `Widht` and/or `Height` properties are mandatory and cannot be bound, as `ImageSource` objects are static in MAUI (a MAUI limitation).  
The compact syntax `ImageSource="{svg:Svg refresh.svg,Height=50}"` is easier to use.

Note: `SvgImageSource` converts the svg image into a static raster image, which is then displayed by another UI control like `Image`. As a result, it supports only one ColorMapping.

# Color Mapping

The `ColorMapping`, `ColorMappingSelected` and `ColorMappingDisabled` properties allow you to modify specific colors in the SVG before displaying it, or based on its state (normal, selected, disabled).

For example, the string `ffffff=>00ff00 000000=>0000FF` means:
* replace `ffffff` (white) by `00ff00` (green)
* and replace `000000` (black) by `0000FF` (blue).

A color mapping is a string containing a list of mappings separated by ",", ";", or a space.  
Each mapping consists of two parts, separated by "=>" (or "="). The left part is the color to replace, and the right part is the new color.  
A color is a standard HTML color code in one of the following format: `AARRGGBB`, `RRGGBB`, or `RGB`. A is the transparency (alpha channel).

Color mappings are bindable: instead of specifying a string, you can use a list of `ColorMapping` objects:

```c#
<svg:SvgImage Source="logo.svg" HorizontalOptions="Start" HeighRequest="32">
    <ColorMapping OldColor="Blue" NewColor="#80000000" />
    <ColorMapping OldColor="#FFF" NewColor="{Binding YourColorProperty}" />
</svg:SvgImage>              
```

# Maui samples

Simple svg image
```xml
<svg:SvgImage Source="logo.svg" HeightRequest="70" HorizontalOptions="Center" VerticalOptions="Center" />
```  

Svg image on a button

```xml
<Button Text="Add Contact" ContentLayout="Right,20" ImageSource="{svg:Svg tabHome.svg,Height=60,ColorMapping='000000=>FF0000'}" />
```  

Svg image for the icon of a TabbedPage tab

```xml
<NavigationPage Title="Home" IconImageSource="{svg:Svg tabHome.svg,Height=60}">
  <x:Arguments>
      <views:HomePage />
  </x:Arguments>
</NavigationPage>
```

Svg image with bindable color mapping

```xml
<svg:SvgImage Source="logo.svg" HeightRequest="60">
    <!-- the ColorMapping property is the default content property: no need to embed it into a svg:SvgImage.ColorMapping tag -->
    <svg:ColorMapping OldColor="#000" NewColor="{Binding CurrentColor}" />
    <svg:SvgImage.ColorMappingSelected>
        <svg:ColorMapping OldColor="#000" NewColor="{Binding CurrentColor}" />
    </svg:SvgImage.ColorMappingSelected>
</svg:SvgImage>
```  

# Common mistakes

If nothing appears, ensure your svg file is displayed correctly in Windows Explorer (after installing this [extension](https://github.com/maphew/svg-explorer-extension/releases)). 

Common errors include:
* Forgetting to set the build action of the svg file to "Embedded resource".
* Missing the `viewBox` attribute at the root of the svg file (open it using a text editor to check).
* The svg color being the same as the background color, especially white or black. Use ColorMapping to adjust the colors, or edit your svg file using [inkscape](http://www.inkscape.org/) or your preferred editor.

The assembly containing the svg resources must have an `Assembly Name` that matches its `Default Namespace`. Otherwise, the svg files will not be found. If you cannot do this, you can still display an svg by specifying the full resource name, like this:

```xml
    <svg:SvgImage
        Source="YourDefaultNamespace.Images.getFDR_01_Ready.svg"
        x:Name="SvgIcon" HorizontalOptions="Fill" VerticalOptions="Start" Margin="8,0,8,0" BackgroundColor="Yellow"
        />
```

You can discover the full name of an embedded resource by opening your assembly (.dll in your bin folder) with the free tool `Telerik JustDecompile`.

[![image.png](https://i.postimg.cc/8cT3hbPM/image.png)](https://postimg.cc/jwkZTPfS)

Another common mistake is setting the `ImageSource` of a `Button` in a style or trigger. Instead, you must set the `Image` property.

```xml
<Style x:Key="FAB" TargetType="Button">
  <!-- correct -->
  <Setter Property="Image" Value="{svg:Svg create.svg,Height=50,ColorMapping='000=>634109'}"/>
  <!-- incorrect 
  <Setter Property="ImageSource" Value="{svg:Svg create.svg,Height=50,ColorMapping='000=>634109'}"/>
  -->
</Style>
```


# Other Receipes

**Android native** (not MAUI Android): set the SVG image height the match the height of a Button
<details>
  <summary>Click to expand</summary>
  
```xml
<?xml version="1.0" encoding="utf-8"?>
<android.support.constraint.ConstraintLayout 
              xmlns:android="http://schemas.android.com/apk/res/android"
              xmlns:app="http://schemas.android.com/apk/res-auto"
              xmlns:tools="http://schemas.android.com/tools"
              android:layout_width="match_parent"
              android:layout_height="match_parent">
    <Button
        android:id="@+id/myinfo"
        app:layout_constraintTop_toTopOf="parent"
        app:layout_constraintLeft_toLeftOf="parent"
        app:layout_constraintRight_toRightOf="parent"
        android:layout_width="0dp"
        android:layout_height="wrap_content"
        android:text="@string/Menu_MyInfo"
        style="@style/Widget.AppCompat.Button.Borderless"
        app:MvxBind="Click SettingsCommand"
        />
    <vapolia.SvgImageView
        app:layout_constraintTop_toTopOf="@+id/myinfo"
        app:layout_constraintBottom_toBottomOf="@+id/myinfo"
        app:layout_constraintLeft_toLeftOf="parent"
        android:layout_width="wrap_content"
        android:layout_height="0dp"
        tools:layout_width="30dp"
        app:colorMapping="000000=e01a1a"
        app:colorMappingSelected="000000=ff3030"
        app:colorMappingDisabled="000000=1a1a1a"
        app:aspect="fit"
        app:svg="info.svg" />
```

Note that since the SVG has an intrinsic width calculated from its height and aspect ratio, the width displayed by the designer is incorrect. You can correct this in the designer by assigning a design-time only value to `layout_width` using the `tools` prefix: `tools:layout_width="30dp"` which requires the `xmlns:tools` namespace declaration.
</details>


**Android native**  (not MAUI Android): set the icon for the back button in the toolbar

```csharp
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable("backward.svg", "000000=>FFFFFF");
```

**iOS/Android: Load an SVG from a background thread**
<details>
    <summary>Click to expand</summary>
See [this gist](https://gist.github.com/softlion/17aea986a8ea3594595ddf869ee49b37)
</details>

**iOS storyboard** (not maui ios): usage in an xcode storyboard
<details>
  <summary>Click to expand</summary>
Open your storyboard file using `Open with xcode`. Add an Image view (UIImageView), set its custcom `Class` property to `UISvgImageView`, and optionally add a new `User Defined Runtime Attributes` as required:

![Attribute Inspector](https://image.ibb.co/e5N0uw/Prt_Scr_capture_11.jpg)

| Key Path | Type | Sample Value
| --------- | ----- | ----
| BundleName | String | info.svg
| ColorMapping | String | 000000=e01a1a
| ColorMappingSelected | String | 000000=ff3030

To size your svg, set contraints on only one dimension. The other dimension will be calculated based on the first dimension and the SVG's aspect ratio. If you set constraints on both dimensions, the SVG will stretch. You can prevent this by setting `Aspect` to `Fit` (along with `ContentMode` set to `AspectFit`):

| Key Path | Type | Sample Value
| --------- | ----- | ----
| Aspect    | String | Fit

When only one dimension is constrained, the designer cannot determine how to set the other dimension and will display contraint errors. The solution is to set the `intrinsic size` to a manual value in the dimension that has no contraint (in the dimension property pane of the designer).

![Intrinsic Size](https://image.ibb.co/bzeDEw/Prt_Scr_capture_12.jpg)

1. Select the `UISvgImageView` view.
2. Open the size inspector (⌘Shift5).
3. Change the "Intrinsic Size" drop-down from "Default (System Defined)" to "Placeholder."
4. Enter a reasonable estimate for your view's runtime width **or** height. Use Width if you have set contraints on the height, and height otherwise.

These constraints are removed at compile-time, meaning they will not affect your running app. The layout engine will add constraints as necessary at runtime to respect your view's intrinsicContentSize.
</details>
  
# Reference

## MAUI

Default namespace:
  
```xml
xmlns:svg="https://vapolia.eu/svg"
```

### SvgImage control
`SvgImage` displays an image in up to two states: normal, selected

```xml
<svg:SvgImage Source="union.svg" HeightRequest="70" HorizontalOptions="Center" VerticalOptions="Center" />
```

| Property | Type | Notes
| --------- | ----- | ---
Source | string or `SvgSource` | svg to display.
ColorMapping | string or `ObservableCollection<ColorMapping>` | see color mapping reference. Default to none.
ColorMappingSelected | string or `ObservableCollection<ColorMapping>` | color mapping when IsSelected="True". Default to none.
IsSelected | bool | used to switch color mapping
IsSelectionEnabled | bool | True by default: the value of IsSelected is also inherited from the parent container
Command | ICommand | if set, execute this command on tap
CommandParameter | object | parameter to send when calling Command.Execute
Width | double | Optional. You can also specify the width only and height will be computed from the aspect ratio
Height | double | Optional
Aspect | SvgAspect | Fit, Fill, FitCrop. Useful only if both width and height are forced. Default to Fit to maintain the aspect ratio.
IsLoadAsync | bool | set to False to disable async image loading, making the image appear immediatly. Default to True.
IsHighlightEnabled | bool | if set, ColorMappingSelected is used while the image is pressed (until the finger is released)
ViewportTransform | IMatrix | transform the svg using any matrix before displaying it

### SvgImageSource class

`SvgImageSource` inherits from `ImageSource`, use it on any `ImageSource` property. For example `Page.IconImageSource`.
It can also be transformed into a `FileImageSource` by calling `CreateFileImageSource()`.

SvgImageSource can be used in Button.ImageSource, ToolbarItem.IconImageSource, ...

```xml
<svg:SvgImageSource Source="tabHome.svg" Height="50" />
```

| Property | Type | Notes
| --------- | ----- | ---
Source | string or `SvgSource` | svg to display.
Width | double | Optional. You can also specify the width only and height will be computed from the aspect ratio.
Height | double | Optional.
ColorMapping | string or `ObservableCollection<ColorMapping>` | see color mapping reference. Default to none.
Aspect | SvgAspect | Fit, Fill, FitCrop. Useful only if both width and height are forced. Default to Fit to maintain the aspect ratio.
PreventTintOnIos | bool | Default to false. Prevents tinting on iOS, thus always displaying the original image.

All properties are bindable, but Xamarin Forms does not support changing them after the control using this SvgImageSource is rendered.
Alternatively, you can bind the ImageSource property on the target control, and define SvgImageSource in styles.
Example:
```xml
    <svg:SvgImageSource x:Key="NormalIcon" Source="icon_normal.svg" Height="80" />
    <svg:SvgImageSource x:Key="SelectedIcon" Source="icon_selected.svg" Height="80" ColorMapping="FFF=>000" />

    <Style x:Key="NormalIconStyle" TargetType="ImageButton">
      <Setter Property="Source" Value="{StaticResource NormalIcon}"/>
      <Setter Property="BackgroundColor" Value="Transparent"/>
    </Style>

    <Style x:Key="SelectedIconStyle" TargetType="ImageButton">
      <Setter Property="Source" Value="{StaticResource SelectedIcon}"/>
      <Setter Property="BackgroundColor" Value="Transparent"/>
   </Style>
```
And usage:
```xml
    <ImageButton Style="{Binding StyleKeyToUse}" />
```
```csharp
     public string StyleKeyToUse {get;set;} = "NormalIconStyle"; 
     //Don't forget to call OnPropertyChanged(nameof(StyleKeyToUse)) after each change.
```

## Android native (not maui android)
<details>
  <summary>Click to expand</summary>
Layout properties:

| Tag | Type | Default value | Notes
| --------- | ----- | ---- | ---
app:svg | string or resource id | (required) | .net embedded resource file path and name, or android resource id
app:colorMapping | string | (null) | example: FF000000=FF808080
app:colorMappingSelected | string | (null) | example: FF000000=FFa0a0a0;FFFFFFFF=00000000
app:colorMappingDisabled | string | (null)
app:traceEnabled | bool | false
app:loadAsync | bool | true
app:aspect | enum | fit | fit, fill of fit_crop (new v3.1.0). fit_crop: Scale the image uniformly (maintain the image's aspect ratio) so that both dimensions (width and height) of the image will be equal to or larger than the corresponding dimension of the view (minus padding). 
android:adjustViewBounds | bool | true | if true and aspect is not Fill, the svg view will grow or shrink depending on the svg size.
android:autoMirrored | bool | false | true to mirror image in RTL languages

`android:padding` is respected, and included in the width/height measurement.  
`android:gravity` is respected, and included in the width/height measurement. If the svg is smaller than its view, this property controls its centering.
</details>

## iOS native (not maui ios)
<details>
  <summary>Click to expand</summary>

`UISvgImageView` inherits `UIImageView`, so it's easy to use it in an xcode storyboard: drag an `UIImageView` and set its custom class to `UISvgImageView`. To set specific svg properties, add `User Defined Runtime Attributes` in the same pane where you set the custom class.

Attributes (supported in `User Defined Runtime Attributes`):

| Key Path | Type | Default value | Notes
| --------- | ----- | ---- | ---
BundleName | string | (required) | Svg path. Example: res:images.logo
BundleString | string | (optional) | exclusive with BundleName. The svg content as a string.
ColorMapping | string | (null) | example: FF000000=FF808080
ColorMappingSelected | string | (null) | example: FF000000=FFa0a0a0;FFFFFFFF=00000000
TraceEnabled | bool | false
IsLoadAsync | bool | true | set to false to force the svg to appear immediatly, or if it disappears sometimes
AlignmentMode | string | TopLeft | TopLeft, CenterHorizontally, CenterVertically, Center. Can be combined (in code only).
Aspect | string | Fit | Fit, Fill, FitCrop.
FillWidth | number | 0 | The width the svg would like to have. 0 to let the OS decides using UI constraints or Frame value.
FillHeight | number | 0 | The height the svg would like to have. 0 to let the OS decides using UI constraints or Frame value.

`UIImageView.ContentMode` is forced by `UISvgImageView`, so it has no impact. Use `Aspect` instead.
</details>


## Advanced configuration

The SVG files that don't specify the full path of an assembly are automatically searched in the embedded resources of all loaded assemblies. If you have assemblies that are loaded late and not detected, or if you prefer to manually specify the assemblies, call AddResourceAssembly. Example:

```csharp
public class App : Application
{
    public App()
    {
        Vapolia.Svgs.Config.AddResourceAssembly(typeof(App), ...);
        ...
    }
...
```

## Tools

On Windows install the [microsoft powertoys](https://docs.microsoft.com/fr-fr/windows/powertoys/file-explorer) to preview the SVG files in the Windows Explorer.

## More infos

[More infos](https://vapolia.eu)

[nuget-img]: https://img.shields.io/nuget/vpre/Vapolia.Svg
[nuget-link]: https://www.nuget.org/packages/Vapolia.Svg


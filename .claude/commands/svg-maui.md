# Vapolia.Svg — SVG Component for MAUI, Android & iOS

You are an expert in integrating the **Vapolia.Svg** NuGet package into .NET MAUI, native Android, and native iOS applications. When a user asks for help with SVG images in their MAUI or native project, follow this guide precisely.

---

## Package

**NuGet:** `Vapolia.Svg` (latest: 1.0.5+)  
**Supports:** Android 23+, iOS 14.2+, Windows 10.0.19041.0+, MAUI cross-platform

---

## 1. MAUI Setup

### MauiProgram.cs
```csharp
builder
    .UseMauiApp<App>()
    .UseEasySvg();          // required — add this line
    // .UseEasySvg("eyJhbGc...") // optional license token for production
```

### .csproj — embed SVG files
Create a folder named `VectorImages/` (NOT `Resources/Images/`), place `.svg` files there, then add:
```xml
<ItemGroup>
  <None Remove="VectorImages\**\*.svg" />
  <EmbeddedResource Include="VectorImages\**\*.svg" />
</ItemGroup>
```
Any folder name works (`images/`, `Icons/`, etc.) — just keep it out of `Resources/Images`.

### XAML namespace
```xml
xmlns:svg="https://vapolia.eu/svg"
```

---

## 2. Controls

### SvgImage — standalone SVG display
```xml
<!-- Basic -->
<svg:SvgImage Source="logo.svg" HeightRequest="70" HorizontalOptions="Center" />

<!-- With color mapping -->
<svg:SvgImage Source="logo.svg" HeightRequest="70"
              ColorMapping="ffffff=>00ff00,000000=>0000FF" />

<!-- Remote URL -->
<svg:SvgImage WidthRequest="100"
              Source="https://example.com/image.svg" />

<!-- Inline base64 -->
<svg:SvgImage WidthRequest="100"
              Source="data:image/svg+xml;base64,PD94bWwgdmVyc2lvb..." />

<!-- Bindable color mapping with selected state -->
<svg:SvgImage Source="logo.svg" HeightRequest="60">
    <svg:ColorMapping OldColor="#000" NewColor="{Binding CurrentColor}" />
    <svg:SvgImage.ColorMappingSelected>
        <svg:ColorMapping OldColor="#000" NewColor="{Binding CurrentColor}" />
    </svg:SvgImage.ColorMappingSelected>
</svg:SvgImage>

<!-- With tap command -->
<svg:SvgImage Source="icon.svg" HeightRequest="44"
              Command="{Binding TapCommand}"
              BackgroundColor="Transparent" />
```

### SvgImage Key Properties
| Property | Type | Notes |
|---|---|---|
| `Source` | string | Filename, URL, base64, or inline SVG |
| `HeightRequest` / `WidthRequest` | double | Aspect ratio preserved if only one set |
| `ColorMapping` | string or list | Normal state color replacement |
| `ColorMappingSelected` | string or list | Applied when `IsSelected="True"` |
| `ColorMappingDisabled` | string or list | Applied when control disabled |
| `IsSelected` | bool | Toggle selected appearance |
| `Aspect` | SvgAspect | `Fit` (default), `Fill`, `FitCrop` |
| `Command` / `CommandParameter` | ICommand / object | Tap handler for MVVM |
| `BackgroundColor` | Color | Required for tap/command on some platforms |
| `IsLoadAsync` | bool | Default: true |

### SvgImageSource — for Button icons, Tab icons, NavigationBar
Width and/or Height are **mandatory** (MAUI limitation: ImageSource is static).
```xml
<!-- Button with SVG icon -->
<Button Text="Add Contact"
        ContentLayout="Right,20"
        ImageSource="{svg:Svg tabHome.svg,Height=60,ColorMapping='000000=>FF0000'}" />

<!-- TabbedPage / Shell tab icon -->
<NavigationPage Title="Home" IconImageSource="{svg:Svg tabHome.svg,Height=60}">
    <x:Arguments><views:HomePage /></x:Arguments>
</NavigationPage>

<!-- Page icon -->
<ContentPage IconImageSource="{svg:Svg myicon.svg,Height=60}" ...>

<!-- Standard MAUI Image control -->
<Image Source="{svg:Svg no_image_available.svg, Width=40, ColorMapping='000=>666'}"
       WidthRequest="40" />
```

> **Note:** `SvgImageSource` renders to a raster image — supports only **one** `ColorMapping`, no selected/disabled states.

---

## 3. Source Schemes

| Prefix | Meaning | Example |
|---|---|---|
| *(none)* or `res:` | Embedded resource | `logo.svg` or `res:MyApp.Images.logo.svg` |
| `file:` | Native bundled resource | `file:logo.svg` |
| `nfile:` | Device file system | `nfile:/data/user/0/com.app/files/img.svg` |
| `string:` | Inline SVG content | `string:<svg xmlns="...">...</svg>` |
| `http:` / `https:` | Remote URL | `https://example.com/image.svg` |

---

## 4. Color Mapping Syntax

Format: `oldColor=>newColor` — multiple mappings separated by `,`, `;`, or space.  
Color formats: `AARRGGBB`, `RRGGBB`, `RGB` (hex, no `#` needed in string form).

```
"000=>FF0000"                       → black to red
"ffffff=>00ff00,000000=>0000FF"     → white→green AND black→blue
"FFF=>666"                          → white→gray
"000000=>e01a1a"                    → black→dark red
```

Bindable form using `ColorMapping` objects in a `ColorMappings` collection:
```xml
<svg:SvgImage Source="icon.svg" HeightRequest="32">
    <svg:ColorMapping OldColor="Blue" NewColor="#80000000" />
    <svg:ColorMapping OldColor="#FFF" NewColor="{Binding AccentColor}" />
</svg:SvgImage>
```

Resource dictionary reuse:
```xml
<svg:ColorMappings x:Key="ButtonIconColors">
    <svg:ColorMapping OldColor="#000" NewColor="{StaticResource PrimaryColor}" />
</svg:ColorMappings>

<svg:SvgImage Source="icon.svg" ColorMapping="{StaticResource ButtonIconColors}" />
```

---

## 5. Native Android (without MAUI)

### MainActivity.cs
```csharp
Vapolia.Svgs.Config.AddResourceAssembly(typeof(MainActivity));
```

### Layout XML
```xml
<vapolia.SvgImageView
    app:svg="car.svg"
    app:colorMapping="000000=>FF0000"
    app:colorMappingSelected="000000=>FF3030"
    app:colorMappingDisabled="000000=>1A1A1A"
    app:aspect="fit"
    android:layout_width="200dp"
    android:layout_height="wrap_content" />
```

### Programmatic (toolbar/navigation icon)
```csharp
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable("backward.svg", "000000=>FFFFFF");
```

---

## 6. Native iOS (without MAUI)

### AppDelegate.cs
```csharp
using Vapolia.Svgs;
// Load from bundle
var svg = await SvgFactory.FromUri(SvgSource.FromNativeFile("info.svg"));
```

### Xcode Interface Builder (User Defined Runtime Attributes on `UISvgImageView`)
| Key Path | Type | Example |
|---|---|---|
| `BundleName` | String | `info.svg` |
| `ColorMapping` | String | `000000=>FF0000` |
| `ColorMappingSelected` | String | `000000=>0000FF` |
| `Aspect` | String | `Fit`, `Fill`, `FitCrop` |
| `IsLoadAsync` | Boolean | `YES` |

---

## 7. Windows limitations

- `ColorMappings` not supported on Windows
- `Command` requires a non-transparent `BackgroundColor`
- Async loading always enabled

---

## 8. Common Mistakes & Troubleshooting

| Symptom | Cause | Fix |
|---|---|---|
| Nothing displays | SVG not set as `EmbeddedResource` | Check `.csproj` `<EmbeddedResource Include="...">` |
| Nothing displays | SVG missing `viewBox` attribute | Open SVG in text editor, add `viewBox="0 0 W H"` to `<svg>` tag |
| Nothing displays | SVG color = background color (e.g. white on white) | Use `ColorMapping` or edit SVG |
| SVG not found | `Assembly Name` ≠ `Default Namespace` | Use full resource name: `YourNamespace.Folder.file.svg` |
| Nothing displays | `UseEasySvg()` missing | Add to `MauiProgram.cs` |
| Button ImageSource broken | Set in Style/Trigger | Set on `Image` property, not `ImageSource` in styles |
| Color binding not working | Using `SvgImageSource` | Use `SvgImage` instead — `SvgImageSource` is raster-only |

To discover the full embedded resource name, open the compiled `.dll` with **Telerik JustDecompile** or run:
```csharp
var names = GetType().Assembly.GetManifestResourceNames()
    .Where(r => r.EndsWith(".svg"));
```

---

## 9. Full MAUI Page Example

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:svg="https://vapolia.eu/svg"
             x:Class="MyApp.MainPage"
             IconImageSource="{svg:Svg appicon.svg,Height=60}">
    <VerticalStackLayout Padding="16" Spacing="12">

        <!-- Simple image -->
        <svg:SvgImage Source="logo.svg" HeightRequest="80" HorizontalOptions="Center" />

        <!-- Recolored -->
        <svg:SvgImage Source="logo.svg" HeightRequest="50"
                      ColorMapping="000=>e01a1a" />

        <!-- Button with SVG icon -->
        <Button Text="Next" ImageSource="{svg:Svg arrow.svg,Height=20}"
                ContentLayout="Right,5"
                Command="{Binding NextCommand}" />

    </VerticalStackLayout>
</ContentPage>
```

using Foundation;
using UIKit;
using Vapolia.Svgs;

namespace SvgIosNativeDemo;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new UIViewController();
        vc.View!.AddSubview(new UILabel(Window!.Frame)
        {
            BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Hello, iOS!",
            AutoresizingMask = UIViewAutoresizing.All,
        });
        Window.RootViewController = vc;

        // make the window visible
        Window.MakeKeyAndVisible();

        //Test Loading from file system
        var svgString = """<svg xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 24 24" height="24px" viewBox="0 0 24 24" width="24px" fill="#000000"><g><path d="M0,0h24v24H0V0z" fill="none"/></g><g><polygon points="6.23,20.23 8,22 18,12 8,2 6.23,3.77 14.46,12"/></g></svg>""";
        var svgPath = Path.GetTempFileName();
        File.WriteAllText(svgPath, svgString);

        Task.Run(async () =>
        {
            var svg = SvgFactory.FromUri(SvgSource.FromNativeFile(svgPath))
                .GetAwaiter().GetResult();
            if (svg == null)
                Console.WriteLine("Background loading: error");
            else
                Console.WriteLine("Background loading: success");
        });

        return true;
    }
}
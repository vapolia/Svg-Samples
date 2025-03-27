using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Vapolia.UserInteraction;

namespace SvgMauiDemo;

public class MainPageViewModel : INotifyPropertyChanged
{
    private int imageIndex;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand NextImageCommand { get; }
    public ICommand PrevImageCommand { get; }
    public ICommand AddCommand { get; }
    
    public string Image { get; private set; }
    public string ImageName => Image?.Split(".")[^2];
    public ObservableCollection<string> TextItems { get; }


    public MainPageViewModel()
    {
        var images = GetType().Assembly.GetManifestResourceNames()
            .Where(r => r.EndsWith(".svg"))
            //.Select(r => )
            .OrderBy(r => r.Split(".")[^2])
            .ToList();

        // TextItems = new (new[]
        // {
        //     "item 1",
        //     "item 2",
        //     "item 3",
        //     "item 4",
        // });
        
        NextImageCommand = new Command(() =>
        {
            Image = images[imageIndex];
            PropertyChanged?.Invoke(this, new (nameof(Image)));
            PropertyChanged?.Invoke(this, new (nameof(ImageName)));
            
            //Crashes on Windows
            //SemanticScreenReader.Announce($"Image {imageIndex}");

            imageIndex = ++imageIndex % images.Count;
        });

        NextImageCommand.Execute(null);

        PrevImageCommand = new Command(() =>
        {
            imageIndex = (imageIndex-2+images.Count) % images.Count;
            Image = images[imageIndex];
            PropertyChanged?.Invoke(this, new (nameof(Image)));
            PropertyChanged?.Invoke(this, new (nameof(ImageName)));

            imageIndex = ++imageIndex % images.Count;
        });

        AddCommand = new Command(() =>
        {
            UserInteraction.Alert("Add item clicked !");
        });
    }
}
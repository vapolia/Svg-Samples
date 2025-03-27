using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMauiDemo.Views;

public partial class Grids : ContentPage
{
    public List<string> TextItems { get; }
   
    public Grids()
    {
        InitializeComponent();

        TextItems = [
            "item 1",
            "item 2",
            "item 3",
            "item 4",
        ];

        BindingContext = this;
    }
}
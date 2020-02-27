using System;
using System.Numerics;
using System.Threading;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PlantUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Random random;

        public MainPage()
        {
            random = new Random();
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                DrawingCanvas.Children.Add(CreateRect());
            }
        }
        private UIElement CreateRect()
        {
            var color = new AcrylicBrush
            {
                TintColor = CreateRandomColor(),
                TintOpacity = 0.5
            };

            var rect = new Rectangle
            {
                Width = random.Next(50, 200),
                Height = random.Next(50, 200),
                Translation = new Vector3(random.Next(0, 400), random.Next(0, 400), random.Next(0, 400)),
                Fill = color
            };

            return rect;
        }

        private Color CreateRandomColor()
        {
            return Color.FromArgb(255, (byte) random.Next(0, 255), (byte) random.Next(0, 255), (byte) random.Next(0, 255));
        }
    }
}

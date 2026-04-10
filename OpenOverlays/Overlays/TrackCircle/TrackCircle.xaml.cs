using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OpenOverlays.Internals;
using OpenOverlays.Data.Models;

namespace OpenOverlays.Overlays.TrackCircle;

public partial class TrackCircle : Overlay
{
    private iRacingData _data = new();
    private const double Radius = 95;
    private const double CenterX = 115;
    private const double CenterY = 115;

    public TrackCircle() : base("Track Circle", "Circular track map showing car positions.")
    {
        InitializeComponent();
        _setWindowSize(250, 250);
    }

    public override void _updateWindow()
    {
        CarCanvas.Children.Clear();

        if (_data.Drivers == null) return;

        foreach (var driver in _data.Drivers)
        {
            // Position on circle based on lap distance percentage (0-1)
            // 0 is usually Start/Finish line
            double angle = (driver.LapDistPct * 360) - 90; // Offset -90 to start at top
            double angleRad = angle * Math.PI / 180.0;

            double x = CenterX + Radius * Math.Cos(angleRad);
            double y = CenterY + Radius * Math.Sin(angleRad);

            Ellipse carDot = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = GetDriverBrush(driver),
                ToolTip = driver.UserName
            };

            Canvas.SetLeft(carDot, x - 5);
            Canvas.SetTop(carDot, y - 5);
            CarCanvas.Children.Add(carDot);
        }
    }

    private Brush GetDriverBrush(DriverModel driver)
    {
        if (driver.Idx == _data.PlayerIdx)
            return BrandingBrushes.PlayerBrush;
        
        return driver.IsAI ? Brushes.Gray : Brushes.White;
    }

    public override void _getData()
    {
        _data = MainWindow.IRacingData;
    }

    protected override void _scaleWindow(double scale)
    {
        // Simple scaling of the main grid
        var transform = new ScaleTransform(scale, scale);
        this.LayoutTransform = transform;
    }
}

public static class BrandingBrushes
{
    public static readonly Brush PlayerBrush = new SolidColorBrush(Color.FromRgb(0, 209, 255)); // HUD Cyan
}

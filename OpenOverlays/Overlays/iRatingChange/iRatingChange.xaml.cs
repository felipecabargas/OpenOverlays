using System.Linq;
using System.Windows.Media;
using OpenOverlays.Internals;
using OpenOverlays.Data.Models;

namespace OpenOverlays.Overlays.iRatingChange;

public partial class iRatingChange : Overlay
{
    private iRacingData _data = new();

    public iRatingChange() : base("iRating Change", "Estimated iRating gain or loss for the current session.")
    {
        InitializeComponent();
        _setWindowSize(140, 60);
    }

    public override void _updateWindow()
    {
        if (_data.Drivers == null || _data.Drivers.Length == 0)
        {
            iRatingText.Text = "+0";
            SOFLabel.Text = "SOF 0";
            return;
        }

        var player = _data.Drivers.FirstOrDefault(d => d.Idx == _data.PlayerIdx);
        if (player != null)
        {
            double gain = player.RatingChange;
            string prefix = gain >= 0 ? "+" : "";
            iRatingText.Text = $"{prefix}{gain:F0}";
            
            // Set Color
            iRatingText.Foreground = gain >= 0 
                ? new SolidColorBrush(Color.FromRgb(50, 205, 50))  // Performance Green
                : new SolidColorBrush(Color.FromRgb(255, 69, 0));  // Alert Red
        }

        SOFLabel.Text = $"SOF {_data.SessionData.SOF}";
    }

    public override void _getData()
    {
        _data = MainWindow.IRacingData;
    }

    protected override void _scaleWindow(double scale)
    {
        var transform = new ScaleTransform(scale, scale);
        this.LayoutTransform = transform;
    }
}

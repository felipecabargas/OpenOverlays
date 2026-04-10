using System.IO;
using Path = System.IO.Path;

namespace OpenOverlays.API.Overlays.SetupHider;

public class SetupHiderModel
{
    public bool InGarage { get; set; }
    
    public SetupHiderModel()
    {
        InGarage = MainWindow.IRacingData.InGarage;
        
    }
    
}
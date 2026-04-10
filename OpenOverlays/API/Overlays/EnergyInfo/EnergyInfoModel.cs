using OpenOverlays.Data.Models;

namespace OpenOverlays.API.Overlays.EnergyInfo;

public class EnergyInfoModel
{
    public float EnergyLevelPct { get; set; }

    public EnergyInfoModel()
    {
        iRacingData iRacingData = MainWindow.IRacingData;
        EnergyLevelPct = iRacingData.LocalCarTelemetry.EngeryLevelPct;
    }
}
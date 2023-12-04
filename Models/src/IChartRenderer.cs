namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// Interface IChartRenderer
    /// </summary>
    public interface IChartRenderer
    {
        string GetContainer(int width, int height);
        string GetScript(int width, int height);
    }
} // End Partial class

namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    // IAspNetMakerPage interface // DN
    public interface IAspNetMakerPage
    {
        Task<IActionResult> Run();
        IActionResult Terminate(string url = "");
    }
} // End Partial class

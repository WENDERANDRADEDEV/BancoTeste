namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    // ICaptcha interface // DN
    public interface ICaptcha
    {
        string FailureMessage { get; set; }
        string ResponseField { get; set; }
        string Response { get; set; }
        string ElementName { get; }
        string ElementId { get; }
        string SessionName { get; }
        string GetHtml();
        string GetConfirmHtml();
        bool Validate();
        string GetScript();
        void SetDefaultFailureMessage();
    }
} // End Partial class
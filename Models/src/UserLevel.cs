namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// User level class
    /// </summary>
    public class UserLevel
    {
        // User level ID
        [SqlKata.Column("")]
        public int Id { set; get; }

        // Name
        [SqlKata.Column("")]
        public string Name { set; get; } = "";
    }
} // End Partial class

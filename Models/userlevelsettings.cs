namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    // Configuration
    public static partial class Config
    {
        //
        // ASP.NET Maker 2023 user level settings
        //

        // User level info
        public static List<UserLevel> UserLevels = new ()
        {
            new () { Id = -2, Name = "Anonymous" }
        };

        // User level priv info
        public static List<UserLevelPermission> UserLevelPermissions = new ()
        {
            new () { Table = "{CC2C6F46-3970-4EF2-BEBD-4F3187212A89}TADS_QRCODE", Id = -2, Permission = 0 }
        };

        // User level table info // DN
        public static List<UserLevelTablePermission> UserLevelTablePermissions = new ()
        {
            new () { TableName = "TADS_QRCODE", TableVar = "TADS_QRCODE", Caption = "TADS QRCODE", Allowed = true, ProjectId = "{CC2C6F46-3970-4EF2-BEBD-4F3187212A89}", Url = "TadsQrcodeList" }
        };
    }
} // End Partial class

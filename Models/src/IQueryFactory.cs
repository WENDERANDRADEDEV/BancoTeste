namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// IQueryFactory interface
    /// </summary>
    interface IQueryFactory
    {
        // Get query factory
        QueryFactory GetQueryFactory(bool main);

        // Get query builder
        QueryBuilder GetQueryBuilder(bool main, string output = "");

        // Get query builder with output Id (use secondary connection)
        QueryBuilder GetQueryBuilder(string output);

        // Get query builder without output Id (use secondary connection)
        QueryBuilder GetQueryBuilder();
    }
} // End Partial class

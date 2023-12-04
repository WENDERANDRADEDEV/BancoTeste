namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// tadsQrcodeUpdate
    /// </summary>
    public static TadsQrcodeUpdate tadsQrcodeUpdate
    {
        get => HttpData.Get<TadsQrcodeUpdate>("tadsQrcodeUpdate")!;
        set => HttpData["tadsQrcodeUpdate"] = value;
    }

    /// <summary>
    /// Page class for TADS_QRCODE
    /// </summary>
    public class TadsQrcodeUpdate : TadsQrcodeUpdateBase
    {
        // Constructor
        public TadsQrcodeUpdate(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TadsQrcodeUpdate() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TadsQrcodeUpdateBase : TadsQrcode
    {
        // Page ID
        public string PageID = "update";

        // Project ID
        public string ProjectID = "{CC2C6F46-3970-4EF2-BEBD-4F3187212A89}";

        // Table name
        public string TableName { get; set; } = "TADS_QRCODE";

        // Page object name
        public string PageObjName = "tadsQrcodeUpdate";

        // Title
        public string? Title = null; // Title for <title> tag

        // Page headings
        public string Heading = "";

        public string Subheading = "";

        public string PageHeader = "";

        public string PageFooter = "";

        // Token
        public string? Token = null; // DN

        public bool CheckToken = Config.CheckToken;

        // Action result // DN
        public IActionResult? ActionResult;

        // Cache // DN
        public IMemoryCache? Cache;

        // Page layout
        public bool UseLayout = true;

        // Page terminated // DN
        private bool _terminated = false;

        // Is terminated
        public bool IsTerminated => _terminated;

        // Is lookup
        public bool IsLookup => IsApi() && RouteValues.TryGetValue("controller", out object? name) && SameText(name, Config.ApiLookupAction);

        // Is AutoFill
        public bool IsAutoFill => IsLookup && SameText(Post("ajax"), "autofill");

        // Is AutoSuggest
        public bool IsAutoSuggest => IsLookup && SameText(Post("ajax"), "autosuggest");

        // Is modal lookup
        public bool IsModalLookup => IsLookup && SameText(Post("ajax"), "modal");

        // Page URL
        private string _pageUrl = "";

        // Constructor
        public TadsQrcodeUpdateBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-update-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (tadsQrcode)
            if (tadsQrcode == null || tadsQrcode is TadsQrcode)
                tadsQrcode = this;

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

            // Open connection
            Conn = Connection; // DN
        }

        // Page action result
        public IActionResult PageResult()
        {
            if (ActionResult != null)
                return ActionResult;
            SetupMenus();
            return Controller.View();
        }

        // Page heading
        public string PageHeading
        {
            get {
                if (!Empty(Heading))
                    return Heading;
                else if (!Empty(Caption))
                    return Caption;
                else
                    return "";
            }
        }

        // Page subheading
        public string PageSubheading
        {
            get {
                if (!Empty(Subheading))
                    return Subheading;
                if (!Empty(TableName))
                    return Language.Phrase(PageID);
                return "";
            }
        }

        // Page name
        public string PageName => "TadsQrcodeUpdate";

        // Page URL
        public string PageUrl
        {
            get {
                if (_pageUrl == "") {
                    _pageUrl = PageName + "?";
                }
                return _pageUrl;
            }
        }

        // Show Page Header
        public IHtmlContent ShowPageHeader()
        {
            string header = PageHeader;
            PageDataRendering(ref header);
            if (!Empty(header)) // Header exists, display
                return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
            return HtmlString.Empty;
        }

        // Show Page Footer
        public IHtmlContent ShowPageFooter()
        {
            string footer = PageFooter;
            PageDataRendered(ref footer);
            if (!Empty(footer)) // Footer exists, display
                return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
            return HtmlString.Empty;
        }

        // Valid post
        protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || Antiforgery != null && HttpContext != null && await Antiforgery.IsRequestValidAsync(HttpContext);

        // Create token
        public void CreateToken()
        {
            Token ??= HttpContext != null ? Antiforgery?.GetAndStoreTokens(HttpContext).RequestToken : null;
            CurrentToken = Token ?? ""; // Save to global variable
        }

        // Set field visibility
        public void SetVisibility()
        {
            ID.Visible = false;
            COD.SetVisibility();
            DES.SetVisibility();
            CMD.SetVisibility();
            FRM.SetVisibility();
            LBL.SetVisibility();
            COR.SetVisibility();
            ETAPA.SetVisibility();
            QRCODE.SetVisibility();
            QRCODE2.SetVisibility();
        }

        // Constructor
        public TadsQrcodeUpdateBase(Controller? controller = null): this() { // DN
            if (controller != null)
                Controller = controller;
        }

        /// <summary>
        /// Terminate page
        /// </summary>
        /// <param name="url">URL to rediect to</param>
        /// <returns>Page result</returns>
        public override IActionResult Terminate(string url = "") { // DN
            if (_terminated) // DN
                return new EmptyResult();

            // Page Unload event
            PageUnload();

            // Global Page Unloaded event
            PageUnloaded();
            if (!IsApi())
                PageRedirecting(ref url);

            // Gargage collection
            Collect(); // DN

            // Terminate
            _terminated = true; // DN

            // Return for API
            if (IsApi()) {
                var result = new Dictionary<string, string> { { "version", Config.ProductVersion } };
                if (!Empty(url)) // Add url
                    result.Add("url", GetUrl(url));
                foreach (var (key, value) in GetMessages()) // Add messages
                    result.Add(key, value);
                return Controller.Json(result);
            } else if (ActionResult != null) { // Check action result
                return ActionResult;
            }

            // Go to URL if specified
            if (!Empty(url)) {
                if (!Config.Debug)
                    ResponseClear();
                if (Response != null && !Response.HasStarted) {
                    // Handle modal response (Assume return to modal for simplicity)
                    if (IsModal) { // Show as modal
                        var result = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
                        string pageName = GetPageName(url);
                        if (pageName != ListUrl) { // Not List page
                            result.Add("caption", GetModalCaption(pageName));
                            result.Add("view", pageName == "TadsQrcodeView" ? "1" : "0"); // If View page, no primary button
                        } else { // List page
                            // result.Add("list", PageID == "search" ? "1" : "0"); // Refresh List page if current page is Search page
                            result.Add("error", FailureMessage); // List page should not be shown as modal => error
                            ClearFailureMessage();
                        }
                        return Controller.Json(result);
                    } else {
                        SaveDebugMessage();
                        return Controller.LocalRedirect(AppPath(url));
                    }
                }
            }
            return new EmptyResult();
        }

        // Get all records from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader? dr)
        {
            var rows = new List<Dictionary<string, object>>();
            while (dr != null && await dr.ReadAsync()) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                if (GetRecordFromDictionary(GetDictionary(dr)) is Dictionary<string, object> row)
                    rows.Add(row);
            }
            return rows;
        }

        // Get all records from the list of records
        #pragma warning disable 1998

        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(List<Dictionary<string, object>>? list)
        {
            var rows = new List<Dictionary<string, object>>();
            if (list != null) {
                foreach (var row in list) {
                    if (GetRecordFromDictionary(row) is Dictionary<string, object> d)
                       rows.Add(row);
                }
            }
            return rows;
        }
        #pragma warning restore 1998

        // Get the first record from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<Dictionary<string, object>?> GetRecordFromRecordset(DbDataReader? dr)
        {
            if (dr != null) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                return GetRecordFromDictionary(GetDictionary(dr));
            }
            return null;
        }

        // Get the first record from the list of records
        protected Dictionary<string, object>? GetRecordFromRecordset(List<Dictionary<string, object>>? list) =>
            list != null && list.Count > 0 ? GetRecordFromDictionary(list[0]) : null;

        // Get record from Dictionary
        protected Dictionary<string, object>? GetRecordFromDictionary(Dictionary<string, object>? dict) {
            if (dict == null)
                return null;
            var row = new Dictionary<string, object>();
            foreach (var (key, value) in dict) {
                if (Fields.TryGetValue(key, out DbField? fld)) {
                    if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
                        if (fld.HtmlTag == "FILE") { // Upload field
                            if (Empty(value)) {
                                // row[key] = null;
                            } else {
                                if (fld.DataType == DataType.Blob) {
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(dict)); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType((byte[])value) }, { "url", url }, { "name", fld.Param + ContentExtension((byte[])value) } };
                                } else if (!fld.UploadMultiple || !ConvertToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + ConvertToString(value))); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType(ConvertToString(value)) }, { "url", url }, { "name", ConvertToString(value) } };
                                } else { // Multiple files
                                    var files = ConvertToString(value).Split(Config.MultipleUploadSeparator);
                                    row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + file)) }, { "name", file } });
                                }
                            }
                        } else {
                            string val = ConvertToString(value);
                            if (fld.DataType == DataType.Date && value is DateTime dt)
                                val = dt.ToString("s");
                            row[key] = ConvertToString(val);
                        }
                    }
                }
            }
            return row;
        }

        // Get record key value from array
        protected string GetRecordKeyValue(Dictionary<string, object> dict) {
            string key = "";
            key += UrlEncode(ConvertToString(dict.ContainsKey("ID") ? dict["ID"] : ID.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
            if (IsAdd || IsCopy || IsGridAdd)
                ID.Visible = false;
        }

        #pragma warning disable 219
        /// <summary>
        /// Lookup data from table
        /// </summary>
        public async Task<Dictionary<string, object>> Lookup(Dictionary<string, string>? dict = null)
        {
            Language = ResolveLanguage();
            Security = ResolveSecurity();
            string? v;

            // Get lookup object
            string fieldName = IsDictionary(dict) && dict.TryGetValue("field", out v) && v != null ? v : Post("field");
            var lookupField = FieldByName(fieldName);
            var lookup = lookupField?.Lookup;
            if (lookup == null) // DN
                return new Dictionary<string, object>();
            string lookupType = IsDictionary(dict) && dict.TryGetValue("ajax", out v) && v != null ? v : (Post("ajax") ?? "unknown");
            int pageSize = -1;
            int offset = -1;
            string searchValue = "";
            if (SameText(lookupType, "modal") || SameText(lookupType, "filter")) {
                searchValue = IsDictionary(dict) && (dict.TryGetValue("q", out v) && v != null || dict.TryGetValue("sv", out v) && v != null)
                    ? v
                    : (Param("q") ?? Post("sv"));
                pageSize = IsDictionary(dict) && (dict.TryGetValue("n", out v) || dict.TryGetValue("recperpage", out v))
                    ? ConvertToInt(v)
                    : (IsNumeric(Param("n")) ? Param<int>("n") : (Post("recperpage", out StringValues rpp) ? ConvertToInt(rpp.ToString()) : 10));
            } else if (SameText(lookupType, "autosuggest")) {
                searchValue = IsDictionary(dict) && dict.TryGetValue("q", out v) && v != null ? v : Param("q");
                pageSize = IsDictionary(dict) && dict.TryGetValue("n", out v) ? ConvertToInt(v) : (IsNumeric(Param("n")) ? Param<int>("n") : -1);
                if (pageSize <= 0)
                    pageSize = Config.AutoSuggestMaxEntries;
            }
            int start = IsDictionary(dict) && dict.TryGetValue("start", out v) ? ConvertToInt(v) : (IsNumeric(Param("start")) ? Param<int>("start") : -1);
            int page = IsDictionary(dict) && dict.TryGetValue("page", out v) ? ConvertToInt(v) : (IsNumeric(Param("page")) ? Param<int>("page") : -1);
            offset = start >= 0 ? start : (page > 0 && pageSize > 0 ? (page - 1) * pageSize : 0);
            string userSelect = Decrypt(IsDictionary(dict) && dict.TryGetValue("s", out v) && v != null ? v : Post("s"));
            string userFilter = Decrypt(IsDictionary(dict) && dict.TryGetValue("f", out v) && v != null ? v : Post("f"));
            string userOrderBy = Decrypt(IsDictionary(dict) && dict.TryGetValue("o", out v) && v != null ? v : Post("o"));

            // Selected records from modal, skip parent/filter fields and show all records
            lookup.LookupType = lookupType; // Lookup type
            lookup.FilterValues.Clear(); // Clear filter values first
            StringValues keys = IsDictionary(dict) && dict.TryGetValue("keys", out v) && !Empty(v)
                ? (StringValues)v
                : (Post("keys[]", out StringValues k) ? (StringValues)k : StringValues.Empty);
            if (!Empty(keys)) { // Selected records from modal
                lookup.FilterFields = new (); // Skip parent fields if any
                pageSize = -1; // Show all records
                lookup.FilterValues.Add(String.Join(",", keys.ToArray()));
            } else { // Lookup values
                string lookupValue = IsDictionary(dict) && (dict.TryGetValue("v0", out v) && v != null || dict.TryGetValue("lookupValue", out v) && v != null)
                    ? v
                    : (Post<string>("v0") ?? Post("lookupValue"));
                lookup.FilterValues.Add(lookupValue);
            }
            int cnt = IsDictionary(lookup.FilterFields) ? lookup.FilterFields.Count : 0;
            for (int i = 1; i <= cnt; i++) {
                var val = UrlDecode(IsDictionary(dict) && dict.TryGetValue("v" + i, out v) ? v : Post("v" + i));
                if (val != null) // DN
                    lookup.FilterValues.Add(val);
            }
            lookup.SearchValue = searchValue;
            lookup.PageSize = pageSize;
            lookup.Offset = offset;
            if (userSelect != "")
                lookup.UserSelect = userSelect;
            if (userFilter != "")
                lookup.UserFilter = userFilter;
            if (userOrderBy != "")
                lookup.UserOrderBy = userOrderBy;
            return await lookup.ToJson(this);
        }
        #pragma warning restore 219

    public string FormClassName = "ew-form ew-update-form";

    public bool IsModal = false;

    public bool IsMobileOrModal = false;

    public DbDataReader? Recordset;

    public List<string> RecordKeys = new ();

    public bool Disabled; // DN

    public int TotalRecords;

    public int UpdateCount = 0;

    /// <summary>
    /// Page run
    /// </summary>
    /// <returns>Page result</returns>
    public override async Task<IActionResult> Run()
    {
            // Is modal
            IsModal = Param<bool>("modal");
            UseLayout = UseLayout && !IsModal;

            // Use layout
            if (!Empty(Param("layout")) && !Param<bool>("layout"))
                UseLayout = false;

            // User profile
            Profile = ResolveProfile();

            // Security
            Security = ResolveSecurity();
            if (TableVar != "")
                Security.LoadTablePermissions(TableVar);

            // Create form object
            CurrentForm ??= new ();
            await CurrentForm.Init();
            CurrentAction = Param("action"); // Set up current action
            SetVisibility();

            // Do not use lookup cache
            if (!Config.LookupCachePageIds.Contains(PageID))
                SetUseLookupCache(false);

            // Global Page Loading event
            PageLoading();

            // Page Load event
            PageLoad();

            // Check token
            if (!await ValidPost())
                End(Language.Phrase("InvalidPostRequest"));

            // Check action result
            if (ActionResult != null) // Action result set by server event // DN
                return ActionResult;

            // Create token
            CreateToken();

            // Hide fields for add/edit
            if (!UseAjaxActions)
                HideFieldsForAddEdit();
            // Use inline delete
            if (UseAjaxActions)
                InlineDelete = true;

        // Check modal
        if (IsModal)
            SkipHeaderFooter = true;
        IsMobileOrModal = IsMobile() || IsModal;

        // Set up Breadcrumb
        SetupBreadcrumb();

        // Try to load keys from list form
        RecordKeys = GetRecordKeys(); // Load record keys
        if (Post("action", out StringValues sv) && !Empty(sv)) {
            // Get action
            CurrentAction = sv.ToString();
            await LoadFormValues(); // Get form values

            // Validate form
            if (!await ValidateForm()) {
                CurrentAction = "show"; // Form error, reset action
                if (!HasInvalidFields()) // No fields selected
                    FailureMessage = Language.Phrase("NoFieldSelected");
            }
        } else {
            await LoadMultiUpdateValues(); // Load initial values to form
        }
        if (RecordKeys.Count <= 0)
            return Terminate("TadsQrcodeList"); // No records selected, return to list
        if (IsUpdate) {
            if (await UpdateRows()) { // Update Records based on key
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("UpdateSuccess"); // Set update success message

                // Do not return Json for UseAjaxActions
                if (IsModal && UseAjaxActions)
                    IsModal = false;
                return Terminate(ReturnUrl); // Return to caller
            } else if (IsModal && UseAjaxActions) { // Return JSON error message
                return Controller.Json(new { success = false, error = GetFailureMessage() });
            } else {
                RestoreFormValues(); // Restore form values
            }
        }

        // Render row
        if (IsConfirm) { // Confirm page
            RowType = RowType.View; // Render view
            Disabled = true; // DN
        } else {
            RowType = RowType.Edit; // Render edit
            Disabled = false; // DN
        }
        ResetAttributes();
        await RenderRow();

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                tadsQrcodeUpdate?.PageRender();
            }
        return PageResult();
    }

    // Load initial values to form if field values are identical in all selected records
    public async Task LoadMultiUpdateValues()
    {
        CurrentFilter = GetFilterFromRecordKeys();
        int i = 1;

        // Load recordset
        using var rs = await LoadRecordset();
        try {
            while (rs != null && await rs.ReadAsync()) {
                if (i == 1) {
                    COD.SetDbValue(rs["COD"]);
                    DES.SetDbValue(rs["DES"]);
                    CMD.SetDbValue(rs["CMD"]);
                    FRM.SetDbValue(rs["FRM"]);
                    LBL.SetDbValue(rs["LBL"]);
                    COR.SetDbValue(rs["COR"]);
                    ETAPA.SetDbValue(rs["ETAPA"]);
                    QRCODE2.SetDbValue(rs["QRCODE2"]);
                } else {
                    if (!CompareValue(COD.DbValue, rs["COD"]))
                        COD.CurrentValue = DbNullValue;
                    if (!CompareValue(DES.DbValue, rs["DES"]))
                        DES.CurrentValue = DbNullValue;
                    if (!CompareValue(CMD.DbValue, rs["CMD"]))
                        CMD.CurrentValue = DbNullValue;
                    if (!CompareValue(FRM.DbValue, rs["FRM"]))
                        FRM.CurrentValue = DbNullValue;
                    if (!CompareValue(LBL.DbValue, rs["LBL"]))
                        LBL.CurrentValue = DbNullValue;
                    if (!CompareValue(COR.DbValue, rs["COR"]))
                        COR.CurrentValue = DbNullValue;
                    if (!CompareValue(ETAPA.DbValue, rs["ETAPA"]))
                        ETAPA.CurrentValue = DbNullValue;
                    if (!CompareValue(QRCODE2.DbValue, rs["QRCODE2"]))
                        QRCODE2.CurrentValue = DbNullValue;
                }
                i++;
            }
        } catch {
            if (Config.Debug)
                throw;
        }
    }

    // Set up key value
    public bool SetupKeyValues(object key)
    {
        string keyFld = "";
        keyFld = ConvertToString(key);
        if (!IsNumeric(keyFld))
            return false;
        ID.OldValue = keyFld;
        return true;
    }

    // Update all selected rows
    public async Task<bool> UpdateRows()
    {
        bool result = false;
        List<string> successKeys = new ();
        List<string> failKeys = new ();
        string thisKey = "";
        string sql;
        List<Dictionary<string, object>> rsold, rsnew;
        if (UseTransaction)
            Connection.BeginTrans(); // Begin transaction

        // Get old recordset
        CurrentFilter = GetFilterFromRecordKeys(false);
        sql = CurrentSql;
        rsold = await Connection.GetRowsAsync(sql);

        // Update all rows
        try {
            foreach (string recordKey in RecordKeys) {
                if (SetupKeyValues(recordKey)) {
                    thisKey = recordKey;
                    SendEmail = false; // Do not send email on update success
                    UpdateCount++; // Update record count for records being updated
                    result = await EditRow(); // Update this row
                } else {
                    result = false;
                }
                if (!result) {
                    if (UseTransaction) { // Update failed
                        successKeys.Clear();
                        break;
                    }
                    failKeys.Add(thisKey);
                } else {
                    successKeys.Add(thisKey);
                }
            }
        } catch (Exception e) {
            FailureMessage = e.Message;
            result = false;
        }

        // Check if any rows updated
        if (successKeys.Count > 0) {
            if (UseTransaction)
                Connection.CommitTrans(); // Commit transaction

            // Set warning message if update some records failed
            if (failKeys.Count > 0)
                WarningMessage = Language.Phrase("UpdateSomeRecordsFailed").Replace("%k", String.Join(", ", failKeys));

            // Get new recordset
            rsnew = await Connection.GetRowsAsync(sql);
        } else {
            if (UseTransaction)
                Connection.RollbackTrans(); // Rollback transaction
        }
        return result;
    }

        // Confirm page
        public bool ConfirmPage = true; // DN

        #pragma warning disable 1998
        // Get upload files
        public async Task GetUploadFiles()
        {
            // Get upload data
            QRCODE.Upload.Index = CurrentForm.Index;
            if (!await QRCODE.Upload.UploadFile()) // DN
                End(QRCODE.Upload.Message);
            QRCODE.MultiUpdate = CurrentForm.GetValue("u_QRCODE");
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'COD' before field var 'x_COD'
            val = CurrentForm.HasValue("COD") ? CurrentForm.GetValue("COD") : CurrentForm.GetValue("x_COD");
            if (!COD.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("COD") && !CurrentForm.HasValue("x_COD")) // DN
                    COD.Visible = false; // Disable update for API request
                else
                    COD.SetFormValue(val);
            }
            COD.MultiUpdate = CurrentForm.GetValue("u_COD");

            // Check field name 'DES' before field var 'x_DES'
            val = CurrentForm.HasValue("DES") ? CurrentForm.GetValue("DES") : CurrentForm.GetValue("x_DES");
            if (!DES.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("DES") && !CurrentForm.HasValue("x_DES")) // DN
                    DES.Visible = false; // Disable update for API request
                else
                    DES.SetFormValue(val);
            }
            DES.MultiUpdate = CurrentForm.GetValue("u_DES");

            // Check field name 'CMD' before field var 'x_CMD'
            val = CurrentForm.HasValue("CMD") ? CurrentForm.GetValue("CMD") : CurrentForm.GetValue("x_CMD");
            if (!CMD.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("CMD") && !CurrentForm.HasValue("x_CMD")) // DN
                    CMD.Visible = false; // Disable update for API request
                else
                    CMD.SetFormValue(val);
            }
            CMD.MultiUpdate = CurrentForm.GetValue("u_CMD");

            // Check field name 'FRM' before field var 'x_FRM'
            val = CurrentForm.HasValue("FRM") ? CurrentForm.GetValue("FRM") : CurrentForm.GetValue("x_FRM");
            if (!FRM.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("FRM") && !CurrentForm.HasValue("x_FRM")) // DN
                    FRM.Visible = false; // Disable update for API request
                else
                    FRM.SetFormValue(val);
            }
            FRM.MultiUpdate = CurrentForm.GetValue("u_FRM");

            // Check field name 'LBL' before field var 'x_LBL'
            val = CurrentForm.HasValue("LBL") ? CurrentForm.GetValue("LBL") : CurrentForm.GetValue("x_LBL");
            if (!LBL.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("LBL") && !CurrentForm.HasValue("x_LBL")) // DN
                    LBL.Visible = false; // Disable update for API request
                else
                    LBL.SetFormValue(val);
            }
            LBL.MultiUpdate = CurrentForm.GetValue("u_LBL");

            // Check field name 'COR' before field var 'x_COR'
            val = CurrentForm.HasValue("COR") ? CurrentForm.GetValue("COR") : CurrentForm.GetValue("x_COR");
            if (!COR.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("COR") && !CurrentForm.HasValue("x_COR")) // DN
                    COR.Visible = false; // Disable update for API request
                else
                    COR.SetFormValue(val);
            }
            COR.MultiUpdate = CurrentForm.GetValue("u_COR");

            // Check field name 'ETAPA' before field var 'x_ETAPA'
            val = CurrentForm.HasValue("ETAPA") ? CurrentForm.GetValue("ETAPA") : CurrentForm.GetValue("x_ETAPA");
            if (!ETAPA.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ETAPA") && !CurrentForm.HasValue("x_ETAPA")) // DN
                    ETAPA.Visible = false; // Disable update for API request
                else
                    ETAPA.SetFormValue(val);
            }
            ETAPA.MultiUpdate = CurrentForm.GetValue("u_ETAPA");

            // Check field name 'QRCODE2' before field var 'x_QRCODE2'
            val = CurrentForm.HasValue("QRCODE2") ? CurrentForm.GetValue("QRCODE2") : CurrentForm.GetValue("x_QRCODE2");
            if (!QRCODE2.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("QRCODE2") && !CurrentForm.HasValue("x_QRCODE2")) // DN
                    QRCODE2.Visible = false; // Disable update for API request
                else
                    QRCODE2.SetFormValue(val);
            }
            QRCODE2.MultiUpdate = CurrentForm.GetValue("u_QRCODE2");

            // Check field name 'ID' before field var 'x_ID'
            val = CurrentForm.HasValue("ID") ? CurrentForm.GetValue("ID") : CurrentForm.GetValue("x_ID");
            if (!ID.IsDetailKey)
                ID.SetFormValue(val);
            await GetUploadFiles(); // Get upload files
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            ID.CurrentValue = ID.FormValue;
            COD.CurrentValue = COD.FormValue;
            DES.CurrentValue = DES.FormValue;
            CMD.CurrentValue = CMD.FormValue;
            FRM.CurrentValue = FRM.FormValue;
            LBL.CurrentValue = LBL.FormValue;
            COR.CurrentValue = COR.FormValue;
            ETAPA.CurrentValue = ETAPA.FormValue;
            QRCODE2.CurrentValue = QRCODE2.FormValue;
        }

        // Load recordset // DN
        public async Task<DbDataReader?> LoadRecordset(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load recordset // DN
            var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));

            // Call Recordset Selected event
            RecordsetSelected(dr);
            return dr;
        }

        // Load rows // DN
        public async Task<List<Dictionary<string, object>>> LoadRows(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load rows // DN
            using var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
            var rows = await Connection.GetRowsAsync(dr);
            dr.Close(); // Close datareader before return
            return rows;
        }

        // Load row based on key values
        public async Task<bool> LoadRow()
        {
            string filter = GetRecordFilter();

            // Call Row Selecting event
            RowSelecting(ref filter);

            // Load SQL based on filter
            CurrentFilter = filter;
            string sql = CurrentSql;
            bool res = false;
            try {
                var row = await Connection.GetRowAsync(sql);
                if (row != null) {
                    await LoadRowValues(row);
                    res = true;
                } else {
                    return false;
                }
            } catch {
                if (Config.Debug)
                    throw;
            }
            return res;
        }

        #pragma warning disable 162, 168, 1998, 4014
        // Load row values from data reader
        public async Task LoadRowValues(DbDataReader? dr = null) => await LoadRowValues(GetDictionary(dr));

        // Load row values from recordset
        public async Task LoadRowValues(Dictionary<string, object>? row)
        {
            row ??= NewRow();

            // Call Row Selected event
            RowSelected(row);
            ID.SetDbValue(row["ID"]);
            COD.SetDbValue(row["COD"]);
            DES.SetDbValue(row["DES"]);
            CMD.SetDbValue(row["CMD"]);
            FRM.SetDbValue(row["FRM"]);
            LBL.SetDbValue(row["LBL"]);
            COR.SetDbValue(row["COR"]);
            ETAPA.SetDbValue(row["ETAPA"]);
            QRCODE.Upload.DbValue = row["QRCODE"];
            QRCODE2.SetDbValue(row["QRCODE2"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("ID", ID.DefaultValue ?? DbNullValue); // DN
            row.Add("COD", COD.DefaultValue ?? DbNullValue); // DN
            row.Add("DES", DES.DefaultValue ?? DbNullValue); // DN
            row.Add("CMD", CMD.DefaultValue ?? DbNullValue); // DN
            row.Add("FRM", FRM.DefaultValue ?? DbNullValue); // DN
            row.Add("LBL", LBL.DefaultValue ?? DbNullValue); // DN
            row.Add("COR", COR.DefaultValue ?? DbNullValue); // DN
            row.Add("ETAPA", ETAPA.DefaultValue ?? DbNullValue); // DN
            row.Add("QRCODE", QRCODE.DefaultValue ?? DbNullValue); // DN
            row.Add("QRCODE2", QRCODE2.DefaultValue ?? DbNullValue); // DN
            return row;
        }

        #pragma warning disable 1998
        // Render row values based on field settings
        public async Task RenderRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

            // ID
            ID.RowCssClass = "row";

            // COD
            COD.RowCssClass = "row";

            // DES
            DES.RowCssClass = "row";

            // CMD
            CMD.RowCssClass = "row";

            // FRM
            FRM.RowCssClass = "row";

            // LBL
            LBL.RowCssClass = "row";

            // COR
            COR.RowCssClass = "row";

            // ETAPA
            ETAPA.RowCssClass = "row";

            // QRCODE
            QRCODE.RowCssClass = "row";

            // QRCODE2
            QRCODE2.RowCssClass = "row";

            // View row
            if (RowType == RowType.View) {
                // ID
                ID.ViewValue = ID.CurrentValue;
                ID.ViewCustomAttributes = "";

                // COD
                COD.ViewValue = ConvertToString(COD.CurrentValue); // DN
                COD.ViewCustomAttributes = "";

                // DES
                DES.ViewValue = ConvertToString(DES.CurrentValue); // DN
                DES.ViewCustomAttributes = "";

                // CMD
                CMD.ViewValue = ConvertToString(CMD.CurrentValue); // DN
                CMD.ViewCustomAttributes = "";

                // FRM
                FRM.ViewValue = ConvertToString(FRM.CurrentValue); // DN
                FRM.ViewCustomAttributes = "";

                // LBL
                LBL.ViewValue = ConvertToString(LBL.CurrentValue); // DN
                LBL.ViewCustomAttributes = "";

                // COR
                COR.ViewValue = ConvertToString(COR.CurrentValue); // DN
                COR.ViewCustomAttributes = "";

                // ETAPA
                ETAPA.ViewValue = ConvertToString(ETAPA.CurrentValue); // DN
                ETAPA.ViewCustomAttributes = "";

                // QRCODE
                if (!IsNull(QRCODE.Upload.DbValue)) {
                    QRCODE.ViewValue = RawUrlEncode(ID.CurrentValue);
                    QRCODE.IsBlobImage = IsImageFile(ContentExtension((byte[])QRCODE.Upload.DbValue));
                } else {
                    QRCODE.ViewValue = "";
                }
                QRCODE.ViewCustomAttributes = "";

                // QRCODE2
                QRCODE2.ViewValue = QRCODE2.CurrentValue;
                QRCODE2.ViewCustomAttributes = "";

                // COD
                COD.HrefValue = "";
                COD.TooltipValue = "";

                // DES
                DES.HrefValue = "";
                DES.TooltipValue = "";

                // CMD
                CMD.HrefValue = "";
                CMD.TooltipValue = "";

                // FRM
                FRM.HrefValue = "";
                FRM.TooltipValue = "";

                // LBL
                LBL.HrefValue = "";
                LBL.TooltipValue = "";

                // COR
                COR.HrefValue = "";
                COR.TooltipValue = "";

                // ETAPA
                ETAPA.HrefValue = "";
                ETAPA.TooltipValue = "";

                // QRCODE
                if (!IsNull(QRCODE.Upload.DbValue)) {
                    QRCODE.HrefValue = AppPath(GetFileUploadUrl(QRCODE, ConvertToString(RawUrlEncode(ID.CurrentValue)))); // DN
                    QRCODE.LinkAttrs["target"] = "";
                    if (QRCODE.IsBlobImage && Empty(QRCODE.LinkAttrs["target"]))
                        QRCODE.LinkAttrs["target"] = "_blank";
                    if (IsExport())
                        QRCODE.HrefValue = FullUrl(ConvertToString(QRCODE.HrefValue), "href");
                } else {
                    QRCODE.HrefValue = "";
                }
                QRCODE.ExportHrefValue = GetFileUploadUrl(QRCODE, ConvertToString(RawUrlEncode(ID.CurrentValue)));
                QRCODE.TooltipValue = "";

                // QRCODE2
                QRCODE2.HrefValue = "";
                QRCODE2.TooltipValue = "";
            } else if (RowType == RowType.Edit) {
                // COD
                COD.SetupEditAttributes();
                if (!COD.Raw)
                    COD.CurrentValue = HtmlDecode(COD.CurrentValue);
                COD.EditValue = HtmlEncode(COD.CurrentValue);
                COD.PlaceHolder = RemoveHtml(COD.Caption);

                // DES
                DES.SetupEditAttributes();
                if (!DES.Raw)
                    DES.CurrentValue = HtmlDecode(DES.CurrentValue);
                DES.EditValue = HtmlEncode(DES.CurrentValue);
                DES.PlaceHolder = RemoveHtml(DES.Caption);

                // CMD
                CMD.SetupEditAttributes();
                if (!CMD.Raw)
                    CMD.CurrentValue = HtmlDecode(CMD.CurrentValue);
                CMD.EditValue = HtmlEncode(CMD.CurrentValue);
                CMD.PlaceHolder = RemoveHtml(CMD.Caption);

                // FRM
                FRM.SetupEditAttributes();
                if (!FRM.Raw)
                    FRM.CurrentValue = HtmlDecode(FRM.CurrentValue);
                FRM.EditValue = HtmlEncode(FRM.CurrentValue);
                FRM.PlaceHolder = RemoveHtml(FRM.Caption);

                // LBL
                LBL.SetupEditAttributes();
                if (!LBL.Raw)
                    LBL.CurrentValue = HtmlDecode(LBL.CurrentValue);
                LBL.EditValue = HtmlEncode(LBL.CurrentValue);
                LBL.PlaceHolder = RemoveHtml(LBL.Caption);

                // COR
                COR.SetupEditAttributes();
                if (!COR.Raw)
                    COR.CurrentValue = HtmlDecode(COR.CurrentValue);
                COR.EditValue = HtmlEncode(COR.CurrentValue);
                COR.PlaceHolder = RemoveHtml(COR.Caption);

                // ETAPA
                ETAPA.SetupEditAttributes();
                if (!ETAPA.Raw)
                    ETAPA.CurrentValue = HtmlDecode(ETAPA.CurrentValue);
                ETAPA.EditValue = HtmlEncode(ETAPA.CurrentValue);
                ETAPA.PlaceHolder = RemoveHtml(ETAPA.Caption);

                // QRCODE
                QRCODE.SetupEditAttributes();
                if (!IsNull(QRCODE.Upload.DbValue)) {
                    QRCODE.EditValue = RawUrlEncode(ID.CurrentValue);
                    QRCODE.IsBlobImage = IsImageFile(ContentExtension((byte[])QRCODE.Upload.DbValue));
                } else {
                    QRCODE.EditValue = "";
                }

                // QRCODE2
                QRCODE2.SetupEditAttributes();
                QRCODE2.EditValue = QRCODE2.CurrentValue; // DN
                QRCODE2.PlaceHolder = RemoveHtml(QRCODE2.Caption);

                // Edit refer script

                // COD
                COD.HrefValue = "";

                // DES
                DES.HrefValue = "";

                // CMD
                CMD.HrefValue = "";

                // FRM
                FRM.HrefValue = "";

                // LBL
                LBL.HrefValue = "";

                // COR
                COR.HrefValue = "";

                // ETAPA
                ETAPA.HrefValue = "";

                // QRCODE
                if (!IsNull(QRCODE.Upload.DbValue)) {
                    QRCODE.HrefValue = AppPath(GetFileUploadUrl(QRCODE, ConvertToString(RawUrlEncode(ID.CurrentValue)))); // DN
                    QRCODE.LinkAttrs["target"] = "";
                    if (QRCODE.IsBlobImage && Empty(QRCODE.LinkAttrs["target"]))
                        QRCODE.LinkAttrs["target"] = "_blank";
                    if (IsExport())
                        QRCODE.HrefValue = FullUrl(ConvertToString(QRCODE.HrefValue), "href");
                } else {
                    QRCODE.HrefValue = "";
                }
                QRCODE.ExportHrefValue = GetFileUploadUrl(QRCODE, ConvertToString(RawUrlEncode(ID.CurrentValue)));

                // QRCODE2
                QRCODE2.HrefValue = "";
            }
            if (RowType == RowType.Add || RowType == RowType.Edit || RowType == RowType.Search) // Add/Edit/Search row
                SetupFieldTitles();

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Validate form
        protected async Task<bool> ValidateForm() {
            int updateCnt = 0;
            if (COD.MultiUpdateSelected)
                updateCnt++;
            if (DES.MultiUpdateSelected)
                updateCnt++;
            if (CMD.MultiUpdateSelected)
                updateCnt++;
            if (FRM.MultiUpdateSelected)
                updateCnt++;
            if (LBL.MultiUpdateSelected)
                updateCnt++;
            if (COR.MultiUpdateSelected)
                updateCnt++;
            if (ETAPA.MultiUpdateSelected)
                updateCnt++;
            if (QRCODE.MultiUpdateSelected)
                updateCnt++;
            if (QRCODE2.MultiUpdateSelected)
                updateCnt++;
            if (updateCnt == 0) {
                return false;
            }

            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            if (COD.Required) {
                if (COD.MultiUpdate != "" && !COD.IsDetailKey && Empty(COD.FormValue)) {
                    COD.AddErrorMessage(ConvertToString(COD.RequiredErrorMessage).Replace("%s", COD.Caption));
                }
            }
            if (DES.Required) {
                if (DES.MultiUpdate != "" && !DES.IsDetailKey && Empty(DES.FormValue)) {
                    DES.AddErrorMessage(ConvertToString(DES.RequiredErrorMessage).Replace("%s", DES.Caption));
                }
            }
            if (CMD.Required) {
                if (CMD.MultiUpdate != "" && !CMD.IsDetailKey && Empty(CMD.FormValue)) {
                    CMD.AddErrorMessage(ConvertToString(CMD.RequiredErrorMessage).Replace("%s", CMD.Caption));
                }
            }
            if (FRM.Required) {
                if (FRM.MultiUpdate != "" && !FRM.IsDetailKey && Empty(FRM.FormValue)) {
                    FRM.AddErrorMessage(ConvertToString(FRM.RequiredErrorMessage).Replace("%s", FRM.Caption));
                }
            }
            if (LBL.Required) {
                if (LBL.MultiUpdate != "" && !LBL.IsDetailKey && Empty(LBL.FormValue)) {
                    LBL.AddErrorMessage(ConvertToString(LBL.RequiredErrorMessage).Replace("%s", LBL.Caption));
                }
            }
            if (COR.Required) {
                if (COR.MultiUpdate != "" && !COR.IsDetailKey && Empty(COR.FormValue)) {
                    COR.AddErrorMessage(ConvertToString(COR.RequiredErrorMessage).Replace("%s", COR.Caption));
                }
            }
            if (ETAPA.Required) {
                if (ETAPA.MultiUpdate != "" && !ETAPA.IsDetailKey && Empty(ETAPA.FormValue)) {
                    ETAPA.AddErrorMessage(ConvertToString(ETAPA.RequiredErrorMessage).Replace("%s", ETAPA.Caption));
                }
            }
            if (QRCODE.Required) {
                if (QRCODE.MultiUpdate != "" && QRCODE.Upload.FileName == "" && !QRCODE.Upload.KeepFile) {
                    QRCODE.AddErrorMessage(ConvertToString(QRCODE.RequiredErrorMessage).Replace("%s", QRCODE.Caption));
                }
            }
            if (QRCODE2.Required) {
                if (QRCODE2.MultiUpdate != "" && !QRCODE2.IsDetailKey && Empty(QRCODE2.FormValue)) {
                    QRCODE2.AddErrorMessage(ConvertToString(QRCODE2.RequiredErrorMessage).Replace("%s", QRCODE2.Caption));
                }
            }

            // Return validate result
            validateForm = validateForm && !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateForm = validateForm && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateForm;
        }
        #pragma warning restore 1998

        // Update record based on key values
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> EditRow() { // DN
            bool result = false;
            Dictionary<string, object> rsold;
            string oldKeyFilter = GetRecordFilter();
            string filter = ApplyUserIDFilters(oldKeyFilter);

            // Load old row
            CurrentFilter = filter;
            string sql = CurrentSql;
            try {
                using var rsedit = await Connection.GetDataReaderAsync(sql);
                if (rsedit == null || !await rsedit.ReadAsync()) {
                    FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                    return JsonBoolResult.FalseResult;
                }
                rsold = Connection.GetRow(rsedit);
                LoadDbValues(rsold);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Set new row
            Dictionary<string, object> rsnew = new ();

            // COD
            COD.SetDbValue(rsnew, COD.CurrentValue, COD.ReadOnly || COD.MultiUpdate != "1");

            // DES
            DES.SetDbValue(rsnew, DES.CurrentValue, DES.ReadOnly || DES.MultiUpdate != "1");

            // CMD
            CMD.SetDbValue(rsnew, CMD.CurrentValue, CMD.ReadOnly || CMD.MultiUpdate != "1");

            // FRM
            FRM.SetDbValue(rsnew, FRM.CurrentValue, FRM.ReadOnly || FRM.MultiUpdate != "1");

            // LBL
            LBL.SetDbValue(rsnew, LBL.CurrentValue, LBL.ReadOnly || LBL.MultiUpdate != "1");

            // COR
            COR.SetDbValue(rsnew, COR.CurrentValue, COR.ReadOnly || COR.MultiUpdate != "1");

            // ETAPA
            ETAPA.SetDbValue(rsnew, ETAPA.CurrentValue, ETAPA.ReadOnly || ETAPA.MultiUpdate != "1");

            // QRCODE
            if (QRCODE.Visible && !QRCODE.ReadOnly && ConvertToString(QRCODE.MultiUpdate) == "1" && !QRCODE.Upload.KeepFile) {
                rsnew["QRCODE"] = new SqlBinaryParameter(QRCODE.Upload.Value);
            }

            // QRCODE2
            QRCODE2.SetDbValue(rsnew, QRCODE2.CurrentValue, QRCODE2.ReadOnly || QRCODE2.MultiUpdate != "1");

            // Update current values
            SetCurrentValues(rsnew);

            // Call Row Updating event
            bool updateRow = RowUpdating(rsold, rsnew);
            if (updateRow) {
                try {
                    if (rsnew.Count > 0)
                        result = await UpdateAsync(rsnew, null, rsold) > 0;
                    else
                        result = true;
                    if (result) {
                    }
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    return JsonBoolResult.FalseResult;
                }
            } else {
                if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                    // Use the message, do nothing
                } else if (!Empty(CancelMessage)) {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("UpdateCancelled");
                }
                result = false;
            }

            // Call Row Updated event
            if (result)
                RowUpdated(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiEditAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, true);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TadsQrcodeList")), "", TableVar, true);
            string pageId = "update";
            breadcrumb.Add("update", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Setup lookup options
        public async Task SetupLookupOptions(DbField fld)
        {
            if (fld.Lookup == null)
                return;
            Func<string>? lookupFilter = null;
            dynamic conn = Connection;
            if (fld.Lookup.Options.Count is int c && c == 0) {
                // Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup Selecting server event
                var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

                // Set up lookup cache
                if (!fld.HasLookupOptions && fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
                    int totalCnt = await TryGetRecordCountAsync(sql, conn);
                    if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
                        return;
                    var dict = new Dictionary<string, Dictionary<string, object>>();
                    var values = new List<object>();
                    List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
                    if (rs != null) {
                        for (int i = 0; i < rs.Count; i++) {
                            var row = rs[i];
                            row = fld.Lookup?.RenderViewRow(row, Resolve(fld.Lookup.LinkTable));
                            string key = row?.Values.First()?.ToString() ?? String.Empty;
                            if (!dict.ContainsKey(key) && row != null)
                                dict.Add(key, row);
                        }
                    }
                    fld.Lookup?.SetOptions(dict);
                }
            }
        }

        // Close recordset
        public void CloseRecordset()
        {
            using (Recordset) {} // Dispose
        }

        // Page Load event
        public virtual void PageLoad() {
            //Log("Page Load");
        }

        // Page Unload event
        public virtual void PageUnload() {
            //Log("Page Unload");
        }

        // Page Redirecting event
        public virtual void PageRedirecting(ref string url) {
            //url = newurl;
        }

        // Message Showing event
        // type = ""|"success"|"failure"|"warning"
        public virtual void MessageShowing(ref string msg, string type) {
            // Note: Do not change msg outside the following 4 cases.
            if (type == "success") {
                //msg = "your success message";
            } else if (type == "failure") {
                //msg = "your failure message";
            } else if (type == "warning") {
                //msg = "your warning message";
            } else {
                //msg = "your message";
            }
        }

        // Page Load event
        public virtual void PageRender() {
            //Log("Page Render");
        }

        // Page Data Rendering event
        public virtual void PageDataRendering(ref string header) {
            // Example:
            //header = "your header";
        }

        // Page Data Rendered event
        public virtual void PageDataRendered(ref string footer) {
            // Example:
            //footer = "your footer";
        }

        // Page Breaking event
        public void PageBreaking(ref bool brk, ref string content) {
            // Example:
            //	brk = false; // Skip page break, or
            //	content = "<div style=\"page-break-after:always;\">&nbsp;</div>"; // Modify page break content
        }

        // Form Custom Validate event
        public virtual bool FormCustomValidate(ref string customError) {
            //Return error message in customError
            return true;
        }
    } // End page class
} // End Partial class

namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// tadsQrcodeSearch
    /// </summary>
    public static TadsQrcodeSearch tadsQrcodeSearch
    {
        get => HttpData.Get<TadsQrcodeSearch>("tadsQrcodeSearch")!;
        set => HttpData["tadsQrcodeSearch"] = value;
    }

    /// <summary>
    /// Page class for TADS_QRCODE
    /// </summary>
    public class TadsQrcodeSearch : TadsQrcodeSearchBase
    {
        // Constructor
        public TadsQrcodeSearch(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TadsQrcodeSearch() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TadsQrcodeSearchBase : TadsQrcode
    {
        // Page ID
        public string PageID = "search";

        // Project ID
        public string ProjectID = "{CC2C6F46-3970-4EF2-BEBD-4F3187212A89}";

        // Table name
        public string TableName { get; set; } = "TADS_QRCODE";

        // Page object name
        public string PageObjName = "tadsQrcodeSearch";

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
        public TadsQrcodeSearchBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-search-table";

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
        public string PageName => "TadsQrcodeSearch";

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
            ID.SetVisibility();
            COD.SetVisibility();
            DES.SetVisibility();
            CMD.SetVisibility();
            FRM.SetVisibility();
            LBL.SetVisibility();
            COR.SetVisibility();
            ETAPA.SetVisibility();
            QRCODE.Visible = false;
            QRCODE2.SetVisibility();
        }

        // Constructor
        public TadsQrcodeSearchBase(Controller? controller = null): this() { // DN
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

        public string FormClassName = "ew-form ew-search-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

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

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;

            // Get action
            CurrentAction = CurrentForm.GetValue("action");
            if (IsSearch) {
                // Build search string for advanced search, remove blank field
                LoadSearchValues(); // Get search values
                string srchStr = ValidateSearch() ? BuildAdvancedSearch() : "";
                if (!Empty(srchStr)) {
                    srchStr = "TadsQrcodeList?" + srchStr;
                    // Do not return Json for UseAjaxActions
                    if (IsModal && UseAjaxActions)
                        IsModal = false;
                    return Terminate(srchStr); // Go to List page
                }
            }

            // Restore search settings from Session
            if (!HasInvalidFields())
                LoadAdvancedSearch();

            // Render row for search
            RowType = RowType.Search;
            ResetAttributes();
            await RenderRow();

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                tadsQrcodeSearch?.PageRender();
            }
            return PageResult();
        }

        // Build advanced search
        protected string BuildAdvancedSearch() {
            string srchUrl = "";
            BuildSearchUrl(ref srchUrl, ID); // ID
            BuildSearchUrl(ref srchUrl, COD); // COD
            BuildSearchUrl(ref srchUrl, DES); // DES
            BuildSearchUrl(ref srchUrl, CMD); // CMD
            BuildSearchUrl(ref srchUrl, FRM); // FRM
            BuildSearchUrl(ref srchUrl, LBL); // LBL
            BuildSearchUrl(ref srchUrl, COR); // COR
            BuildSearchUrl(ref srchUrl, ETAPA); // ETAPA
            BuildSearchUrl(ref srchUrl, QRCODE2); // QRCODE2
            if (!Empty(srchUrl))
                srchUrl += "&";
            srchUrl += "cmd=search";
            return srchUrl;
        }

        // Build search URL
        protected void BuildSearchUrl(ref string url, DbField fld, bool oprOnly = false) {
            bool isValid;
            string wrk = "";
            string fldParm = fld.Param;
            string ctl = "x_" + fldParm;
            string ctl2 = "y_" + fldParm;
            if (fld.IsMultiSelect) { // DN
                ctl += "[]";
                ctl2 += "[]";
            }
            string fldVal = CurrentForm.GetValue(ctl);
            string fldOpr = CurrentForm.GetValue("z_" + fldParm);
            string fldCond = CurrentForm.GetValue("v_" + fldParm);
            string fldVal2 = CurrentForm.GetValue(ctl2);
            string fldOpr2 = CurrentForm.GetValue("w_" + fldParm);
            DataType fldDataType = fld.IsVirtual ? DataType.String : fld.DataType;
            fldVal = ConvertSearchValue(fldVal, fldOpr, fld); // For testing if numeric only
            fldVal2 = ConvertSearchValue(fldVal2, fldOpr2, fld); // For testing if numeric only
            fldOpr = ConvertSearchOperator(fldOpr, fld, fldVal);
            fldOpr2 = ConvertSearchOperator(fldOpr2, fld, fldVal2);
            if ((new [] { "BETWEEN", "NOT BETWEEN" }).Contains(fldOpr)) {
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal, fldOpr, fld) && IsNumericSearchValue(fldVal2, fldOpr2, fld);
                if (!Empty(fldVal) && !Empty(fldVal2) && isValid)
                    wrk = ctl + "=" + UrlEncode(fldVal) + "&" + ctl2 + "=" + UrlEncode(fldVal2) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
            } else {
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal, fldOpr, fld);
                if (!Empty(fldVal) && isValid && IsValidOperator(fldOpr)) {
                    wrk = ctl + "=" + UrlEncode(fldVal) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
                } else if ((new [] { "IS NULL", "IS NOT NULL", "IS EMPTY", "IS NOT EMPTY" }).Contains(fldOpr) || !Empty(fldOpr) && oprOnly && IsValidOperator(fldOpr)) {
                    wrk = "z_" + fldParm + "=" + UrlEncode(fldOpr);
                }
                isValid = fldDataType != DataType.Number || fld.VirtualSearch || IsNumericSearchValue(fldVal2, fldOpr2, fld);
                if (!Empty(fldVal2) && isValid && IsValidOperator(fldOpr2)) {
                    if (!Empty(wrk))
                        wrk += "&v_" + fldParm + "=" + fldCond + "&";
                    wrk += ctl2 + "=" + UrlEncode(fldVal2) + "&w_" + fldParm + "=" + UrlEncode(fldOpr2);
                } else if ((new [] { "IS NULL", "IS NOT NULL", "IS EMPTY", "IS NOT EMPTY" }).Contains(fldOpr2) || !Empty(fldOpr2) && oprOnly && IsValidOperator(fldOpr2)) {
                    if (!Empty(wrk))
                        wrk += "&v_" + fldParm + "=" + UrlEncode(fldCond) + "&";
                    wrk += "w_" + fldParm + "=" + UrlEncode(fldOpr2);
                }
            }
            if (!Empty(wrk)) {
                if (!Empty(url))
                    url += "&";
                url += wrk;
            }
        }

        // Load search values for validation // DN
        protected void LoadSearchValues() {
            // ID
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_ID"))
                    ID.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_ID");
            if (Form.ContainsKey("z_ID"))
                ID.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_ID");

            // COD
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_COD"))
                    COD.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_COD");
            if (Form.ContainsKey("z_COD"))
                COD.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_COD");

            // DES
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_DES"))
                    DES.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_DES");
            if (Form.ContainsKey("z_DES"))
                DES.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_DES");

            // CMD
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_CMD"))
                    CMD.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_CMD");
            if (Form.ContainsKey("z_CMD"))
                CMD.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_CMD");

            // FRM
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_FRM"))
                    FRM.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_FRM");
            if (Form.ContainsKey("z_FRM"))
                FRM.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_FRM");

            // LBL
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_LBL"))
                    LBL.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_LBL");
            if (Form.ContainsKey("z_LBL"))
                LBL.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_LBL");

            // COR
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_COR"))
                    COR.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_COR");
            if (Form.ContainsKey("z_COR"))
                COR.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_COR");

            // ETAPA
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_ETAPA"))
                    ETAPA.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_ETAPA");
            if (Form.ContainsKey("z_ETAPA"))
                ETAPA.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_ETAPA");

            // QRCODE2
            if (!IsAddOrEdit)
                if (Form.ContainsKey("x_QRCODE2"))
                    QRCODE2.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_QRCODE2");
            if (Form.ContainsKey("z_QRCODE2"))
                QRCODE2.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_QRCODE2");
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

                // ID
                ID.HrefValue = "";
                ID.TooltipValue = "";

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

                // QRCODE2
                QRCODE2.HrefValue = "";
                QRCODE2.TooltipValue = "";
            } else if (RowType == RowType.Search) {
                // ID
                ID.SetupEditAttributes();
                ID.EditValue = ID.AdvancedSearch.SearchValue; // DN
                ID.PlaceHolder = RemoveHtml(ID.Caption);

                // COD
                COD.SetupEditAttributes();
                if (!COD.Raw)
                    COD.AdvancedSearch.SearchValue = HtmlDecode(COD.AdvancedSearch.SearchValue);
                COD.EditValue = HtmlEncode(COD.AdvancedSearch.SearchValue);
                COD.PlaceHolder = RemoveHtml(COD.Caption);

                // DES
                DES.SetupEditAttributes();
                if (!DES.Raw)
                    DES.AdvancedSearch.SearchValue = HtmlDecode(DES.AdvancedSearch.SearchValue);
                DES.EditValue = HtmlEncode(DES.AdvancedSearch.SearchValue);
                DES.PlaceHolder = RemoveHtml(DES.Caption);

                // CMD
                CMD.SetupEditAttributes();
                if (!CMD.Raw)
                    CMD.AdvancedSearch.SearchValue = HtmlDecode(CMD.AdvancedSearch.SearchValue);
                CMD.EditValue = HtmlEncode(CMD.AdvancedSearch.SearchValue);
                CMD.PlaceHolder = RemoveHtml(CMD.Caption);

                // FRM
                FRM.SetupEditAttributes();
                if (!FRM.Raw)
                    FRM.AdvancedSearch.SearchValue = HtmlDecode(FRM.AdvancedSearch.SearchValue);
                FRM.EditValue = HtmlEncode(FRM.AdvancedSearch.SearchValue);
                FRM.PlaceHolder = RemoveHtml(FRM.Caption);

                // LBL
                LBL.SetupEditAttributes();
                if (!LBL.Raw)
                    LBL.AdvancedSearch.SearchValue = HtmlDecode(LBL.AdvancedSearch.SearchValue);
                LBL.EditValue = HtmlEncode(LBL.AdvancedSearch.SearchValue);
                LBL.PlaceHolder = RemoveHtml(LBL.Caption);

                // COR
                COR.SetupEditAttributes();
                if (!COR.Raw)
                    COR.AdvancedSearch.SearchValue = HtmlDecode(COR.AdvancedSearch.SearchValue);
                COR.EditValue = HtmlEncode(COR.AdvancedSearch.SearchValue);
                COR.PlaceHolder = RemoveHtml(COR.Caption);

                // ETAPA
                ETAPA.SetupEditAttributes();
                if (!ETAPA.Raw)
                    ETAPA.AdvancedSearch.SearchValue = HtmlDecode(ETAPA.AdvancedSearch.SearchValue);
                ETAPA.EditValue = HtmlEncode(ETAPA.AdvancedSearch.SearchValue);
                ETAPA.PlaceHolder = RemoveHtml(ETAPA.Caption);

                // QRCODE2
                QRCODE2.SetupEditAttributes();
                QRCODE2.EditValue = QRCODE2.AdvancedSearch.SearchValue; // DN
                QRCODE2.PlaceHolder = RemoveHtml(QRCODE2.Caption);
            }
            if (RowType == RowType.Add || RowType == RowType.Edit || RowType == RowType.Search) // Add/Edit/Search row
                SetupFieldTitles();

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        // Validate search
        protected bool ValidateSearch() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            if (!CheckInteger(ConvertToString(ID.AdvancedSearch.SearchValue))) {
                ID.AddErrorMessage(ID.GetErrorMessage(false));
            }

            // Return validate result
            bool validateSearch = !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateSearch = validateSearch && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateSearch;
        }

        // Load advanced search
        public void LoadAdvancedSearch()
        {
            ID.AdvancedSearch.Load();
            COD.AdvancedSearch.Load();
            DES.AdvancedSearch.Load();
            CMD.AdvancedSearch.Load();
            FRM.AdvancedSearch.Load();
            LBL.AdvancedSearch.Load();
            COR.AdvancedSearch.Load();
            ETAPA.AdvancedSearch.Load();
            QRCODE2.AdvancedSearch.Load();
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("TadsQrcodeList")), "", TableVar, true);
            string pageId = "search";
            breadcrumb.Add("search", pageId, url);
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

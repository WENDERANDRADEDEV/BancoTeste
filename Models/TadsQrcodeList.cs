namespace ASPNETMaker2023.Models;

// Partial class
public partial class TADS_QRCODE_02 {
    /// <summary>
    /// tadsQrcodeList
    /// </summary>
    public static TadsQrcodeList tadsQrcodeList
    {
        get => HttpData.Get<TadsQrcodeList>("tadsQrcodeList")!;
        set => HttpData["tadsQrcodeList"] = value;
    }

    /// <summary>
    /// Page class for TADS_QRCODE
    /// </summary>
    public class TadsQrcodeList : TadsQrcodeListBase
    {
        // Constructor
        public TadsQrcodeList(Controller controller) : base(controller)
        {
        }

        // Constructor
        public TadsQrcodeList() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class TadsQrcodeListBase : TadsQrcode
    {
        // Page ID
        public string PageID = "list";

        // Project ID
        public string ProjectID = "{CC2C6F46-3970-4EF2-BEBD-4F3187212A89}";

        // Table name
        public string TableName { get; set; } = "TADS_QRCODE";

        // Page object name
        public string PageObjName = "tadsQrcodeList";

        // Title
        public string? Title = null; // Title for <title> tag

        // Grid form hidden field names
        public string FormName = "fTADS_QRCODElist";

        public string FormActionName = "";

        public string FormBlankRowName = "";

        public string FormKeyCountName = "";

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
        public TadsQrcodeListBase()
        {
            // CSS class name as context
            TableVar = "TADS_QRCODE";
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);
            FormActionName = Config.FormRowActionName;
            FormBlankRowName = Config.FormBlankRowName;
            FormKeyCountName = Config.FormKeyCountName;

            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // CSS class name as context
            ContextClass = CheckClassName(TableVar);
            TableGridClass = AppendClass(TableGridClass, ContextClass);

            // Language object
            Language = ResolveLanguage();

            // Table object (tadsQrcode)
            if (tadsQrcode == null || tadsQrcode is TadsQrcode)
                tadsQrcode = this;

            // Initialize URLs
            AddUrl = "TadsQrcodeAdd";
            InlineAddUrl = PageUrl + "action=add";
            GridAddUrl = PageUrl + "action=gridadd";
            GridEditUrl = PageUrl + "action=gridedit";
            MultiEditUrl = PageUrl + "action=multiedit";
            MultiDeleteUrl = "TadsQrcodeDelete";
            MultiUpdateUrl = "TadsQrcodeUpdate";

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

            // Open connection
            Conn = Connection; // DN

            // Other options
            OtherOptions["addedit"] = new () {
                TagClassName = "ew-add-edit-option",
                UseDropDownButton = false,
                DropDownButtonPhrase = "ButtonAddEdit",
                UseButtonGroup = true
            };

            // Other options
            OtherOptions["detail"] = new () { TagClassName = "ew-detail-option" };
            OtherOptions["action"] = new () { TagClassName = "ew-action-option" };

            // Column visibility
            OtherOptions["column"] = new () {
                TableVar = TableVar,
                TagClassName = "ew-columns-option",
                ButtonGroupClass = "ew-column-dropdown",
                UseDropDownButton = true,
                DropDownButtonPhrase = "Columns",
                DropDownAutoClose = "outside",
                UseButtonGroup = false
            };
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
        public string PageName => "TadsQrcodeList";

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

        // Update URLs
        public string InlineAddUrl = "";

        public string GridAddUrl = "";

        public string GridEditUrl = "";

        public string MultiEditUrl = "";

        public string MultiDeleteUrl = "";

        public string MultiUpdateUrl = "";

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
            QRCODE.SetVisibility();
            QRCODE2.SetVisibility();
        }

        // Constructor
        public TadsQrcodeListBase(Controller? controller = null): this() { // DN
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

        /// <summary>
        /// Run chart
        /// </summary>
        /// <param name="chartVar">Chart variable name</param>
        /// <returns>Page result</returns>
        public async Task<IActionResult> RunChart(string chartVar = "") { // DN
            IActionResult res = await Run();
            DbChart? chart = ChartByParam(chartVar);
            if (!IsTerminated && chart != null) {
                string chartClass = (chart.PageBreakType == "before") ? "ew-chart-bottom" : "ew-chart-top";
                int chartWidth = Query.TryGetValue("width", out StringValues sv) ? ConvertToInt(sv) : -1;
                int chartHeight = Query.TryGetValue("height", out StringValues sv2) ? ConvertToInt(sv2) : -1;
                return Controller.Content(ConvertToString(await chart.Render(chartClass, chartWidth, chartHeight)), "text/html", Encoding.UTF8);
            }
            return res;
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
                            if (fld.DataType == DataType.Memo && fld.MemoMaxLength > 0 && !Empty(val))
                                val = TruncateMemo(val, fld.MemoMaxLength, fld.TruncateMemoRemoveHtml);
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

        // Properties
        private Pager? _pager; // DN

        public int SelectedCount = 0;

        public int SelectedIndex = 0;

        public int DisplayRecords = 20; // Number of display records

        public int StartRecord;

        public int StopRecord;

        public int TotalRecords = -1;

        public int RecordRange = 10;

        public string PageSizes = "10,20,50,-1"; // Page sizes (comma separated)

        public string DefaultSearchWhere = ""; // Default search WHERE clause

        public string SearchWhere = ""; // Search WHERE clause

        public string SearchPanelClass = "ew-search-panel collapse show"; // Search panel class

        public int SearchColumnCount = 0; // For extended search

        public int SearchFieldsPerRow = 1; // For extended search

        public int RecordCount = 0; // Record count

        public int InlineRowCount = 0;

        public int StartRowCount = 1;

        public List<Tuple<string, Dictionary<string, string>>> Attributes = new (); // Row attributes and cell attributes

        public object RowIndex = 0; // Row index

        public int KeyCount = 0; // Key count

        public string MultiColumnGridClass = "row-cols-md";

        public string MultiColumnEditClass = "col-12 w-100";

        public string MultiColumnCardClass = "card h-100 ew-card";

        public string MultiColumnListOptionsPosition = "bottom-start";

        public string DbMasterFilter = ""; // Master filter

        public string DbDetailFilter = ""; // Detail filter

        public bool MasterRecordExists;

        public string MultiSelectKey = "";

        public string UserAction = ""; // User action

        public bool RestoreSearch = false;

        public string HashValue = ""; // Hash Value

        public SubPages? DetailPages; // Detail pages object

        public DbDataReader? Recordset;

        public string TopContentClass = "ew-top";

        public string MiddleContentClass = "ew-middle";

        public string BottomContentClass = "ew-bottom";

        public List<string> RecordKeys = new ();

        public bool IsModal = false;

        private string FilterForModalActions = "";

        private bool UseInfiniteScroll = false;

        // Pager
        public Pager Pager
        {
            get {
                _pager ??= new PrevNextPager(this, StartRecord, RecordsPerPage, TotalRecords, PageSizes, RecordRange, AutoHidePager, AutoHidePageSizeSelector);
                return _pager;
            }
        }

        /// <summary>
        /// Load recordset from filter
        /// <param name="filter">Record filter</param>
        /// </summary>
        public async Task LoadRecordsetFromFilter(string filter)
        {
            // Set up list options
            await SetupListOptions();

            // Search options
            SetupSearchOptions();

            // Other options
            SetupOtherOptions();

            // Set visibility
            SetVisibility();

            // Load recordset
            TotalRecords = LoadRecordCount(filter);
            StartRecord = 1;
            StopRecord = DisplayRecords;
            CurrentFilter = filter;
            Recordset = await LoadRecordset();
        }

        #pragma warning disable 219
        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            // Multi column button position
            MultiColumnListOptionsPosition = Config.MultiColumnListOptionsPosition;
            DashboardReport = DashboardReport || Param<bool>(Config.PageDashboard);

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

            // Get export parameters
            string custom = "";
            if (!Empty(Param("export"))) {
                Export = Param("export");
                custom = Param("custom");
            } else {
                ExportReturnUrl = CurrentUrl();
            }
            ExportType = Export; // Get export parameter, used in header
            if (!Empty(ExportType))
                SkipHeaderFooter = true;
            if (!Empty(Export) && !SameText(Export, "print") && Empty(custom)) // No layout for export // DN
                UseLayout = false;
            CurrentAction = Param("action"); // Set up current action

            // Get grid add count
            int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
            if (gridaddcnt > 0)
                GridAddRowCount = gridaddcnt;

            // Set up list options
            await SetupListOptions();

            // Setup export options
            SetupExportOptions();
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

            // Setup other options
            SetupOtherOptions();

            // Set up custom action (compatible with old version)
            ListActions.Add(CustomActions);

            // Load default values for add
            LoadDefaultValues();

            // Update form name to avoid conflict
            if (IsModal)
                FormName = "fTADS_QRCODEgrid";

            // Set up infinite scroll
            UseInfiniteScroll = Param<bool>("infinitescroll");

            // Search filters
            string srchAdvanced = ""; // Advanced search filter
            string srchBasic = ""; // Basic search filter
            string filter = ""; // Filter
            string query = ""; // Query builder

            // Get command
            Command = Get("cmd").ToLower();

            // Process list action first
            var result = await ProcessListAction();
            if (result is not EmptyResult) // Ajax request
                return result;

            // Set up records per page
            SetupDisplayRecords();

            // Handle reset command
            ResetCommand();

            // Set up Breadcrumb
            if (!IsExport())
                SetupBreadcrumb();
            StringValues sv;

            // Check QueryString parameters
            if (Get("action", out sv)) {
                CurrentAction = sv.ToString();
            } else {
                if (Post("action", out sv)) {
                    if (sv.ToString() != UserAction)
                        CurrentAction = sv.ToString(); // Get action
                } else if (SameString(Session[Config.SessionInlineMode], "gridedit")) { // Previously in grid edit mode
                    if (Query.ContainsKey(Config.TableStartRec) || Query.ContainsKey(Config.TablePageNumber)) // Stay in grid edit mode if paging
                        GridEditMode();
                    else // Reset grid edit
                        ClearInlineMode();
                }
            }

            // Clear inline mode
            if (IsCancel)
                ClearInlineMode();

            // Switch to grid edit mode
            if (IsGridEdit)
                GridEditMode();
            var gridUpdate = false;
            // Grid Update
            if (IsPost() && (IsGridUpdate || IsMultiUpdate || IsGridOverwrite) && (SameString(Session[Config.SessionInlineMode], "gridedit") || SameString(Session[Config.SessionInlineMode], "multiedit"))) {
                if (await ValidateGridForm()) {
                    gridUpdate = await GridUpdate();
                } else {
                    gridUpdate = false;
                }
                if (gridUpdate) {
                    // Handle modal grid edit and multi edit, redirect to list page directly
                    if (IsModal && !UseAjaxActions)
                        return Terminate("TadsQrcodeList");
                } else {
                    EventCancelled = true;
                    if (UseAjaxActions) {
                        if (IsModal)
                            AddHeader("Modal-Error", "?1");
                        else
                            return Controller.Json(new { success = false, error = GetFailureMessage() });
                    }
                    if (IsMultiUpdate) { // Stay in Multi-Edit mode
                        var records = GetGridFormValues().Select(row => row.ToDictionary(kvp => kvp.Key, kvp => (object)(kvp.Value ?? "")));
                        FilterForModalActions = GetFilterFromRecords(records);
                    } else { // Stay in grid edit mode
                        GridEditMode();
                    }
                }
            }

            // Switch to inline edit mode
            if (IsEdit) {
                await InlineEditMode();
            // Inline Update
            } else if (IsPost() && (IsUpdate || IsOverwrite) && SameString(Session[Config.SessionInlineMode], "edit")) {
                SetKey(Post(OldKeyName));
                // Return JSON error message if UseAjaxActions
                if (!await InlineUpdate() && UseAjaxActions)
                    return Controller.Json(new { success = false, error = GetFailureMessage() });
            }

            // Switch to inline add mode
            if (IsAdd || IsCopy) { // DN
                await InlineAddMode();
            // Insert Inline
            } else if (IsPost() && IsInsert && SameString(Session[Config.SessionInlineMode], "add")) {
                SetKey(Post(OldKeyName));
                // Return JSON error message if UseAjaxActions
                if (!await InlineInsert() && UseAjaxActions)
                    return Controller.Json(new { success = false, error = GetFailureMessage() });
            }

            // Switch to grid add mode
            if (IsGridAdd) {
                GridAddMode();
            // Grid Insert
            } else if (IsPost() && IsGridInsert && SameString(Session[Config.SessionInlineMode], "gridadd")) {
                var gridInsert = false;
                if (await ValidateGridForm())
                    gridInsert = await GridInsert();
                if (gridInsert) {
                    // Handle modal grid add, redirect to list page directly
                    if (IsModal && !UseAjaxActions)
                        return Terminate("TadsQrcodeList");
                } else {
                    EventCancelled = true;
                    if (UseAjaxActions) {
                        if (IsModal)
                            AddHeader("Modal-Error", "?1");
                        else
                            return Controller.Json(new { success = false, error = GetFailureMessage() });
                    }
                    GridAddMode(); // Stay in grid add mode
                }
            }

            // Hide list options
            if (IsExport()) {
                ListOptions.HideAllOptions(new () {"sequence"});
                ListOptions.UseDropDownButton = false; // Disable drop down button
                ListOptions.UseButtonGroup = false; // Disable button group
            } else if (IsGridAdd || IsGridEdit || IsMultiEdit || IsConfirm) {
                ListOptions.HideAllOptions();
                ListOptions.UseDropDownButton = false; // Disable drop down button
                ListOptions.UseButtonGroup = false; // Disable button group
            }

            // Hide options
            if (IsExport() || !(Empty(CurrentAction) || IsSearch)) {
                ExportOptions.HideAllOptions();
                FilterOptions.HideAllOptions();
                ImportOptions.HideAllOptions();
            }

            // Hide other options
            if (IsExport()) {
                foreach (var (key, value) in OtherOptions)
                    value.HideAllOptions();
            }

            // Show grid delete link for grid add / grid edit
            if (AllowAddDeleteRow) {
                if (IsGridAdd || IsGridEdit) {
                    var item = ListOptions["griddelete"];
                    if (item != null)
                        item.Visible = true;
                }
            }

            // Get default search criteria
            AddFilter(ref DefaultSearchWhere, BasicSearchWhere(true));
            AddFilter(ref DefaultSearchWhere, AdvancedSearchWhere(true));

            // Get basic search values
            LoadBasicSearchValues();

            // Get and validate search values for advanced search
            if (Empty(UserAction)) // Skip if user action
                LoadSearchValues(); // Get search values

            // Process filter list
            var filterResult = await ProcessFilterList();
            if (filterResult != null) {
                // Clean output buffer
                if (!Config.Debug)
                    Response?.Clear();
                return Controller.Json(filterResult);
            }
            CurrentForm?.ResetIndex();
            if (!ValidateSearch()) {
                // Nothing to do
            }

            // Restore search parms from Session if not searching / reset / export
            if ((IsExport() || Command != "search" && Command != "reset" && Command != "resetall") && Command != "json" && CheckSearchParms())
                RestoreSearchParms();

            // Call Recordset SearchValidated event
            RecordsetSearchValidated();

            // Set up sorting order
            SetupSortOrder();

            // Get basic search criteria
            if (!HasInvalidFields())
                srchBasic = BasicSearchWhere();

            // Get search criteria for advanced search
            if (!HasInvalidFields())
                srchAdvanced = AdvancedSearchWhere();

            // Get query builder criteria
            query = QueryBuilderWhere();

            // Restore display records
            if (Command != "json" && (RecordsPerPage == -1 || RecordsPerPage > 0)) {
                DisplayRecords = RecordsPerPage; // Restore from Session
            } else {
                DisplayRecords = 20; // Load default
                RecordsPerPage = DisplayRecords; // Save default to session
            }

            // Load search default if no existing search criteria
            if (!CheckSearchParms() && Empty(query)) {
                // Load basic search from default
                BasicSearch.LoadDefault();
                if (!Empty(BasicSearch.Keyword))
                    srchBasic = BasicSearchWhere(); // Save to session

                // Load advanced search from default
                if (LoadAdvancedSearchDefault())
                    srchAdvanced = AdvancedSearchWhere(); // Save to session
            }

            // Restore search settings from Session
            if (!HasInvalidFields())
                LoadAdvancedSearch();

            // Build search criteria
            if (!Empty(query)) {
                AddFilter(ref SearchWhere, query);
            } else {
                AddFilter(ref SearchWhere, srchAdvanced);
                AddFilter(ref SearchWhere, srchBasic);
            }

            // Call Recordset Searching event
            RecordsetSearching(ref SearchWhere);

            // Save search criteria
            if (Command == "search" && !RestoreSearch) {
                SessionSearchWhere = SearchWhere; // Save to Session (rename as SessionSearchWhere property)
                StartRecord = 1; // Reset start record counter
                StartRecordNumber = StartRecord;
            } else if (Command != "json" && Empty(query)) {
                SearchWhere = SessionSearchWhere;
            }

            // Build filter
            filter = "";
            AddFilter(ref filter, DbDetailFilter);
            AddFilter(ref filter, SearchWhere);

            // Set up filter
            if (Command == "json") {
                UseSessionForListSql = false; // Do not use session for ListSql
                CurrentFilter = filter;
            } else {
                SessionWhere = filter;
                CurrentFilter = "";
            }
            Filter = ApplyUserIDFilters(filter);
            if (IsGridAdd) {
                CurrentFilter = "0=1";
                StartRecord = 1;
                DisplayRecords = GridAddRowCount;
                TotalRecords = DisplayRecords;
                StopRecord = DisplayRecords;
            } else if ((IsEdit || IsCopy || IsInlineInserted || IsInlineUpdated) && UseInfiniteScroll) { // Get current record only
                CurrentFilter = IsInlineUpdated ? GetRecordFilter() : GetFilterFromRecordKeys();
                TotalRecords = ListRecordCount();
                StartRecord = 1;
                StopRecord = DisplayRecords;
                Recordset = await LoadRecordset();
            } else if (
                UseInfiniteScroll && IsGridInserted ||
                UseInfiniteScroll && (IsGridEdit || IsGridUpdated) ||
                IsMultiEdit ||
                UseInfiniteScroll && IsMultiUpdated
            ) { // Get current records only
                CurrentFilter = FilterForModalActions; // Restore filter
                TotalRecords = ListRecordCount();
                StartRecord = 1;
                StopRecord = DisplayRecords;
                Recordset = await LoadRecordset();
            } else {
                TotalRecords = await ListRecordCountAsync();
                StopRecord = DisplayRecords;
                StartRecord = 1;
                if (DisplayRecords <= 0 || (IsExport() && ExportAll)) // Display all records
                    DisplayRecords = TotalRecords;
                if (!(IsExport() && ExportAll)) // Set up start record position
                    SetupStartRecord();

                // Recordset
                bool selectLimit = UseSelectLimit;

                // Set up list action columns, must be before LoadRecordset // DN
                foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Allowed)) {
                    if (act.Select == Config.ActionMultiple && ListOptions["checkbox"] is ListOption listOpt) { // Show checkbox column if multiple action
                        listOpt.Visible = true;
                    } else if (act.Select == Config.ActionSingle) { // Show list action column
                            ListOptions["listactions"]?.SetVisible(true); // Set visible if any list action is allowed
                    }
                }
                if (selectLimit)
                    Recordset = await LoadRecordset(StartRecord - 1, DisplayRecords);

                // Set no record found message
                if ((Empty(CurrentAction) || IsSearch) && TotalRecords == 0) {
                    if (SearchWhere == "0=101")
                        WarningMessage = Language.Phrase("EnterSearchCriteria");
                    else
                        WarningMessage = Language.Phrase("NoRecord");
                }
            }

            // Search options
            SetupSearchOptions();

            // Set up search panel class
            if (!Empty(SearchWhere)) {
                if (!Empty(query)) { // Hide search panel if using QueryBuilder
                    SearchPanelClass = RemoveClass(SearchPanelClass, "show");
                } else {
                    SearchPanelClass = AppendClass(SearchPanelClass, "show");
                }
            }

            // API list action
            if (IsApi()) {
                if (CurrentPageName().EndsWith(Config.ApiListAction)) { // DN
                    if (!IsExport()) {
                        if (!Connection.SelectOffset && Recordset != null) { // DN
                            for (var i = 1; i <= StartRecord - 1; i++) // Move to first record
                                await Recordset.ReadAsync();
                        }
                        using (Recordset) {
                            return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, await GetRecordsFromRecordset(Recordset)}, {"totalRecordCount", TotalRecords}, {"version", Config.ProductVersion} });
                        }
                    }
                } else if (!Empty(FailureMessage)) {
                    return Controller.Json(new { success = false, error = GetFailureMessage() });
                }
                return new EmptyResult();
            }

            // Render other options
            RenderOtherOptions();

            // Set ReturnUrl in header if necessary
            if (TempData["Return-Url"] != null)
                AddHeader("Return-Url", ConvertToString(TempData["Return-Url"]));

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                tadsQrcodeList?.PageRender();
            }
            return PageResult();
        }
        #pragma warning restore 219

        // Get page number
        public int PageNumber => DisplayRecords > 0 && StartRecord > 0 ? ConvertToInt(Math.Ceiling((double)StartRecord / DisplayRecords)) : 1;

        // Set up number of records displayed per page
        protected void SetupDisplayRecords() {
            string wrk = Get(Config.TableRecordsPerPage);
            if (!Empty(wrk)) {
                if (IsNumeric(wrk)) {
                    DisplayRecords = ConvertToInt(wrk);
                } else {
                    if (SameText(wrk, "all")) { // Display all records
                        DisplayRecords = -1;
                    } else {
                        DisplayRecords = 20; // Non-numeric, load default
                    }
                }
                RecordsPerPage = DisplayRecords; // Save to Session
                // Reset start position
                StartRecord = 1;
                StartRecordNumber = StartRecord;
            }
        }

        // Exit inline mode
        protected void ClearInlineMode() {
            LastAction = CurrentAction; // Save last action
            CurrentAction = ""; // Clear action
            Session[Config.SessionInlineMode] = ""; // Clear inline mode
        }

        // Switch to grid add mode
        protected void GridAddMode() {
            CurrentAction = "gridadd";
            Session[Config.SessionInlineMode] = "gridadd"; // Enabled grid add
            HideFieldsForAddEdit();
        }

        // Switch to grid edit mode
        protected void GridEditMode() {
            CurrentAction = "gridedit";
            Session[Config.SessionInlineMode] = "gridedit"; // Enabled grid edit
            HideFieldsForAddEdit();
        }

        // Switch to Inline Edit Mode
        protected async Task InlineEditMode() { // DN
            bool inlineEdit = true;
            object? rv;
            StringValues qv;
            if (RouteValues.TryGetValue("ID", out rv)) { // DN
                ID.QueryValue = UrlDecode(rv); // DN
            } else if (Get("ID", out qv)) {
                ID.QueryValue = qv;
            } else {
                inlineEdit = false;
            }
            if (inlineEdit) {
                if (await LoadRow()) {
                    OldKey = GetKey(true); // Get from CurrentValue
                    SetKey(OldKey); // Set to OldValue
                    Session[Config.SessionInlineMode] = "edit"; // Enabled inline edit
                }
            }
        }

        // Peform update to Inline Edit record
        protected async Task<bool> InlineUpdate() {
            CurrentForm!.Index = 1;
            await LoadFormValues(); // Get form values

            // Validate Form
            bool inlineUpdate = true;
            if (!await ValidateForm()) {
                inlineUpdate = false; // Form error, reset action
            } else {
                inlineUpdate = false;
                SendEmail = true; // Send email on update success
                inlineUpdate = await EditRow(); // Update record
            }
            if (inlineUpdate) { // Update success
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("UpdateSuccess"); // Set up success message
                ClearInlineMode(); // Clear inline edit mode
            } else {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
                EventCancelled = true; // Cancel event
                CurrentAction = "edit"; // Stay in edit mode
            }
            return inlineUpdate;
        }

        // Check inline edit key
        public bool CheckInlineEditKey()
        {
            if (!SameString(ID.OldValue, ID.CurrentValue))
                return false;
            return true;
        }

        #pragma warning disable 1998
        // Switch to Inline Add mode
        protected async Task InlineAddMode() {
            CurrentAction = "add";
            Session[Config.SessionInlineMode] = "add"; // Enabled inline add
        }
        #pragma warning restore 1998

        // Perform update to Inline Add/Copy record
        protected async Task<bool> InlineInsert() {
            var c = await GetConnection2Async(DbId);
            var row = await LoadOldRecord(c); // Load old record
            CurrentForm!.Index = 0;
            await LoadFormValues(); // Get form values

            // Validate form
            if (!await ValidateForm()) {
                EventCancelled = true; // Set event cancelled
                CurrentAction = "add"; // Stay in add mode
                return false;
            }
            SendEmail = true; // Send email on add success
            if (await AddRow(row)) { // Add record
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("AddSuccess"); // Set up add success message
                ClearInlineMode(); // Clear inline add mode
                return true;
            } else { // Add failed
                EventCancelled = true; // Set event cancelled
                CurrentAction = "add"; // Stay in add mode
            }
            return false;
        }

        // Perform update to grid
        public async Task<bool> GridUpdate()
        {
            bool gridUpdate = true;

            // Get old recordset
            CurrentFilter = BuildKeyFilter();
            if (Empty(CurrentFilter))
                CurrentFilter = "0=1";
            string sql = CurrentSql;
            List<Dictionary<string, object>> rsold = await Connection.GetRowsAsync(sql);

            // Call Grid Updating event
            if (!GridUpdating(rsold)) {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("GridEditCancelled"); // Set grid edit cancelled message
                EventCancelled = true;
                return false;
            }

            // Begin transaction
            if (UseTransaction)
                Connection.BeginTrans();
            string wrkFilter = "";
            string key = "";

            // Update row index and get row key
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking // DN
            LoadDefaultValues();

            // Update all rows based on key
            try {
                for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                    CurrentForm!.Index = rowindex;
                    SetKey(CurrentForm.GetValue(OldKeyName));
                    string rowaction = CurrentForm.GetValue(FormActionName);

                    // Load all values and keys
                    if (rowaction != "insertdelete" && rowaction != "hide") { // Skip insert then deleted rows / hidden rows for grid edit
                        await LoadFormValues(); // Get form values
                        if (Empty(rowaction) || rowaction == "edit" || rowaction == "delete") {
                            gridUpdate = !Empty(OldKey); // Key must not be empty
                        } else {
                            gridUpdate = true;
                        }

                        // Skip empty row
                        if (rowaction == "insert" && EmptyRow()) {
                            // No action required
                        } else if (gridUpdate) { // Validate form and insert/update/delete record
                            if (rowaction == "delete") {
                                CurrentFilter = GetRecordFilter();
                                gridUpdate = await DeleteRows(); // Delete this row
                            } else {
                                if (rowaction == "insert") {
                                    gridUpdate = await AddRow(); // Insert this row
                                } else {
                                    if (!Empty(OldKey)) {
                                        SendEmail = false; // Do not send email on update success
                                        gridUpdate = await EditRow(); // Update this row
                                    }
                                } // End update
                                if (gridUpdate) // Get inserted or updated filter
                                    AddFilter(ref wrkFilter, GetRecordFilter(), "OR");
                            }
                        }
                        if (gridUpdate) {
                            if (!Empty(key))
                                key += ", ";
                            key += OldKey;
                        } else {
                            EventCancelled = true;
                            break;
                        }
                    }
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                gridUpdate = false;
            }
            if (gridUpdate) {
                if (UseTransaction)
                    Connection.CommitTrans(); // Commit transaction
                FilterForModalActions = wrkFilter;

                // Get new recordset
                List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

                // Call Grid Updated event
                GridUpdated(rsold, rsnew);
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("UpdateSuccess"); // Set up update success message
                ClearInlineMode(); // Clear inline edit mode
            } else {
                if (UseTransaction)
                    Connection.RollbackTrans(); // Rollback transaction
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
            }
            return gridUpdate;
        }

        // Build filter for all keys
        protected string BuildKeyFilter() {
            string wrkFilter = "";

            // Update row index and get row key
            int rowindex = 1;
            CurrentForm!.Index = rowindex;
            string thisKey = CurrentForm.GetValue(OldKeyName);
            while (!Empty(thisKey)) {
                SetKey(thisKey);
                if (!Empty(OldKey)) {
                    string filter = GetRecordFilter();
                    if (!Empty(wrkFilter))
                        wrkFilter += " OR ";
                    wrkFilter += filter;
                } else {
                    wrkFilter = "0=1";
                    break;
                }

                // Update row index and get row key
                rowindex++; // next row
                CurrentForm!.Index = rowindex;
                thisKey = CurrentForm.GetValue(OldKeyName);
            }
            return wrkFilter;
        }

        // Perform Grid Add
        #pragma warning disable 168, 219

        public async Task<bool> GridInsert()
        {
            int addcnt = 0;
            bool gridInsert = false;

            // Call Grid Inserting event
            if (!GridInserting()) {
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("GridAddCancelled"); // Set grid add cancelled message
                EventCancelled = true;
                return false;
            }

            // Begin transaction
            if (UseTransaction)
                Connection.BeginTrans();

            // Init key filter
            string wrkFilter = "";
            string key = "";

            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking // DN
            LoadDefaultValues();

            // Insert all rows
            try {
                for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                    // Load current row values
                    CurrentForm!.Index = rowindex;
                    string rowaction = CurrentForm.GetValue(FormActionName);
                    Dictionary<string, object>? rsold = null;
                    if (!Empty(rowaction) && rowaction != "insert")
                        continue; // Skip
                    if (rowaction == "insert") {
                        OldKey = CurrentForm.GetValue(OldKeyName);
                        rsold = await LoadOldRecord(); // Load old record
                    }
                    await LoadFormValues(); // Get form values
                    if (!EmptyRow()) {
                        addcnt++;
                        SendEmail = false; // Do not send email on insert success
                        gridInsert = await AddRow(rsold); // Insert row (already validated by validateGridForm())
                        if (gridInsert) {
                            if (!Empty(key))
                                key += Config.CompositeKeySeparator;
                            key += ConvertToString(ID.CurrentValue);

                            // Add filter for this record
                            AddFilter(ref wrkFilter, GetRecordFilter(), "OR");
                        } else {
                            EventCancelled = true;
                            break;
                        }
                    }
                }
                if (addcnt == 0) { // No record inserted
                    FailureMessage = Language.Phrase("NoAddRecord");
                    gridInsert = false;
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                gridInsert = false;
            }
            if (gridInsert) {
                if (UseTransaction)
                    Connection.CommitTrans(); // Commit transaction

                // Get new recordset
                CurrentFilter = wrkFilter;
                FilterForModalActions = wrkFilter;
                string sql = CurrentSql;
                List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

                // Call Grid Inserted event
                GridInserted(rsnew);
                if (Empty(SuccessMessage))
                    SuccessMessage = Language.Phrase("InsertSuccess"); // Set up insert success message
                ClearInlineMode(); // Clear grid add mode
            } else {
                if (UseTransaction)
                    Connection.RollbackTrans(); // Rollback transaction
                if (Empty(FailureMessage))
                    FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
            }
            return gridInsert;
        }
        #pragma warning restore 168, 219

        // Check if empty row
        public bool EmptyRow()
        {
            if (CurrentForm == null)
                return true;
            if (CurrentForm.HasValue("x_COD") && CurrentForm.HasValue("o_COD") && !SameString(COD.CurrentValue, COD.DefaultValue) &&
            !(COD.IsForeignKey && CurrentMasterTable != "" && SameString(COD.CurrentValue, COD.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_DES") && CurrentForm.HasValue("o_DES") && !SameString(DES.CurrentValue, DES.DefaultValue) &&
            !(DES.IsForeignKey && CurrentMasterTable != "" && SameString(DES.CurrentValue, DES.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_CMD") && CurrentForm.HasValue("o_CMD") && !SameString(CMD.CurrentValue, CMD.DefaultValue) &&
            !(CMD.IsForeignKey && CurrentMasterTable != "" && SameString(CMD.CurrentValue, CMD.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_FRM") && CurrentForm.HasValue("o_FRM") && !SameString(FRM.CurrentValue, FRM.DefaultValue) &&
            !(FRM.IsForeignKey && CurrentMasterTable != "" && SameString(FRM.CurrentValue, FRM.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_LBL") && CurrentForm.HasValue("o_LBL") && !SameString(LBL.CurrentValue, LBL.DefaultValue) &&
            !(LBL.IsForeignKey && CurrentMasterTable != "" && SameString(LBL.CurrentValue, LBL.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_COR") && CurrentForm.HasValue("o_COR") && !SameString(COR.CurrentValue, COR.DefaultValue) &&
            !(COR.IsForeignKey && CurrentMasterTable != "" && SameString(COR.CurrentValue, COR.SessionValue)))
                return false;
            if (CurrentForm.HasValue("x_ETAPA") && CurrentForm.HasValue("o_ETAPA") && !SameString(ETAPA.CurrentValue, ETAPA.DefaultValue) &&
            !(ETAPA.IsForeignKey && CurrentMasterTable != "" && SameString(ETAPA.CurrentValue, ETAPA.SessionValue)))
                return false;
            if (!Empty(QRCODE.Upload.Value))
                return false;
            if (CurrentForm.HasValue("x_QRCODE2") && CurrentForm.HasValue("o_QRCODE2") && !SameString(QRCODE2.CurrentValue, QRCODE2.DefaultValue) &&
            !(QRCODE2.IsForeignKey && CurrentMasterTable != "" && SameString(QRCODE2.CurrentValue, QRCODE2.SessionValue)))
                return false;
            return true;
        }

        // Validate grid form
        public async Task<bool> ValidateGridForm()
        {
            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;

            // Load default values for emptyRow checking
            LoadDefaultValues();

            // Validate all records
            for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                // Load current row values
                CurrentForm!.Index = rowindex;
                string rowaction = CurrentForm.GetValue(FormActionName);
                if (rowaction != "delete" && rowaction != "insertdelete" && rowaction != "hide") {
                    await LoadFormValues(); // Get form values
                    if (rowaction == "insert" && EmptyRow()) {
                        // Ignore
                    } else if (!await ValidateForm()) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Get all form values of the grid
        public List<Dictionary<string, string?>> GetGridFormValues()
        {
            // Get row count
            CurrentForm?.ResetIndex();
            int rowcnt = CurrentForm?.GetInt(FormKeyCountName) ?? 0;
            List<Dictionary<string, string?>> rows = new ();

            // Loop through all records
            for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
                // Load current row values
                CurrentForm!.Index = rowindex;
                string rowaction = CurrentForm.GetValue(FormActionName);
                if (rowaction != "delete" && rowaction != "insertdelete") {
                    LoadFormValues().GetAwaiter().GetResult(); // Load form values (sync)
                    if (rowaction == "insert" && EmptyRow()) {
                        // Ignore
                    } else {
                        rows.Add(GetFormValues()); // Return row as array
                    }
                }
            }
            return rows; // Return as array of array
        }

        // Restore form values for current row
        public async Task RestoreCurrentRowFormValues(object index)
        {
            // Get row based on current index
            if (index is int idx)
                CurrentForm!.Index = idx;
            string rowaction = CurrentForm.GetValue(FormActionName);
            await LoadFormValues(); // Load form values
            // Set up invalid status correctly
            ResetFormError();
            if (rowaction == "insert" && EmptyRow()) {
                // Ignore
            } else {
                await ValidateForm();
            }
        }

        // Reset form status
        public void ResetFormError()
        {
            ID.ClearErrorMessage();
            COD.ClearErrorMessage();
            DES.ClearErrorMessage();
            CMD.ClearErrorMessage();
            FRM.ClearErrorMessage();
            LBL.ClearErrorMessage();
            COR.ClearErrorMessage();
            ETAPA.ClearErrorMessage();
            QRCODE.ClearErrorMessage();
            QRCODE2.ClearErrorMessage();
        }

        #pragma warning disable 162, 1998
        // Get list of filters
        public async Task<string> GetFilterList()
        {
            string filterList = "";

            // Initialize
            var filters = new JObject(); // DN
            filters.Merge(JObject.Parse(ID.AdvancedSearch.ToJson())); // Field ID
            filters.Merge(JObject.Parse(COD.AdvancedSearch.ToJson())); // Field COD
            filters.Merge(JObject.Parse(DES.AdvancedSearch.ToJson())); // Field DES
            filters.Merge(JObject.Parse(CMD.AdvancedSearch.ToJson())); // Field CMD
            filters.Merge(JObject.Parse(FRM.AdvancedSearch.ToJson())); // Field FRM
            filters.Merge(JObject.Parse(LBL.AdvancedSearch.ToJson())); // Field LBL
            filters.Merge(JObject.Parse(COR.AdvancedSearch.ToJson())); // Field COR
            filters.Merge(JObject.Parse(ETAPA.AdvancedSearch.ToJson())); // Field ETAPA
            filters.Merge(JObject.Parse(QRCODE2.AdvancedSearch.ToJson())); // Field QRCODE2
            filters.Merge(JObject.Parse(BasicSearch.ToJson()));

            // Return filter list in JSON
            if (filters.HasValues)
                filterList = "\"data\":" + filters.ToString();
            return (filterList != "") ? "{" + filterList + "}" : "null";
        }

        // Process filter list
        protected async Task<object?> ProcessFilterList() {
            if (Post("cmd") == "resetfilter") {
                RestoreFilterList();
            }
            return null;
        }
        #pragma warning restore 162, 1998

        // Restore list of filters
        protected bool RestoreFilterList() {
            // Return if not reset filter
            if (Post("cmd") != "resetfilter")
                return false;
            var filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(Post("filter"));
            Command = "search";
            string? sv;

            // Field ID
            if (filter?.TryGetValue("x_ID", out sv) ?? false) {
                ID.AdvancedSearch.SearchValue = sv;
                ID.AdvancedSearch.SearchOperator = filter["z_ID"];
                ID.AdvancedSearch.SearchCondition = filter["v_ID"];
                ID.AdvancedSearch.SearchValue2 = filter["y_ID"];
                ID.AdvancedSearch.SearchOperator2 = filter["w_ID"];
                ID.AdvancedSearch.Save();
            }

            // Field COD
            if (filter?.TryGetValue("x_COD", out sv) ?? false) {
                COD.AdvancedSearch.SearchValue = sv;
                COD.AdvancedSearch.SearchOperator = filter["z_COD"];
                COD.AdvancedSearch.SearchCondition = filter["v_COD"];
                COD.AdvancedSearch.SearchValue2 = filter["y_COD"];
                COD.AdvancedSearch.SearchOperator2 = filter["w_COD"];
                COD.AdvancedSearch.Save();
            }

            // Field DES
            if (filter?.TryGetValue("x_DES", out sv) ?? false) {
                DES.AdvancedSearch.SearchValue = sv;
                DES.AdvancedSearch.SearchOperator = filter["z_DES"];
                DES.AdvancedSearch.SearchCondition = filter["v_DES"];
                DES.AdvancedSearch.SearchValue2 = filter["y_DES"];
                DES.AdvancedSearch.SearchOperator2 = filter["w_DES"];
                DES.AdvancedSearch.Save();
            }

            // Field CMD
            if (filter?.TryGetValue("x_CMD", out sv) ?? false) {
                CMD.AdvancedSearch.SearchValue = sv;
                CMD.AdvancedSearch.SearchOperator = filter["z_CMD"];
                CMD.AdvancedSearch.SearchCondition = filter["v_CMD"];
                CMD.AdvancedSearch.SearchValue2 = filter["y_CMD"];
                CMD.AdvancedSearch.SearchOperator2 = filter["w_CMD"];
                CMD.AdvancedSearch.Save();
            }

            // Field FRM
            if (filter?.TryGetValue("x_FRM", out sv) ?? false) {
                FRM.AdvancedSearch.SearchValue = sv;
                FRM.AdvancedSearch.SearchOperator = filter["z_FRM"];
                FRM.AdvancedSearch.SearchCondition = filter["v_FRM"];
                FRM.AdvancedSearch.SearchValue2 = filter["y_FRM"];
                FRM.AdvancedSearch.SearchOperator2 = filter["w_FRM"];
                FRM.AdvancedSearch.Save();
            }

            // Field LBL
            if (filter?.TryGetValue("x_LBL", out sv) ?? false) {
                LBL.AdvancedSearch.SearchValue = sv;
                LBL.AdvancedSearch.SearchOperator = filter["z_LBL"];
                LBL.AdvancedSearch.SearchCondition = filter["v_LBL"];
                LBL.AdvancedSearch.SearchValue2 = filter["y_LBL"];
                LBL.AdvancedSearch.SearchOperator2 = filter["w_LBL"];
                LBL.AdvancedSearch.Save();
            }

            // Field COR
            if (filter?.TryGetValue("x_COR", out sv) ?? false) {
                COR.AdvancedSearch.SearchValue = sv;
                COR.AdvancedSearch.SearchOperator = filter["z_COR"];
                COR.AdvancedSearch.SearchCondition = filter["v_COR"];
                COR.AdvancedSearch.SearchValue2 = filter["y_COR"];
                COR.AdvancedSearch.SearchOperator2 = filter["w_COR"];
                COR.AdvancedSearch.Save();
            }

            // Field ETAPA
            if (filter?.TryGetValue("x_ETAPA", out sv) ?? false) {
                ETAPA.AdvancedSearch.SearchValue = sv;
                ETAPA.AdvancedSearch.SearchOperator = filter["z_ETAPA"];
                ETAPA.AdvancedSearch.SearchCondition = filter["v_ETAPA"];
                ETAPA.AdvancedSearch.SearchValue2 = filter["y_ETAPA"];
                ETAPA.AdvancedSearch.SearchOperator2 = filter["w_ETAPA"];
                ETAPA.AdvancedSearch.Save();
            }

            // Field QRCODE2
            if (filter?.TryGetValue("x_QRCODE2", out sv) ?? false) {
                QRCODE2.AdvancedSearch.SearchValue = sv;
                QRCODE2.AdvancedSearch.SearchOperator = filter["z_QRCODE2"];
                QRCODE2.AdvancedSearch.SearchCondition = filter["v_QRCODE2"];
                QRCODE2.AdvancedSearch.SearchValue2 = filter["y_QRCODE2"];
                QRCODE2.AdvancedSearch.SearchOperator2 = filter["w_QRCODE2"];
                QRCODE2.AdvancedSearch.Save();
            }
            if (filter?.TryGetValue(Config.TableBasicSearch, out string? keyword) ?? false)
                BasicSearch.SessionKeyword = keyword;
            if (filter?.TryGetValue(Config.TableBasicSearchType, out string? type) ?? false)
                BasicSearch.SessionType = type;
            return true;
        }

        // Advanced search WHERE clause based on QueryString
        public string AdvancedSearchWhere(bool def = false) {
            string where = "";
            BuildSearchSql(ref where, ID, def, false); // ID
            BuildSearchSql(ref where, COD, def, false); // COD
            BuildSearchSql(ref where, DES, def, false); // DES
            BuildSearchSql(ref where, CMD, def, false); // CMD
            BuildSearchSql(ref where, FRM, def, false); // FRM
            BuildSearchSql(ref where, LBL, def, false); // LBL
            BuildSearchSql(ref where, COR, def, false); // COR
            BuildSearchSql(ref where, ETAPA, def, false); // ETAPA
            BuildSearchSql(ref where, QRCODE2, def, false); // QRCODE2

            // Set up search command
            if (!def && !Empty(where) && (new[] { "", "reset", "resetall" }).Contains(Command))
                Command = "search";
            if (!def && Command == "search") {
                ID.AdvancedSearch.Save(); // ID
                COD.AdvancedSearch.Save(); // COD
                DES.AdvancedSearch.Save(); // DES
                CMD.AdvancedSearch.Save(); // CMD
                FRM.AdvancedSearch.Save(); // FRM
                LBL.AdvancedSearch.Save(); // LBL
                COR.AdvancedSearch.Save(); // COR
                ETAPA.AdvancedSearch.Save(); // ETAPA
                QRCODE2.AdvancedSearch.Save(); // QRCODE2

                // Clear rules for QueryBuilder
                SessionRules = "";
            }
            return where;
        }

        // Parse query builder rule function
        protected string ParseRules(Dictionary<string, object>? group, string fieldName = "") {
            if (group == null)
                return "";
            string condition = group.ContainsKey("condition") ? ConvertToString(group["condition"]) : "AND";
            if (!(new [] { "AND", "OR" }).Contains(condition))
                throw new System.Exception("Unable to build SQL query with condition '" + condition + "'");
            List<string> parts = new ();
            string where = "";
            var groupRules = group.ContainsKey("rules") ? group["rules"] : null;
            if (groupRules is IEnumerable<object> rules) {
                foreach (object rule in rules) {
                    var subRules = JObject.FromObject(rule).ToObject<Dictionary<string, object>>();
                    if (subRules == null)
                        continue;
                    if (subRules.ContainsKey("rules")) {
                        parts.Add("(" + " " + ParseRules(subRules, fieldName) + " " + ")" + " ");
                    } else {
                        string field = subRules.ContainsKey("field") ? ConvertToString(subRules["field"]) : "";
                        var fld = FieldByParam(field);
                        if (fld == null)
                            throw new System.Exception("Failed to find field '" + field + "'");
                        if (Empty(fieldName) || fld.Name == fieldName) { // Field name not specified or matched field name
                            string opr = subRules.ContainsKey("operator") ? ConvertToString(subRules["operator"]) : "";
                            string fldOpr = Config.ClientSearchOperators.FirstOrDefault(o => o.Value == opr).Key;
                            Dictionary<string, object>? ope = Config.QueryBuilderOperators.ContainsKey(opr) ? Config.QueryBuilderOperators[opr] : null;
                            if (ope == null || Empty(fldOpr))
                                throw new System.Exception("Unknown SQL operation for operator '" + opr + "'");
                            int nb_inputs = ope.ContainsKey("nb_inputs") ? ConvertToInt(ope["nb_inputs"]) : 0;
                            object val = subRules.ContainsKey("value") ? subRules["value"] : "";
                            if (nb_inputs > 0 && !Empty(val) || IsNullOrEmptyOperator(fldOpr)) {
                                string fldVal = val is List<object> list
                                    ? (list[0] is IEnumerable<string> ? String.Join(Config.MultipleOptionSeparator, list[0]) : ConvertToString(list[0]))
                                    : ConvertToString(val);
                                bool useFilter = fld.UseFilter; // Query builder does not use filter
                                try {
                                    if (fld.IsMultiSelect) {
                                        parts.Add(!Empty(fldVal) ? GetMultiSearchSql(fld, fldOpr, ConvertSearchValue(fldVal, fldOpr, fld), DbId) : "");
                                    } else {
                                        string fldVal2 = fldOpr.Contains("BETWEEN")
                                            ? (val is List<object> list2 && list2.Count > 1
                                                ? (list2[1] is IEnumerable<string> ? String.Join(Config.MultipleOptionSeparator, list2[1]) : ConvertToString(list2[1]))
                                                : "")
                                            : ""; // BETWEEN
                                        parts.Add(GetSearchSql(
                                            fld,
                                            ConvertSearchValue(fldVal, fldOpr, fld), // fldVal
                                            fldOpr,
                                            "", // fldCond not used
                                            ConvertSearchValue(fldVal2, fldOpr, fld), // $fldVal2
                                            "", // fldOpr2 not used
                                            DbId
                                        ));
                                    }
                                } finally {
                                    fld.UseFilter = useFilter;
                                }
                            }
                        }
                    }
                }
                where = String.Join(" " + condition + " ", parts);
                bool not = group.ContainsKey("not") ? ConvertToBool(group["not"]) : false;
                if (not)
                    where = "NOT (" + where + ")";
            }
            return where;
        }

        // Quey builder WHERE clause
        public string QueryBuilderWhere(string fieldName = "")
        {
            // Get rules by query builder
            string rules = "";
            if (Post("rules", out StringValues sv))
                rules = sv.ToString();
            else
                rules = SessionRules;

            // Decode and parse rules
            string where = !Empty(rules) ? ParseRules(JsonConvert.DeserializeObject<Dictionary<string, object>>(rules), fieldName) : "";

            // Clear other search and save rules to session
            if (!Empty(where) && Empty(fieldName)) { // Skip if get query for specific field
                ResetSearchParms();
                SessionRules = rules;
            }

            // Return query
            return where;
        }

        // Build search SQL
        public void BuildSearchSql(ref string where, DbField fld, bool def, bool multiValue)
        {
            string fldParm = fld.Param;
            string fldVal = def ? ConvertToString(fld.AdvancedSearch.SearchValueDefault) : ConvertToString(fld.AdvancedSearch.SearchValue);
            string fldOpr = def ? fld.AdvancedSearch.SearchOperatorDefault : fld.AdvancedSearch.SearchOperator;
            string fldCond = def ? fld.AdvancedSearch.SearchConditionDefault : fld.AdvancedSearch.SearchCondition;
            string fldVal2 = def ? ConvertToString(fld.AdvancedSearch.SearchValue2Default) : ConvertToString(fld.AdvancedSearch.SearchValue2);
            string fldOpr2 = def ? fld.AdvancedSearch.SearchOperator2Default : fld.AdvancedSearch.SearchOperator2;
            fldVal = ConvertSearchValue(fldVal, fldOpr, fld);
            fldVal2 = ConvertSearchValue(fldVal2, fldOpr2, fld);
            fldOpr = ConvertSearchOperator(fldOpr, fld, fldVal);
            fldOpr2 = ConvertSearchOperator(fldOpr2, fld, fldVal2);
            string wrk = "";
            if (Config.SearchMultiValueOption == 1 && !fld.UseFilter || !IsMultiSearchOperator(fldOpr))
                multiValue = false;
            if (multiValue) {
                wrk = !Empty(fldVal) ? GetMultiSearchSql(fld, fldOpr, fldVal, DbId) : ""; // Field value 1
                string wrk2 = !Empty(fldVal2) ? GetMultiSearchSql(fld, fldOpr2, fldVal2, DbId) : ""; // Field value 2
                AddFilter(ref wrk, wrk2, fldCond);
            } else {
                wrk = GetSearchSql(fld, fldVal, fldOpr, fldCond, fldVal2, fldOpr2, DbId);
            }
            string cond = SearchOption == "AUTO" && (new[] {"AND", "OR"}).Contains(BasicSearch.Type)
                ? BasicSearch.Type
                : SameText(SearchOption, "OR") ? "OR" : "AND";
            AddFilter(ref where, wrk, cond);
        }

        // Show list of filters
        public void ShowFilterList()
        {
            // Initialize
            string filterList = "",
                filter = "",
                captionClass = IsExport("email") ? "ew-filter-caption-email" : "ew-filter-caption",
                captionSuffix = IsExport("email") ? ": " : "";

            // Field ID
            filter = QueryBuilderWhere("ID");
            if (Empty(filter))
                BuildSearchSql(ref filter, ID, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + ID.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field COD
            filter = QueryBuilderWhere("COD");
            if (Empty(filter))
                BuildSearchSql(ref filter, COD, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + COD.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field DES
            filter = QueryBuilderWhere("DES");
            if (Empty(filter))
                BuildSearchSql(ref filter, DES, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + DES.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field CMD
            filter = QueryBuilderWhere("CMD");
            if (Empty(filter))
                BuildSearchSql(ref filter, CMD, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + CMD.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field FRM
            filter = QueryBuilderWhere("FRM");
            if (Empty(filter))
                BuildSearchSql(ref filter, FRM, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + FRM.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field LBL
            filter = QueryBuilderWhere("LBL");
            if (Empty(filter))
                BuildSearchSql(ref filter, LBL, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + LBL.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field COR
            filter = QueryBuilderWhere("COR");
            if (Empty(filter))
                BuildSearchSql(ref filter, COR, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + COR.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field ETAPA
            filter = QueryBuilderWhere("ETAPA");
            if (Empty(filter))
                BuildSearchSql(ref filter, ETAPA, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + ETAPA.Caption + "</span>" + captionSuffix + filter + "</div>";

            // Field QRCODE2
            filter = QueryBuilderWhere("QRCODE2");
            if (Empty(filter))
                BuildSearchSql(ref filter, QRCODE2, false, false);
            if (!Empty(filter))
                filterList += "<div><span class=\"" + captionClass + "\">" + QRCODE2.Caption + "</span>" + captionSuffix + filter + "</div>";
            if (!Empty(BasicSearch.Keyword))
                filterList += "<div><span class=\"" + captionClass + "\">" + Language.Phrase("BasicSearchKeyword") + "</span>" + captionSuffix + BasicSearch.Keyword + "</div>";

            // Show Filters
            if (!Empty(filterList)) {
                string message = "<div id=\"ew-filter-list\" class=\"callout callout-info d-table\"><div id=\"ew-current-filters\">" +
                    Language.Phrase("CurrentFilters") + "</div>" + filterList + "</div>";
                MessageShowing(ref message, "");
                Write(message);
            } else { // Output empty tag
                Write("<div id=\"ew-filter-list\"></div>");
            }
        }

        // Return basic search WHERE clause based on search keyword and type
        public string BasicSearchWhere(bool def = false) {
            string searchStr = "";

            // Fields to search
            List<DbField> searchFlds = new ();
            searchFlds.Add(COD);
            searchFlds.Add(DES);
            searchFlds.Add(CMD);
            searchFlds.Add(FRM);
            searchFlds.Add(LBL);
            searchFlds.Add(COR);
            searchFlds.Add(ETAPA);
            searchFlds.Add(QRCODE2);
            string searchKeyword = def ? BasicSearch.KeywordDefault : BasicSearch.Keyword;
            string searchType = def ? BasicSearch.TypeDefault : BasicSearch.Type;

            // Get search SQL
            if (!Empty(searchKeyword)) {
                List<string> list = BasicSearch.KeywordList(def);
                searchStr = GetQuickSearchFilter(searchFlds, list, searchType, BasicSearch.BasicSearchAnyFields, DbId);
                if (!def && (new[] {"", "reset", "resetall"}).Contains(Command))
                    Command = "search";
            }
            if (!def && Command == "search") {
                BasicSearch.SessionKeyword = searchKeyword;
                BasicSearch.SessionType = searchType;

                // Clear rules for QueryBuilder
                SessionRules = "";
            }
            return searchStr;
        }

        // Check if search parm exists
        protected bool CheckSearchParms() {
            // Check basic search
            if (BasicSearch.IssetSession)
                return true;
            if (ID.AdvancedSearch.IssetSession)
                return true;
            if (COD.AdvancedSearch.IssetSession)
                return true;
            if (DES.AdvancedSearch.IssetSession)
                return true;
            if (CMD.AdvancedSearch.IssetSession)
                return true;
            if (FRM.AdvancedSearch.IssetSession)
                return true;
            if (LBL.AdvancedSearch.IssetSession)
                return true;
            if (COR.AdvancedSearch.IssetSession)
                return true;
            if (ETAPA.AdvancedSearch.IssetSession)
                return true;
            if (QRCODE2.AdvancedSearch.IssetSession)
                return true;
            return false;
        }

        // Clear all search parameters
        protected void ResetSearchParms() {
            SearchWhere = "";
            SessionSearchWhere = SearchWhere;

            // Clear basic search parameters
            ResetBasicSearchParms();

            // Clear advanced search parameters
            ResetAdvancedSearchParms();

            // Clear queryBuilder
            SessionRules = "";
        }

        // Load advanced search default values
        protected bool LoadAdvancedSearchDefault() {
        return false;
        }

        // Clear all basic search parameters
        protected void ResetBasicSearchParms() {
            BasicSearch.UnsetSession();
        }

        // Clear all advanced search parameters
        protected void ResetAdvancedSearchParms() {
            ID.AdvancedSearch.UnsetSession();
            COD.AdvancedSearch.UnsetSession();
            DES.AdvancedSearch.UnsetSession();
            CMD.AdvancedSearch.UnsetSession();
            FRM.AdvancedSearch.UnsetSession();
            LBL.AdvancedSearch.UnsetSession();
            COR.AdvancedSearch.UnsetSession();
            ETAPA.AdvancedSearch.UnsetSession();
            QRCODE2.AdvancedSearch.UnsetSession();
        }

        // Restore all search parameters
        protected void RestoreSearchParms() {
            RestoreSearch = true;

            // Restore basic search values
            BasicSearch.Load();

            // Restore advanced search values
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

        // Set up sort parameters
        protected void SetupSortOrder() {
            // Load default Sorting Order
            if (Command != "json") {
                string defaultSort = ""; // Set up default sort
                if (Empty(SessionOrderBy) && !Empty(defaultSort))
                    SessionOrderBy = defaultSort;
            }

            // Check for "order" parameter
            if (Get("order", out StringValues sv)) {
                CurrentOrder = sv.ToString();
                CurrentOrderType = Get("ordertype");
                UpdateSort(ID); // ID
                UpdateSort(COD); // COD
                UpdateSort(DES); // DES
                UpdateSort(CMD); // CMD
                UpdateSort(FRM); // FRM
                UpdateSort(LBL); // LBL
                UpdateSort(COR); // COR
                UpdateSort(ETAPA); // ETAPA
                UpdateSort(QRCODE2); // QRCODE2
                StartRecordNumber = 1; // Reset start position
            }

            // Update field sort
            UpdateFieldSort();
        }

        /// <summary>
        /// Reset command
        /// cmd=reset (Reset search parameters)
        /// cmd=resetall (Reset search and master/detail parameters)
        /// cmd=resetsort (Reset sort parameters)
        /// </summary>
        protected void ResetCommand() {
            // Get reset cmd
            if (Command.ToLower().StartsWith("reset")) {
                // Reset search criteria
                if (SameText(Command, "reset") || SameText(Command, "resetall"))
                    ResetSearchParms();

                // Reset (clear) sorting order
                if (SameText(Command, "resetsort")) {
                    string orderBy = "";
                    SessionOrderBy = orderBy;
                    ID.Sort = "";
                    COD.Sort = "";
                    DES.Sort = "";
                    CMD.Sort = "";
                    FRM.Sort = "";
                    LBL.Sort = "";
                    COR.Sort = "";
                    ETAPA.Sort = "";
                    QRCODE2.Sort = "";
                }

                // Reset start position
                StartRecord = 1;
                StartRecordNumber = StartRecord;
            }
        }

        #pragma warning disable 1998
        // Set up list options
        protected async Task SetupListOptions() {
            ListOption item;

            // "griddelete"
            if (AllowAddDeleteRow) {
                item = ListOptions.Add("griddelete");
                item.CssClass = "text-nowrap";
                item.OnLeft = true;
                item.Visible = false; // Default hidden
            }

            // Add group option item
            item = ListOptions.AddGroupOption();
            item.Body = "";
            item.OnLeft = true;
            item.Visible = false;

            // "view"
            item = ListOptions.Add("view");
            item.CssClass = "text-nowrap";
            item.Visible = true;
            item.OnLeft = true;

            // "edit"
            item = ListOptions.Add("edit");
            item.CssClass = "text-nowrap";
            item.Visible = true;
            item.OnLeft = true;

            // "copy"
            item = ListOptions.Add("copy");
            item.CssClass = "text-nowrap";
            item.Visible = true;
            item.OnLeft = true;

            // List actions
            item = ListOptions.Add("listactions");
            item.CssClass = "text-nowrap";
            item.OnLeft = true;
            item.Visible = false;
            item.ShowInButtonGroup = false;
            item.ShowInDropDown = false;

            // "checkbox"
            item = ListOptions.Add("checkbox");
            item.CssStyle = "white-space: nowrap; text-align: center; vertical-align: middle; margin: 0px;";
            item.Visible = (true || true);
            item.OnLeft = true;
            item.Header = "<div class=\"form-check\"><input type=\"checkbox\" name=\"key\" id=\"key\" class=\"form-check-input\" data-ew-action=\"select-all-keys\"></div>";
            if (item.OnLeft)
                item.MoveTo(0);
            item.ShowInDropDown = false;
            item.ShowInButtonGroup = false;

            // Drop down button for ListOptions
            ListOptions.UseDropDownButton = true;
            ListOptions.DropDownButtonPhrase = "ButtonListOptions";
            ListOptions.UseButtonGroup = false;
            if (ListOptions.UseButtonGroup && IsMobile())
                ListOptions.UseDropDownButton = true;

            //ListOptions.ButtonClass = ""; // Class for button group

            // Call ListOptions Load event
            ListOptionsLoad();
            SetupListOptionsExt();
            ListOptions[ListOptions.GroupOptionName]?.SetVisible(ListOptions.GroupOptionVisible);
        }
        #pragma warning restore 1998

        // Set up list options (extensions)
        protected void SetupListOptionsExt() {
            // Set up list options (to be implemented by extensions)
        }

        // Add "hash" parameter to URL
        public string UrlAddHash(string url, string hash)
        {
            return UseAjaxActions ? url : UrlAddQuery(url, "hash=" + hash);
        }

        // Render list options
        #pragma warning disable 168, 219, 1998

        public async Task RenderListOptions()
        {
            ListOption? listOption;
            bool isVisible = false; // DN
            ListOptions.LoadDefault();

            // Call ListOptions Rendering event
            ListOptionsRendering();

            // Set up row action and key
            if (IsNumeric(RowIndex) && RowType != RowType.View) {
                CurrentForm!.Index = ConvertToInt(RowIndex);
                var actionName = FormActionName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                var oldKeyName = OldKeyName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                var blankRowName = FormBlankRowName.Replace("k_", "k" + ConvertToString(RowIndex) + "_");
                if (!Empty(RowAction))
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + actionName + "\" id=\"" + actionName + "\" value=\"" + RowAction + "\">";
                string oldKey = GetKey(false); // Get from OldValue
                if (!Empty(oldKeyName) && !Empty(oldKey)) {
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + oldKeyName + "\" id=\"" + oldKeyName + "\" value=\"" + HtmlEncode(oldKey) + "\">";
                }
                if (RowAction == "insert" && IsConfirm && EmptyRow())
                    MultiSelectKey += "<input type=\"hidden\" name=\"" + blankRowName + "\" id=\"" + blankRowName + "\" value=\"1\">";
            }

            // "delete"
            if (AllowAddDeleteRow) {
                if (IsGridAdd || IsGridEdit) {
                    var options = ListOptions;
                    options.UseButtonGroup = true; // Use button group for grid delete button
                    listOption = options["griddelete"];
                    listOption?.SetBody("<a class=\"ew-grid-link ew-grid-delete\" title=\"" + Language.Phrase("DeleteLink", true) + "\" data-caption=\"" + Language.Phrase("DeleteLink", true) + "\" data-ew-action=\"delete-grid-row\" data-rowindex=\"" + RowIndex + "\">" + Language.Phrase("DeleteLink") + "</a>");
                }
            }

            // "copy"
            listOption = ListOptions["copy"];
            if (IsInlineAddRow || IsInlineCopyRow) { // Inline Add/Copy
                ListOptions.CustomItem = "copy"; // Show copy column only
                string divClass = listOption?.OnLeft ?? false ? " class=\"text-end\"" : "";
                string insertCaption = Language.Phrase("InsertLink");
                string insertTitle = Language.Phrase("InsertLink", true);
                string cancelCaption = Language.Phrase("CancelLink");
                string cancelTitle = Language.Phrase("CancelLink", true);
                string inlineInsertUrl = AppPath(PageName);
                if (UseAjaxActions) {
                    listOption?.SetBody($@"<div{divClass}>" +
                            $@"<button type=""button"" class=""ew-grid-link ew-inline-insert"" title=""{insertTitle}"" data-caption=""{insertTitle}"" data-ew-action=""inline"" data-action=""insert"" data-key="""" data-url=""{inlineInsertUrl}"">{insertCaption}</button>" +
                            $@"<button type=""button"" class=""ew-grid-link ew-inline-cancel"" title=""{cancelTitle}"" data-caption=""{cancelTitle}"" data-ew-action=""inline"" data-action=""cancel"">{cancelCaption}</button>" +
                        "</div>");
                } else {
                    string cancelurl = AddMasterUrl(PageUrl + "action=cancel");
                    listOption?.SetBody($@"<div{divClass}>" +
                            $@"<button class=""ew-grid-link ew-inline-insert"" title=""{insertTitle}"" data-caption=""{insertTitle}"" form=""fTADS_QRCODElist"" formaction=""{inlineInsertUrl}"">{insertCaption}</button>" +
                            $@"<a class=""ew-grid-link ew-inline-cancel"" title=""{cancelTitle}"" data-caption=""{insertTitle}"" href=""{cancelurl}"">{cancelCaption}</a>" +
                            @"<input type=""hidden"" name=""action"" id=""action"" value=""insert"">" +
                        "</div>");
                }
                return;
            }

            // "edit"
            listOption = ListOptions["edit"];
            if (IsInlineEditRow) { // Inline-Edit
                ListOptions.CustomItem = "edit"; // Show edit column only
                    string divClass = listOption?.OnLeft ?? false ? " class=\"text-end\"" : "";
                    string updateCaption = Language.Phrase("UpdateLink");
                    string updateTitle = Language.Phrase("UpdateLink", true);
                    string cancelCaption = Language.Phrase("CancelLink");
                    string cancelTitle = Language.Phrase("CancelLink", true);
                    string oldKey = HtmlEncode(GetKey(true));
                    string inlineUpdateUrl = HtmlEncode(AppPath(UrlAddHash(PageName, "r" + RowCount + "_" + TableVar)));
                    string cancelurl = AddMasterUrl(PageUrl + "action=cancel");
                    if (UseAjaxActions) {
                        string inlineCancelUrl = InlineEditUrl + "?action=cancel";
                        listOption?.SetBody($@"<div{divClass}>" +
                            $@"<button type=""button"" class=""ew-grid-link ew-inline-update"" title=""{updateTitle}"" data-caption=""{updateTitle}"" data-ew-action=""inline"" data-action=""update"" data-key=""{oldKey}"" data-url=""{inlineUpdateUrl}"">{updateCaption}</button>" +
                            $@"<button type=""button"" class=""ew-grid-link ew-inline-cancel"" title=""{cancelTitle}"" data-caption=""{cancelTitle}"" data-ew-action=""inline"" data-action=""cancel"" data-key=""{oldKey}"" data-url=""{inlineCancelUrl}"">{cancelCaption}</button>" +
                        "</div>");
                    } else {
                        listOption?.SetBody($@"<div{divClass}>" +
                            $@"<button class=""ew-grid-link ew-inline-update"" title=""{updateTitle}"" data-caption=""{updateTitle}"" form=""fTADS_QRCODElist"" formaction=""{inlineUpdateUrl}"">{updateCaption}</button>" +
                            $@"<a class=""ew-grid-link ew-inline-cancel"" title=""{cancelTitle}"" data-caption=""{updateTitle}"" href=""{cancelurl}"">{cancelCaption}</a>" +
                            @"<input type=""hidden"" name=""action"" id=""action"" value=""update"">" +
                        "</div>");
                    }
                listOption?.AddBody("<input type=\"hidden\" name=\"" + OldKeyName + "\" id=\"" + OldKeyName + "\" value=\"" + HtmlEncode(ID.CurrentValue) + "\">");
                return;
            }

            // "view"
            listOption = ListOptions["view"];
            string viewcaption = Language.Phrase("ViewLink", true);
            isVisible = true;
            if (isVisible) {
                if (ModalView && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-table=""TADS_QRCODE"" data-caption=""{viewcaption}"" data-ew-action=""modal"" data-action=""view"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(ViewUrl)) + "\" data-btn=\"null\">" + Language.Phrase("ViewLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-view"" title=""{viewcaption}"" data-caption=""{viewcaption}"" href=""" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // "edit"
            listOption = ListOptions["edit"];
            string editcaption = Language.Phrase("EditLink", true);
            isVisible = true;
            if (isVisible) {
                if (ModalEdit && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-edit"" title=""{editcaption}"" data-table=""TADS_QRCODE"" data-caption=""{editcaption}"" data-ew-action=""modal"" data-action=""edit"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(EditUrl)) + "\" data-btn=\"SaveBtn\">" + Language.Phrase("EditLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-edit"" title=""{editcaption}"" data-caption=""{editcaption}"" href=""" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("EditLink") + "</a>");
                string inlineEditCaption = Language.Phrase("InlineEditLink");
                string inlineEditTitle = Language.Phrase("InlineEditLink", true);
                if (UseAjaxActions)
                    listOption?.AddBody($@"<a class=""ew-row-link ew-inline-edit"" title=""{inlineEditTitle}"" data-caption=""{inlineEditTitle}"" data-ew-action=""inline"" data-action=""edit"" data-key=""" + HtmlEncode(GetKey(true)) + "\" data-url=\"" + HtmlEncode(AppPath(InlineEditUrl)) + "\">" + inlineEditCaption + "</a>");
                else
                    listOption?.AddBody($@"<a class=""ew-row-link ew-inline-edit"" title=""{inlineEditTitle}"" data-caption=""{inlineEditTitle}"" href=""" + HtmlEncode(UrlAddHash(AppPath(InlineEditUrl), "r" + RowCount + "_" + TableVar)) + "\">" + inlineEditCaption + "</a>");
            } else {
                listOption?.Clear();
            }

            // "copy"
            listOption = ListOptions["copy"];
            string copycaption = Language.Phrase("CopyLink", true);
            isVisible = true;
            if (isVisible) {
                if (ModalAdd && !IsMobile())
                    listOption?.SetBody($@"<a class=""ew-row-link ew-copy"" title=""{copycaption}"" data-table=""TADS_QRCODE"" data-caption=""{copycaption}"" data-ew-action=""modal"" data-action=""add"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(CopyUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("CopyLink") + "</a>");
                else
                    listOption?.SetBody($@"<a class=""ew-row-link ew-copy"" title=""{copycaption}"" data-caption=""{copycaption}"" href=""" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("CopyLink") + "</a>");
            } else {
                listOption?.Clear();
            }

            // Set up list action buttons
            listOption = ListOptions["listactions"];
            if (listOption != null && !IsExport() && CurrentAction == "") {
                string body = "";
                var links = new List<string>();
                foreach (var (key, act) in ListActions.Items) {
                    if (act.Select == Config.ActionSingle && act.Allowed) {
                        var action = act.Action;
                        string caption = act.Caption;
                        var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon.Replace(" ew-icon", "")) + "\" data-caption=\"" + HtmlTitle(caption) + "\"></i> " : "";
                        string link = "<li><button type=\"button\" class=\"dropdown-item ew-action ew-list-action\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"fTADS_QRCODElist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + " " + caption + "</button></li>";
                        if (!Empty(link)) {
                            links.Add(link);
                            if (Empty(body)) // Setup first button
                                body = "<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlTitle(caption) + "\" data-caption=\"" + HtmlTitle(caption) + "\" data-ew-action=\"submit\" form=\"fTADS_QRCODElist\" data-key=\"" + KeyToJson(true) + "\"" + act.ToDataAttrs() + ">" + icon + caption + "</button>";
                        }
                    }
                }
                if (links.Count > 1) { // More than one buttons, use dropdown
                    body = "<button type=\"button\" class=\"dropdown-toggle btn btn-default ew-actions\" title=\"" + Language.Phrase("ListActionButton", true) + "\" data-bs-toggle=\"dropdown\">" + Language.Phrase("ListActionButton") + "</button>";
                    string content = links.Aggregate("", (result, link) => result + "<li>" + link + "</li>");
                    body += "<ul class=\"dropdown-menu" + (listOption?.OnLeft ?? false ? "" : " dropdown-menu-right") + "\">" + content + "</ul>";
                    body = "<div class=\"btn-group btn-group-sm\">" + body + "</div>";
                }
                if (links.Count > 0)
                    listOption?.SetBody(body);
            }

            // "checkbox"
            listOption = ListOptions["checkbox"];
            listOption?.SetBody("<div class=\"form-check\"><input type=\"checkbox\" id=\"key_m_" + RowCount + "\" name=\"key_m[]\" class=\"form-check-input ew-multi-select\" value=\"" + HtmlEncode(ID.CurrentValue) + "\" data-ew-action=\"select-key\"></div>");
            RenderListOptionsExt();

            // Call ListOptions Rendered event
            ListOptionsRendered();
        }

        // Render list options (extensions)
        protected void RenderListOptionsExt() {
            // Render list options (to be implemented by extensions)
        }

        // Set up other options
        protected void SetupOtherOptions() {
            ListOptions option;
            ListOption item;
            var options = OtherOptions;
            option = options["addedit"];

            // Add
            item = option.Add("add");
            string addTitle = Language.Phrase("AddLink", true);
            if (ModalAdd && !IsMobile())
                item.Body = $@"<a class=""ew-add-edit ew-add"" title=""{addTitle}"" data-table=""TADS_QRCODE"" data-caption=""{addTitle}"" data-ew-action=""modal"" data-action=""add"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-url=\"" + HtmlEncode(AppPath(AddUrl)) + "\" data-btn=\"AddBtn\">" + Language.Phrase("AddLink") + "</a>";
            else
                item.Body = $@"<a class=""ew-add-edit ew-add"" title=""{addTitle}"" data-caption=""{addTitle}"" href=""" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("AddLink") + "</a>";
            item.Visible = AddUrl != "";

            // Inline Add
            item = option.Add("inlineadd");
            string inlineAddTitle = Language.Phrase("InlineAddLink", true);
            if (UseAjaxActions)
                item.Body = $@"<button class=""ew-add-edit ew-inline-add"" title=""{inlineAddTitle}"" data-caption=""{inlineAddTitle}"" data-ew-action=""inline"" data-action=""add"" data-position=""top"" data-url=""" + HtmlEncode(AppPath(InlineAddUrl)) + "\">" + Language.Phrase("InlineAddLink") + "</button>";
            else
                item.Body = $@"<a class=""ew-add-edit ew-inline-add"" title=""{inlineAddTitle}"" data-caption=""{inlineAddTitle}"" href=""" + HtmlEncode(AppPath(InlineAddUrl)) + "\">" + Language.Phrase("InlineAddLink") + "</a>";
            item.Visible = InlineAddUrl != "";
            item = option.Add("gridadd");
            string gridAddTitle = Language.Phrase("GridAddLink", true);
            if (ModalGridAdd && !IsMobile())
                item.Body = $@"<button class=""ew-add-edit ew-grid-add"" title=""{gridAddTitle}"" data-caption=""{gridAddTitle}"" data-ew-action=""modal"" data-ajax=""" + (UseAjaxActions ? "true" : "false") + "\" data-action=\"add\" data-position=\"top\" data-btn=\"AddBtn\" data-url=\"" + HtmlEncode(AppPath(GridAddUrl)) + "\">" + Language.Phrase("GridAddLink") + "</button>";
            else
                item.Body = $@"<a class=""ew-add-edit ew-grid-add"" title=""{gridAddTitle}"" data-caption=""{gridAddTitle}"" href=""" + HtmlEncode(AppPath(GridAddUrl)) + "\">" + Language.Phrase("GridAddLink") + "</a>";
            item.Visible = GridAddUrl != "";

            // Add grid edit
            option = options["addedit"];
            item = option.Add("gridedit");
            string gridEditTitle = Language.Phrase("GridEditLink", true);
            if (ModalGridEdit && !IsMobile())
                item.Body = $@"<a class=""ew-add-edit ew-grid-edit"" title=""{gridEditTitle}"" data-caption=""{gridEditTitle}"" data-ew-action=""modal"" data-btn=""GridSaveLink"" data-url=""" + HtmlEncode(AppPath(GridEditUrl)) + "\">" + Language.Phrase("GridEditLink") + "</a>";
            else
                item.Body = $@"<a class=""ew-add-edit ew-grid-edit"" title=""{gridEditTitle}"" data-caption=""{gridEditTitle}"" href=""" + HtmlEncode(AppPath(GridEditUrl)) + "\">" + Language.Phrase("GridEditLink") + "</a>";
            item.Visible = GridEditUrl != "";
            option = options["action"];

            // Add multi delete
            item = option.Add("multidelete");
            string deleteTitle = Language.Phrase("DeleteSelectedLink", true);
            item.Body = $@"<button type=""button"" class=""ew-action ew-multi-delete"" title=""{deleteTitle}"" data-caption=""{deleteTitle}"" form=""fTADS_QRCODElist""" +
                " data-ew-action=\"" + (UseAjaxActions ? "inline" : "submit") + "\"" +
                (UseAjaxActions ? " data-action=\"delete\"" : "") +
                " data-url=\"" + HtmlEncode(AppPath(MultiDeleteUrl)) + "\"" +
                (InlineDelete ? " data-msg=\"" + HtmlEncode(Language.Phrase("DeleteConfirm")) + "\" data-data='{\"action\":\"delete\"}'" : " data-data='{\"action\":\"show\"}'") +
                ">" + Language.Phrase("DeleteSelectedLink") + "</button>";
            item.Visible = true;

            // Add multi update
            item = option.Add("multiupdate");
            string updateTitle = Language.Phrase("UpdateSelectedLink", true);
            item.Body = $@"<button type=""button"" class=""ew-action ew-multi-update"" title=""{updateTitle}"" data-table=""TADS_QRCODE"" data-caption=""{updateTitle}"" form=""fTADS_QRCODElist"" data-ew-action=""" +
                (ModalUpdate && !IsMobile() ? "modal" : "submit") + "\"" +
                (ModalUpdate && !IsMobile() ? " data-action=\"update\"" : "") +
                (UseAjaxActions ? " data-ajax=\"true\"" : "") +
                " data-url=\"" + HtmlEncode(AppPath(MultiUpdateUrl)) + "\">" + Language.Phrase("UpdateSelectedLink") + "</button>";
            item.Visible = true;

            // Show column list for column visibility
            if (UseColumnVisibility) {
                option = OtherOptions["column"];
                item = option.AddGroupOption();
                item.Body = "";
                item.Visible = UseColumnVisibility;
                CreateColumnOption(option.Add("ID")); // DN
                CreateColumnOption(option.Add("COD")); // DN
                CreateColumnOption(option.Add("DES")); // DN
                CreateColumnOption(option.Add("CMD")); // DN
                CreateColumnOption(option.Add("FRM")); // DN
                CreateColumnOption(option.Add("LBL")); // DN
                CreateColumnOption(option.Add("COR")); // DN
                CreateColumnOption(option.Add("ETAPA")); // DN
                CreateColumnOption(option.Add("QRCODE")); // DN
                CreateColumnOption(option.Add("QRCODE2")); // DN
            }

            // Set up options default
            foreach (var (key, opt) in options) {
                if (key != "column") { // Always use dropdown for column
                    opt.UseDropDownButton = true;
                    opt.UseButtonGroup = true;
                }
                //opt.ButtonClass = ""; // Class for button group
                item = opt.AddGroupOption();
                item.Body = "";
                item.Visible = false;
            }
            options["addedit"].DropDownButtonPhrase = "ButtonAddEdit";
            options["detail"].DropDownButtonPhrase = "ButtonDetails";
            options["action"].DropDownButtonPhrase = "ButtonActions";

            // Filter button
            item = FilterOptions.Add("savecurrentfilter");
            item.Body = "<a class=\"ew-save-filter\" data-form=\"fTADS_QRCODEsrch\" data-ew-action=\"none\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
            item.Visible = true;
            item = FilterOptions.Add("deletefilter");
            item.Body = "<a class=\"ew-delete-filter\" data-form=\"fTADS_QRCODEsrch\" data-ew-action=\"none\">" + Language.Phrase("DeleteFilter") + "</a>";
            item.Visible = true;
            FilterOptions.UseDropDownButton = true;
            FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
            FilterOptions.DropDownButtonPhrase = "Filters";

            // Add group option item
            item = FilterOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;
        }

        // Create new column option // DN
        public void CreateColumnOption(ListOption item)
        {
            var field = FieldByName(item.Name);
            if (field?.Visible ?? false) {
                item.Body = "<button class=\"dropdown-item\">" +
                    "<div class=\"form-check ew-dropdown-checkbox\">" +
                    "<div class=\"form-check-input ew-dropdown-check-input\" data-field=\"" + field.Param + "\"></div>" +
                    "<label class=\"form-check-label ew-dropdown-check-label\">" + field.Caption + "</label></div></button>";
            }
        }

        // Render other options
        public void RenderOtherOptions()
        {
            ListOptions option;
            ListOption? item;
            var options = OtherOptions;
            if (!IsGridAdd && !IsGridEdit && !IsMultiEdit) { // Not grid add/grid edit/multi edit mode
                option = options["action"];

                // Set up list action buttons
                foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Select == Config.ActionMultiple)) {
                    item = option.Add("custom_" + act.Action);
                    string caption = act.Caption;
                    var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon) + "\" data-caption=\"" + HtmlEncode(caption) + "\"></i>" + caption : caption;
                    item.Body = "<<button type=\"button\" class=\"btn btn-default ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" data-ew-action=\"submit\" form=\"fTADS_QRCODElist\"" + act.ToDataAttrs() + ">" + icon + "</button>";
                    item.Visible = act.Allowed;
                }

                // Hide multi edit, grid edit and other options
                if (TotalRecords <= 0) {
                    option = options["addedit"];
                    option?["gridedit"]?.SetVisible(false);
                    option = options["action"];
                    option.HideAllOptions();
                }
            } else {
                // Hide all options first
                foreach (var (key, opt) in options)
                    opt.HideAllOptions();
                if (IsGridAdd) {
                        if (AllowAddDeleteRow) {
                            // Add add blank row
                            option = options["addedit"];
                            option.UseDropDownButton = false;
                            item = option.Add("addblankrow");
                            item.Body = "<a type=\"button\" class=\"ew-add-edit ew-add-blank-row\" title=\"" + Language.Phrase("AddBlankRow", true) + "\" data-caption=\"" + Language.Phrase("AddBlankRow", true) + "\" data-ew-action=\"add-grid-row\">" + Language.Phrase("AddBlankRow") + "</a>";
                            item.Visible = true;
                        }
                    if (!(ModalGridAdd && !IsMobile())) {
                        option = options["action"];
                        option.UseDropDownButton = false;
                        // Add grid insert
                        item = option.Add("gridinsert");
                        item.Body = "<button class=\"ew-action ew-grid-insert\" title=\"" + Language.Phrase("GridInsertLink", true) + "\" data-caption=\"" + Language.Phrase("GridInsertLink", true) + "\" form=\"fTADS_QRCODElist\" formaction=\"" + AppPath(PageName) + "\">" + Language.Phrase("GridInsertLink") + "</button>";
                        // Add grid cancel
                        item = option.Add("gridcancel");
                        item.Body = "<a type=\"button\" class=\"ew-action ew-grid-cancel\" title=\"" + Language.Phrase("GridCancelLink", true) + "\" data-caption=\"" + Language.Phrase("GridCancelLink", true) + "\" href=\"" + HtmlEncode(AppPath(AddMasterUrl(PageUrl + "action=cancel"))) + "\">" + Language.Phrase("GridCancelLink") + "</a>";
                    }
                }
                if (IsGridEdit) {
                        if (AllowAddDeleteRow) {
                            // Add add blank row
                            option = options["addedit"];
                            option.UseDropDownButton = false;
                            item = option.Add("addblankrow");
                            item.Body = "<button class=\"ew-add-edit ew-add-blank-row\" title=\"" + Language.Phrase("AddBlankRow", true) + "\" data-caption=\"" + Language.Phrase("AddBlankRow", true) + "\" data-ew-action=\"add-grid-row\">" + Language.Phrase("AddBlankRow") + "</button>";
                            item.Visible = true;
                        }
                    if (!(ModalGridEdit && !IsMobile())) {
                        option = options["action"];
                        option.UseDropDownButton = false;
                            item = option.Add("gridsave");
                            item.Body = "<button class=\"ew-action ew-grid-save\" title=\"" + Language.Phrase("GridSaveLink", true) + "\" data-caption=\"" + Language.Phrase("GridSaveLink", true) + "\" form=\"fTADS_QRCODElist\" formaction=\"" + AppPath(PageName) + "\">" + Language.Phrase("GridSaveLink") + "</button>";
                            item = option.Add("gridcancel");
                            item.Body = "<a class=\"ew-action ew-grid-cancel\" title=\"" + Language.Phrase("GridCancelLink", true) + "\" data-caption=\"" + Language.Phrase("GridCancelLink", true) + "\" href=\"" + HtmlEncode(AppPath(AddMasterUrl(PageUrl + "action=cancel"))) + "\">" + Language.Phrase("GridCancelLink") + "</a>";
                    }
                }
            }
        }

        // Process list action
        public async Task<IActionResult> ProcessListAction()
        {
            string filter = GetFilterFromRecordKeys();
            string userAction = Post("action");
            if (filter != "" && userAction != "") {
                // Check permission first
                string actionCaption = userAction;
                foreach (var (key, act) in ListActions.Items) {
                    if (SameString(key, userAction)) {
                        actionCaption = act.Caption;
                        if (CustomActions.ContainsKey(userAction)) {
                            UserAction = userAction;
                            CurrentAction = "";
                        }
                        if (!act.Allowed) {
                            string errmsg = Language.Phrase("CustomActionNotAllowed").Replace("%s", actionCaption);
                            if (Post("ajax") == userAction) // Ajax
                                return Controller.Content("<p class=\"text-danger\">" + errmsg + "</p>", "text/plain", Encoding.UTF8);
                            else
                                FailureMessage = errmsg;
                            return new EmptyResult();
                        }
                    }
                }
                CurrentFilter = filter;
                string sql = CurrentSql;
                var rsuser = await Connection.GetRowsAsync(sql);
                ActionValue = Post("actionvalue");

                // Call row custom action event
                if (rsuser != null) {
                    if (UseTransaction)
                        Connection.BeginTrans();
                    bool processed = true;
                    SelectedCount = rsuser.Count();
                    SelectedIndex = 0;
                    foreach (var row in rsuser) {
                        SelectedIndex++;
                        processed = RowCustomAction(userAction, row);
                        if (!processed)
                            break;
                    }
                    if (processed) {
                        if (UseTransaction)
                            Connection.CommitTrans(); // Commit the changes
                        if (Empty(SuccessMessage))
                            SuccessMessage = Language.Phrase("CustomActionCompleted").Replace("%s", actionCaption); // Set up success message
                    } else {
                        if (UseTransaction)
                            Connection.RollbackTrans(); // Rollback changes

                        // Set up error message
                        if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                            // Use the message, do nothing
                        } else if (!Empty(CancelMessage)) {
                            FailureMessage = CancelMessage;
                            CancelMessage = "";
                        } else {
                            FailureMessage = Language.Phrase("CustomActionFailed").Replace("%s", actionCaption);
                        }
                    }
                }
                CurrentAction = ""; // Clear action
                if (Post("ajax") == userAction) { // Ajax
                    if (ActionResult != null) // Action result set by Row CustomAction event // DN
                        return ActionResult;
                    string msg = "";
                    if (SuccessMessage != "") {
                        msg = "<p class=\"text-success\">" + SuccessMessage + "</p>";
                        ClearSuccessMessage(); // Clear message
                    }
                    if (FailureMessage != "") {
                        msg = "<p class=\"text-danger\">" + FailureMessage + "</p>";
                        ClearFailureMessage(); // Clear message
                    }
                    if (!Empty(msg))
                        return Controller.Content(msg, "text/plain", Encoding.UTF8);
                }
            }
            return new EmptyResult(); // Not ajax request
        }

        // Set up Grid
        public async Task SetupGrid()
        {
            if (ExportAll && IsExport()) {
                StopRecord = TotalRecords;
            } else {
                // Set the last record to display
                if (TotalRecords > StartRecord + DisplayRecords - 1) {
                    StopRecord = StartRecord + DisplayRecords - 1;
                } else {
                    StopRecord = TotalRecords;
                }
            }

            // Restore number of post back records
            if (CurrentForm != null && (IsConfirm || EventCancelled)) {
                CurrentForm!.ResetIndex(); // DN
                if (CurrentForm!.HasValue(FormKeyCountName) && (IsGridAdd || IsGridEdit || IsConfirm)) {
                    KeyCount = CurrentForm.GetInt(FormKeyCountName);
                    StopRecord = StartRecord + KeyCount - 1;
                }
            }
            if (Recordset != null && Recordset.HasRows) {
                if (!Connection.SelectOffset) { // DN
                    for (int i = 1; i <= StartRecord - 1; i++) { // Move to first record
                        if (await Recordset.ReadAsync())
                            RecordCount++;
                    }
                } else {
                    RecordCount = StartRecord - 1;
                }
            } else if (IsGridAdd && !AllowAddDeleteRow && StopRecord == 0) { // Grid-Add with no records
                StopRecord = GridAddRowCount;
            } else if (IsAdd && TotalRecords == 0) { // Inline-Add with no records
                StopRecord = 1;
            }

            // Initialize aggregate
            RowType = RowType.AggregateInit;
            ResetAttributes();
            await RenderRow();
            if (IsAdd || IsCopy || IsInlineInserted) {
                RowIndex = 0;
                if (UseInfiniteScroll)
                    StopRecord = StartRecord; // Show this record only
            }
            if (IsEdit)
                RowIndex = 1;
            if ((IsGridAdd || IsGridEdit)) // Render template row first
                RowIndex = "$rowindex$";
        }

        // Set up Row
        public async Task SetupRow()
        {
            if (IsGridAdd || IsGridEdit) {
                if (SameString(RowIndex, "$rowindex$")) { // Render template row first
                    await LoadRowValues();

                    // Set row properties
                    ResetAttributes();
                    RowAttrs.Add("data-rowindex", ConvertToString(RowIndex));
                    RowAttrs.Add("id", "r0_TADS_QRCODE");
                    RowAttrs.Add("data-rowtype", ConvertToString((int)RowType.Add));
                    RowAttrs.Add("data-inline", (IsAdd || IsCopy || IsEdit) ? "true" : "false");
                    RowAttrs.AppendClass("ew-template");

                    // Render row
                    RowType = RowType.Add;
                    await RenderRow();

                    // Render list options
                    await RenderListOptions();

                    // Reset record count for template row
                    RecordCount--;
                    return;
                }
            }
            if (CurrentForm != null && (IsGridAdd || IsGridEdit || IsConfirm || IsMultiEdit)) {
                RowIndex = ConvertToInt(RowIndex) + 1;
                CurrentForm!.SetIndex(ConvertToInt(RowIndex));
                if (CurrentForm!.HasValue(FormActionName) && (IsConfirm || EventCancelled))
                    RowAction = CurrentForm.GetValue(FormActionName);
                else if (IsGridAdd)
                    RowAction = "insert";
                else
                    RowAction = "";
            }

            // Set up key count
            KeyCount = ConvertToInt(RowIndex);

            // Init row class and style
            ResetAttributes();
            CssClass = "";
            if (IsCopy && InlineRowCount == 0 && !await LoadRow()) { // Inline copy
                CurrentAction = "add";
            }
            if (IsAdd && InlineRowCount == 0 || IsGridAdd) {
                await LoadRowValues(); // Load default values
                OldKey = "";
                SetKey(OldKey);
            } else if (IsInlineInserted && UseInfiniteScroll) {
                // Nothing to do, just use current values
            } else if (!(IsCopy && InlineRowCount == 0)) {
                await LoadRowValues(Recordset); // Load row values
                if (IsGridEdit || IsMultiEdit) {
                    OldKey = GetKey(true); // Get from CurrentValue
                    SetKey(OldKey);
                }
            }
            RowType = RowType.View; // Render view
            if ((IsAdd || IsCopy) && InlineRowCount == 0 || IsGridAdd) // Add
                RowType = RowType.Add; // Render add
            if (IsGridAdd && EventCancelled && !CurrentForm!.HasValue(FormBlankRowName)) // Insert failed
                await RestoreCurrentRowFormValues(RowIndex); // Restore form values
            if (IsEdit || IsInlineUpdated || IsInlineEditCancelled) { // Inline edit/updated/cancelled
                if (CheckInlineEditKey() && InlineRowCount == 0) {
                    if (IsEdit) { // Inline edit
                        RowAction = "edit";
                        RowType = RowType.Edit; // Render edit
                    } else { // Inline updated
                        RowAction = "";
                        RowType = RowType.View; // Render view
                        RowAttrs.Add("data-oldkey", GetKey()); // Set up old key
                    }
                } else if (UseInfiniteScroll) {
                    RowAction = "hide";
                }
            }
            if (IsGridEdit) { // Grid edit
                if (EventCancelled)
                    await RestoreCurrentRowFormValues(RowIndex); // Restore form values
                if (RowAction == "insert")
                    RowType = RowType.Add; // Render add
                else
                    RowType = RowType.Edit; // Render edit
            }
            if (IsEdit && RowType == RowType.Edit && EventCancelled) { // Update failed
                CurrentForm!.Index = 1;
                RestoreFormValues(); // Restore form values
            }
            if (IsGridEdit && (RowType == RowType.Edit || RowType == RowType.Add) && EventCancelled) // Update failed
                await RestoreCurrentRowFormValues(RowIndex); // Restore form values

            // Inline Add/Copy row (row 0)
            if (RowType == RowType.Add && (IsAdd || IsCopy)) {
                InlineRowCount++;
                RecordCount--; // Reset record count for inline add/copy row
                if (TotalRecords == 0) // Reset stop record if no records
                    StopRecord = 0;
            } else {
                // Inline Edit row
                if (RowType == RowType.Edit && IsEdit)
                    InlineRowCount++;
                RowCount++; // Increment row count
            }

            // Set up row attributes
            RowAttrs.Add("data-rowindex", ConvertToString(tadsQrcodeList.RowCount));
            RowAttrs.Add("data-key", GetKey(true));
            RowAttrs.Add("id", "r" + ConvertToString(tadsQrcodeList.RowCount) + "_TADS_QRCODE");
            RowAttrs.Add("data-rowtype", ConvertToString((int)RowType));
            RowAttrs.AppendClass(tadsQrcodeList.RowCount % 2 != 1 ? "ew-table-alt-row" : "");
            if (IsAdd && tadsQrcodeList.RowType == RowType.Add || IsEdit && tadsQrcodeList.RowType == RowType.Edit) // Inline-Add/Edit row
                RowAttrs.AppendClass("table-active");

            // Render row
            await RenderRow();

            // Render list options
            await RenderListOptions();
        }

        // Confirm page
        public bool ConfirmPage = false; // DN

        #pragma warning disable 1998
        // Get upload files
        public async Task GetUploadFiles()
        {
            // Get upload data
            QRCODE.Upload.Index = CurrentForm.Index;
            if (!await QRCODE.Upload.UploadFile()) // DN
                End(QRCODE.Upload.Message);
        }
        #pragma warning restore 1998

        // Load default values
        protected void LoadDefaultValues() {
        }

        // Load basic search values // DN
        protected void LoadBasicSearchValues() {
            if (Get(Config.TableBasicSearch, out StringValues keyword))
                BasicSearch.Keyword = keyword.ToString();
            if (!Empty(BasicSearch.Keyword) && Empty(Command))
                Command = "search";
            if (Get(Config.TableBasicSearchType, out StringValues type))
                BasicSearch.Type = type.ToString();
        }

        // Load search values for validation // DN
        protected void LoadSearchValues() {
            // Load query builder rules
            string rules = Post("rules");
            if (!Empty(rules) && Empty(Command)) {
                QueryRules = rules;
                Command = "search";
            }

            // ID
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_ID"))
                    ID.AdvancedSearch.SearchValue = Get("x_ID");
                else
                    ID.AdvancedSearch.SearchValue = Get("ID"); // Default Value // DN
            if (!Empty(ID.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_ID"))
                ID.AdvancedSearch.SearchOperator = Get("z_ID");

            // COD
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_COD"))
                    COD.AdvancedSearch.SearchValue = Get("x_COD");
                else
                    COD.AdvancedSearch.SearchValue = Get("COD"); // Default Value // DN
            if (!Empty(COD.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_COD"))
                COD.AdvancedSearch.SearchOperator = Get("z_COD");

            // DES
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_DES"))
                    DES.AdvancedSearch.SearchValue = Get("x_DES");
                else
                    DES.AdvancedSearch.SearchValue = Get("DES"); // Default Value // DN
            if (!Empty(DES.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_DES"))
                DES.AdvancedSearch.SearchOperator = Get("z_DES");

            // CMD
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_CMD"))
                    CMD.AdvancedSearch.SearchValue = Get("x_CMD");
                else
                    CMD.AdvancedSearch.SearchValue = Get("CMD"); // Default Value // DN
            if (!Empty(CMD.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_CMD"))
                CMD.AdvancedSearch.SearchOperator = Get("z_CMD");

            // FRM
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_FRM"))
                    FRM.AdvancedSearch.SearchValue = Get("x_FRM");
                else
                    FRM.AdvancedSearch.SearchValue = Get("FRM"); // Default Value // DN
            if (!Empty(FRM.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_FRM"))
                FRM.AdvancedSearch.SearchOperator = Get("z_FRM");

            // LBL
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_LBL"))
                    LBL.AdvancedSearch.SearchValue = Get("x_LBL");
                else
                    LBL.AdvancedSearch.SearchValue = Get("LBL"); // Default Value // DN
            if (!Empty(LBL.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_LBL"))
                LBL.AdvancedSearch.SearchOperator = Get("z_LBL");

            // COR
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_COR"))
                    COR.AdvancedSearch.SearchValue = Get("x_COR");
                else
                    COR.AdvancedSearch.SearchValue = Get("COR"); // Default Value // DN
            if (!Empty(COR.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_COR"))
                COR.AdvancedSearch.SearchOperator = Get("z_COR");

            // ETAPA
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_ETAPA"))
                    ETAPA.AdvancedSearch.SearchValue = Get("x_ETAPA");
                else
                    ETAPA.AdvancedSearch.SearchValue = Get("ETAPA"); // Default Value // DN
            if (!Empty(ETAPA.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_ETAPA"))
                ETAPA.AdvancedSearch.SearchOperator = Get("z_ETAPA");

            // QRCODE2
            if (!IsAddOrEdit)
                if (Query.ContainsKey("x_QRCODE2"))
                    QRCODE2.AdvancedSearch.SearchValue = Get("x_QRCODE2");
                else
                    QRCODE2.AdvancedSearch.SearchValue = Get("QRCODE2"); // Default Value // DN
            if (!Empty(QRCODE2.AdvancedSearch.SearchValue) && Command == "")
                Command = "search";
            if (Query.ContainsKey("z_QRCODE2"))
                QRCODE2.AdvancedSearch.SearchOperator = Get("z_QRCODE2");
        }

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'ID' before field var 'x_ID'
            val = CurrentForm.HasValue("ID") ? CurrentForm.GetValue("ID") : CurrentForm.GetValue("x_ID");
            if (!ID.IsDetailKey && !IsGridAdd && !IsAdd)
                ID.SetFormValue(val);

            // Check field name 'COD' before field var 'x_COD'
            val = CurrentForm.HasValue("COD") ? CurrentForm.GetValue("COD") : CurrentForm.GetValue("x_COD");
            if (!COD.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("COD") && !CurrentForm.HasValue("x_COD")) // DN
                    COD.Visible = false; // Disable update for API request
                else
                    COD.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_COD"))
                COD.OldValue = CurrentForm.GetValue("o_COD");

            // Check field name 'DES' before field var 'x_DES'
            val = CurrentForm.HasValue("DES") ? CurrentForm.GetValue("DES") : CurrentForm.GetValue("x_DES");
            if (!DES.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("DES") && !CurrentForm.HasValue("x_DES")) // DN
                    DES.Visible = false; // Disable update for API request
                else
                    DES.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_DES"))
                DES.OldValue = CurrentForm.GetValue("o_DES");

            // Check field name 'CMD' before field var 'x_CMD'
            val = CurrentForm.HasValue("CMD") ? CurrentForm.GetValue("CMD") : CurrentForm.GetValue("x_CMD");
            if (!CMD.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("CMD") && !CurrentForm.HasValue("x_CMD")) // DN
                    CMD.Visible = false; // Disable update for API request
                else
                    CMD.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_CMD"))
                CMD.OldValue = CurrentForm.GetValue("o_CMD");

            // Check field name 'FRM' before field var 'x_FRM'
            val = CurrentForm.HasValue("FRM") ? CurrentForm.GetValue("FRM") : CurrentForm.GetValue("x_FRM");
            if (!FRM.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("FRM") && !CurrentForm.HasValue("x_FRM")) // DN
                    FRM.Visible = false; // Disable update for API request
                else
                    FRM.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_FRM"))
                FRM.OldValue = CurrentForm.GetValue("o_FRM");

            // Check field name 'LBL' before field var 'x_LBL'
            val = CurrentForm.HasValue("LBL") ? CurrentForm.GetValue("LBL") : CurrentForm.GetValue("x_LBL");
            if (!LBL.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("LBL") && !CurrentForm.HasValue("x_LBL")) // DN
                    LBL.Visible = false; // Disable update for API request
                else
                    LBL.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_LBL"))
                LBL.OldValue = CurrentForm.GetValue("o_LBL");

            // Check field name 'COR' before field var 'x_COR'
            val = CurrentForm.HasValue("COR") ? CurrentForm.GetValue("COR") : CurrentForm.GetValue("x_COR");
            if (!COR.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("COR") && !CurrentForm.HasValue("x_COR")) // DN
                    COR.Visible = false; // Disable update for API request
                else
                    COR.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_COR"))
                COR.OldValue = CurrentForm.GetValue("o_COR");

            // Check field name 'ETAPA' before field var 'x_ETAPA'
            val = CurrentForm.HasValue("ETAPA") ? CurrentForm.GetValue("ETAPA") : CurrentForm.GetValue("x_ETAPA");
            if (!ETAPA.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ETAPA") && !CurrentForm.HasValue("x_ETAPA")) // DN
                    ETAPA.Visible = false; // Disable update for API request
                else
                    ETAPA.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_ETAPA"))
                ETAPA.OldValue = CurrentForm.GetValue("o_ETAPA");

            // Check field name 'QRCODE2' before field var 'x_QRCODE2'
            val = CurrentForm.HasValue("QRCODE2") ? CurrentForm.GetValue("QRCODE2") : CurrentForm.GetValue("x_QRCODE2");
            if (!QRCODE2.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("QRCODE2") && !CurrentForm.HasValue("x_QRCODE2")) // DN
                    QRCODE2.Visible = false; // Disable update for API request
                else
                    QRCODE2.SetFormValue(val);
            }
            if (CurrentForm.HasValue("o_QRCODE2"))
                QRCODE2.OldValue = CurrentForm.GetValue("o_QRCODE2");
            await GetUploadFiles(); // Get upload files
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            if (!IsGridAdd && !IsAdd)
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
                    if (!EventCancelled)
                        HashValue = GetRowHash(row); // Get hash value for record
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

        #pragma warning disable 618, 1998
        // Load old record
        protected async Task<Dictionary<string, object>?> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? cnn = null) {
            // Load old record
            Dictionary<string, object>? row = null;
            bool validKey = !Empty(OldKey);
            if (validKey) {
                SetKey(OldKey);
                CurrentFilter = GetRecordFilter();
                string sql = CurrentSql;
                try {
                    row = await (cnn ?? Connection).GetRowAsync(sql);
                } catch {
                    row = null;
                }
            }
            await LoadRowValues(row); // Load row values
            return row;
        }
        #pragma warning restore 618, 1998

        #pragma warning disable 1998
        // Render row values based on field settings
        public async Task RenderRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

            // ID

            // COD

            // DES

            // CMD

            // FRM

            // LBL

            // COR

            // ETAPA

            // QRCODE

            // QRCODE2

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
            } else if (RowType == RowType.Add) {
                // ID

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
                QRCODE.Upload.DbValue = DbNullValue;
                if (IsNumeric(RowIndex) && !EventCancelled)
                    await RenderUploadField(QRCODE, ConvertToInt(RowIndex));

                // QRCODE2
                QRCODE2.SetupEditAttributes();
                QRCODE2.EditValue = QRCODE2.CurrentValue; // DN
                QRCODE2.PlaceHolder = RemoveHtml(QRCODE2.Caption);

                // Add refer script

                // ID
                ID.HrefValue = "";

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
            } else if (RowType == RowType.Edit) {
                // ID
                ID.SetupEditAttributes();
                ID.EditValue = ID.CurrentValue;
                ID.ViewCustomAttributes = "";

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
                if (IsNumeric(RowIndex) && !EventCancelled)
                    await RenderUploadField(QRCODE, ConvertToInt(RowIndex));

                // QRCODE2
                QRCODE2.SetupEditAttributes();
                QRCODE2.EditValue = QRCODE2.CurrentValue; // DN
                QRCODE2.PlaceHolder = RemoveHtml(QRCODE2.Caption);

                // Edit refer script

                // ID
                ID.HrefValue = "";

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

        // Validate search
        protected bool ValidateSearch() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;

            // Return validate result
            bool validateSearch = !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateSearch = validateSearch && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateSearch;
        }

        #pragma warning disable 1998
        // Validate form
        protected async Task<bool> ValidateForm() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            if (ID.Required) {
                if (!ID.IsDetailKey && Empty(ID.FormValue)) {
                    ID.AddErrorMessage(ConvertToString(ID.RequiredErrorMessage).Replace("%s", ID.Caption));
                }
            }
            if (COD.Required) {
                if (!COD.IsDetailKey && Empty(COD.FormValue)) {
                    COD.AddErrorMessage(ConvertToString(COD.RequiredErrorMessage).Replace("%s", COD.Caption));
                }
            }
            if (DES.Required) {
                if (!DES.IsDetailKey && Empty(DES.FormValue)) {
                    DES.AddErrorMessage(ConvertToString(DES.RequiredErrorMessage).Replace("%s", DES.Caption));
                }
            }
            if (CMD.Required) {
                if (!CMD.IsDetailKey && Empty(CMD.FormValue)) {
                    CMD.AddErrorMessage(ConvertToString(CMD.RequiredErrorMessage).Replace("%s", CMD.Caption));
                }
            }
            if (FRM.Required) {
                if (!FRM.IsDetailKey && Empty(FRM.FormValue)) {
                    FRM.AddErrorMessage(ConvertToString(FRM.RequiredErrorMessage).Replace("%s", FRM.Caption));
                }
            }
            if (LBL.Required) {
                if (!LBL.IsDetailKey && Empty(LBL.FormValue)) {
                    LBL.AddErrorMessage(ConvertToString(LBL.RequiredErrorMessage).Replace("%s", LBL.Caption));
                }
            }
            if (COR.Required) {
                if (!COR.IsDetailKey && Empty(COR.FormValue)) {
                    COR.AddErrorMessage(ConvertToString(COR.RequiredErrorMessage).Replace("%s", COR.Caption));
                }
            }
            if (ETAPA.Required) {
                if (!ETAPA.IsDetailKey && Empty(ETAPA.FormValue)) {
                    ETAPA.AddErrorMessage(ConvertToString(ETAPA.RequiredErrorMessage).Replace("%s", ETAPA.Caption));
                }
            }
            if (QRCODE.Required) {
                if (QRCODE.Upload.FileName == "" && !QRCODE.Upload.KeepFile) {
                    QRCODE.AddErrorMessage(ConvertToString(QRCODE.RequiredErrorMessage).Replace("%s", QRCODE.Caption));
                }
            }
            if (QRCODE2.Required) {
                if (!QRCODE2.IsDetailKey && Empty(QRCODE2.FormValue)) {
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

        // Delete records (based on current filter)
        protected async Task<JsonBoolResult> DeleteRows() { // DN
            List<Dictionary<string, object>>? rsold = null;
            bool result = true;
            try {
                string sql = CurrentSql;
                using var rs = await Connection.GetDataReaderAsync(sql);
                if (rs == null) {
                    return JsonBoolResult.FalseResult;
                } else if (!rs.HasRows) {
                    FailureMessage = Language.Phrase("NoRecord"); // No record found
                    return JsonBoolResult.FalseResult;
                } else { // Clone old rows
                    rsold = await Connection.GetRowsAsync(rs);
                }
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }
            List<string> successKeys = new (), failKeys = new ();
            try {
                // Call Row Deleting event
                if (result)
                    result = rsold.All(row => RowDeleting(row));
                if (result) {
                    foreach (var row in rsold) {
                        try {
                            result = await DeleteAsync(row) > 0;
                        } catch (Exception e) {
                            if (Config.Debug)
                                throw;
                            FailureMessage = e.Message; // Set up error message
                            result = false;
                        }
                        if (!result) {
                            if (UseTransaction) {
                                successKeys.Clear();
                                break;
                            }
                            failKeys.Add(GetKey(row)); // DN
                        } else {
                            if (Config.DeleteUploadFiles)
                                DeleteUploadedFiles(row);
                            RowDeleted(row);
                            successKeys.Add(GetKey(row)); // DN
                        }
                    }
                }
                result = successKeys.Count > 0;
                if (!result) {
                    // Set up error message
                    if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                        // Use the message, do nothing
                    } else if (!Empty(CancelMessage)) {
                        FailureMessage = CancelMessage;
                        CancelMessage = "";
                    } else {
                        FailureMessage = Language.Phrase("DeleteCancelled");
                    }
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                result = false;
            }

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                var rows = await GetRecordsFromRecordset(rsold);
                string table = TableVar;
                d.Add(table, RouteValues.Count > 2 && rows.Count == 1 ? rows[0] : rows); // If single-delete, route values are controller/action/id (count > 2)
                d.Add("action", Config.ApiDeleteAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, true);
            }
            return new JsonBoolResult(d, result);
        }

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
            COD.SetDbValue(rsnew, COD.CurrentValue, COD.ReadOnly);

            // DES
            DES.SetDbValue(rsnew, DES.CurrentValue, DES.ReadOnly);

            // CMD
            CMD.SetDbValue(rsnew, CMD.CurrentValue, CMD.ReadOnly);

            // FRM
            FRM.SetDbValue(rsnew, FRM.CurrentValue, FRM.ReadOnly);

            // LBL
            LBL.SetDbValue(rsnew, LBL.CurrentValue, LBL.ReadOnly);

            // COR
            COR.SetDbValue(rsnew, COR.CurrentValue, COR.ReadOnly);

            // ETAPA
            ETAPA.SetDbValue(rsnew, ETAPA.CurrentValue, ETAPA.ReadOnly);

            // QRCODE
            if (QRCODE.Visible && !QRCODE.ReadOnly && !QRCODE.Upload.KeepFile) {
                rsnew["QRCODE"] = new SqlBinaryParameter(QRCODE.Upload.Value);
            }

            // QRCODE2
            QRCODE2.SetDbValue(rsnew, QRCODE2.CurrentValue, QRCODE2.ReadOnly);

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

        // Load row hash
        protected async Task LoadRowHash() {
            string filter = GetRecordFilter();

            // Load SQL based on filter
            CurrentFilter = filter;
            string sql = CurrentSql;
            try {
                var row = await Connection.GetRowAsync(sql);
                if (row != null) {
                    HashValue = GetRowHash(row);
                } else {
                    HashValue = "";
                }
            } catch {
                if (Config.Debug)
                    throw;
                HashValue = "";
            }
        }

        // Get Row Hash
        public string GetRowHash(Dictionary<string, object>? row)
        {
            if (row == null)
                return "";
            string hash = "";
            hash += GetFieldHash(row["COD"], DataType.String); // COD
            hash += GetFieldHash(row["DES"], DataType.String); // DES
            hash += GetFieldHash(row["CMD"], DataType.String); // CMD
            hash += GetFieldHash(row["FRM"], DataType.String); // FRM
            hash += GetFieldHash(row["LBL"], DataType.String); // LBL
            hash += GetFieldHash(row["COR"], DataType.String); // COR
            hash += GetFieldHash(row["ETAPA"], DataType.String); // ETAPA
            hash += GetFieldHash(row["QRCODE"], DataType.Blob); // QRCODE
            hash += GetFieldHash(row["QRCODE2"], DataType.Memo); // QRCODE2
            return hash;
        }

        // Get Row Hash
        public string GetRowHash(DbDataReader? dr)
        {
            if (dr == null)
                return "";
            var row = new Dictionary<string, object>();
            row.Add("COD", dr["COD"]); // COD
            row.Add("DES", dr["DES"]); // DES
            row.Add("CMD", dr["CMD"]); // CMD
            row.Add("FRM", dr["FRM"]); // FRM
            row.Add("LBL", dr["LBL"]); // LBL
            row.Add("COR", dr["COR"]); // COR
            row.Add("ETAPA", dr["ETAPA"]); // ETAPA
            row.Add("QRCODE", dr["QRCODE"]); // QRCODE
            row.Add("QRCODE2", dr["QRCODE2"]); // QRCODE2
            return GetRowHash(row);
        }

        // Add record
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> AddRow(Dictionary<string, object>? rsold = null) { // DN
            bool result = false;

            // Set new row
            Dictionary<string, object> rsnew = new ();
            try {
                // COD
                COD.SetDbValue(rsnew, COD.CurrentValue);

                // DES
                DES.SetDbValue(rsnew, DES.CurrentValue);

                // CMD
                CMD.SetDbValue(rsnew, CMD.CurrentValue);

                // FRM
                FRM.SetDbValue(rsnew, FRM.CurrentValue);

                // LBL
                LBL.SetDbValue(rsnew, LBL.CurrentValue);

                // COR
                COR.SetDbValue(rsnew, COR.CurrentValue);

                // ETAPA
                ETAPA.SetDbValue(rsnew, ETAPA.CurrentValue);

                // QRCODE
                if (QRCODE.Visible && !QRCODE.Upload.KeepFile) {
                    rsnew["QRCODE"] = new SqlBinaryParameter(QRCODE.Upload.Value);
                }

                // QRCODE2
                QRCODE2.SetDbValue(rsnew, QRCODE2.CurrentValue);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Update current values
            SetCurrentValues(rsnew);

            // Load db values from rsold
            LoadDbValues(rsold);

            // Call Row Inserting event
            bool insertRow = RowInserting(rsold, rsnew);
            if (insertRow) {
                try {
                    result = ConvertToBool(await InsertAsync(rsnew));
                    rsnew["ID"] = ID.CurrentValue!;
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    result = false;
                }
            } else {
                if (SuccessMessage != "" || FailureMessage != "") {
                    // Use the message, do nothing
                } else if (CancelMessage != "") {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("InsertCancelled");
                }
                result = false;
            }

            // Call Row Inserted event
            if (result)
                RowInserted(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiAddAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, result);
            }
            return new JsonBoolResult(d, result);
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

        // Get export HTML tag
        protected string GetExportTag(string type, bool custom = false) {
            string exportUrl = AppPath(CurrentPageName()); // DN
            if (type == "print" || custom) { // Printer friendly / custom export
                exportUrl += "?export=" + type + (custom ? "&amp;custom=1" : "");
            } else {
                exportUrl = AppPath(Config.ApiUrl + Config.ApiExportAction + "/" + type + "/" + TableVar);
            }
            if (SameText(type, "excel")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" form=\"fTADS_QRCODElist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"excel\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToExcel") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
            } else if (SameText(type, "word")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" form=\"fTADS_QRCODElist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"word\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToWord") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) + "\">" + Language.Phrase("ExportToWord") + "</a>";
            } else if (SameText(type, "pdf")) {
                if (custom)
                    return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" form=\"fTADS_QRCODElist\" data-url=\"" + exportUrl + "\" data-ew-action=\"export\" data-export=\"pdf\" data-custom=\"true\" data-export-selected=\"false\">" + Language.Phrase("ExportToPDF") + "</button>";
                else
                    return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPdf", true)) + "\">" + Language.Phrase("ExportToPDF") + "</a>";
            } else if (SameText(type, "html")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-html\" title=\"" + HtmlEncode(Language.Phrase("ExportToHtml", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToHtml", true)) + "\">" + Language.Phrase("ExportToHtml") + "</a>";
            } else if (SameText(type, "xml")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-xml\" title=\"" + HtmlEncode(Language.Phrase("ExportToXml", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToXml", true)) + "\">" + Language.Phrase("ExportToXml") + "</a>";
            } else if (SameText(type, "csv")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-csv\" title=\"" + HtmlEncode(Language.Phrase("ExportToCsv", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToCsv", true)) + "\">" + Language.Phrase("ExportToCsv") + "</a>";
            } else if (SameText(type, "email")) {
                string url = custom ? " data-url=\"" + exportUrl + "\"" : "";
                return "<button type=\"button\" class=\"btn btn-default ew-export-link ew-email\" title=\"" + Language.Phrase("ExportToEmail", true) + "\" data-caption=\"" + Language.Phrase("ExportToEmail", true) + "\" form=\"fTADS_QRCODElist\" data-ew-action=\"email\" data-custom=\"false\" data-hdr=\"" + Language.Phrase("ExportToEmail", true) + "\" data-export-selected=\"false\"" + url + ">" + Language.Phrase("ExportToEmail") + "</button>";
            } else if (SameText(type, "print")) {
                return "<a href=\"" + exportUrl + "\" class=\"btn btn-default ew-export-link ew-print\" title=\"" + HtmlEncode(Language.Phrase("PrinterFriendly", true)) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("PrinterFriendly", true)) + "\">" + Language.Phrase("PrinterFriendly") + "</a>";
            }
            return "";
        }

        // Set up export options
        protected void SetupExportOptions() {
            ListOption item;

            // Printer friendly
            item = ExportOptions.Add("print");
            item.Body = GetExportTag("print");
            item.Visible = true;

            // Export to Excel
            item = ExportOptions.Add("excel");
            item.Body = GetExportTag("excel");
            item.Visible = true;

            // Export to Word
            item = ExportOptions.Add("word");
            item.Body = GetExportTag("word");
            item.Visible = true;

            // Export to HTML
            item = ExportOptions.Add("html");
            item.Body = GetExportTag("html");
            item.Visible = true;

            // Export to XML
            item = ExportOptions.Add("xml");
            item.Body = GetExportTag("xml");
            item.Visible = true;

            // Export to CSV
            item = ExportOptions.Add("csv");
            item.Body = GetExportTag("csv");
            item.Visible = true;

            // Export to PDF
            item = ExportOptions.Add("pdf");
            item.Body = GetExportTag("pdf");
            item.Visible = true;

            // Export to Email
            item = ExportOptions.Add("email");
            item.Body = GetExportTag("email");
            item.Visible = true;

            // Drop down button for export
            ExportOptions.UseButtonGroup = true;
            ExportOptions.UseDropDownButton = true;
            if (ExportOptions.UseButtonGroup && IsMobile())
                ExportOptions.UseDropDownButton = true;
            ExportOptions.DropDownButtonPhrase = "ButtonExport";

            // Add group option item
            item = ExportOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;
        }

        // Set up search options
        protected void SetupSearchOptions() {
            ListOption item;

            // Search button
            item = SearchOptions.Add("searchtoggle");
            var searchToggleClass = !Empty(SearchWhere) ? " active" : " active";
            item.Body = "<a class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" role=\"button\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-ew-action=\"search-toggle\" data-form=\"fTADS_QRCODEsrch\" aria-pressed=\"" + (searchToggleClass == " active" ? "true" : "false") + "\">" + Language.Phrase("SearchLink") + "</a>";
            item.Visible = true;

            // Show all button
            item = SearchOptions.Add("showall");
            if (UseCustomTemplate || !UseAjaxActions)
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-show-all\" role=\"button\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" data-ew-action=\"refresh\" data-url=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
            item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

            // Advanced search button
            item = SearchOptions.Add("advancedsearch");
            if (ModalSearch && !IsMobile())
                item.Body = "<a class=\"btn btn-default ew-advanced-search\" title=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-table=\"TADS_QRCODE\" data-caption=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-ew-action=\"modal\" data-url=\"" + AppPath("TadsQrcodeSearch") + "\" data-btn=\"SearchBtn\">" + Language.Phrase("AdvancedSearch", false) + "</a>";
            else
                item.Body = "<a class=\"btn btn-default ew-advanced-search\" title=\"" + Language.Phrase("AdvancedSearch", true) + "\" data-caption=\"" + Language.Phrase("AdvancedSearch", true) + "\" href=\"" + AppPath("TadsQrcodeSearch") + "\">" + Language.Phrase("AdvancedSearch", false) + "</a>";
            item.Visible = true;

            // Button group for search
            SearchOptions.UseDropDownButton = false;
            SearchOptions.UseButtonGroup = true;
            SearchOptions.DropDownButtonPhrase = "ButtonSearch";

            // Add group option item
            item = SearchOptions.AddGroupOption();
            item.Body = "";
            item.Visible = false;

            // Hide search options
            if (IsExport() || !Empty(CurrentAction) && CurrentAction != "search")
                SearchOptions.HideAllOptions();
        }

        // Check if any search fields
        public bool HasSearchFields()
        {
            return true;
        }

        // Render search options
        protected void RenderSearchOptions()
        {
            if (!HasSearchFields() && SearchOptions["searchtoggle"] is ListOption opt)
                opt.Visible = false;
        }

        #pragma warning disable 168

        /// <summary>
        /// Export data
        /// </summary>
        public async Task ExportData(dynamic? doc)
        {
            // Load recordset // DN
            DbDataReader? dr = null;
            if (TotalRecords < 0)
                TotalRecords = await ListRecordCountAsync();
            StartRecord = 1;

            // Export all
            if (ExportAll) {
                DisplayRecords = TotalRecords;
                StopRecord = TotalRecords;
            } else { // Export one page only
                SetupStartRecord(); // Set up start record position
                // Set the last record to display
                if (DisplayRecords < 0) {
                    StopRecord = TotalRecords;
                } else {
                    StopRecord = StartRecord + DisplayRecords - 1;
                }
            }
            CloseRecordset(); // DN
            dr = await LoadRecordset(StartRecord - 1, (DisplayRecords <= 0) ? TotalRecords : DisplayRecords); // DN
            if (doc == null) { // DN
                RemoveHeader("Content-Type"); // Remove header
                RemoveHeader("Content-Disposition");
                FailureMessage = Language.Phrase("ExportClassNotFound"); // Export class not found
                return;
            }

            // Call Page Exporting server event
            doc.ExportCustom = !PageExporting(ref doc);

            // Page header
            string header = PageHeader;
            PageDataRendering(ref header);
            doc.Text.Append(header);

            // Export
            if (dr != null)
                await ExportDocument(doc, dr, StartRecord, StopRecord, "");

            // Page footer
            string footer = PageFooter;
            PageDataRendered(ref footer);
            doc.Text.Append(footer);

            // Close recordset
            using (dr) {} // Dispose

            // Export header and footer
            await doc.ExportHeaderAndFooter();

            // Call Page Exported server event
            PageExported(doc);
        }
        #pragma warning restore 168

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            url = Regex.Replace(url, @"\?cmd=reset(all)?$", ""); // Remove cmd=reset / cmd=resetall
            breadcrumb.Add("list", TableVar, url, "", TableVar, true);
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

        // Set up starting record parameters
        public void SetupStartRecord()
        {
            // Exit if DisplayRecords = 0
            if (DisplayRecords == 0)
                return;
            string pageNo = Get(Config.TablePageNumber);
            string startRec = Get(Config.TableStartRec);
            bool infiniteScroll = false;
            infiniteScroll = Param<bool>("infinitescroll");
            if (!Empty(pageNo) && IsNumeric(pageNo)) {
                int page = ConvertToInt(pageNo);
                StartRecord = (page - 1) * DisplayRecords + 1;
                if (StartRecord <= 0)
                    StartRecord = 1;
                else if (StartRecord >= ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1)
                    StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1;
            } else if (!Empty(startRec) && IsNumeric(startRec)) {
                StartRecord = ConvertToInt(startRec);
            } else if (!infiniteScroll) {
                StartRecord = StartRecordNumber;
            }

            // Check if correct start record counter
            if (StartRecord <= 0) // Avoid invalid start record counter
                StartRecord = 1; // Reset start record counter
            else if (StartRecord > TotalRecords) // Avoid starting record > total records
                StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
            else if ((StartRecord - 1) % DisplayRecords != 0)
                StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
            if (!infiniteScroll)
                StartRecordNumber = StartRecord;
        }

        // Get page count
        public int PageCount
        {
            get {
                return ConvertToInt(Math.Ceiling((double)TotalRecords / DisplayRecords));
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

        // ListOptions Load event
        public virtual void ListOptionsLoad() {
            // Example:
            //var opt = ListOptions.Add("new");
            //opt.Header = "xxx";
            //opt.OnLeft = true; // Link on left
            //opt.MoveTo(0); // Move to first column
        }

        // ListOptions Rendering event
        public virtual void ListOptionsRendering() {
            //xxxGrid.DetailAdd = (...condition...); // Set to true or false conditionally
            //xxxGrid.DetailEdit = (...condition...); // Set to true or false conditionally
            //xxxGrid.DetailView = (...condition...); // Set to true or false conditionally
        }

        // ListOptions Rendered event
        public virtual void ListOptionsRendered() {
            //Example:
            //ListOptions["new"].Body = "xxx";
        }

        // Row Custom Action event
        public virtual bool RowCustomAction(string action, Dictionary<string, object> row) {
            // Return false to abort
            return true;
        }

        // Page Exporting event
        // doc = export document object
        public virtual bool PageExporting(ref dynamic doc) {
            //doc.Text.Append("<p>" + "my header" + "</p>"); // Export header
            //return false; // Return false to skip default export and use Row_Export event
            return true; // Return true to use default export and skip Row_Export event
        }

        // Page Exported event
        // doc = export document object
        public virtual void PageExported(dynamic doc) {
            //doc.Text.Append("my footer"); // Export footer
            //Log("Text: {Text}", doc.Text.ToString());
        }

        // Grid Inserting event
        public virtual bool GridInserting() {
            // Enter your code here
            // To reject grid insert, set return value to false
            return true;
        }

        // Grid Inserted event
        public virtual void GridInserted(List<Dictionary<string, object>> rsnew) {
            //Log("Grid Inserted");
        }

        // Grid Updating event
        public virtual bool GridUpdating(List<Dictionary<string, object>> rsold) {
            // Enter your code here
            // To reject grid update, set return value to false
            return true;
        }

        // Grid Updated event
        public virtual void GridUpdated(List<Dictionary<string, object>> rsold, List<Dictionary<string, object>> rsnew) {
            //Log("Grid Updated");
        }
    } // End page class
} // End Partial class

namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("TadsQrcodeList/{ID?}", Name = "TadsQrcodeList-TADS_QRCODE-list")]
    [Route("Home/TadsQrcodeList/{ID?}", Name = "TadsQrcodeList-TADS_QRCODE-list-2")]
    public async Task<IActionResult> TadsQrcodeList()
    {
        // Create page object
        tadsQrcodeList = new GLOBALS.TadsQrcodeList(this);
        tadsQrcodeList.Cache = _cache;

        // Run the page
        return await tadsQrcodeList.Run();
    }

    // add
    [Route("TadsQrcodeAdd/{ID?}", Name = "TadsQrcodeAdd-TADS_QRCODE-add")]
    [Route("Home/TadsQrcodeAdd/{ID?}", Name = "TadsQrcodeAdd-TADS_QRCODE-add-2")]
    public async Task<IActionResult> TadsQrcodeAdd()
    {
        // Create page object
        tadsQrcodeAdd = new GLOBALS.TadsQrcodeAdd(this);

        // Run the page
        return await tadsQrcodeAdd.Run();
    }

    // view
    [Route("TadsQrcodeView/{ID?}", Name = "TadsQrcodeView-TADS_QRCODE-view")]
    [Route("Home/TadsQrcodeView/{ID?}", Name = "TadsQrcodeView-TADS_QRCODE-view-2")]
    public async Task<IActionResult> TadsQrcodeView()
    {
        // Create page object
        tadsQrcodeView = new GLOBALS.TadsQrcodeView(this);

        // Run the page
        return await tadsQrcodeView.Run();
    }

    // edit
    [Route("TadsQrcodeEdit/{ID?}", Name = "TadsQrcodeEdit-TADS_QRCODE-edit")]
    [Route("Home/TadsQrcodeEdit/{ID?}", Name = "TadsQrcodeEdit-TADS_QRCODE-edit-2")]
    public async Task<IActionResult> TadsQrcodeEdit()
    {
        // Create page object
        tadsQrcodeEdit = new GLOBALS.TadsQrcodeEdit(this);

        // Run the page
        return await tadsQrcodeEdit.Run();
    }

    // update
    [Route("TadsQrcodeUpdate", Name = "TadsQrcodeUpdate-TADS_QRCODE-update")]
    [Route("Home/TadsQrcodeUpdate", Name = "TadsQrcodeUpdate-TADS_QRCODE-update-2")]
    public async Task<IActionResult> TadsQrcodeUpdate()
    {
        // Create page object
        tadsQrcodeUpdate = new GLOBALS.TadsQrcodeUpdate(this);

        // Run the page
        return await tadsQrcodeUpdate.Run();
    }

    // delete
    [Route("TadsQrcodeDelete/{ID?}", Name = "TadsQrcodeDelete-TADS_QRCODE-delete")]
    [Route("Home/TadsQrcodeDelete/{ID?}", Name = "TadsQrcodeDelete-TADS_QRCODE-delete-2")]
    public async Task<IActionResult> TadsQrcodeDelete()
    {
        // Create page object
        tadsQrcodeDelete = new GLOBALS.TadsQrcodeDelete(this);

        // Run the page
        return await tadsQrcodeDelete.Run();
    }

    // search
    [Route("TadsQrcodeSearch", Name = "TadsQrcodeSearch-TADS_QRCODE-search")]
    [Route("Home/TadsQrcodeSearch", Name = "TadsQrcodeSearch-TADS_QRCODE-search-2")]
    public async Task<IActionResult> TadsQrcodeSearch()
    {
        // Create page object
        tadsQrcodeSearch = new GLOBALS.TadsQrcodeSearch(this);

        // Run the page
        return await tadsQrcodeSearch.Run();
    }
}

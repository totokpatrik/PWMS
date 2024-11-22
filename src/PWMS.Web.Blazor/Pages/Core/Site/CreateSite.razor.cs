using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Core.Sites.Models;
using PWMS.Web.Blazor.Services;
using PWMS.Web.Blazor.Services.Core;

namespace PWMS.Web.Blazor.Pages.Core.Site;

public partial class CreateSite
{
    [Inject] ISiteService SiteService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    CreateSiteDto _createSiteDto = new();

    private MudForm _form = new();
    private bool _loading = false;
    private bool success;

    private FluentValueValidator<string> _siteNameValidator = new FluentValueValidator<string>(x => x
        .NotEmpty()
        .Length(1, 20));

    private async Task FormKeyDownAsync(KeyboardEventArgs e)
    {

        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Create();
        }
    }
    protected async Task Create()
    {
        if (_form.IsValid)
        {
            _loading = true;
            await SiteService.CreateAsync(_createSiteDto);
            NavigationManager.NavigateTo("/site");
            _loading = false;
        }
    }
    protected void Cancel()
    {
        NavigationManager.NavigateTo("/site");
    }
}
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PWMS.Application.Addresses.Models;
using PWMS.Web.Blazor.Services;

namespace PWMS.Web.Blazor.Pages.Configuration;

public partial class FormAddress
{
    [Parameter]
    public EventCallback<int> OnFormSubmitted { get; set; }
    [Parameter]
    public string SubmitButtonTitle { get; set; } = "Submit";
    [Parameter]
    public CreateAddressDto? CreateAddressDto { get; set; }
    [Parameter]
    public UpdateAddressDto? UpdateAddressDto { get; set; }
    private MudForm _form = new();
    private bool _loading = false;
    private bool success;

    private FluentValueValidator<string> _addressLineValidator = new FluentValueValidator<string>(x => x
    .NotEmpty()
    .Length(1, 100));

    protected override void OnInitialized()
    {
        _form.Validate();
        base.OnInitialized();
    }
    private async Task FormKeyDownAsync(KeyboardEventArgs e)
    {

        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await SubmitFOrm();
        }
    }

    private async Task SubmitFOrm()
    {
        await _form.Validate();
        if (_form.IsValid)
        {
            _loading = true;

            await Task.Delay(1000);

            _loading = false;

            //await OnFormSubmitted.InvokeAsync();
        }
    }
}
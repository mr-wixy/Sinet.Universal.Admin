using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Integration;
using Volo.Abp.Users;

namespace Sinet.Universal.Admin.RCL.Pages.Authentication.Components;

public partial class Login
{
    private bool _show;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public bool HideLogo { get; set; }

    [Parameter]
    public double Width { get; set; } = 410;

    [Parameter]
    public StringNumber? Elevation { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public EventCallback<string> NameChanged { get; set; }

    [Parameter]
    public string? Pass { get; set; }

    [Parameter]
    public EventCallback<string> PassChanged { get; set; }

    [Parameter]
    public string CreateAccountRoute { get; set; } = $"pages/authentication/register-v1";

    [Parameter]
    public string ForgotPasswordRoute { get; set; } = $"pages/authentication/forgot-password-v1";

    [Parameter]
    public EventCallback<MouseEventArgs> OnLogin { get; set; }

    [Inject]
    [NotNull]
    public IServerInfoAppService? ServerInfoAppService { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var status = await ServerInfoAppService.GetServerStatus();
        await base.OnInitializedAsync();
    }

}


using Sinet.Universal.Admin.RCL.Data.App.User;
using Sinet.Universal.Admin.RCL.Data.App.User.Dto;
using Volo.Abp.Account;
using Volo.Abp.Identity;

namespace Sinet.Universal.Admin.RCL.Pages.App.Role
{
    public partial class List
    {
        [Inject]
        public IIdentityRoleAppService IdentityRoleAppService { get; set; }

        public List<IdentityRoleDto> RoleList {  get; set; }

        public bool? _visible;
        private List<int> _pageSizes = new() { 10, 25, 50, 100 };
        private readonly List<DataTableHeader<IdentityRoleDto>> _headers = new()
        {
            new() { Text = "RoleName", Value = nameof(IdentityRoleDto.Name), CellClass = "" },
            new() { Text = "IsPublic", Value = nameof(IdentityRoleDto.IsPublic) },
            new() { Text = "IsDefault", Value = nameof(IdentityRoleDto.IsDefault) },
            new() { Text = "IsStatic", Value = nameof(IdentityRoleDto.IsStatic) },
            new() { Text = "ACTIONS", Value = "Action", Sortable = false }
        };
        private readonly Dictionary<string, string> _roleIconMap = UserService.GetRoleIconMap();

        protected override async Task OnInitializedAsync()
        {
            var roles = await IdentityRoleAppService.GetAllListAsync();
            RoleList = roles.Items.ToList();
            base.OnInitializedAsync();
        }

        private void NavigateToDetails(Guid id)
        {
            Nav.NavigateTo($"/app/user/view/{id}");
        }

        private void NavigateToEdit(Guid id)
        {
            Nav.NavigateTo($"/app/role/edit/{id}");
        }

        private void AddUserData(UserDto userData)
        {
            //_userPage.UserDatas.Insert(0, userData);
        }
    }
}

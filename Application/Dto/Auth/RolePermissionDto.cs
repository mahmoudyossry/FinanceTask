using System.ComponentModel.DataAnnotations;

namespace Finance.Application.Dto
{
    public class RolePermissionDto : EntityDto
    {
        public long PermissionId { get; set; }

        public string PermissionName { get; set; }
    }
}

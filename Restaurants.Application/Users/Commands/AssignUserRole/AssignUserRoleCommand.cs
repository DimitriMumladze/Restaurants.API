using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEMail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

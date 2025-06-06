using Auth.Models.InModels;
using Auth.Utilities;

namespace Auth.Services.Contract
{
    public interface IAuth_Service
    {
        //AddUser
        Task<ResponseManager> Login(LoginIM loginIM);

        Task<ResponseManager> GetUsers();

        Task<ResponseManager> AddUser(AddUserIM addUserIM);

        Task<ResponseManager> ChangePassword(ChangePasswordIM changePasswordIM);

        Task<ResponseManager> DeleteUser(DeleteUserIM deleteUserIM);
    }
}

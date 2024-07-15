using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Models.Verification;

namespace BringMeBackAPI.Services.Verifications.Interfaces
{
    public interface IVerificationService
    {
        Task<Verification> CreateVerificationAsync(int userId);
        Task<Verification> GetVerificationByIdAsync(int id);
        Task<bool> UseVerificationCodeAsync(int id);
    }
     

}

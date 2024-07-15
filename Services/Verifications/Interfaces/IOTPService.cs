using BringMeBackAPI.Models.Verification;

namespace BringMeBackAPI.Services.Verifications.Interfaces
{
    public interface IOTPService
    {
        Task<OTP> GenerateOTPAsync(int userId);
        Task<OTP> ValidateOTPAsync(int userId, string code);
    }

}

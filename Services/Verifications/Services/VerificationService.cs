using BringMeBack.Data;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Services.Verifications.Interfaces;

namespace BringMeBackAPI.Services.Verifications.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly ApplicationDbContext _context;

        public VerificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Verification> CreateVerificationAsync(int userId)
        {
            var code = GenerateUniqueVerificationCode();
            var verification = new Verification
            {
                UserId = userId,
                VerificationCode = code,
                CreatedAt = DateTime.UtcNow,
                IsUsed = false
            };
            _context.Verifications.Add(verification);
            await _context.SaveChangesAsync();

            // Send the verification code via Email
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                SendVerificationEmail(user.Email, code);
            }

            return verification;
        }

        public async Task<Verification> GetVerificationByIdAsync(int id)
        {
            return await _context.Verifications.FindAsync(id);
        }

        public async Task<bool> UseVerificationCodeAsync(int id)
        {
            var verification = await _context.Verifications.FindAsync(id);
            if (verification == null || verification.IsUsed)
            {
                return false;
            }

            verification.IsUsed = true;
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateUniqueVerificationCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper();
        }

        private void SendVerificationEmail(string email, string code)
        {
            // Integration with Email service goes here
            Console.WriteLine($"Sending Verification Code {code} to email {email}");
        }
    }


}

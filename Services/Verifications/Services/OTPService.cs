using BringMeBack.Data;
using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Services.Verifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Services.Verifications.Services
{
    public class OTPService : IOTPService
    {
        private readonly ApplicationDbContext _context;

        public OTPService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OTP> GenerateOTPAsync(int userId)
        {
            var code = GenerateUniqueCode();
            var otp = new OTP
            {
                UserId = userId,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            _context.OTPs.Add(otp);
            await _context.SaveChangesAsync();

            // Send the OTP via SMS
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                SendOTPViaSMS(user.PhoneNumber, code);
            }

            return otp;
        }

        public async Task<OTP> ValidateOTPAsync(int userId, string code)
        {
            var otp = await _context.OTPs
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Code == code && !o.IsUsed && o.ExpiresAt > DateTime.UtcNow);
            if (otp != null)
            {
                otp.IsUsed = true;
                await _context.SaveChangesAsync();
            }
            return otp;
        }

        private string GenerateUniqueCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        private void SendOTPViaSMS(string phoneNumber, string code)
        {
            // Integration with SMS service goes here
            Console.WriteLine($"Sending OTP {code} to phone number {phoneNumber}");
        }
    }


}

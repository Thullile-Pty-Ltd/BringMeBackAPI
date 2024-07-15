using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Services.Verifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPsController : ControllerBase
    {
        private readonly IOTPService _otpService;

        public OTPsController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<OTP>> GenerateOTP(int userId)
        {
            var otp = await _otpService.GenerateOTPAsync(userId);
            return CreatedAtAction(nameof(ValidateOTP), new { userId = otp.UserId, code = otp.Code }, otp);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<OTP>> ValidateOTP(int userId, string code)
        {
            var otp = await _otpService.ValidateOTPAsync(userId, code);
            if (otp == null)
            {
                return BadRequest();
            }
            return Ok(otp);
        }
    }


}

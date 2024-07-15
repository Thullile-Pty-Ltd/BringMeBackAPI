using BringMeBackAPI.Models.Verification;
using BringMeBackAPI.Services.Verifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationsController : ControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationsController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Verification>> CreateVerification(int userId)
        {
            var verification = await _verificationService.CreateVerificationAsync(userId);
            return CreatedAtAction(nameof(GetVerification), new { id = verification.VerificationId }, verification);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Verification>> GetVerification(int id)
        {
            var verification = await _verificationService.GetVerificationByIdAsync(id);
            if (verification == null)
            {
                return NotFound();
            }
            return verification;
        }

        [HttpPost("{id}/use")]
        public async Task<IActionResult> UseVerification(int id)
        {
            var success = await _verificationService.UseVerificationCodeAsync(id);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }


}

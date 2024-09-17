using Microsoft.AspNetCore.Mvc;
using SecurePDFSign.Contracts;
using SecurePDFSign.Models;

namespace SecurePDFSign.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SignController(
        IPdfSignerService pdfSignerService, 
        ICertificateService certificateService) : ControllerBase
    {
        [HttpPost("sign")]
        public IActionResult SignPdf([FromBody] SignatureRequest request)
        {
            if(string.IsNullOrEmpty(request.Base64Data))
            {
                return BadRequest("No PDF content was provided.");
            }

            var certificate = certificateService.GetCertificateFromStore();
            var signedData = pdfSignerService.SignData(request.Base64Data, certificate);

            return Ok(signedData);
        }

        [HttpPost("verify")]
        public IActionResult VerifyPdfSignature([FromBody] VerificationRequest request)
        {
            if (string.IsNullOrEmpty(request.Base64Data))
            {
                return BadRequest("No PDF content was provided.");
            }

            var certificate = certificateService.GetCertificateFromStore();
            var IsVerified = pdfSignerService.VerifyData(request.Base64Data, request.Base64Signature, certificate);

            return Ok(IsVerified);
        }
    }
}

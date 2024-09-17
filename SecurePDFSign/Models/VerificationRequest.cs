namespace SecurePDFSign.Models
{
    public class VerificationRequest
    {
        public string Base64Data { get; set; }

        public string Base64Signature { get; set; }
    }
}

using iText.Signatures;
using Org.BouncyCastle.Security;
using SecurePDFSign.Contracts;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SecurePDFSign.Services
{
    public class PdfSignerService : IPdfSignerService
    {
        public string SignData(string base64Pdf, X509Certificate2 certificate)
        {
            using(RSA rsa = certificate.GetRSAPrivateKey())
            {
                if (rsa == null)
                {
                    throw new Exception("Private key is not found in the certificate");
                }

                var pdfBytes = Convert.FromBase64String(base64Pdf);
                var signature = rsa.SignData(pdfBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(signature);
            }
        }

        public bool VerifyData(string base64Pdf, string signature, X509Certificate2 certificate)
        {
            using(RSA rsa = certificate.GetRSAPublicKey())
            {
                var pdfBytes = Convert.FromBase64String(base64Pdf);
                var signatureBytes = Convert.FromBase64String(signature);
                return rsa.VerifyData(pdfBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
    }
}

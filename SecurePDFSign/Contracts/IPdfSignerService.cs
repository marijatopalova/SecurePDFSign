using System.Security.Cryptography.X509Certificates;

namespace SecurePDFSign.Contracts
{
    public interface IPdfSignerService
    {
        string SignData(string base64Pdf, X509Certificate2 certificate);

        bool VerifyData(string base64Pdf, string signature, X509Certificate2 certificate);
    }
}

using System.Security.Cryptography.X509Certificates;

namespace SecurePDFSign.Contracts
{
    public interface ICertificateService
    {
        X509Certificate2 GetCertificateFromStore();
    }
}

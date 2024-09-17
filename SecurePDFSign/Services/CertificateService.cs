using SecurePDFSign.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace SecurePDFSign.Services
{
    public class CertificateService : ICertificateService
    {
        public X509Certificate2 GetCertificateFromStore()
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);

            foreach (var item in certificates)
            {
                if (!item.HasPrivateKey)
                    certificates.Remove(item);
            }

            if (certificates.Count > 0) 
            { 
                return certificates[0];
            }
            else
            {
                throw new Exception("Certificate not found.");
            }
        }
    }
}

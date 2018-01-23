using System;
using System.Security.Cryptography.X509Certificates;

namespace JwtSecuring.Infrastructure
{
    public static class CertificateHelper
    {
        public static X509Certificate2 LoadCertificateFromStore()
        {
            var thumbPrint = "FE0ECC720708A7620A5F0C3F56C948DBE8845787";

            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates
                    .Find(X509FindType.FindByThumbprint, thumbPrint, true);
                if (certCollection.Count == 0)
                {
                    throw new Exception("The specified certificate wasn't found");
                }
                return certCollection[0];
            }
        }
    }
}

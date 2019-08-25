using System;
using System.Text;
using System.Security.Cryptography;

namespace EwidOperaty.Tools
{
    public static class SecurityTools
    {
        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes("GISNET*Grzegorz*Gogolewski");

        public static string ProtectString(this string input)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(input), Entropy, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static string UnProtectString(this string encryptedData)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), Entropy, DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }



        }
    }
}

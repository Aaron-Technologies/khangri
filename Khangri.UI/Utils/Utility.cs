using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Khangri.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Khangri.UI.Utils
{
    public interface IUtility
    {
        string DecryptString(string cipherText);
        string EncryptString(string plainText);
       // string GenerateJwt(Claim[] claims, out DateTime validity);
        string GetErrorMessageFromDataSet(DataSet dataSet, int columnCount = 3);
        string GetValueFromTempSuccess(DataSet dataSet, int columnCount = 3);
        void InvalidateLoginCookie(string userId);
        public DataTable GetDataTable();
        void ForceCreateDirectory(string directoryName);
    }

    public class Utility : IUtility
    {
        private readonly MiscSettings _settings;
        private readonly IHttpContextAccessor _contextAccessor;
        // private readonly JwtOptions _jwtOptions;
        //IOptions<JwtOptions> jwtOptions --inside Constructor parameter
        public Utility(IOptions<MiscSettings> settings, IHttpContextAccessor contextAccessor)
        {
            _settings = settings.Value;
            _contextAccessor = contextAccessor;
           // _jwtOptions = jwtOptions?.Value;
        }

        public string GetErrorMessageFromDataSet(DataSet dataSet, int columnCount = 3)
        {
            var msg = String.Empty;
            if (dataSet == null || dataSet.Tables == null || dataSet.Tables.Count == 0)
            {
                return msg;
            }
            var table = dataSet.Tables[0];
            if (table == null || table.Columns.Count < columnCount || table.Rows.Count < 1)
            {
                return msg;
            }
            try
            {
                var status = Convert.ToBoolean(table.Rows[0][0]);
                if (!status)
                {
                    msg = Convert.ToString(table.Rows[0][columnCount - 1]);
                }
                return msg;
            }
            catch
            {
                return msg;
            }

        }

        public string GetValueFromTempSuccess(DataSet dataSet, int columnCount = 3)
        {
            var value = String.Empty;
            if (dataSet == null || dataSet.Tables == null || dataSet.Tables.Count == 0)
            {
                return value;
            }
            var table = dataSet.Tables[0];
            if (table == null || table.Columns.Count < columnCount || table.Rows.Count < 1)
            {
                return value;
            }
            try
            {
                var status = Convert.ToBoolean(table.Rows[0][0]);
                if (status)
                {
                    value = Convert.ToString(table.Rows[0][columnCount - 1]);
                }
                return value;
            }
            catch
            {
                return value;
            }

        }


        public string EncryptString(string plainText)
        {
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_settings.EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(_settings.IV);

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptString(string cipherText)
        {
            // Check arguments.


            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_settings.EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(_settings.IV);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public void InvalidateLoginCookie(string userId)
        {
            var cookie = new CookieOptions()
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true
            };
            _contextAccessor.HttpContext.Response.Cookies.Append(Constants.UserRenewCookieName, this.EncryptString(userId.ToString()), cookie);
        }

        //public string GenerateJwt(Claim[] claims, out DateTime validity)
        //{
        //    var stringToken = String.Empty;
        //    var issuer = _jwtOptions.Issuer;
        //    var audience = _jwtOptions.Audience;
        //    var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        //    validity = DateTime.UtcNow.AddHours(_jwtOptions.TimeOut);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = validity,
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials
        //    (new SymmetricSecurityKey(key),
        //    SecurityAlgorithms.HmacSha512Signature)
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    stringToken = tokenHandler.WriteToken(token);

        //    return stringToken;
        //}

        public DataTable GetDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(long));
            for (int i = 1; i <= 10; i++)
            {
                dt.Columns.Add($"ColValue{i}", typeof(string));
            }
            return dt;
        }

        public void ForceCreateDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}

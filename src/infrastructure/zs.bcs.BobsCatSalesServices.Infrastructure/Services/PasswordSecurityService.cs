using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;

namespace zs.bcs.BobsCatSalesServices.Infrastructure.Services
{
    /// <summary>
    /// An implementation of a service that secures passwords.
    /// </summary>
    public class PasswordSecurityService : IPasswordSecurityService
    {
        private static readonly int _saltLength = 16; // TODO config me
        private readonly ILogger<PasswordSecurityService> _logger;

        public PasswordSecurityService(
            ILogger<PasswordSecurityService> logger
            )
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the hashed value of the provided password with the salt value used.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The password data as hash/salt</returns>
        public Task<Tuple<string, string>> GetPasswordData(string password, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPasswordData");

            var salt = GetPasswordSalt();

            var passwordHash = HashPassword(salt, password);

            _logger.LogTrace("Exit GetPasswordData");

            var result = new Tuple<string, string>(passwordHash, salt);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets the hashed value of the provided password using the provided salt value.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The hashed value of the password.</returns>
        public Task<string> GetPasswordHash(string password, string passwordSalt, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPasswordHash");

            var result = HashPassword(passwordSalt, password);

            _logger.LogTrace("Exit GetPasswordHash");

            return Task.FromResult(result);
        }

        private string GetPasswordSalt()
        {
            _logger.LogTrace("Enter GetPasswordSalt");

            byte[] saltBytes = new byte[_saltLength];

            new Random().NextBytes(saltBytes);

            _logger.LogTrace("Exit GetPasswordSalt");

            return saltBytes.ToString();
        }

        private string HashPassword(string salt, string password)
        {
            _logger.LogTrace("Enter HashPassword");

            var saltedPassword = salt + password;

            var result = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                result = builder.ToString();
            }

            _logger.LogTrace("Exit HashPassword");

            return result;
        }
    }
}

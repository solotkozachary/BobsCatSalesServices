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
        public Task<Tuple<byte[], byte[]>> GetPasswordData(string password, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPasswordData");

            var salt = GetPasswordSalt();

            var passwordHash = HashPassword(salt, password);

            _logger.LogTrace("Exit GetPasswordData");

            var result = new Tuple<byte[], byte[]>(passwordHash, salt);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets the hashed value of the provided password using the provided salt value.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The hashed value of the password.</returns>
        public Task<byte[]> GetPasswordHash(string password, byte[] passwordSalt, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPasswordHash");

            var result = HashPassword(passwordSalt, password);

            _logger.LogTrace("Exit GetPasswordHash");

            return Task.FromResult(result);
        }

        private byte[] GetPasswordSalt()
        {
            _logger.LogTrace("Enter GetPasswordSalt");

            byte[] saltBytes = new byte[_saltLength];

            new Random().NextBytes(saltBytes);

            _logger.LogTrace("Exit GetPasswordSalt");

            return saltBytes;
        }

        private byte[] HashPassword(byte[] salt, string password)
        {
            _logger.LogTrace("Enter HashPassword");

            //var saltedPassword = salt + password;

            byte[] result = null;

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                for (int i = 0; i < passwordBytes.Length; i++)
                {
                    combinedBytes[i] = passwordBytes[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    combinedBytes[passwordBytes.Length + i] = salt[i];
                }

                result = sha256.ComputeHash(combinedBytes);
            }

            _logger.LogTrace("Exit HashPassword");

            return result;
        }
    }
}

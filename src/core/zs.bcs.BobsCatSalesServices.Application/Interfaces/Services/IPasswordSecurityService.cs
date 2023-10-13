using System;
using System.Threading;
using System.Threading.Tasks;

namespace zs.bcs.BobsCatSalesServices.Application.Interfaces.Services
{
    /// <summary>
    /// A contract for a service that secures passwords.
    /// </summary>
    public interface IPasswordSecurityService
    {
        /// <summary>
        /// Gets the hashed value of the provided password with the salt value used.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The password data as hash/salt</returns>
        Task<Tuple<byte[], byte[]>> GetPasswordData(string password, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the hashed value of the provided password using thw provided salt value.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The hashed value of the password.</returns>
        Task<byte[]> GetPasswordHash(string password, byte[] passwordSalt, CancellationToken cancellationToken);
    }
}

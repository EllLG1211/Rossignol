using ApiEntities;

namespace API_REST.Services
{
    public interface IAccountServices
    {
        /// <summary>
        /// Gets a user's info from their ID
        /// </summary>
        /// <param name="id">user GUID</param>
        /// <returns>the user, or null if no user with this ID is found</returns>
        public UserEntity? GetUser(string id);

        /// <summary>
        /// Get a user's info from their email
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>the user, or null if no user with this email is found</returns>
        public UserEntity? GetUserByEmail(string email);

        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="user">The account data for the user to add</param>
        /// <returns>true if the user was able to be created</returns>
        public bool AddUser(AccountEntity user);

        /// <summary>
        /// Delete an account
        /// </summary>
        /// <param name="id">The id for the user to delete</param>
        /// <returns>true if the user was found and deleted</returns>
        bool DeleteUser(string id);

        /// <summary>
        /// Update a user account
        /// </summary>
        /// <param name="id">The ID for the user to update</param>
        /// <param name="account">true if the update was successful</param>
        /// <returns></returns>
        bool UpdateUser(string id, AccountEntity account);
    }
}

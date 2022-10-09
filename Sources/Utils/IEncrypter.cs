namespace Encryption
{
    public interface IEncrypter : IEncryptionSpecifier, IDisposable
    {
        /// <summary>
        /// Method to cipher a string using a password key of any length
        /// </summary>
        /// <param name="key">the key used for encryption</param>
        /// <param name="toEncrypt">the string to encrypt</param>
        /// <returns>an array of bytes containing the encrypted string</returns>
        /// <exception cref="ArgumentNullException">Thrown if one of either arguments given is null</exception>
        byte[] Encrypt(string key, string toEncrypt);
    }
}

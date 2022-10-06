namespace Utils
{
    public interface IDecrypter
    {
        /// <summary>
        /// Method to decrypt a byte array containing an encrypted string
        /// </summary>
        /// <param name="key">AES key used to encrypt the array</param>
        /// <param name="entry">the encrypted byte array</param>
        /// <returns>the deciphered string</returns>
        /// <exception cref="CryptographicException">Thrown if the password is incorrect</exception>
        /// <exception cref="ArgumentNullException">Thrown if one of either arguments given is null</exception>
        string Decrypt(string key, byte[] entry);
    }
}

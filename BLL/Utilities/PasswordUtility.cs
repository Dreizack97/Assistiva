using System.Security.Cryptography;
using System.Text;

namespace BLL.Utilities
{
    /// <summary>
    /// Proporciona utilidades para la generación y gestión segura de contraseñas.
    /// </summary>
    /// <remarks>
    /// <para>Esta clase implementa:</para>
    /// <list type="bullet">
    /// <item>Generación de contraseñas complejas</item>
    /// <item>Creación de salt criptográfico</item>
    /// <item>Encriptación básica de contraseñas usando SHA-256</item>
    /// <item>Verificación de contraseñas</item>
    /// </list>
    /// </remarks>
    public static class PasswordUtility
    {
        /// <summary>
        /// Genera una contraseña aleatoria con complejidad configurable.
        /// </summary>
        /// <param name="lengthPassword">Longitud deseada (mínimo 6 caracteres)</param>
        /// <returns>Contraseña con al menos: 1 minúscula, 1 mayúscula, 1 dígito y 1 carácter especial</returns>
        /// <exception cref="ArgumentException">Si <paramref name="lengthPassword"/> es menor que 6</exception>
        /// <example>
        /// <code>
        /// string password = PasswordUtility.GeneratePassword(12); // Ej: "aT4#kL9!vXq@"
        /// </code>
        /// </example>
        public static string GeneratePassword(int lengthPassword = 8)
        {
            if (lengthPassword < 6)
                throw new ArgumentException("La longitud de la contraseña debe ser de al menos 6 caracteres.", nameof(lengthPassword));

            const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string DIGITS = "0123456789";
            const string SPECIAL_CHARS = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            Random random = new Random();

            char[] password = new char[lengthPassword];

            password[0] = LOWERCASE[random.Next(LOWERCASE.Length)];
            password[1] = UPPERCASE[random.Next(UPPERCASE.Length)];
            password[2] = DIGITS[random.Next(DIGITS.Length)];
            password[3] = SPECIAL_CHARS[random.Next(SPECIAL_CHARS.Length)];

            string allChars = LOWERCASE + UPPERCASE + DIGITS + SPECIAL_CHARS;

            for (int i = 4; i < lengthPassword; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            password = password.OrderBy(x => random.Next()).ToArray();

            return new string(password);
        }

        /// <summary>
        /// Genera un salt criptográficamente seguro de 32 bytes (256 bits).
        /// </summary>
        /// <returns>Array de bytes representando el salt</returns>
        /// <remarks>
        /// Utiliza <see cref="RandomNumberGenerator"/> para garantizar seguridad criptográfica.
        /// </remarks>
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// Deriva una clave criptográfica combinando un salt y una contraseña usando SHA-256.
        /// </summary>
        /// <param name="salt">Salt generado por <see cref="GenerateSalt"/></param>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Hash de 32 bytes</returns>
        /// <exception cref="ArgumentNullException">Si <paramref name="salt"/> o <paramref name="password"/> son null</exception>
        /// <exception cref="ArgumentException">Si <paramref name="salt"/> está vacío</exception>
        public static byte[] EncryptPassword(byte[] salt, string password)
        {
            ArgumentNullException.ThrowIfNull(salt, nameof(salt));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            if (salt.Length == 0)
                throw new ArgumentException("El salt no puede estar vacío.", nameof(salt));

            byte[] encryptedPassword;

            using (SHA256 sha256 = SHA256.Create())
            {
                encryptedPassword = sha256.ComputeHash(salt.Concat(Encoding.Unicode.GetBytes(password)).ToArray());
            }

            return encryptedPassword;
        }

        /// <summary>
        /// Verifica si una contraseña coincide con un hash previamente generado.
        /// </summary>
        /// <param name="salt">Salt usado originalmente.</param>
        /// <param name="encryptedPassword">Hash almacenado.</param>
        /// <param name="password">Contraseña a verificar.</param>
        /// <returns>True si la contraseña es válida.</returns>
        /// <remarks>
        /// <para>
        /// Este método utiliza <see cref="CryptographicOperations.FixedTimeEquals"/> 
        /// para prevenir ataques de tiempo.
        /// </para>
        /// </remarks>
        public static bool VerifyPassword(byte[] salt, byte[] encryptedPassword, string password)
        {
            ArgumentNullException.ThrowIfNull(salt, nameof(salt));
            ArgumentNullException.ThrowIfNull(encryptedPassword, nameof(encryptedPassword));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            byte[] hashPassword;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesPassword = Encoding.Unicode.GetBytes(password);
                byte[] combinedBytes = salt.Concat(bytesPassword).ToArray();

                hashPassword = sha256.ComputeHash(combinedBytes);
            }

            return CryptographicOperations.FixedTimeEquals(hashPassword, encryptedPassword);
        }
    }
}

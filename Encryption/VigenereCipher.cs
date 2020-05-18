using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encryption
{
    internal static class VigenereCipher
    {
        internal static readonly List<char> alphabet = new List<char>
            {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'};

        private static Func<char, char, int> index;

        public static string Encrypt(string decryptedText, string key)
        {
            index = (c1, c2) => (alphabet.IndexOf(c1) + alphabet.IndexOf(c2)) % alphabet.Count;
            return Crypt(decryptedText, key);
        }

        public static string Decrypt(string encryptedText, string key)
        {
            index = (c1, c2) => (alphabet.IndexOf(c1) + alphabet.Count - alphabet.IndexOf(c2)) % alphabet.Count;
            return Crypt(encryptedText, key);
        }

        private static string Crypt(string text, string key)
        {
            key = key.ToLower();
            var keyText = new StringBuilder(text.Where(c => alphabet.Contains(char.ToLower(c))).ToList().Count);
            for (var i = 0; i < keyText.Capacity; i++)
                keyText.Append(key[i % key.Length]);
            var answer = new StringBuilder(text.Length);
            var j = 0;
            foreach (var c in text)
            {
                var lower_c = char.ToLower(c);
                if (!alphabet.Contains(lower_c))
                {
                    answer.Append(c);
                    continue;
                }

                var newC = alphabet[index(lower_c, keyText[j++])];
                answer.Append(char.IsUpper(c) ? char.ToUpper(newC) : newC);
            }

            return answer.ToString();
        }
    }
}
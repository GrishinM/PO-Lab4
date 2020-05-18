﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Spire.Doc;

namespace Encryption
{
    public class Encryptor
    {
        private string key;

        public string Key
        {
            get => key;
            set
            {
                if (!value.ToLower().All(c => VigenereCipher.alphabet.Contains(c)))
                    throw new MyException("Невалидное значение ключа!");
                key = value;
            }
        }

        private Func<string, string, string> crypt;

        public string EncryptFile(string path)
        {
            crypt = VigenereCipher.Encrypt;
            return f(path);
        }

        public string EncryptText(string decryptedText)
        {
            return VigenereCipher.Encrypt(decryptedText, Key);
        }

        public string DecryptFile(string path)
        {
            crypt = VigenereCipher.Decrypt;
            return f(path);
        }

        public string DecryptText(string encryptedText)
        {
            return VigenereCipher.Decrypt(encryptedText, Key);
        }

        private string f(string path)
        {
            if (!File.Exists(path))
                throw new MyException("Файла не существует!");
            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".txt":
                    using (var reader = new StreamReader(path, Encoding.Default))
                        return crypt(reader.ReadToEnd(), Key);
                case ".docx":
                    var text = String.Join(Environment.NewLine, new Document(path).GetText().Split(new[] {Environment.NewLine}, StringSplitOptions.None).Skip(1));
                    return crypt(string.Join("", text.Take(text.Length - 2)), Key);
                default:
                    throw new MyException("Неподдерживаемое расширение файла!");
            }
        }
    }
}
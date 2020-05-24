using System;
using Encryption;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private readonly Encryptor encryptor = new Encryptor {Key = "кларнет"};

        private readonly string directory = TestContext.CurrentContext.TestDirectory + @"\";

        private string encryptedFile => directory + "encrypted.txt";

        private string decryptedFile => directory + "decrypted.txt";

        private const string encryptedText = "Хлрь б Пюкьы дшхтц цобнрюё";

        private const string decryptedText = "Карл у Клары украл кораллы";

        [Test]
        public void Test1()
        {
            Assert.AreEqual(decryptedText, encryptor.DecryptFile(encryptedFile));
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(encryptedText, encryptor.EncryptFile(decryptedFile));
        }
    }
}
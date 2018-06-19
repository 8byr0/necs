using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteEncryptCS.Encryption;

namespace NoteEncryptCSTest
{
    [TestClass] 
    public class TestDataEncrypter
    {
        [TestMethod]
        public void TestEncryption()
        {
            string pass = "aPass";
            string toEncrypt = "hello";

            string encrypted = DataEncrypter.Encrypt(toEncrypt, pass);
        }

        [TestMethod]
        public void TestDecryption()
        {
        }

        [TestMethod]
        public void TestEncryptionThenDecryption()
        {
            string pass = "aPass";
            string toEncrypt = "hello";

            string encrypted = DataEncrypter.Encrypt(toEncrypt, pass);

            string decrypted = DataEncrypter.Decrypt(encrypted, pass);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [TestMethod]
        public void TestDecryptionThenEncryption()
        {
        }

       
    }
}

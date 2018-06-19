using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.Factory;
using TestStack.White.UIItems.WindowItems;
using System.IO;
using NoteEncryptCS;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;

namespace NoteEncryptCSTest
{
    [TestClass]
    public class TestConnexionWindow
    {
        [TestMethod]
        public void TestWindowCreation()
        {
            Application application = Application.Launch(@"D:\Dev\C#\NoteEncryptCS\NoteEncryptCS\bin\Debug\NoteEncryptCS.exe");
            Window window = application.GetWindow("Connexion");
            
            application.Close();
        }

        [TestMethod]
        public void TestNewButtonDisabledIfFormEmpty()
        {
            Application application = Application.Launch(@"D:\Dev\C#\NoteEncryptCS\NoteEncryptCS\bin\Debug\NoteEncryptCS.exe");
            Window window = application.GetWindow("Connexion");

            Button newArchiveButton = window.Get<Button>("NewArchiveButton");
            Assert.IsNotNull(newArchiveButton);

            Assert.IsFalse(newArchiveButton.Enabled);

            application.Close();
        }

        [TestMethod]
        public void TestNewButtonEnabledIfFormFilled()
        {
            Application application = Application.Launch(@"D:\Dev\C#\NoteEncryptCS\NoteEncryptCS\bin\Debug\NoteEncryptCS.exe");
            Window window = application.GetWindow("Connexion");

            Button newArchiveButton = window.Get<Button>("NewArchiveButton");
            Assert.IsNotNull(newArchiveButton);

            TextBox newArchiveName = window.Get<TextBox>("NewArchiveName");
            Assert.IsNotNull(newArchiveName);
            newArchiveName.Text = "My archive name";

            TextBox newArchivePassword= window.Get<TextBox>("NewArchivePassword");
            Assert.IsNotNull(newArchivePassword);
            newArchivePassword.Text = "password";

            Assert.IsTrue(newArchiveButton.Enabled);

            newArchiveButton.Click();

            //var errorBox = window.MessageBox("error");
            //Assert.IsNotNull(errorBox);

            //var message = errorBox.Get<Label>(SearchCriteria.Indexed(0));
            //Assert.AreEqual("Cannot create new archive.", message.Name);

            //errorBox.Get<Button>(SearchCriteria.ByText("OK")).Click();
            window.Dispose();
            application.Close();
        }


        [TestMethod]
        public void TestCreateArchiveThenOpenIt()
        {
            // Launch application
            Application application = Application.Launch(@"D:\Dev\C#\NoteEncryptCS\NoteEncryptCS\bin\Debug\NoteEncryptCS.exe");
            Window window = application.GetWindow("Connexion");

            // NEW archive button 
            Button newArchiveButton = window.Get<Button>("NewArchiveButton");
            Assert.IsNotNull(newArchiveButton);
            Assert.IsFalse(newArchiveButton.Enabled);

            TextBox newArchiveName = window.Get<TextBox>("NewArchiveName");
            Assert.IsNotNull(newArchiveName);
            newArchiveName.Text = "a_new_archive";

            TextBox newArchivePassword = window.Get<TextBox>("NewArchivePassword");
            Assert.IsNotNull(newArchivePassword);
            newArchivePassword.Text = "password";

            Assert.IsTrue(newArchiveButton.Enabled);

            newArchiveButton.Click();

            // Should be disabled then
            Assert.IsFalse(newArchiveButton.Enabled);

            // OPEN AN ARCHIVE
            Button existingArchiveButton = window.Get<Button>("OpenArchiveButton");
            Assert.IsNotNull(existingArchiveButton);
            Assert.IsFalse(existingArchiveButton.Enabled);

            // Should be created at this point
            ComboBox archivesList = window.Get<ComboBox>("ListArchive");
            Assert.IsNotNull(archivesList);

            archivesList.Select("a_new_archive");
            Assert.IsFalse(existingArchiveButton.Enabled);

            Assert.AreEqual("a_new_archive", archivesList.SelectedItemText);

            TextBox existingPassword = window.Get<TextBox>("ExistingArchivePassphrase");
            Assert.IsNotNull(existingPassword);

            existingPassword.Text = "password";


            
            Assert.IsTrue(existingArchiveButton.Enabled);

            existingArchiveButton.Click();

            Assert.IsFalse(window.Visible);
            application.Close();
        }
    }
}

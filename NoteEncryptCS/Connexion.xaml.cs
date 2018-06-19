using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoteEncryptCS.Encryption;
using NoteEncryptCS.Entity;
using NoteEncryptCS.Entity.Dal;

namespace NoteEncryptCS
{
    /// <summary>
    /// Logique d'interaction pour ConnexionWindow.xaml
    /// </summary>
    public partial class ConnexionWindow : Window
    {
        public ConnexionWindow()
        {
            RoutedCommand firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.Enter, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OnOpenExistingButtonClicked));

            InitializeComponent();
            Title = "Connexion";
            IDatabaseInitializer<DataBaseContext> init = new CreateDatabaseIfNotExists<DataBaseContext>();
            //IDatabaseInitializer<DataBaseContext> init = new DropCreateDatabaseAlways<DataBaseContext>();
            //IDatabaseInitializer<DataBaseContext> init = new DropCreateDatabaseIfModelChanges<DataBaseContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new DataBaseContext());

            ListArchive.ItemsSource = NoteEncryptCSManager.GetInstance().GetArchives();
            ListArchive.SelectedIndex = 0;



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }



        public bool IsOpenArchiveFormFilled()
        {
            return ExistingArchivePassphrase.Password.Length > 0;
        }


        /// <summary>
        /// Methode for try connexion with password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOpenExistingButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenArchiveButton.Visibility = Visibility.Hidden;
                LoadingLabel.Visibility = Visibility.Visible;

                DataEncrypter.Decrypt(((Archive)ListArchive.SelectedItem).OwnerEncrypted, ExistingArchivePassphrase.Password);

                SettingsProvider.getInstance().PassPhrase = ExistingArchivePassphrase.Password;
                SettingsProvider.getInstance().LoadedArchive = NoteEncryptCSManager.GetInstance().GetArchive(((Archive)ListArchive.SelectedItem).Name, ExistingArchivePassphrase.Password);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
            catch (Exception exc)
            {
                //display a message invalid password
                OpenArchiveButton.Visibility = Visibility.Visible;
                LoadingLabel.Visibility = Visibility.Hidden;
                ExistingArchivePassphrase.Clear();
                MessageBox.Show("Unable to decrypt archive, check your password and try again", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void OnNewButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string archiveName = NewArchiveName.Text;
                string passphrase = NewArchivePassword.Password;
                NoteEncryptCSManager.GetInstance().CreateArchive(archiveName, passphrase);
                ListArchive.ItemsSource = NoteEncryptCSManager.GetInstance().GetArchives();
                ListArchive.SelectedIndex = ListArchive.Items.Count - 1;

                NewArchiveName.Clear();
                NewArchivePassword.Clear();
            }
            catch (Exception exc)
            {
                NewArchivePassword.Clear();
                //display a message invalid password
                MessageBox.Show("Cannot create new archive.", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenArchivePassChanged(object sender, RoutedEventArgs e)
        {
            // Cast TextBox.


            OpenArchiveButton.IsEnabled = ExistingArchivePassphrase.Password.Length > 0;

        }

        private void NewArchiveFormChanged(object sender, RoutedEventArgs e)
        {
            NewArchiveButton.IsEnabled = (NewArchiveName.Text.Length > 0) && (NewArchivePassword.Password.Length > 0);
        }
        private void OnOpenExistingFormSubmitted(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (sender.Equals(ExistingArchivePassphrase))
                {
                    if (ExistingArchivePassphrase.Password.Length > 0)
                    {
                        this.OnOpenExistingButtonClicked(null, null);
                    }
                }
                else if (sender.Equals(NewArchiveName) || sender.Equals(NewArchivePassword) ) 
                {
                    if ( (NewArchivePassword.Password.Length > 0) && (NewArchiveName.Text.Length >  0))
                    {
                        this.OnNewButtonClicked(null, null);
                    }
                }
            }
        }
    }
}

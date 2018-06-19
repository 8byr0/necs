using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NoteEncryptCS.Encryption;
using NoteEncryptCS.Entity;
using NoteEncryptCS.Entity.Dal;

namespace NoteEncryptCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NoteEncryptCSManager manager = NoteEncryptCSManager.GetInstance();
        ObservableCollection<Note> NotesList;
        public MainWindow()
        {

            DataEncrypter.Decrypt(SettingsProvider.getInstance().LoadedArchive);
            NotesList = SettingsProvider.getInstance().LoadedArchive.Notes;

            // retrieve notes
            //            manager.retrieveNotes();

            // Load GUI
            InitializeComponent();

            //recive All save notes
            listbox1.ItemsSource = NotesList;

            

        }

        private void OnNewNoteButtonClicked(object sender, RoutedEventArgs e)
        {
            String input = NewItemInput.Text;
            if (input.Length > 0)
            {
                Note myNote = new Note();
                myNote.Name = input;
                myNote.Content = "";
                myNote.LastUpdate = DateTime.Now.ToString();
                myNote.CreationTime = DateTime.Now.ToString();

                myNote.Owner = SettingsProvider.getInstance().LoadedArchive.OwnerEncrypted;

                myNote.save();
                SettingsProvider.getInstance().LoadedArchive.Notes.Add(myNote);

                //using (DalNote dalNote = new DalNote())
                //{
                //    dalNote.CreateNote(oldNoteToCreate);
                //}
                listbox1.SelectedIndex = listbox1.Items.Count - 1;
                Listbox1_SelectionChanged(null, null);

                NewItemInput.Clear();
            }
            else
            {
                MessageBox.Show("Note title cannote be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedIndex != -1)
            {
                try
                {
                    int index = listbox1.SelectedIndex;
                    ((Note)listbox1.Items.GetItemAt(index)).delete();

                    SettingsProvider.getInstance().LoadedArchive.Notes.RemoveAt(index);
                    NewItemInput.Clear();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Unable to delete Note, error: " + exc.Message);
                }
            }
            else
            {
                MessageBox.Show("select oldNote for remove");
            }
        }

        void Listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox1.SelectedIndex == -1)
            {
                loadedView.Visibility = Visibility.Hidden;
                SaveBtn.Visibility = Visibility.Hidden;
                RemoveBtn.Visibility = Visibility.Hidden;

                return;
            };
            loadedView.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
            RemoveBtn.Visibility = Visibility.Visible;
            int index = listbox1.SelectedIndex;



            //loadedView.DataContext = manager.NotesList.GetNote(index);
            loadedView.DataContext = SettingsProvider.getInstance().LoadedArchive.Notes[index];
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("save oldNote");

            int index = listbox1.SelectedIndex;

            // OldNote curentlyOldNote = manager.NotesList.GetNote(index);
            Note curentlyOldNote = SettingsProvider.getInstance().LoadedArchive.Notes[index];

            curentlyOldNote.save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            //Environment.Exit(0);
        }


        public void OnCloseArchiveButtonClicked(object sender, RoutedEventArgs e)
        {
            ConnexionWindow connectWindow = new ConnexionWindow();
            connectWindow.Show();
            SettingsProvider.getInstance().LoadedArchive = null;
            SettingsProvider.getInstance().PassPhrase = "";
            this.Close();

        }

        public void OnSaveArchiveButtonClosed(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("'Bravo' Archive properly saved !", "Success !", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void OnDeleteArchiveButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("We won't be able to revert this operation...", "Are you really sure ?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                NoteEncryptCSManager.GetInstance().DeleteArchive(SettingsProvider.getInstance().LoadedArchive);
                ConnexionWindow connectWindow = new ConnexionWindow();
                connectWindow.Show();
                SettingsProvider.getInstance().PassPhrase = "";
                this.Close();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                MessageBox.Show("I think that's a good decision.", "Yeah man !");
            }
        }

        private void OnNewNoteTitleUpdated(object sender, RoutedEventArgs e)
        {
            // Cast TextBox.
            NewNoteButton.IsEnabled = (NewItemInput.Text.Length > 0);

        }
        private void OnNewNoteFormSubmitted(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(NewItemInput.Text.Length > 0)
                {
                    this.OnNewNoteButtonClicked(null, null);
                }
            }
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour Close.xaml
    /// </summary>
    public partial class Close : Window
    {
        MainWindow main;
        bool fermer;//fermer tt la fenetre 
        bool ouvrir;//ouvrir un fichier
        bool nouveau;
        public Close(MainWindow w, bool fermer, bool ouvrir, bool nouveau)
        {
            main = w;
            this.fermer = fermer;
            this.ouvrir = ouvrir;
            this.nouveau = nouveau;
            InitializeComponent();
        }

        private void Oui_Click(object sender, RoutedEventArgs e)
        {
            if (main.filename != null)//le fichier existe
            {
                this.Close();
                File.Delete(main.filename);
                main.SerializeToXAML(main.filename);
                if (fermer)
                {
                    main.Close();
                }
            }
            else//le fichier n'existe pas deja on doit crée un autre 
            {
                this.Close();
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "UIElement File"; // Default file name
                dlg.DefaultExt = ".xaml"; // Default file extension
                dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension
                                                         // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document

                    string filename = dlg.FileName;
                    main.filename = filename;
                    main.SerializeToXAML(filename);
                    if (fermer)
                    {
                        main.Close();
                    }

                    //melissa 

                }
            }
            if (ouvrir)
            {
                //on doit attendre la fermiture de la fenetre precedente pour lancer celle ci 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".xaml"; // Default file extension
                dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension
                                                         // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                // Process open file dialog box results
                if (result == true)
                {
                    string filename = dlg.FileName;
                    if (filename != main.filename)//c pas le meme fichier ouvert dejà
                    {
                        main.filename = filename;
                        main.DeSerializeXAML(filename);
                    }

                }
            }
            if (nouveau)
            {
                main.Grille.Children.Clear();
                main.getcircuit().Clear();
                main.filename = null;
            }
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (fermer)
            {
                main.Close();
            }
            if (ouvrir)
            {
                //on doit attendre la fermiture de la fenetre precedente pour lancer celle ci 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".xaml"; // Default file extension
                dlg.Filter = "Xaml File (.xaml)|*.xaml"; // Filter files by extension
                                                         // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                // Process open file dialog box results
                if (result == true)
                {
                    string filename = dlg.FileName;
                    if (filename != main.filename)//c pas le meme fichier ouvert dejà
                    {
                        main.filename = filename;
                        main.DeSerializeXAML(filename);
                    }

                }
            }
            if (nouveau)
            {
                main.Grille.Children.Clear();
                main.getcircuit().Clear();
                main.filename = null;
            }
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.IsEnabled = true;


        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.BorderBrush = Brushes.Blue;
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.BorderBrush = Brushes.White;
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt";

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                StreamWriter writer = new StreamWriter(fileDialog.OpenFile());
                writer.Write(tb_main.Text);
                writer.Close();

                string title = "Texteditor | " + fileDialog.FileName;
                lbl_name.Content = title;
                Title = title;
            }
        }

        private void btn_Load(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            fileDialog.Multiselect = false;

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                StreamReader writer = new StreamReader(fileDialog.OpenFile());
                tb_main.Text = "";
                while (!writer.EndOfStream)
                    tb_main.Text += writer.ReadLine() + "\n";

                string title = "Texteditor | " + fileDialog.FileName;
                lbl_name.Content = title;
                Title = title;
                writer.Close();
            }
        }

        private void updateInfo(object sender, TextChangedEventArgs e)
        {
            int lines = tb_main.LineCount, chars = tb_main.Text.Count();
            lbl_info.Content = lines  + " Line" + (lines != 1 ? "s" : "") + "  | " + chars + " Character" + (chars != 1 ? "s" : "");
        }
    }
}

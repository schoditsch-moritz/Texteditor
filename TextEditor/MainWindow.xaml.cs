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
        public const int DEFAULT_FONT_SIZE = 15;
        private int zoom, currentFontSize;
        private string currentPath;

        public MainWindow()
        {
            zoom = 100;
            currentFontSize = DEFAULT_FONT_SIZE;
            currentPath = "";

            InitializeComponent();
        }

        private void updateInfo(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int lines = textBox.LineCount, chars = textBox.Text.Count();
            lbl_info.Content = lines + " Line" + (lines != 1 ? "s" : "") + "  | " + chars + " Character" + (chars != 1 ? "s" : "");
        }

        private void Zoom(object sender, MouseWheelEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;
            if (e.Delta > 0) zoom += 10;
            else zoom -= 10;

            if (zoom <= 10) zoom = 10;
            else if (zoom >= 600) zoom = 600;

            ZoomInOut();
        }

        private void ResetZoom(object sender, RoutedEventArgs e)
        {
            zoom = 100;
            ZoomInOut();
        }

        private void SaveFile(object sender, ExecutedRoutedEventArgs e)
        {
            if (currentPath != "")
            {
                StreamWriter writer = new StreamWriter(currentPath);
                writer.Write(tb_main.Text);
                writer.Close();

                tb_main.Focus();
            } else SaveFileForce(sender, e);
        }

        private void SaveFileForce(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt";

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                StreamWriter writer = new StreamWriter(fileDialog.OpenFile());
                writer.Write(tb_main.Text);
                writer.Close();
                currentPath = fileDialog.FileName;

                string title = "Texteditor | " + currentPath;
                lbl_name.Content = title;
                Title = title;
            }
            tb_main.Focus();
        }

        private void OpenFile(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            fileDialog.Multiselect = false;

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                StreamReader reader = new StreamReader(fileDialog.OpenFile());
                tb_main.Text = "";
                while (!reader.EndOfStream)
                    tb_main.Text += reader.ReadLine() + "\n";

                currentPath = fileDialog.FileName;


                string title = "Texteditor | " + currentPath;
                lbl_name.Content = title;
                Title = title;
                reader.Close();
            }
            tb_main.Focus();
        }

        private void QuitProgram(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void NewFile(object sender, ExecutedRoutedEventArgs e)
        {
            tb_main.Clear();
            Title = "Texteditor";
            lbl_name.Content = "Texteditor";
            currentPath = "";
        }

        private void ZoomInOut()
        {
            tb_main.FontSize = currentFontSize + (zoom - 100) / 10;
            lbl_zoom_info.Content = zoom + "%";
            tb_main.Focus();
        }
    }
}

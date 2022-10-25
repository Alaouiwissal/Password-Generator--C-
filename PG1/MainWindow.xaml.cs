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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PG1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PasswordInfo> PasswordList { get; set; } = new ObservableCollection<PasswordInfo>();


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public class PasswordInfo
        {
            public DateTime DateCreated { get; set; }
            public string Password { get; set; }
        }

        void AddPasswordToList(string pass)
        {
            PasswordList.Add(new PasswordInfo() { DateCreated = DateTime.Now, Password = pass });
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            string passwordChars = "";

            if ((bool)Uppercase.IsChecked) passwordChars += "QWERTYUIOPASDFGHJKLZXCVBNM";
            if ((bool)Lowercase.IsChecked) passwordChars += "qwertyuiopasdfghjklzxcvbnm";
            if ((bool)Symbols.IsChecked) passwordChars += "!$% &*^.,>< ";
            if ((bool)Numbers.IsChecked) passwordChars += "1234567890";

            if (passwordChars.Length > 0)
            {
                if (LengthPasswordTB.Text == string.Empty)
                {
                    MessageBox.Show("Enter the password length.");
                }
                else
                {

                    int passwordLenght = 0;
                    int.TryParse(LengthPasswordTB.Text, out passwordLenght);

                    if (passwordLenght == 0) return;


                    //string chars = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    Random random = new Random();
                    String result = new string(Enumerable.Repeat(passwordChars, passwordLenght).Select(x => x[random.Next(x.Length)]).ToArray());

                    Password.FontStyle = FontStyles.Normal;
                    Password.FontSize = 16;
                    Password.Text = result;
                    AddPasswordToList(result);

                }
            }
            else
            {
                MessageBox.Show("Choose the password option.");
            }

        }
        private void LengthPasswordTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                int intResult = 0;
                bool result = Int32.TryParse(box.Text, out intResult);

                if (result && intResult < 60)
                {
                    box.Background = Brushes.White;
                    Generatebtn.IsEnabled = true;
                }
                else
                {
                    box.Background = Brushes.Red;
                    Generatebtn.IsEnabled = false;
                }

            }
        }
        private void Copybtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Password.Text);
        }

        private void DataGridCopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Console.WriteLine("Hello world");
                if (button.DataContext is PasswordInfo info)
                {
                    Clipboard.SetText(info.Password);
                    //MessageBox.Show("Password copied!");
                }
                e.Handled = true;
            }
        }

        private void SetColorMode(string color)
        {
            Properties.Settings.Default.ColorMode = color;
            // and to save the settings
            Properties.Settings.Default.Save();
        }
        private void LightMode_checked(object sender, RoutedEventArgs e)
        {
            SetColorMode( "Light");
        }
        private void WhiteMode_checked(object sender, RoutedEventArgs e)
        {
            SetColorMode("White");

        }

        private void DarkMode_checked(object sender, RoutedEventArgs e)
        {
            SetColorMode("Dark");
        }

        private void BlackMode_checked(object sender, RoutedEventArgs e)
        {
            SetColorMode("Black");
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}

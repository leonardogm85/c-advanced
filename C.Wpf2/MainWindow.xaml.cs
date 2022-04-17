using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace C.Wpf2
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (sender as TextBlock)!;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.Foreground = Brushes.Blue;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (sender as TextBlock)!;
            textBlock.FontWeight = FontWeights.Normal;
            textBlock.Foreground = Brushes.Black;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text2.Text = Text1.Text;
        }
    }
}

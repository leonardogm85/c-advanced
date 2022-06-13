using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace C.Wpf05
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

        public string? Cpf { get; set; }

        private void Window_Initialized(object sender, EventArgs e)
        {
            TextCpf.DataContext = this;
        }
    }

    class CpfValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Valid(value as string))
            {
                return ValidationResult.ValidResult;
            }

            return new ValidationResult(false, "Invalid CPF");
        }

        private bool Valid(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var withoutMask = string.Concat(value.Where(char.IsDigit));

            if (withoutMask.Length != 11)
            {
                return false;
            }

            var invalid = new string[]
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            if (invalid.Any(i => i == withoutMask))
            {
                return false;
            }

            var add = 0;

            for (var i = 0; i < 9; i++)
            {
                add += int.Parse(withoutMask[i].ToString()) * (10 - i);
            }

            var rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(withoutMask[9].ToString()))
            {
                return false;
            }

            add = 0;

            for (var i = 0; i < 10; i++)
            {
                add += int.Parse(withoutMask[i].ToString()) * (11 - i);
            }

            rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(withoutMask[10].ToString()))
            {
                return false;
            }

            return true;
        }
    }
}

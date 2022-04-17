using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace C.Wpf4
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

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            ListPersons.ItemsSource = new List<Person>
            {
                new("José", "Silva"),
                new("Alberto", "Oliveira"),
                new("João", "Júnior"),
                new("Maria", "Vargas"),
                new("Alice", "Fagundes")
            };
        }
    }

    class Person : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public Person(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value)
                {
                    return;
                }

                _firstName = value;

                if (PropertyChanged == null)
                {
                    return;
                }

                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FullName)));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value)
                {
                    return;
                }

                _lastName = value;

                if (PropertyChanged == null)
                {
                    return;
                }

                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FullName)));
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

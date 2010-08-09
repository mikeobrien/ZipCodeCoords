using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZipCodes;

namespace TestHarness
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            Coordinate coordinate = Spatial.Search(ZipCodeTextBox.Text);

            ResultsListBox.Items.Clear();

            ResultsListBox.Items.Add("Zip: " + coordinate.Zip.ToString());
            ResultsListBox.Items.Add("Lat: " + coordinate.Latitude);
            ResultsListBox.Items.Add("Long: " + coordinate.Longitude);

            ResultsListBox.Items.Add("Zip: " + Spatial.Search(coordinate.Latitude, coordinate.Longitude).Zip.Value.ToString());
        }
    }
}

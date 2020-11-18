using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public mycontext ctx;

        public MainWindow()
        {
            ctx = new mycontext();
            ctx.Organizations = new ObservableCollection<IOrganization>()
            {
                new NFLCLFOrganization(),
                new NCAAOrganization()
            };
            ctx.SelectedOrganization = ctx.Organizations.Where(x => x.Name == "NFL/CFL").FirstOrDefault();
            ctx.Stats = new QuarterbackStats() { PassingAttempts = 1M };

            DataContext = ctx;

            InitializeComponent();

            cboSelectedOrg.SelectedIndex = 0;
        }

        private void btnCalculate(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as mycontext;
            dc.SelectedOrganization.CalculatePasserRating(dc.Stats);
        }

        private void btnClear(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as mycontext;
            dc.Stats = new QuarterbackStats() { PassingAttempts = 1M };
        }

        private void ChangedOrganization(object sender, SelectionChangedEventArgs args)
        {
            var selected = ((sender as ComboBox).SelectedItem as IOrganization);
            var dc = DataContext as mycontext;
            dc.SelectedOrganization = selected;
        }

        private void ValidateIsIntNoZero(object sender, TextCompositionEventArgs e)
        {
            // only allow integers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ValidateIsInt(object sender, TextCompositionEventArgs e)        
        {
            // only allow integers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

    public class mycontext : INotifyPropertyChanged
    {
        private IOrganization selectedOrg;
        private QuarterbackStats stats;
        public string Status { get; set; }
        public ObservableCollection<IOrganization> Organizations { get; set; }
        public IOrganization SelectedOrganization
        {
            get { return selectedOrg; }
            set
            {
                if (selectedOrg == value) return;
                selectedOrg = value;
                OnPropertyChanged("SelectedOrganization");
            }
        }
        public QuarterbackStats Stats
        {
            get { return stats; }
            set
            {
                if (stats == value) return;
                stats = value;
                OnPropertyChanged("Stats");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string SelectedOrganization)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(SelectedOrganization));
            }
        }
    }
}

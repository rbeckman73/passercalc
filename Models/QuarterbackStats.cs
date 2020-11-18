using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class QuarterbackStats : INotifyPropertyChanged
    {
        private decimal passerRating;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedOrganization { get; set; }
        public decimal PassingAttempts { get; set; }
        public decimal Completions { get; set; }
        public decimal PassingYards { get; set; }
        public decimal TouchdownPasses { get; set; }
        public decimal Interceptions { get; set; }
        public decimal PasserRating
        {
            get { return passerRating; }
            set
            {
                passerRating = value;
                this.OnPropertyChanged("PasserRating");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

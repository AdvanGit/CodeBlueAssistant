using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WpfApp1.Context;

namespace WpfApp1.Model
{
    public class Route : INotifyPropertyChanged //трансфер, транзакции
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));}

        private Patient patient;    
        private Staff staffOrigin;
        private Staff staffDestination;
        private DateTime createDateTime;
        private DateTime visitDateTime;


        public Route()
        {
            createDateTime = System.DateTime.Now;
        }

        public DateTime VisitDateTime
        {
            get
            {
                return visitDateTime;
            }
            set
            {
                visitDateTime = value;
                OnPropertyChanged("VisitDateTime");
            }
        }


    }
}

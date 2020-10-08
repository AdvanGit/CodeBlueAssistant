using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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
            patient = new Patient { FirstName = "Очень", MidName = "Больной", LastName = "Человек" };
            staffOrigin = new Staff { FirstName = "Ресепшен", MidName = "Вашу", LastName = "Мать"};
            staffDestination = new Staff { FirstName = "Доктор", MidName = "Соколов", LastName = "Премудрый"};
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

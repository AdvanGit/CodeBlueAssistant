using Hospital.Domain.Model;
using Hospital.WPF.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Hospital.WPF.Services.States
{
    internal class ViewStateFactory
    {
        internal ICollection<UserControl> GetDefaultViews(Staff staff)
        {
            Collection<UserControl> viewCollection = new Collection<UserControl>();

            switch (staff.Role)
            {
                case Role.Ambulatorer:
                    viewCollection.Add(new Schedule());
                    break;
                case Role.Registrator:
                    viewCollection.Add(new Registrator());
                    break;
                case Role.Manager:
                    break;
                case Role.Administrator:
                    viewCollection.Add(new Registrator());
                    viewCollection.Add(new Schedule());
                    break;
                default:
                    break;
            }

            return viewCollection;
        }

        internal void ChаngeViewState(ref ICollection<UserControl> BodiesCollection, Role role)
        {
            switch (role)
            {
                case Role.Ambulatorer:
                    BodiesCollection.Clear();
                    BodiesCollection.Add(new Schedule());
                    break;
                case Role.Registrator:
                    BodiesCollection.Clear();
                    BodiesCollection.Add(new Registrator());
                    break;
                case Role.Administrator:
                    BodiesCollection.Clear();
                    BodiesCollection.Add(new Registrator());
                    BodiesCollection.Add(new Schedule());
                    break;
                default:
                    break;
            }
        }

    }
}

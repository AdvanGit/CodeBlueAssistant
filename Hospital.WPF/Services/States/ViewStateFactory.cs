using Hospital.Domain.Security;
using Hospital.WPF.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Hospital.WPF.Services.States
{
    internal class ViewStateFactory
    {

        internal ICollection<UserControl> GetDefaultViews(IAccount account)
        {
            Collection<UserControl> viewCollection = new Collection<UserControl>();

            switch (account.GetRole())
            {
                case Role.Doctor:
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
                case Role.Unspecified:
                    break;
                default:
                    break;
            }

            return viewCollection;
        }

        internal void ChаngeViewState(ref ICollection<UserControl> BodiesCollection, IAccount account)
        {
            switch (account.GetRole())
            {
                case Role.Doctor:
                    BodiesCollection.Clear();
                    BodiesCollection.Add(new Schedule());
                    break;
                case Role.Registrator:
                    BodiesCollection.Clear();
                    BodiesCollection.Add(new Registrator());
                    break;
                case Role.Manager:
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

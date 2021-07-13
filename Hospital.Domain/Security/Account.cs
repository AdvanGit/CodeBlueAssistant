using Hospital.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Domain.Security
{
    public class Account<T>: IAccount where T: User
    {
        private Role _role;
        private int _id;
        private User currentUser;

        public int Id => _id;


        public Role GetRole()
        {
            if (typeof(T) == typeof(Patient)) return Role.Unspecified;
            else if (typeof(T) == typeof(Staff)) return ((Staff)currentUser).Role;
            else return Role.Unspecified;

        }
    }
}

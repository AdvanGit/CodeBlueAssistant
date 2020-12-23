using System;

namespace Hospital.EntityFramework.Filters
{
    public class RegistratorFilter
    {
        public bool IsName { get; set; }
        public bool IsQualification { get; set; }
        public bool IsDate { get; set; }
        public bool IsDepartment { get; set; }
        public bool IsAdress { get; set; }
        public bool IsFree { get; set; }
        public bool IsGroup { get; set; }

        public DateTime DateTime { get; set; }
    }
}

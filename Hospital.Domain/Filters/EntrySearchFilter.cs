using Hospital.Domain.Model;
using System;

namespace Hospital.Domain.Filters
{
    public class EntrySearchFilter
    {
        public bool IsName { get; set; }
        public bool IsQualification { get; set; }
        public bool IsDate { get; set; }
        public bool IsDepartment { get; set; }
        public bool IsAdress { get; set; }
        public bool IsFree { get; set; }
        public bool IsNearest { get; set; }

        public DepartmentType DepartmentType { get; set; }

        public DateTime DateTime { get; set; }
    }
}

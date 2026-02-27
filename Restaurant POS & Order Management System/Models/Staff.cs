using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class Staff
    {
        public int StaffId { get; private set; }
        public string Name { get; private set; }

        public StaffRole StaffRole { get; private set; }

        public bool IsOnDuty {  get; private set; }

        public Staff(int staffId,string name, StaffRole staffRole)
        {
            if (staffId <= 0)
            {
                throw new ArgumentException("The staff ID should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The staff name should not be empty");               
            }
            StaffId = staffId;
            Name = name.ToUpper();
            StaffRole = staffRole;
            IsOnDuty=true;
        }
        public Staff(int staffId, string name, StaffRole staffRole,bool isOnDuty)
        {
            if (staffId <= 0)
            {
                throw new ArgumentException("The staff ID should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The staff name should not be empty");
            }
            StaffId = staffId;
            Name = name.ToUpper();
            StaffRole = staffRole;
            IsOnDuty =isOnDuty;
        }
    }
}

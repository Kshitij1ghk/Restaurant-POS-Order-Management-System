using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class Table
    {
        public int TableNumber { get; private set; }

        public int Capacity {  get; private set; }
        public TableStatus TableStatus { get; private set; }

        public Table(int tableNumber, int capacity)
        {
            if (tableNumber<= 0)
            {
                throw new ArgumentException("Table number is invalid must be greater than zero");
            }
            if (capacity < 1)
            {
                throw new ArgumentException("Table Capacity must be at least 1!");
            }

            TableNumber = tableNumber;
            Capacity = capacity;
            this.TableStatus = TableStatus.AVAILABLE;
        }  
    }
}

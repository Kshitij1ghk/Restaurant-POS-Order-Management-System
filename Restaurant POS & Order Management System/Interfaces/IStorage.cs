using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Interfaces
{
    public interface IStorage
    {
        void SaveMenuItems(Dictionary<int,MenuItem> menuitems);
        Dictionary<int, MenuItem> LoadMenuItems();

        void SaveOrders(Dictionary<int, Order> orders);
        Dictionary<int, Order> LoadOrders();

        void SaveOrderItems(Dictionary<int,List<OrderItem>> orderaItems);
        Dictionary<int, List<OrderItem>> LoadOrderItems();

        void SaveTables(Dictionary<int,Table> tables);
        Dictionary<int, Table> LoadTables();

        void SaveStaff(Dictionary<int,Staff> staffs);
        Dictionary<int, Staff> LoadStaff();

        void SaveReceipts(Dictionary<int,Receipt> receipts);
        Dictionary<int, Receipt> LoadReceipts();    
    }
}

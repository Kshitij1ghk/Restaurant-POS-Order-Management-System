using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public MenuCategory MenuCategory { get; private set; }

        public FoodCategory FoodCategory { get; private set; }

        public string Description { get; private set; }

        public bool IsAvailable {  get; private set; }

        public MenuItem(int menuItemId,string name,decimal price,MenuCategory menucategory,FoodCategory foodcategory
            ,string description)
        {
            if (menuItemId <= 0)
            {
                throw new ArgumentException(" Invalid Input! the id should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty! ");
            }
            if(price <= 0)
            {
                throw new ArgumentException(" Price cannot be zero !");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(" Description can't be empty!");
            }


            MenuItemId = menuItemId;
            Name = name.ToUpper();
            Price = price;
            MenuCategory= menucategory;
            FoodCategory= foodcategory;
            Description = description;

            IsAvailable = true;
        }
    }
}

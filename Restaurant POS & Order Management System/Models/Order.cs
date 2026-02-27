using Restaurant_POS___Order_Management_System;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class Order
    {
        public int OrderId{ get; private set; }

        public OrderType OrderType{ get; private set; }

        public OrderStatus Status { get; private set; }

        public int? TableNumber {  get; private set; }

        public int StaffId {  get; private set; }

        public DateTime CreatedAt{  get; private set; }

        public DateTime? CompletedAt { get; private set; }//? means the value is allowed to be null

        public PaymentMethod? PaymentMethod{ get; private set; }

        public decimal TotalAmount {  get; private set; }

        
        
        public Order(int orderId,OrderType orderType,int? tableNumber,int staffId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order id cant be less than or equal to zero");
            }
            if (staffId<=0)
            {
                throw new ArgumentException("staff id cannot be less than or equal to zero");
            }

            //Buisness Rule Validation
            if(orderType==OrderType.DINE_IN && tableNumber == null)
            {
                throw new ArgumentException("DINE IN order must have a table.");
            }
            if(orderType==OrderType.TAKEAWAY && tableNumber != null)
            {
                throw new ArgumentException("Takeawat order cannot have a table.");
            }
            
            OrderId=orderId;
            OrderType=orderType;
            Status = OrderStatus.PENDING;
            TableNumber = tableNumber;
            StaffId = staffId;
            CreatedAt = DateTime.Now;
            CompletedAt=null;
            PaymentMethod=null;
            TotalAmount=0m;
            
        }
        public Order(int orderId, OrderType orderType,OrderStatus status,int? tableNumber,int staffId,DateTime createdAt,
            DateTime? completedAt,PaymentMethod? paymentMethod,decimal totalAmount)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order id cant be less than or equal to zero");
            }
            if (staffId <= 0)
            {
                throw new ArgumentException("staff id cannot be less than or equal to zero");
            }
            if (totalAmount < 0)
            {
                throw new ArgumentException("Total Amount cant be less than zero");
            }

            //Buisness Rule Validation
            if (orderType == OrderType.DINE_IN && tableNumber == null)
            {
                throw new ArgumentException("DINE IN order must have a table.");
            }
            if (orderType == OrderType.TAKEAWAY && tableNumber != null)
            {
                throw new ArgumentException("Takeawat order cannot have a table.");
            }

            OrderId = orderId;
            OrderType = orderType;
            Status = status;
            TableNumber = tableNumber;
            StaffId = staffId;
            CreatedAt = createdAt;
            CompletedAt = completedAt;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;

        }
    }
}

// Example
///Order 101
///DINE_IN
//Table 5
//Waiter Rahul
//Automatically
//Status = PENDING
//CreatedAt = NOW
//PaymentMethod = null
//CompletedAt = null
//Total = 0

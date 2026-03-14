using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class Receipt
    {
        public int ReceiptId {  get; private set; }

        public int OrderId {  get; private set; }

        public decimal SubTotal {  get; private set; }//before tax

        public decimal TaxAmount {  get; private set; }

        public decimal TotalAmount { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public DateTime PaidAt {  get; private set; }

        public Receipt(int receiptId, int orderId, decimal subTotal, decimal taxAmount, decimal totalAmount, PaymentMethod paymentMethod)
        {
            if (receiptId <= 0)
            {
                throw new ArgumentException("reciept Id cannot be less than or equal to zero ");
            }
            if (orderId <= 0)
            {
                throw new ArgumentException("Order Id cannot be less than or eqaul to zero");
            }
            if(subTotal < 0)
            {
                throw new ArgumentException("Sub Total cannot be less than or eqaul to zero");
            }
            if (taxAmount<0)
            {
                throw new ArgumentException("Tax Amount cannot be less than or eqaul to zero");
            }
            if (totalAmount <= 0)
            {
                throw new ArgumentException("total amount cannot be less than or eqaul to zero");
            }
            if (totalAmount < subTotal)
            {
                throw new ArgumentException("sub total cannot be greater than total amount");
            }
            if (totalAmount != subTotal + taxAmount)
            {
                throw new ArgumentException("Total amount must equal subtotal plus tax");
            }
            
            ReceiptId = receiptId;
            OrderId = orderId;
            SubTotal = subTotal;
            TaxAmount = taxAmount;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            PaidAt = DateTime.Now;
        }
        public Receipt(int receiptId, int orderId, decimal subTotal, decimal taxAmount, decimal totalAmount, PaymentMethod paymentMethod,DateTime paidAt)
        {
            if (receiptId <= 0)
            {
                throw new ArgumentException("reciept Id cannot be less than or equal to zero ");
            }
            if (orderId <= 0)
            {
                throw new ArgumentException("Order Id cannot be less than or eqaul to zero");
            }
            if (subTotal < 0)
            {
                throw new ArgumentException("Sub Total cannot be less than or eqaul to zero");
            }
            if (taxAmount < 0)
            {
                throw new ArgumentException("Tax Amount cannot be less than or eqaul to zero");
            }
            if (totalAmount <= 0)
            {
                throw new ArgumentException("total amount cannot be less than or eqaul to zero");
            }
            if (totalAmount < subTotal)
            {
                throw new ArgumentException("sub total cannot be greater than total amount");
            }
            if (totalAmount != subTotal + taxAmount)
            {
                throw new ArgumentException("Total amount must equal subtotal plus tax");
            }

            ReceiptId = receiptId;
            OrderId = orderId;
            SubTotal = subTotal;
            TaxAmount = taxAmount;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            PaidAt =paidAt;
        }
    }

}

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace myYardSale.Models
{
    public class Customer
    {
        [Key]
        public long customerId { get; set; }

        [Required]
        [Display(Name="")]
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }

    }

    public class Product
    {
        [Key]
        public long productId { get; set; }

        [Required]
        [Display(Name="Product's Name: ")]
        public string product_name { get; set; }

        [Required]
        [Display(Name="Description: ")]
        public string description { get; set; }

        [Required]
        [Display(Name="Quantity/How Many: ")]
        public int product_quantity { get; set; }

        [Display(Name="Image Url: ")]
        public string image { get; set; }
        public List<Order> Order { get; set; }

        public Product()
        {
            Order = new List<Order>();

        }
    }

    public class Order
    {
        [Key]
        public long orderId { get; set; }
        public int order_quantity { get; set; }
        public DateTime created_at { get; set; }
        public long customerId { get; set; }
        public Customer Customer { get; set; }
        public long productId { get; set; }
        public Product Product { get; set; }

        public Order()
        {
            created_at = DateTime.Now;
        }
    }
}
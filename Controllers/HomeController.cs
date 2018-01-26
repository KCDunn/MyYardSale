using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
       private StoreContext _context;

        public HomeController(StoreContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            ViewBag.LatestProducts = _context.products.OrderByDescending(p => p.productId).Take(4).ToList();
            ViewBag.LatestOrders = _context.orders.OrderByDescending(date => date.created_at).Take(3)
                .Include(o => o.Product).ToList();
            ViewBag.LatestCustomers = _context.customers.OrderByDescending(date => date.created_at).Take(4).ToList();
            return View();
        } 
        
        [Route("orders")]
        public IActionResult Orders()
        {
            List<Customer> allCustomers = _context.customers.OrderBy(c => c.name).ToList();
            List<Product> allProducts = _context.products.OrderBy(p => p.product_name).ToList();
            ViewBag.Customers = allCustomers;
            ViewBag.Products = allProducts;

            List<Order> allOrders = _context.orders.ToList();
            ViewBag.Orders = allOrders;
            
            // List<Order> allOrders = _context.orders.OrderByDescending(order => order.orderId).ToList();
            // ViewBag.Orders = allOrders;

            return View();
        } 

        [Route("products")]
        public IActionResult Products()
        {
            List<Product> AllProducts = _context.products.ToList();
            ViewBag.AllProducts = AllProducts;
            return View();
        }
        
        [Route("show/{prodId}")]
        public IActionResult Show(int prodId)
        {
            ViewBag.thisProduct = _context.products.SingleOrDefault(p => p.productId == prodId);
            return View();
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult Customers()
        {
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.AllCustomers = AllCustomers;
            return View();
        } 

        [Route("settings")]
        public IActionResult Settings() => View();
         

        [HttpPost]
        [Route("NewCustomer")]
        public IActionResult NewCustomer(Customer newCust)
        {
            // if (newCust == null)
            // {
            //     throw new ArgumentNullException(nameof(newCust));
            // }

            if (ModelState.IsValid)
            {
                Customer newCustomer = new Customer
                {
                    name = newCust.name,
                    created_at = DateTime.Now
                };
                _context.Add(newCustomer);
                _context.SaveChanges();
                // return RedirectToAction("Customers");
            }
            List<Customer> AllCustomers = _context.customers.ToList();
            ViewBag.AllCustomers = AllCustomers;
            return View("Customers");
        }

        [HttpPost]
        [Route("newProduct")]
        public IActionResult NewProduct(Product newProd)
        {
            if(ModelState.IsValid)
            {
                
                Product thisProduct = new Product
                {
                    product_name = newProd.product_name,
                    description = newProd.description,
                    image = newProd.image,
                    product_quantity = newProd.product_quantity
                };
                _context.Add(thisProduct);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }

            return View("Products");
        }

        [HttpPost]
        [Route("newOrder")]
        public IActionResult NewOrder(Order newOrd)
        {
            if(ModelState.IsValid)
            {
                
                Product thisProduct = _context.products.SingleOrDefault(p => p.productId == newOrd.productId);
                
                Order thisOrder = new Order{
                customerId = newOrd.customerId,
                order_quantity = newOrd.order_quantity,
                productId = newOrd.productId
            };
            _context.orders.Add(thisOrder);
            thisProduct.product_quantity -= newOrd.order_quantity;
            _context.SaveChanges();
            
            return RedirectToAction("Orders");
                
                
            }
            return View("Orders");

        }

        
    }
}

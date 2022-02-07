using ITSCaseAPI.Context;
using ITSCaseAPI.DTO;
using ITSCaseAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITSCaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly RetailCompanyContext _context;
        private readonly ElasticClient _client;


        public CustomerOrderController(RetailCompanyContext context, ElasticClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: api/<CustomerOrderController>
        [HttpPost("order")]
        public ActionResult<CustomerOrder> CreateCustomerOrder(CustomerOrderDto customerOrderDto)
        {
            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder.Customer = new Customer();
            customerOrder.Customer = _context.Customer.Find(customerOrderDto.Customer.CustomerId);
            if (customerOrder.Customer != null)
            {
                customerOrder.OrderItems = new List<OrderItem>();
                foreach (var item in customerOrderDto.OrderItems)
                {
                    OrderItem newItem = new OrderItem();
                    newItem.Product = _context.Product.Find(item.ProductId);
                    newItem.Quantity = item.Quantity;
                    customerOrder.OrderItems.Add(newItem);
                }

                _context.CustomerOrder.Add(customerOrder);
                _context.SaveChanges();
                return CreatedAtAction("GetCustomerOrder", new { customerOrderId = customerOrder.CustomerOrderId }, customerOrder);

            }
            return NoContent();

        }

        [HttpGet("order/{CustomerOrderId}")]
        public ActionResult<CustomerOrder> GetCustomerOrder(int customerOrderId)
        {
            CustomerOrder customerOrder = _context.CustomerOrder.Find(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound();
            }
            return customerOrder;
        }
        [HttpDelete("order/{CustomerOrderId}")]
        public ActionResult<CustomerOrder> DeleteCustomerOrder(int customerOrderId)
        {
            CustomerOrder customerOrder = _context.CustomerOrder.Find(customerOrderId);
            if (customerOrder == null)
            {
                return NotFound();
            }
            _context.OrderItem.RemoveRange(_context.OrderItem.Where(item => item.CustomerOrder.CustomerOrderId == customerOrderId)); //CustumerOrdera bağlı itemların silinmesi
            _context.CustomerOrder.Remove(customerOrder);
            _context.SaveChanges();
            return customerOrder;
        }

        [HttpPut("order")]
        public ActionResult<CustomerOrder> ChangeCustomerOrder(CustomerOrderDto order)
        {
            CustomerOrder customerOrder = _context.CustomerOrder.Find(order.CustomerOrderId);
            Customer customer = _context.Customer.Find(order.Customer.CustomerId);
            if (customerOrder == null || customer == null)
            {
                return NoContent();
            }
            customerOrder.Customer.Address = order.Customer.Address;
            customerOrder.Customer.Name = order.Customer.Name;
            customerOrder.OrderItems = new List<OrderItem>();

            foreach (var item in order.OrderItems)
            {
                if (_context.Product.Find(item.ProductId) != null)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.Product = _context.Product.Find(item.ProductId);
                    orderItem.Quantity = item.Quantity;
                    customerOrder.OrderItems.Add(orderItem);
                }


            }

            _context.CustomerOrder.Update(customerOrder);
            _context.SaveChanges();

            return Ok(order);
        }

        [HttpPost("order/{customerOrderId}/product")]
        public ActionResult<CustomerOrder> AddNewProductToCustomerOrder(int customerOrderId, List<OrderItemDto> orderItems)
        {

            CustomerOrder customerOrder = _context.CustomerOrder.Find(customerOrderId);
            if (customerOrder == null)
            {
                return NoContent();
            }
            customerOrder.OrderItems = new List<OrderItem>();
            customerOrder.OrderItems.AddRange(_context.OrderItem.Where(item => item.CustomerOrder.CustomerOrderId == customerOrderId).Include(p => p.Product));
            foreach (var item in orderItems)
            {
                if (_context.Product.Find(item.ProductId) != null)
                {
                    OrderItem orderItem = new OrderItem();
                    if (customerOrder.OrderItems.Any(i => i.Product.Id == item.ProductId))
                    {
                        customerOrder.OrderItems.Where(i => i.Product.Id == item.ProductId).First().Quantity += item.Quantity;
                    } else
                    {
                        orderItem.Product = _context.Product.Find(item.ProductId);
                        orderItem.Quantity = item.Quantity;
                        customerOrder.OrderItems.Add(orderItem);
                    }

                }
                else
                {
                    return NoContent();
                }

            }
            _context.CustomerOrder.Update(customerOrder);
            _context.SaveChanges();
            return Ok(customerOrder);
        }
        [HttpDelete("order/{customerOrderId}/product")] 
        public ActionResult<CustomerOrder> DeleteProductToCustomerOrder(int customerOrderId, List<OrderItemDto> orderItems)
        {
            CustomerOrder customerOrder = _context.CustomerOrder.Find(customerOrderId);
            if (customerOrder == null)
            {
                return NoContent();
            }
            customerOrder.OrderItems = new List<OrderItem>();
            customerOrder.OrderItems.AddRange(_context.OrderItem.Where(item => item.CustomerOrder.CustomerOrderId == customerOrderId).Include(p => p.Product));
            foreach (var item in orderItems)
            {
                if (_context.Product.Find(item.ProductId) != null)
                {
                    OrderItem orderItem = new OrderItem();
                    if (customerOrder.OrderItems.Any(i => i.Product.Id == item.ProductId))
                    {
                        if (customerOrder.OrderItems.Where(i => i.Product.Id == item.ProductId).First().Quantity > item.Quantity)
                        {
                            customerOrder.OrderItems.Where(i => i.Product.Id == item.ProductId).First().Quantity -= item.Quantity;
                        }
                        else
                        {
                            customerOrder.OrderItems.RemoveAll(i => i.Product.Id == item.ProductId);
                        }
                        
                    }
                    else
                    {
                        continue;
                    }

                }
                else
                {
                    return NoContent();
                }

            }
            _context.CustomerOrder.Update(customerOrder);
            _context.SaveChanges();
            return Ok(customerOrder);

        }
        

        [HttpGet("products")]
        public ActionResult<Product> SearchInProducts(string productTitle)
        {
            var products = _context.Product.ToList(); //Elastic Search için productların importu
            var bulkResponse = _client.Bulk(b => b
                .IndexMany(products)
            );

            var searchedProducts = _context.Product.Where(p => p.Title.ToLower().Contains(productTitle)).ToList();
            //var searchResponse = _client.Search<Product>(s => s.Query(q => q.QueryString(r => r.DefaultField(f => f.Title).Name(productTitle))));
            //var searchResponse = searchResponse.Documents.ToList();

            return Ok(searchedProducts);

        }

    }
}

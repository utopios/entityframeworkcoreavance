using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cqrs.Models;

namespace cqrs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetOrders()
        {
            // return await _context.Orders.AsNoTracking()
            //     .Include(o => o.Customer)
            //     .Include(o => o.OrderItems)
            //     .ThenInclude(p =>p.Product)
            //     .ToListAsync();
            // var result = (from o in _context.Orders
            //     join oi in _context.OrderItems on o.Id equals oi.OrderId
            //     join p in _context.Products on oi.ProductId equals p.Id
            //     
            //     select new
            //     {
            //         Order = o,
            //         OrderItem = oi,
            //         Product = p
            //     }).ToList();
            // return result;
            
            var result = await (from o in _context.Orders
                join c in _context.Customers on o.CustomerId equals c.Id
                join oi in _context.OrderItems on o.Id equals oi.OrderId into orderItems
                from oi in orderItems.DefaultIfEmpty()
                join p in _context.Products on oi.ProductId equals p.Id into products
                from p in products.DefaultIfEmpty()
                select new
                {
                    OrderId = o.Id,
                    Customer = new
                    {
                        c.Id,
                        c.Name
                    },
                    OrderItem = oi == null ? null : new
                    {
                        oi.Id,
                        oi.Quantity,
                        Product = p == null ? null : new
                        {
                            p.Id,
                            p.Name,
                            p.Price
                        }
                    }
                }).ToListAsync();
            return result;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            // return _context.Orders.Include(o => o.Customer)
            //     .Include(o => o.OrderItems)
            //     .ThenInclude(p => p.Product).First();
            var order = _context.Orders.First();
            await _context.Entry(order).Reference(o => o.Customer).LoadAsync();
            var customerName = order.Customer.Name;
            _context.Entry(order)
                .Collection(o => o.OrderItems)
                .Query()
                .Include(oi => oi.Product)
                .Load();
            return order;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

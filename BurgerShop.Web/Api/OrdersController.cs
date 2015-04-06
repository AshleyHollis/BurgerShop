using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using BurgerShop.Core.Models;
using BurgerShop.Data;

namespace App.BurgerShop.Web.Api
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController()
        {
            _orderService = new OrderService();
        }

        public OrdersController(IOrderService orderService)
        {
            if (orderService == null) throw new ArgumentNullException("orderService");
            _orderService = orderService;
        }

        // GET: api/Orders
        public IEnumerable<OrderDTO> GetOrders()
        {
            return _orderService.GetAll();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrderDTO))]
        public IHttpActionResult GetOrder(int id)
        {
            OrderDTO order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //// PUT: api/Orders/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutOrder(int id, OrderDTO order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Orders
        [ResponseType(typeof(OrderDTO))]
        public IHttpActionResult PostOrder(OrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrder = _orderService.Create(order);

            return CreatedAtRoute("DefaultApi", new { id = createdOrder.Id }, createdOrder);
        }

        //// DELETE: api/Orders/5
        //[ResponseType(typeof(OrderDTO))]
        //public IHttpActionResult DeleteOrder(int id)
        //{
        //    OrderDTO order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Orders.Remove(order);
        //    db.SaveChanges();

        //    return Ok(order);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool OrderExists(int id)
        //{
        //    return db.Orders.Count(e => e.Id == id) > 0;
        //}
    }
}
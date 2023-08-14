using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly NorthwindDbContext _context;
        private readonly string apiAddress = "https://localhost:44373";
        public OrdersController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {


            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {
                List<Order> orders = new List<Order>();
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Order";
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    orders = JsonConvert.DeserializeObject<List<Order>>(Response);

                }
                return View(orders);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Order? e = new Order();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Order/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    e = JsonConvert.DeserializeObject<Order>(Response);

                }
                return View(e);

            }
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            
            string Baseurl = apiAddress;
            string request = "api/Order";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.PostAsJsonAsync<Order>(request,order);


                if (Res.IsSuccessStatusCode)
                {


                    return RedirectToAction("Index");

                }
                return BadRequest();
            }
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Order? e = new Order();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Order/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    e = JsonConvert.DeserializeObject<Order>(Response);

                }
                return View(e);

            }
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,  Order order)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Order/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.PutAsJsonAsync<Order>(request, order);

                // check for con success 
                if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return RedirectToAction("Index");

                }
                else { return BadRequest(); }
            }
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Order?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.DeleteAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    return RedirectToAction("Index");

                }
                else return BadRequest();

            }
        }

        private bool OrderExists(int? id)
        {
          return (_context.Orders?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindDbContext _context;
        private readonly string apiAddress = "https://localhost:44373";

        public CustomersController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {
                List<Customer> customers = new List<Customer>();
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Customer";
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    customers = JsonConvert.DeserializeObject<List<Customer>>(Response);
                    return View(customers);

                }
                else return NotFound(); 
            }
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            Customer? c = new Customer();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Customer/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Customer>(Response);

                }
                return View(c);

            }
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Customer customer)
        {
            string Baseurl = apiAddress;
            string request = "api/Customer";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.PostAsJsonAsync<Customer>(request, customer);


                if (Res.IsSuccessStatusCode)
                {


                    return RedirectToAction("Index");

                }
                return BadRequest();
            }
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {
                Customer c = new Customer();
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Customer/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Customer>(Response);

                }
                return View(c);

            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Customer customer)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Customer/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.PutAsJsonAsync<Customer>(request, customer);

                // check for con success 
                if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return RedirectToAction("Index");

                }
                else { return BadRequest(); }
            }
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            Customer? c = new Customer();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Customer/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Customer>(Response);

                }
                return View(c);

            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Customer?id=" + id;
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

        private bool CustomerExists(string id)
        {
          return (_context.Customers?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

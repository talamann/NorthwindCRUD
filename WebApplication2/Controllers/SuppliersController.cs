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
    public class SuppliersController : Controller
    {
        private readonly NorthwindDbContext _context;
        private readonly string apiAddress = "https://localhost:44373";
        public SuppliersController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {
                List<Supplier>? supps = new List<Supplier>();
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Supplier";
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    supps = JsonConvert.DeserializeObject<List<Supplier>>(Response);
                    return View(supps);

                }
                else return NotFound();
            }
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Supplier? c = new Supplier();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Supplier/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Supplier>(Response);

                }
                return View(c);

            }
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Supplier supplier)
        {
            string Baseurl = apiAddress;
            string request = "api/Supplier";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.PostAsJsonAsync<Supplier>(request, supplier);


                if (Res.IsSuccessStatusCode)
                {


                    return RedirectToAction("Index");

                }
                return BadRequest();
            }
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Supplier? c = new Supplier();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Supplier/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Supplier>(Response);

                }
                return View(c);

            }
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supplier supplier)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Supplier/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.PutAsJsonAsync<Supplier>(request, supplier);

                // check for con success 
                if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return RedirectToAction("Index");

                }
                else { return BadRequest(); }
            }
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Supplier?id=" + id;
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

        private bool SupplierExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

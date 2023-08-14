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
    public class CategoriesController : Controller
    {
        private readonly NorthwindDbContext _context;
        private readonly string apiAddress = "https://localhost:44373";
        public CategoriesController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {
                List<Category> ctg = new List<Category>();
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Categories";
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    ctg = JsonConvert.DeserializeObject<List<Category>>(Response);
                    return View(ctg);

                }
                else return NotFound();
            }
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Category? c = new Category();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Categories/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Category>(Response);

                }
                return View(c);

            }
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            string Baseurl = apiAddress;
            string request = "api/Categories";
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.PostAsJsonAsync<Category>(request, category);


                if (Res.IsSuccessStatusCode)
                {


                    return RedirectToAction("Index");

                }
                return BadRequest();
            }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Category? c = new Category();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Categories/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Category>(Response);

                }
                return View(c);

            }
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Categories/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.PutAsJsonAsync<Category>(request, category);

                // check for con success 
                if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return RedirectToAction("Index");

                }
                else { return BadRequest(); }
            }
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Category? c = new Category();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Categories/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    c = JsonConvert.DeserializeObject<Category>(Response);

                }
                return View(c);

            }
        }

        // POST: Categories/Delete/5
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
                string request = "api/Categories?id="+id;
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

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}

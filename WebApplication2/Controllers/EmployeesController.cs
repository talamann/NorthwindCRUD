using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly NorthwindDbContext _context;
        private readonly string apiAddress = "https://localhost:44373";

        public EmployeesController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<Employee> e = new List<Employee>();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Employee";
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    e = JsonConvert.DeserializeObject<List<Employee>>(Response);
                    return View(e);
                }
                else return BadRequest();

            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                Employee e = new Employee();
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Employee/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);
                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    e = JsonConvert.DeserializeObject<Employee>(Response);
                    return View(e);

                }
                else return NotFound();
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
           Employee? e = new Employee();

            string Baseurl = apiAddress;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "api/Employee";
                //get data from nw api 
                HttpResponseMessage Res = await client.PostAsJsonAsync<Employee>(request,employee);

                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    return RedirectToAction("Index");

                }
                else return BadRequest();

            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                Employee e = new Employee();
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Employee/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);
                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    e = JsonConvert.DeserializeObject<Employee>(Response);
                    return View(e);

                }
                else return NotFound();
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,lastName,firstName,title,titleOfCourtesy,birthDate,hireDate,notes,reportsTo")] Employee employee)
        {
            using (var client = new HttpClient())
            {
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Employee?id="+id;
                //get data from nw api 
                HttpResponseMessage Res = await client.PutAsJsonAsync<Employee>(request, employee);

                // check for con success 
                if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
                {
                    return RedirectToAction("Index");

                }
                else { return BadRequest(); }
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                Employee e = new Employee();
                string Baseurl = apiAddress;
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string request = "/api/Employee/id?id=" + id;
                //get data from nw api 
                HttpResponseMessage Res = await client.GetAsync(request);
                // check for con success 
                if (Res.IsSuccessStatusCode)
                {

                    //Storing the response details recieved from web api   
                    var Response = Res.Content.ReadAsStringAsync().Result;


                    e = JsonConvert.DeserializeObject<Employee>(Response);
                    return View(e);

                }
                else return NotFound();
            }
        }

        // POST: Employees/Delete/5
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
                string request = "api/Employee?id=" + id;
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

        private bool EmployeeExists(int? id)
        {
          return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

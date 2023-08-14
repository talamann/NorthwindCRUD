
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using ConsoleApp2;

string apiAddress = "https://localhost:44373";
string Baseurl = apiAddress;
Employee e = new Employee();
e.Id = 0;
e.firstName = "s";
e.lastName = "m";
e.title = "s";
e.titleOfCourtesy = "s";
e.reportsTo = "s";
e.birthDate = "s";
e.hireDate = "s";
e.notes = "s";

using (var client = new HttpClient())
{

    client.BaseAddress = new Uri(Baseurl);

    client.DefaultRequestHeaders.Clear();
    //Define request data format  
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    string request = "/api/Employee?id=0";
    //get data from nw api 
    HttpResponseMessage Res = await client.PutAsJsonAsync<Employee>(request, e);

    // check for con success 
    if (Res.StatusCode.Equals(HttpStatusCode.NoContent))
    {
        Console.WriteLine("Put request success");

    }
    else { Console.WriteLine(Res.StatusCode.ToString()); }
}
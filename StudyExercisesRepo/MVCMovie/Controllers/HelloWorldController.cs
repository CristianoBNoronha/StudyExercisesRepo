using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace MVCMovie.Controllers;

public class HelloWorldController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Hello " + name;
        ViewData["NumTimes"] = numTimes;

        return View();

        //As suggestion never uses string interpolation.  
        //Uses HtmlEncoder.Default.Encode to protect the app from malicious input, such as through JavaScript.
        //return HtmlEncoder.Default.Encode($"Hello {name}, Numtimes is: {numTimes}");
    }
}
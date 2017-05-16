using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ADO_Practice.Models;


namespace ADO_Practice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // connection string - need ip address, port number, database name, user name & password...
            //SqlConnection c = new SqlConnection("Server=.\\sqlexpress;northwind;Trusted_Connection-True;");

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = c;

            //cmd.CommandText = "select count(*) from customers";
            //***********to SEE HOW CUSTOMERS USING ENTITIY FRAMEWORK ******
            //cmd.CommandType = System.Data.CommandType.Text;

            //c.Open();

            //int result = int.Parse(cmd.ExecuteScalar().ToString());

            return View();
        }

    public ActionResult About()
        {
            NorthwindEntities NorthwindDB = new NorthwindEntities();

            NorthwindDB.Customers.Count();

            int count = NorthwindDB.Customers.Count();

            ViewBag.Message = $"We have {count} Customers.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
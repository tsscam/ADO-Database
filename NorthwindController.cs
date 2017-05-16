using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Practice.Models;
using System.Data.SqlClient;

namespace ADO_Practice.Controllers
{
    public class NorthwindController : Controller
    {
        // GET: Northwind
        public ActionResult ListAllCustomers()
        {
            NorthwindEntities NE = new NorthwindEntities();

            //select * from customers
            List<Customer> CustomerList = NE.Customers.ToList();

            ViewBag.Cities = GetAllCities();

            ViewBag.CustomerList = CustomerList;

            return View();
        }
        public ActionResult SearchCustomers(string CustomerID)
        {
            NorthwindEntities NE = new NorthwindEntities();

            List<Customer> CustomerList = NE.Customers.Where(x => x.
            CustomerID.ToUpper().
            Contains(CustomerID.ToUpper())).ToList();

            //foreach example.......
            /* 
             * List<Customer> CustomerList = new List<Customer>();
                foreach (Customer element in NE.Customers)
            {
              if (element.CustomerID.ToUpper().Contains(CustomerID.ToUpper())).
                       
            }
                CustomerList.Add(element);           
             
             */
            ViewBag.Cities = GetAllCities();

            ViewBag.CustomerList = CustomerList;

            return View("ListAllCustomers");

        }
        public ActionResult SearchCustomersbyCity(string City)
        {
            NorthwindEntities NE = new NorthwindEntities();

            List<Customer> CustomerList = NE.Customers.Where(x => x.
            City.ToUpper().
            Contains(City.ToUpper())).ToList();

            ViewBag.Cities = GetAllCities();
            ViewBag.CustomerList = CustomerList;

            return View("ListAllCustomers");
        }
        public ActionResult DeleteCustomer(string CustomerID)
        {
            try
            {
                if (CustomerID == null)

                {
                    ViewBag.ErrorMessage("Customer ID incorect");
                    return View("ErrorMessage");
                }

            NorthwindEntities NE = new NorthwindEntities();
                //step #1 "find the custoemr that I need to delete....//
                Customer ToDelete = NE.Customers.Find(CustomerID);
                if(ToDelete == null)
                {
                    ViewBag.ErrorMessage = "Unavailable";
                    return View("ErrorMessage");
                }

            //removes OBJECTS from DATABASE
            
            NE.Customers.Remove(ToDelete);

            //to SAVE EVERYTHING TO DATABASE.....
            NE.SaveChanges();

            //REDIRECT....after DELETE...GO TO LIST ALL CUSTOMER ..
            //EXECUTE the LISTALLCUSTOMER action
            return RedirectToAction("ListAllCustomers", "Northwind");
        }
        catch (System.Data.Entity.Infrastructure.DbUpdateException)
        {

            ViewBag.ErrorMessage = "You Cannot delete a customer with ORDERS";
            return View("ErrorMessage");
        }
          catch (Exception)
            {
                ViewBag.ErrorMessage = "OOOOPS....Something happened unexpected. Try Again!";
                return View("ErrorMessage");
            }
        }
        public ActionResult UpdateCustomer(string CustomerID)
        {
            //first thing...TO SHOW DATA

            NorthwindEntities NE = new NorthwindEntities();

            //update will SHOW up & THE MODEL will be updated......
            Customer ToFind = NE.Customers.Find(CustomerID);

            return View("CustomerDetails", ToFind);
        }
        public List<string> GetAllCities()
        {
            NorthwindEntities NE = new NorthwindEntities();


            return NE.Customers.Select(x => x.City).Distinct().ToList();
          

        }
        public ActionResult SaveUpdates(Customer ToBeUpdated)
        {
            NorthwindEntities NE = new NorthwindEntities();
            //find the original customer record
            Customer ToFind = NE.Customers.Find(ToBeUpdated.CustomerID);

            ToFind.ContactName = ToBeUpdated.ContactName;

            ToFind.Address = ToBeUpdated.Address;

            ToFind.City = ToBeUpdated.City;

            ToFind.ContactTitle = ToBeUpdated.ContactTitle;

            ToFind.Phone = ToBeUpdated.Phone;

            ToFind.Fax = ToBeUpdated.Fax;

            ToFind.CompanyName = ToBeUpdated.CompanyName;

            NE.SaveChanges();
            return RedirectToAction("ListAllCustomers");

        }
        public ActionResult AddCustomer()
        {
            return View();
            

        }
        public ActionResult SaveNewCustomer(Customer NewCustomer)
        {
            // TO DO VALIDATION!!!!! //
            NorthwindEntities NE = new NorthwindEntities();

            NE.Customers.Add(NewCustomer);

            NE.SaveChanges();

            return RedirectToAction("ListAllCustomers");

        }
    }
}
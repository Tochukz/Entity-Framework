using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DBFirst.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                List<Employee> employees = entities.Employees.ToList();
                return View(employees);
            }
        }

        public ActionResult Details(int employeeId)
		{
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                Employee employee = entities.Employees.Find(employeeId);
                return View(employee);
			}
		}

        public ActionResult Create()
		{
            return View();
		}

        [HttpPost]
        public ActionResult Create(string employeeName)
		{
            return View();
		}

        public Employer GetEmployerByEmployeeId(int employeeId)
		{
            Employer employer = null;
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                Employee employee = entities.Employees.Find(employeeId);
                if (employee != null)
				{
                    employer = employee.Employer;
				}
			}
            return employer;
		}
    }
}
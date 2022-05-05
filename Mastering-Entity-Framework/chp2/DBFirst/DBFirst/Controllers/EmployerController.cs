using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBFirst.Controllers
{
    public class EmployerController : Controller
    {
        public ActionResult Index()
        {
            using (DBFirstEntities entites = new DBFirstEntities())
			{
                List<Employer> employers = entites.Employers.ToList();
                return View(employers);
            }
            
        }

        public JsonResult Employer(int employerId)
		{
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                Employer employer = entities.Employers.Find(employerId);
                return Json(employer);
			}
		}

        [HttpPost]
        public JsonResult Create(string name)
		{
            using (DBFirstEntities entities = new DBFirstEntities())
			{             
                Employer employer = entities.Employers.Add(new Employer { EmployerName = name });
                entities.SaveChanges();
                return Json(employer);                
			}
		}

        [HttpPut]
        public JsonResult Update(FormCollection formCollection)
		{
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                int employerId = int.Parse(formCollection["id"]);
                Employer employer = entities.Employers.Find(employerId);
                if (employer == null)
				{
                    return Json(new { });
				}
                employer.EmployerName = formCollection["name"];
                entities.SaveChanges();
                return Json(employer);
			}
		}

        [HttpDelete]
        public JsonResult Delete(int employerId)
        {
            using (DBFirstEntities entities = new DBFirstEntities())
            {
                Employer employer = entities.Employers.Find(employerId);
                entities.Employers.Remove(employer);
                entities.SaveChanges();
                return Json(new { });
            }
        }

        public JsonResult Employees(int employerId)
		{
            List<Employee> employees = null;
            using (DBFirstEntities entities = new DBFirstEntities())
			{
                Employer employer = entities.Employers.Find(employerId);
                if (employer != null)
				{
                    employees = employer.Employees.ToList();
				}
			}
            return Json(employees);
		}
                
        /*
        public JsonResult AddEmployee(Employee modifiedEmployee, int employerId)
        {
            using (DBFirstEntities entities = new DBFirstEntities())
            {
                Employer employer = entities.Employers.Find(employerId);
                Employee employee = employer.Employees.FirstOrDefault(emp => emp.Id == modifiedEmployee.Id);
                if (employee != null)
                {
                    employee.EmployeeName = modifiedEmployee.EmployeeName;
                }
                else
                {
                    employer.Employees.Add(modifiedEmployee);
                }
                entities.SaveChanges();
                return Json(employee);
            }
        }

        */

        [HttpPost]
        public JsonResult AddEmployee(int employerId, FormCollection formCollection)
        {
            using (DBFirstEntities entites = new DBFirstEntities())
            {
                Employer employer = entites.Employers.Find(employerId);
                Employee employee = new Employee
                {
                    EmployeeName = formCollection["name"]
                };
                // If the employee record already exists, in the DB, then it's employerId will be updated, otherwise a new record will be created with the employerId
                employer.Employees.Add(employee);
                entites.SaveChanges();
                return Json(employee);
            }
        }

        [HttpPut]
        public JsonResult UpdateEmployee(int employerId, FormCollection formCollection)
		{
            using (DBFirstEntities entities =  new DBFirstEntities())
			{
                Employer employer = entities.Employers.Find(employerId);
                int employeeId = int.Parse(formCollection["Id"]);
                Employee employee = employer.Employees.FirstOrDefault(emp => emp.Id == employeeId);
                employee.EmployeeName = formCollection["name"];
                employee.EmployerId = int.Parse(formCollection["employerId"]);
                entities.SaveChanges();
                return Json(employee);
			}
		}

        [HttpDelete]
        public JsonResult DeleteEmployee(int employeeId, int employerId)
        {
            using (DBFirstEntities entities = new DBFirstEntities())
            {
                Employer employer = entities.Employers.Find(employerId);
                entities.Employers.Remove(employer);
                Employee employee = employer.Employees.FirstOrDefault(emp => emp.Id == employeeId);
                if (employee != null)
                {
                    employer.Employees.Remove(employee);
                }
                //If you want to remove the relationship between the entities 
                // employer.Employees = null;
                entities.SaveChanges();
                return Json(new { });
            }
        }       
    }
}

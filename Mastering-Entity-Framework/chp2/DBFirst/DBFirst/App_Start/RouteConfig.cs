using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DBFirst
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"employer",
				"employers/{id}",
				new { controller = "Employer", action = "Employer" }
			);
			routes.MapRoute(
			    "create-employer",
				"employers/create",
				new {controller = "Employer", action = "Create"}
			);
			routes.MapRoute(
				"update-employer",
				"employers/update",
				new { controller = "Employer", action = "Update" }
			);
			routes.MapRoute(
				"delete-employer",
				"employers/delete/{id}",
				new { controller = "Employer", action = "Delete"}
			);
		    routes.MapRoute(
				"employer-employees",
				"employer/{employerId}/employees",
				new { controlle = "Employer", action = "Employees", employerId = 0 }
			);
			routes.MapRoute(
			  "employer-add-employee",
			  "employer/{employerId}",
			  new { controller = "Employer", action = "AddEmployee", employerId = 0 }
			);
			routes.MapRoute(
			  "employer-update-employee",
			  "employer/{employerId}",
			  new { controller = "Employer", action = "UpdateEmployee", employerId = 0 }
			);
			routes.MapRoute(
				"employer-delete-employee",
				"employer/delete/{employeeId}/employerId",
				new { controller = "Employer", acion = "DeleteEmployee", employeeId = 0, employerId = 0 }
			);
			routes.MapRoute(
				name: "employers",
				url: "employers",
				defaults: new { controller = "Employer", action = "Index" }
			);
			
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}

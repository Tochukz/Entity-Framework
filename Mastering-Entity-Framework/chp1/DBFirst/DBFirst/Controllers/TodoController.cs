using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DBFirst.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {

            /*
            DBFirstEntities entity = new DBFirstEntities();
            //Lazy loading
            IEnumerable<Todo> todosX = entity.Todos;
            return View(todosX);
            */
           
            using (DBFirstEntities entity = new DBFirstEntities())
			{                
                // Eager loading 
                IEnumerable<Todo> todosY = entity.Todos.ToList();
                          
                //todosY 
                return View(todosY);
            }
             
        }


        public ActionResult Details(int? id = 0)
		{
            using (DBFirstEntities entity = new DBFirstEntities())
			{
                // Todo todo = entity.Todos.Find(id);
                // Todo todo = entity.Todos.FirstOrDefault(item => item.Id == id);
                Todo todo = entity.Todos.SingleOrDefault(item => item.Id == id); 
                if (todo == null)
				{
                    return View(new Todo());
				}
;               return View(todo);

            }
        }

        [HttpPost]
        public ActionResult Create(string TodoItem, bool IsDone)
		{

            using (DBFirstEntities entity = new DBFirstEntities())
            {
                Todo newTodo = new Todo() { IsDone = IsDone, TodoItem = TodoItem };
                entity.Todos.Add(newTodo);
                entity.SaveChanges();
                return RedirectToAction("Details", new { id = newTodo.Id });
            }
            
		}

        public ActionResult Create()
		{
            return View();
		}

        public ActionResult Edit(int? id = 0)
		{
            using(DBFirstEntities entity = new DBFirstEntities())
			{
                Todo todo = entity.Todos.Find(id);
                if (todo == null)
				{
                    return View(new Todo());
				}
                return View(todo);
			}
		}

        [HttpPost]
        public ActionResult Edit(int Id, string TodoItem, bool IsDone)
		{
            /*
            using (DBFirstEntities entity = new DBFirstEntities())
			{
                Todo todo = entity.Todos.Find(Id);
                todo.TodoItem = TodoItem;
                todo.IsDone = IsDone;
                entity.SaveChanges();
                return RedirectToAction("Details", new { id = Id });
			}
            */
            
            // To perform the update in a situation where the entities are passed through multiple layers
            using (DBFirstEntities entity = new DBFirstEntities())
			{
                Todo todo = entity.Todos.Find(Id);
                todo.TodoItem = TodoItem;
                todo.IsDone = IsDone;
                entity.Entry(todo).State = EntityState.Modified; // The model is attached to the DBContext object and marked as modified
                entity.SaveChanges();
                return RedirectToAction("Details", new { id = Id });
            }
            
		}

        public ActionResult Delete(int Id)
		{
            using (DBFirstEntities entity = new DBFirstEntities())
			{
                Todo todo = entity.Todos.Find(Id);
                entity.Todos.Remove(todo);
                entity.SaveChanges();
                return RedirectToAction("Index");
			}
		}
    }
}
/*
  FirstOrDefault() and SingleOrDefault() methods will return null if not found
  SingleOrDefault() method Will throw an exception if it finds multiple matches
*/
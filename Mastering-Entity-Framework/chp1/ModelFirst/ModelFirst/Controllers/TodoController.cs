using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using ModelFirst;

namespace DBFirst.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {

            /*
            ModelFirstContainer entity = new ModelFirstContainer();
            //Lazy loading
            IEnumerable<Todo> TodoesX = entity.Todoes;
            return View(TodoesX);
            */

            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                // Eager loading 
                IEnumerable<Todo> TodoesY = entity.Todoes.ToList();

                //TodoesY 
                return View(TodoesY);
            }

        }


        public ActionResult Details(int? id = 0)
        {
            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                // Todo todo = entity.Todoes.Find(id);
                // Todo todo = entity.Todoes.FirstOrDefault(item => item.Id == id);
                Todo todo = entity.Todoes.SingleOrDefault(item => item.Id == id);
                if (todo == null)
                {
                    return View(new Todo());
                }
; return View(todo);

            }
        }

        [HttpPost]
        public ActionResult Create(string TodoItem, bool IsDone)
        {

            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                Todo newTodo = new Todo() { IsDone = IsDone, TodoItem = TodoItem };
                entity.Todoes.Add(newTodo);
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
            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                Todo todo = entity.Todoes.Find(id);
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
            using (ModelFirstContainer entity = new ModelFirstContainer())
			{
                Todo todo = entity.Todoes.Find(Id);
                todo.TodoItem = TodoItem;
                todo.IsDone = IsDone;
                entity.SaveChanges();
                return RedirectToAction("Details", new { id = Id });
			}
            */

            // To perform the update in a situation where the entities are passed through multiple layers
            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                Todo todo = entity.Todoes.Find(Id);
                todo.TodoItem = TodoItem;
                todo.IsDone = IsDone;
                entity.Entry(todo).State = EntityState.Modified; // The model is attached to the DBContext object and marked as modified
                entity.SaveChanges();
                return RedirectToAction("Details", new { id = Id });
            }

        }

        public ActionResult Delete(int Id)
        {
            using (ModelFirstContainer entity = new ModelFirstContainer())
            {
                Todo todo = entity.Todoes.Find(Id);
                entity.Todoes.Remove(todo);
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
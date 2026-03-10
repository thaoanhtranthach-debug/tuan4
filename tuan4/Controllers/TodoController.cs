using Microsoft.AspNetCore.Mvc;
using tuan4.Models;
using System.Linq;

namespace tuan4.Controllers
{
    public class TodoController : Controller 
    {
        private static List<Todo> _todos = new List<Todo>
        {
            new Todo { Id = 1, TaskName = "Đi chợ", IsCompleted = true },
            new Todo { Id = 2, TaskName = "Chơi thể thao", IsCompleted = false },
            new Todo { Id = 3, TaskName = "Chơi game", IsCompleted = false },
            new Todo { Id = 4, TaskName = "Học bài", IsCompleted = true }
        };

        public IActionResult Index()
        {
            return View(_todos);
        }

        // Return a model with the next Id so the form shows "Mã công việc"
        public IActionResult Create()
        {
            var nextId = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1;
            return View(new Todo { Id = nextId });
        }

        public IActionResult Edit(int id)
        {
            var item = _todos.Find(t => t.Id == id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Todo updated)
        {
            if (updated == null) return BadRequest();
            if (!ModelState.IsValid) return View(updated);

            var item = _todos.Find(t => t.Id == updated.Id);
            if (item == null) return NotFound();

            item.TaskName = updated.TaskName;
            item.IsCompleted = updated.IsCompleted;

            return RedirectToAction("Index");
        }

        // Delete confirmation page
        public IActionResult Delete(int id)
        {
            var item = _todos.Find(t => t.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Actually delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _todos.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                _todos.Remove(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo newItem)
        {
            if (newItem == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(newItem);

            // Ensure unique Id if client tampered or id collision
            if (newItem.Id <= 0 || _todos.Any(t => t.Id == newItem.Id))
            {
                newItem.Id = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1;
            }

            _todos.Add(newItem);
            return RedirectToAction("Index");
        }
    }
}

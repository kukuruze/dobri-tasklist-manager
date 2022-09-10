using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dobri_Tasklist_Manager.Data;
using Dobri_Tasklist_Manager.Models;

namespace Dobri_Tasklist_Manager.Controllers
{
    public class TaskListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskLists
        public async Task<IActionResult> Index()
        {
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Active.CurrentUserId);
            var currentUserTaskLists = _context.TaskLists.Where(x => currentUser.ListsCreatedByMe.Contains(Convert.ToString(x.Id)) || currentUser.ListsSharedWithMe.Contains(Convert.ToString(x.Id)));
            return View(await currentUserTaskLists.ToListAsync());
        }

        // GET: TaskLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        public IActionResult Share(int? id)
        {
            Active.CurrentTaskListId = (int)id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(int id, User user)
        {
            user = _context.Users.FirstOrDefault(x => x.Username == user.Username);
            user.ListsSharedWithMe += "," + Convert.ToString(id);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: TaskLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfCreation,DateOfLastEdit,IdCreator,IdLastEditor")] TaskList taskList)
        {
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Active.CurrentUserId);
            currentUser.ListsCreatedByMe += "," + Convert.ToString(taskList.Id);
            taskList.DateOfCreation = DateTime.Now;
            taskList.DateOfLastEdit = DateTime.Now;
            taskList.IdLastEditor = Active.CurrentUserId;
            taskList.IdCreator = Active.CurrentUserId;
            if (ModelState.IsValid)
            {
                _context.Add(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskList);
        }

        public IActionResult Tasks(int? id)
        {
            Active.CurrentTaskListId = (int)id;
            return RedirectToAction("Index", "ToDo");
        }

        // GET: TaskLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists.FindAsync(id);
            if (taskList == null)
            {
                return NotFound();
            }
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfCreation,DateOfLastEdit,IdCreator,IdLastEditor")] TaskList taskList)
        {
            taskList.DateOfLastEdit = DateTime.Now;
            taskList.IdLastEditor = Active.CurrentUserId;
            if (id != taskList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListExists(taskList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskList);
        }

        // GET: TaskLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskList = await _context.TaskLists.FindAsync(id);
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Active.CurrentUserId);
            if(taskList.IdCreator != currentUser.Id)
            {
                string sharedLists = "";
                foreach (var item in currentUser.ListsSharedWithMe.Split(','))
                {
                    if(item != Convert.ToString(taskList.Id))
                    {
                        if(sharedLists == "")
                        {
                            sharedLists = item;
                        }
                        else
                        {
                            sharedLists += "," + item;
                        }
                    }
                }
                currentUser.ListsSharedWithMe = sharedLists;
                _context.Users.Update(currentUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.TaskLists.Remove(taskList);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TaskListExists(int id)
        {
            return _context.TaskLists.Any(e => e.Id == id);
        }
    }
}

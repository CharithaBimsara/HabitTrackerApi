using System.Diagnostics.Eventing.Reader;
using HabitTrackerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private static List<Habit> habits = new();
        private static int nextId = 1;


        [HttpGet]
        public IActionResult GetHabits() => Ok(habits);

        [HttpPost]
        public IActionResult AddHabit([FromBody] Habit habit)
        {
            habit.Id = nextId++;
            habits.Add(habit);

            return CreatedAtAction(nameof(GetHabits), new { id = habit.Id }, habit);
        }

        [HttpPost("{id}/mark")]
        public IActionResult MarkHabitDone(int id)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null) return NotFound();

            var today = DateTime.UtcNow.Date;
            if (!habit.CompletionDates.Contains(today))
            {
                habit.CompletionDates.Add(today);
            }

            return Ok(habit);
        }

        [HttpGet("{id}/streak")]
        public IActionResult GetStreak(int id)
        {
            var habit = habits.FirstOrDefault(h => h.Id == id);
            if (habit == null) return NotFound();

            var today = DateTime.UtcNow.Date;
            int streak = 0;

            for (int i = 0; i < habit.CompletionDates.Count; i++)
            {
                if (habit.CompletionDates.Contains(today.AddDays(i)))
                {
                    streak++;
                }
                else break;
            }
            
            return Ok(new { habit.Name, streak });
        }

    }
}

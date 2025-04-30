namespace HabitTrackerApi.Models
{
    public class Habit
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public List<DateTime> CompletionDates { get; set; } = new List<DateTime>();
    }
}

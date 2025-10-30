using System.ComponentModel.DataAnnotations;

namespace MiniProjectManager.API.Models.DTOs.Scheduler
{
    public class ScheduleRequestDto
    {
        [Required(ErrorMessage = "Available hours per day is required")]
        [Range(1, 24, ErrorMessage = "Available hours must be between 1 and 24")]
        public int AvailableHoursPerDay { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
    }
}
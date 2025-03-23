using System.ComponentModel.DataAnnotations;

namespace Backend_UtshobKotha.Dtos
{
    public class EventDto
    {
        [Required(ErrorMessage = "Event title is required")]
        [StringLength(100, ErrorMessage = "Event title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
        public int Capacity { get; set; }

        

        [Required(ErrorMessage = "Free or Paid status is required")]
        public bool IsFree { get; set; }

        [Required(ErrorMessage = "Price status is required")]
        public double price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total Enrolled Must be provided (Default 0)")]
        public int CurrentEnrolled { get; set; }


        // Optional base64 string for the event banner (used in JSON requests)
        public string? EventBannerBase64 { get; set; }
    }
}

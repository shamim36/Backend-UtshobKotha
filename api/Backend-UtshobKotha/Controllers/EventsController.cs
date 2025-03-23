using Backend_UtshobKotha.Data;
using Backend_UtshobKotha.Dtos;
using Backend_UtshobKotha.Models;
using Backend_UtshobKotha.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Backend_UtshobKotha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly UtshobKothaDbContext _context;

        public EventsController(UtshobKothaDbContext context)
        {
            _context = context;
        }

        // Endpoint for multipart/form-data (with file upload)
        [HttpPost("multi-part-form-data")]
        public async Task<IActionResult> CreateEventWithBanner([FromForm] EventDto eventDto, IFormFile? eventBanner)
        {
            return await CreateEventInternal(eventDto, eventBanner, null);
        }

        // Endpoint for JSON (with optional base64 banner)
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            return await CreateEventInternal(eventDto, null, eventDto.EventBannerBase64);
        }

        // Endpoint to update an event with JSON (with optional base64 banner)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto eventDto)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            return await UpdateEventInternal(existingEvent, eventDto, null, eventDto.EventBannerBase64);
        }

        // Endpoint to update an event with multipart/form-data (with file upload)
        [HttpPut("update-with-banner/{id}")]
        public async Task<IActionResult> UpdateEventWithBanner(int id, [FromForm] EventDto eventDto, IFormFile? eventBanner)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            return await UpdateEventInternal(existingEvent, eventDto, eventBanner, null);
        }

        private async Task<IActionResult> CreateEventInternal(EventDto eventDto, IFormFile? eventBanner, string? eventBannerBase64)
        {
            // Validate the DTO
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(eventDto);

            if (!Validator.TryValidateObject(eventDto, validationContext, validationResults, true))
            {
                return BadRequest(new { Errors = validationResults.Select(v => v.ErrorMessage) });
            }

            // Validate category separately
            var categoryValidation = new CategoryValidationAttribute();
            var categoryResult = categoryValidation.GetValidationResult(eventDto.Category, new ValidationContext(eventDto));
            if (categoryResult != ValidationResult.Success)
            {
                return BadRequest(new { Errors = new[] { categoryResult.ErrorMessage } });
            }

            // Map DTO to Model
            if (!Enum.TryParse<EventCategory>(eventDto.Category, true, out var category))
            {
                return BadRequest(new { Errors = new[] { "Invalid category format" } });
            }

            // Validate time range
            if (eventDto.EndTime <= eventDto.StartTime)
            {
                return BadRequest(new { Errors = new[] { "End time must be after start time" } });
            }

            // Handle event banner
            byte[]? eventBannerData = await ProcessEventBanner(eventBanner, eventBannerBase64);
            if (eventBannerData == null && (eventBanner != null || !string.IsNullOrEmpty(eventBannerBase64)))
            {
                return BadRequest(new { Errors = new[] { "Invalid event banner data" } });
            }

            var newEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                Category = category,
                EventDate = eventDto.EventDate,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime,
                Location = eventDto.Location,
                Capacity = eventDto.Capacity,
                IsFree = eventDto.IsFree,
                EventBanner = eventBannerData
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
        }

        private async Task<IActionResult> UpdateEventInternal(Event existingEvent, EventDto eventDto, IFormFile? eventBanner, string? eventBannerBase64)
        {
            // Validate the DTO
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(eventDto);

            if (!Validator.TryValidateObject(eventDto, validationContext, validationResults, true))
            {
                return BadRequest(new { Errors = validationResults.Select(v => v.ErrorMessage) });
            }

            // Validate category separately
            var categoryValidation = new CategoryValidationAttribute();
            var categoryResult = categoryValidation.GetValidationResult(eventDto.Category, new ValidationContext(eventDto));
            if (categoryResult != ValidationResult.Success)
            {
                return BadRequest(new { Errors = new[] { categoryResult.ErrorMessage } });
            }

            // Map DTO to Model
            if (!Enum.TryParse<EventCategory>(eventDto.Category, true, out var category))
            {
                return BadRequest(new { Errors = new[] { "Invalid category format" } });
            }

            // Validate time range
            if (eventDto.EndTime <= eventDto.StartTime)
            {
                return BadRequest(new { Errors = new[] { "End time must be after start time" } });
            }

            // Handle event banner
            byte[]? eventBannerData = await ProcessEventBanner(eventBanner, eventBannerBase64);
            if (eventBannerData == null && (eventBanner != null || !string.IsNullOrEmpty(eventBannerBase64)))
            {
                return BadRequest(new { Errors = new[] { "Invalid event banner data" } });
            }

            // Update the existing event
            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.Category = category;
            existingEvent.EventDate = eventDto.EventDate;
            existingEvent.StartTime = eventDto.StartTime;
            existingEvent.EndTime = eventDto.EndTime;
            existingEvent.Location = eventDto.Location;
            existingEvent.Capacity = eventDto.Capacity;
            existingEvent.IsFree = eventDto.IsFree;
            if (eventBannerData != null) // Only update the banner if new data is provided
            {
                existingEvent.EventBanner = eventBannerData;
            }

            await _context.SaveChangesAsync();

            return Ok(existingEvent);
        }

        private async Task<byte[]?> ProcessEventBanner(IFormFile? eventBanner, string? eventBannerBase64)
        {
            byte[]? eventBannerData = null;

            if (eventBanner != null)
            {
                // Check file size (max 2MB as per the form)
                if (eventBanner.Length > 2 * 1024 * 1024)
                {
                    throw new Exception("Event banner must be less than 2MB");
                }

                // Check file type (SVG, PNG, JPG, GIF as per the form)
                var allowedExtensions = new[] { ".svg", ".png", ".jpg", ".jpeg", ".gif" };
                var extension = Path.GetExtension(eventBanner.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new Exception("Event banner must be an SVG, PNG, JPG, or GIF file");
                }

                // Read the file into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await eventBanner.CopyToAsync(memoryStream);
                    eventBannerData = memoryStream.ToArray();
                }
            }
            else if (!string.IsNullOrEmpty(eventBannerBase64))
            {
                try
                {
                    // Remove the data URI prefix if present (e.g., "data:image/png;base64,")
                    var base64String = eventBannerBase64;
                    if (base64String.Contains(","))
                    {
                        base64String = base64String.Split(',')[1];
                    }

                    eventBannerData = Convert.FromBase64String(base64String);

                    // Check size (max 2MB)
                    if (eventBannerData.Length > 2 * 1024 * 1024)
                    {
                        throw new Exception("Event banner must be less than 2MB");
                    }
                }
                catch (FormatException)
                {
                    throw new Exception("Invalid base64 string for event banner");
                }
            }

            return eventBannerData;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }
    }
}

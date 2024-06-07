using OceanCareChat.Data;
using OceanCareChat.Dtos.Events;
using OceanCareChat.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static OceanCareChat.Dtos.Events.EventsDTO;
using OceanCareChat.Migrations;

namespace OceanCareChat.Controller
{
    [Route("/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventsDTO>>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();

            var eventsList = new List<EventsDto>();

            foreach (var userEvent in events)
            {
                eventsList.Add(new EventsDto
                {
                    Id = userEvent.Id,
                    Name = userEvent.Name,
                    Date = userEvent.Date,
                    Location = userEvent.Location,
                    Description = userEvent.Description
                });
            }

            return Ok(eventsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventsDTO>> GetEvent(int id)
        {
            var events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            var eventSelected = new EventsDto
            {
                Id = events.Id,
                Name = events.Name,
                Date = events.Date,
                Location = events.Location,
                Description = events.Description
            };

            return Ok(eventSelected);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, UpdateEventsDTO eventsDTO)
        {
            var eventSelected = await _context.Events.FindAsync(id);

            if (eventSelected == null)
            {
                return NotFound();
            }

            eventSelected.Name = eventsDTO.Name;
            eventSelected.Description = eventsDTO.Description;
            eventSelected.Date = eventsDTO.Date;
            eventSelected.Location = eventsDTO.Location;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EventsDTO>> PostEvent(RegisterEventsDTO eventsDTO)
        {
            var events = new Events
            {
                Name = eventsDTO.Name,
                Description = eventsDTO.Description,
                Date = eventsDTO.Date,
                Location = eventsDTO.Location
            };

            _context.Events.Add(events);
            await _context.SaveChangesAsync();

            var eventCreated = new EventsDto
            {
                Id = events.Id,
                Name = events.Name,
                Date = events.Date,
                Location = events.Location,
                Description = events.Description
            };

            return CreatedAtAction("GetEvent", new { id = events.Id }, eventCreated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EventsDTO>> DeleteEvent(int id)
        {
            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        [HttpGet("{eventId}/users/{userId}")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(int eventId, int userId)
        {
            var userEvent = await _context.UserEvents
               .Where(ue => ue.EventId == eventId && ue.OceanUserId == userId)
               .FirstOrDefaultAsync();
            if (userEvent == null)
            {
                return NotFound();
            }
            return Ok(userEvent);
        }

        [HttpPost("{eventId}/users/{userId}")]
        public async Task<ActionResult> AddUserToEvent(int eventId, int userId)
        {
            var user = await _context.OceanUser.FindAsync(userId);
            var events = await _context.Events.FindAsync(eventId);

            if (user == null || events == null)
            {
                return NotFound();
            }

            var userEvent = new UserEvent
            {
                OceanUserId = userId,
                EventId = eventId
            };

            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserEvent), new { eventId = userEvent.EventId, userId = userEvent.OceanUserId }, new { Id = userEvent.Id, EventId = userEvent.EventId, OceanUserId = userEvent.OceanUserId });
        }

        [HttpGet("/events/list/{userId}")]
        public async Task<ActionResult<IEnumerable<EventsDTO>>> GetUserEvents(int userId)
        {
            var userEvents = await _context.UserEvents
                .Where(ue => ue.OceanUserId == userId)
                .Include(ue => ue.Event)
                .Select(ue => ue.Event)
                .ToListAsync();

            var events = new List<EventsDto>();

            foreach (var userEvent in userEvents)
            {
                events.Add(new EventsDto
                {
                    Id = userEvent.Id,
                    Name = userEvent.Name,
                    Date = userEvent.Date,
                    Location = userEvent.Location,
                    Description = userEvent.Description
                });
            }

            return Ok(events);
        }
    }
}
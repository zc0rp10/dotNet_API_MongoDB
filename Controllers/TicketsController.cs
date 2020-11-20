using BearTracApi.Models;
using BearTracApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BearTracApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _TicketService;

        public TicketsController(TicketService ticketService)
        {
            _TicketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() =>
            _TicketService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public ActionResult<Ticket> Get(string id)
        {
            var ticket = _TicketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public ActionResult<Ticket> Create(Ticket ticket)
        {
            _TicketService.Create(ticket);
            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Ticket ticketIn)
        {
            var ticket = _TicketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _TicketService.Update(id, ticketIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticket = _TicketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _TicketService.Remove(ticket.Id);

            return NoContent();
        }
    }
}
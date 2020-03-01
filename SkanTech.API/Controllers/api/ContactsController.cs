using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkanTech.API.Models;

namespace SkanTech.API.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsContext _context;

        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        // Endpoint that returns all objects in a list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // Endpoint that returns an object based on Email
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContacts(string id)
        {
            var contacts = await _context.Contacts.FindAsync(id);

            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(string id, Contact contact)
        {
            if (id != contact.Email)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(id))
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

        // Endpoint to save a contact to database
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContacts(Contact contact)
        {
            _context.Contacts.Add(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactsExists(contact.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetContacts), new { id = contact.Email }, contact);
        }

        // Endpoint to delete a contact
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContacts(string id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();

            return contacts;
        }

        private bool ContactsExists(string id)
        {
            return _context.Contacts.Any(e => e.Email == id);
        }
    }
}

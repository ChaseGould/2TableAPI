using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwoTableAPI.Models;

namespace TwoTableAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentHistoriesController : ControllerBase
    {
        private readonly TwoTablesContext _context;

        public PaymentHistoriesController(TwoTablesContext context)
        {
            _context = context;
        }


        //does this function return all of the payment histories????
        // GET: api/PaymentHistories
        [HttpGet]
        public IEnumerable<PaymentHistory> GetPaymentHistory()
        {
            return _context.PaymentHistory;
        }

        // GET: api/PaymentHistories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentHistory = await _context.PaymentHistory.FindAsync(id);

            if (paymentHistory == null)
            {
                return NotFound();
            }

            return Ok(paymentHistory);
        }

        // PUT: api/PaymentHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentHistory([FromRoute] int id, [FromBody] PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentHistoryExists(id))
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

        // POST: api/PaymentHistories
        [HttpPost]
        public async Task<IActionResult> PostPaymentHistory([FromBody] PaymentHistory paymentHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentHistory.Add(paymentHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentHistory", new { id = paymentHistory.Id }, paymentHistory);
        }

        // DELETE: api/PaymentHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentHistory = await _context.PaymentHistory.FindAsync(id);
            if (paymentHistory == null)
            {
                return NotFound();
            }

            _context.PaymentHistory.Remove(paymentHistory);
            await _context.SaveChangesAsync();

            return Ok(paymentHistory);
        }

        private bool PaymentHistoryExists(int id)
        {
            return _context.PaymentHistory.Any(e => e.Id == id);
        }
    }
}

using Demo.Models;
using Demo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaAPIRepository _villaRepository;

        public VillaAPIController(IVillaAPIRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
        {
            var villas = await _villaRepository.GetVillasAsync();
            return Ok(villas);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        public async Task<ActionResult<Villa>> GetVilla(int id)
        {
            var villa = await _villaRepository.GetVillaAsync(id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<Villa>> CreateVilla([FromBody] Villa villa)
        {
            var createdVilla = await _villaRepository.CreateVillaAsync(villa);
            return CreatedAtRoute("GetVilla", new { id = createdVilla.Id }, createdVilla);
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] Villa villa)
        {
            if (id != villa.Id)
            {
                return BadRequest();
            }

            try
            {
                await _villaRepository.UpdateVillaAsync(villa);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _villaRepository.VillaExistsAsync(id))
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            await _villaRepository.DeleteVillaAsync(id);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:int}", Name = "PatchVilla")]
        public async Task<IActionResult> PatchVilla(int id, [FromBody] JsonPatchDocument<Villa> patchDoc)
        {
            if (patchDoc == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villaRepository.GetVillaAsync(id);
            if (villa == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _villaRepository.UpdateVillaAsync(villa);

            return NoContent();
        }
    }
}

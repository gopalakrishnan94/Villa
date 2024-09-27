using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas() 
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    public ActionResult<VillaDTO> GetVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        return Ok(villa);
    }
    
    [HttpPost]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) 
    {
        // if (!ModelState.IsValid) {
        //     return BadRequest(ModelState);
        // }
        
        // Custom Validation Error

        if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null) 
        {
            ModelState.AddModelError("CustomError", "Villa Already Exist!");
            return BadRequest(ModelState);
        }

        if (villaDTO == null) {
            return BadRequest();
        }
        if (villaDTO.Id > 0) {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDTO);
        return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public ActionResult<VillaDTO> DeleteVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        VillaStore.villaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public ActionResult<VillaDTO> UpdateVilla(int id, [FromBody]VillaDTO villaDTO) 
    {
        if (villaDTO == null || id != villaDTO.Id) {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        villa.Name = villaDTO.Name;
        villa.Occupancy = villaDTO.Occupancy;
        villa.Sqft = villaDTO.Sqft;

        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public ActionResult<VillaDTO> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
    {
        if (patchDTO == null || id == 0) {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        patchDTO.ApplyTo(villa, ModelState);
        if(!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}

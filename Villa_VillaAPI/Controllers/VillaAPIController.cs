using System;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ApplicationDBContext _dbContext;
    private readonly IMapper _mapper;
    public VillaAPIController(ApplicationDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas() 
    {
        IEnumerable<Villa> villaList = await _dbContext.Villas.ToListAsync();
        return Ok(_mapper.Map<List<VillaDTO>>(villaList));
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    public async Task<ActionResult<VillaDTO>> GetVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        return Ok(_mapper.Map<VillaDTO>(villa));
    }
    
    [HttpPost]
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]CreateVillaDTO createDTO) 
    {
        // if (!ModelState.IsValid) {
        //     return BadRequest(ModelState);
        // }
        
        // Custom Validation Error

        if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null) 
        {
            ModelState.AddModelError("CustomError", "Villa Already Exist!");
            return BadRequest(ModelState);
        }

        if (createDTO == null) {
            return BadRequest();
        }

        Villa villaModel = _mapper.Map<Villa>(createDTO);

        // Villa villaModel = new() {
        //     Name = createDTO.Name,
        //     Details = createDTO.Details,
        //     Occupancy = createDTO.Occupancy,
        //     Rate = createDTO.Rate,
        //     Sqft = createDTO.Sqft,
        //     Amenity = createDTO.Amenity,
        //     ImageUrl = createDTO.ImageUrl
        // };

        await _dbContext.Villas.AddAsync(villaModel);
        await _dbContext.SaveChangesAsync();

        return CreatedAtRoute("GetVilla", new { id = villaModel.Id }, villaModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public async Task<ActionResult<VillaDTO>> DeleteVilla(int id) 
    {
        if (id == 0) {
            return BadRequest();
        }
        var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }
        _dbContext.Villas.Remove(villa);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public async Task<ActionResult<VillaDTO>> UpdateVilla(int id, [FromBody]UpdateVillaDTO updateDTO) 
    {
        if (updateDTO == null || id != updateDTO.Id) {
            return BadRequest();
        }
        var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null) {
            return NotFound();
        }

        Villa villaModel = _mapper.Map<Villa>(updateDTO);

        _dbContext.Villas.Update(villaModel);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public async Task<ActionResult<VillaDTO>> UpdatePartialVilla(int id, JsonPatchDocument<UpdateVillaDTO> patchDTO) 
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        UpdateVillaDTO villaDTO = _mapper.Map<UpdateVillaDTO>(villa);

        patchDTO.ApplyTo(villaDTO, ModelState);

        Villa villaModel = _mapper.Map<Villa>(villaDTO);

        _dbContext.Villas.Update(villaModel);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}

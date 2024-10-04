using System;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Repository.IRepository;

namespace Villa_VillaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;
    protected APIResponse _response;
    public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
    {
        _dbVilla = dbVilla;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetVillas() 
    {
        try {
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    public async Task<ActionResult<APIResponse>> GetVilla(int id) 
    {
        try {
            if (id == 0) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]CreateVillaDTO createDTO) 
    {
        try {
            // if (!ModelState.IsValid) {
            //     return BadRequest(ModelState);
            // }
            
            // Custom Validation Error

            if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null) 
            {
                ModelState.AddModelError("CustomError", "Villa Already Exist!");
                _response.Result = ModelState;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            if (createDTO == null) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Villa villaModel = _mapper.Map<Villa>(createDTO);

            await _dbVilla.CreateAsync(villaModel);

            _response.Result = _mapper.Map<VillaDTO>(villaModel);
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetVilla", new { id = villaModel.Id }, _response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public async Task<ActionResult<APIResponse>> DeleteVilla(int id) 
    {
        try {
            if (id == 0) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            await _dbVilla.RemoveAsync(villa);

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]UpdateVillaDTO updateDTO) 
    {
        try {
            if (updateDTO == null || id != updateDTO.Id) {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);
            if (villa == null) {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            Villa villaModel = _mapper.Map<Villa>(updateDTO);

            await _dbVilla.UpdateAsync(villaModel);

            _response.Result = _mapper.Map<VillaDTO>(villaModel);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        } 
        catch (Exception ex) {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public async Task<ActionResult<VillaDTO>> UpdatePartialVilla(int id, JsonPatchDocument<UpdateVillaDTO> patchDTO) 
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);
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

        await _dbVilla.UpdateAsync(villaModel);

        return NoContent();
    }
}

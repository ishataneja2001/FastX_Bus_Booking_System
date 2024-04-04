﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastXBookingSample.Models;
using FastXBookingSample.Repository;
using AutoMapper;
using FastXBookingSample.DTO;
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Exceptions;

namespace FastXBookingSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardingPointsController : ControllerBase
    {
        private readonly IBoardingPointRepository _boardingPointRepository;
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;

        public BoardingPointsController(IBoardingPointRepository boardingPointRepository, IBusRepository busRepository,IMapper mapper)
        {
            _boardingPointRepository = boardingPointRepository;
            _busRepository = busRepository;
            _mapper = mapper;
        }




        // GET: api/BoardingPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardingPointDto>>> GetBoardingPoints(int busid)
        {
            try
            {
                if (!_busRepository.BusExists(busid))
                    return BadRequest(ModelState);
                return Ok(_mapper.Map<List<BoardingPointDto>>(_boardingPointRepository.GetBoardingPointsByBusId(busid)));
            }catch(BoardingPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }





        [HttpPut("{id}")]
        [ProducesResponseType(200,Type=typeof(string))]
        [ProducesResponseType(400)]
        // PUT: api/BoardingPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutBoardingPoint(int id, BoardingPointDto boardingPointdto)
        {
            try
            {
                if (id != boardingPointdto.BoardingId)
                {
                    return BadRequest();
                }

                return Ok(_boardingPointRepository.UpdateBoardingPoints(id, _mapper.Map<BoardingPoint>(boardingPointdto)));
            }catch (BoardingPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




        // POST: api/BoardingPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<ActionResult<BoardingPointDto>> PostBoardingPoint(BoardingPointDto boardingPointdto)
        {
            try
            {
                return Ok(_boardingPointRepository.PostBoardingPoint(_mapper.Map<BoardingPoint>(boardingPointdto)));

            }
            catch (BoardingPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }





        // DELETE: api/BoardingPoints/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBoardingPoint(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(_boardingPointRepository.DeleteBoardingPoints(id));
            }
            catch (BoardingPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Patch
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<BoardingPoint> boardingPointPatch)
        {
            try
            {
                return Ok(_boardingPointRepository.PatchBoardingPoint(id, boardingPointPatch));

            }
            catch (BoardingPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

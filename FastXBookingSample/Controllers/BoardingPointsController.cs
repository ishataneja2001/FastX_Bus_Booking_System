using System;
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
            if (!_busRepository.BusExists(busid))
                return BadRequest(ModelState);
            return Ok(_mapper.Map<List<BoardingPointDto>>(_boardingPointRepository.GetBoardingPointsByBusId(busid)));
        }





        [HttpPut("{id}")]
        [ProducesResponseType(200,Type=typeof(string))]
        [ProducesResponseType(400)]
        // PUT: api/BoardingPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutBoardingPoint(int id, BoardingPointDto boardingPointdto)
        {
            if (id != boardingPointdto.BoardingId)
            {
                return BadRequest();
            }

            return Ok(_boardingPointRepository.UpdateBoardingPoints(id,_mapper.Map<BoardingPoint>(boardingPointdto)));
        }




        // POST: api/BoardingPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<ActionResult<BoardingPointDto>> PostBoardingPoint(BoardingPointDto boardingPointdto)
        {
            return Ok(_boardingPointRepository.PostBoardingPoint(_mapper.Map<BoardingPoint>(boardingPointdto)));
        }





        // DELETE: api/BoardingPoints/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBoardingPoint(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_boardingPointRepository.DeleteBoardingPoints(id));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Customers;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerUseCase _create;
        private readonly IGetAllCustomersUseCase _getAll;
        private readonly IGetCustomerByIdUseCase _getById;
        private readonly IUpdateCustomerUseCase _update;
        private readonly IDeleteCustomerUseCase _delete;
        private readonly IMapper _mapper;

        public CustomerController(
            ICreateCustomerUseCase create,
            IGetAllCustomersUseCase getAll,
            IGetCustomerByIdUseCase getById,
            IUpdateCustomerUseCase update,
            IDeleteCustomerUseCase delete,
            IMapper mapper)
        {
            _create = create;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<CustomerDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null) return NotFound();
            var dto = _mapper.Map<CustomerDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerDto dto)
        {
            var entity = await _create.ExecuteAsync(dto);
            var resultDto = _mapper.Map<CustomerDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.CustomerID }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDto dto)
        {
            var entity = await _update.ExecuteAsync(id, dto);
            if (entity == null) return NotFound();
            var resultDto = _mapper.Map<CustomerDto>(entity);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _delete.ExecuteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }


}

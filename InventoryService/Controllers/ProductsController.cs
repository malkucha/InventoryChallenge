using AutoMapper;
using Business.BusinessEntities;
using Business.BusinessLogic;
using Business.BusinessLogic.Parameters;
using InventoryService.Messaging;
using InventoryService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ProductService _service;
        private readonly Publisher _publisher;

        public ProductsController(IMapper mapper, ProductService service, Publisher publisher)
        {
            _mapper = mapper;
            _service = service;
            _publisher = publisher;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductBusinessEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductBusinessEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductBusinessEntity), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(_mapper.Map<ProductCreateParameter>(model));

            // Publicar evento en RabbitMQ
            _publisher.PublishProductEvent(new { Id = created.Id, Name = created.Name }, "product.created");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ProductViewModel model)
        {
            var param = _mapper.Map<ProductUpdateParameter>(model);
            param.Id = id;
            await _service.UpdateAsync(param);

            // Publicar evento en RabbitMQ
            _publisher.PublishProductEvent(new { Id = id, Name = model.Name }, "product.updated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            // Publicar evento en RabbitMQ
            _publisher.PublishProductEvent(new { Id = id }, "product.deleted");

            return NoContent();
        }
    }
}

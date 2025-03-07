using Microsoft.AspNetCore.Mvc;
using webbuilder.api.dtos;
using webbuilder.api.services;

namespace webbuilder.api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElementsController : ControllerBase
    {
        private readonly IElementsService _elementsService;

        public ElementsController(IElementsService elementsService)
        {
            _elementsService = elementsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateElementDto element)
        {
            var result = await _elementsService.CreateElement(element);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _elementsService.GetElements();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _elementsService.DeleteElement(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<IActionResult> Put([FromBody] UpdateElementDto element)
        {
            var result = await _elementsService.UpdateElement(element);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}
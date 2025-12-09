using KorpInventory.Application.Commands.CreateProduct;
using KorpInventory.Application.Commands.DeleteProduct;
using KorpInventory.Application.Commands.UpdateProduct;
using KorpInventory.Application.Commands.UpdateStock;
using KorpInventory.Application.Queries.GetAllProducts;
using KorpInventory.Application.Queries.GetProductById;
using KorpInventory.Application.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KorpInventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var products = await _mediator.Send(query);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar produtos.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetProductByIdQuery { Id = id };
                var product = await _mediator.Send(query);

                if (product == null)
                    return NotFound(new { message = $"Produto com ID {id} não encontrado." });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar produto.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            try
            {
                var product = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar produto.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest(new { message = "ID da URL não corresponde ao ID do corpo da requisição." });

                var product = await _mediator.Send(command);

                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar produto.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao deletar produto.", error = ex.Message });
            }
        }

        [HttpPost("update-stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Estoque atualizado com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar estoque.", error = ex.Message });
            }
        }
    }
}

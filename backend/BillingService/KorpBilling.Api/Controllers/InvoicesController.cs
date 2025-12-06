using KorpBilling.Application.Commands.CreateInvoice;
using KorpBilling.Application.Commands.PrintInvoice;
using KorpBilling.Application.Queries.GetAllInvoices;
using KorpBilling.Application.Queries.GetInvoiceById;
using KorpBilling.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KorpBilling.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todas as notas fiscais
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvoiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllInvoicesQuery();
                var invoices = await _mediator.Send(query);
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar notas fiscais.", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtém uma nota fiscal por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetInvoiceByIdQuery { Id = id };
                var invoice = await _mediator.Send(query);

                if (invoice == null)
                    return NotFound(new { message = $"Nota fiscal com ID {id} não encontrada." });

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar nota fiscal.", error = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova nota fiscal
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InvoiceViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var command = new CreateInvoiceCommand
                {
                    Items = viewModel.Items.Select(i => new InvoiceItemCommand
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                };

                var invoice = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar nota fiscal.", error = ex.Message });
            }
        }

        /// <summary>
        /// Imprime uma nota fiscal (fecha a nota e atualiza o estoque)
        /// </summary>
        [HttpPost("{id}/print")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Print(int id)
        {
            try
            {
                var command = new PrintInvoiceCommand { InvoiceId = id };
                await _mediator.Send(command);

                return Ok(new
                {
                    message = "Nota fiscal impressa com sucesso.",
                    details = "Status atualizado para Fechada e estoque atualizado."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro ao comunicar com o serviço de estoque.",
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao imprimir nota fiscal.", error = ex.Message });
            }
        }
    }
}

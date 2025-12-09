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

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceViewModel viewModel)
        {
            try
            {
                var command = new CreateInvoiceCommand
                {
                    Items = viewModel.Items.Select(i => new InvoiceItemCommand
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Code = i.Code,
                        Description = i.Description,
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

        [HttpPost("{id}/print")]
        public async Task<IActionResult> Print(int id)
        {
            try
            {
                var command = new PrintInvoiceCommand { InvoiceId = id };
                await _mediator.Send(command);

                return Ok(new { message = "Nota fiscal impressa com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}

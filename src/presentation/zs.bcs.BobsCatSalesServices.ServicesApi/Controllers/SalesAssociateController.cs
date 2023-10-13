using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Commands;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Queries;
using zs.bcs.BobsCatSalesServices.ServicesApi.Contracts.Requests;

namespace zs.bcs.BobsCatSalesServices.ServicesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesAssociateController : ControllerBase
    {
        private readonly ILogger<SalesAssociateController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesAssociateController(
            ILogger<SalesAssociateController> logger,
            IMediator mediator,
            IMapper mapper
            )
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateSalesAssociate")]
        public async Task<string> CreateSalesAssociate(CreateSalesAssociateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateSalesAssociate");

            var command = _mapper.Map<CreateSalesAssociateCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogTrace("Exit CreateSalesAssociate - SalesAssociateEntityKey:{SalesAssociateEntityKey}", result);

            return result;
        }

        [HttpGet(Name = "GetSalesAssociateByEmailAddress")]
        public async Task<Domain.Entity.SalesAssociate.SalesAssociate> GetSalesAssociateByEmailAddress(string emailAddress, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetSalesAssociateByEmailAddress");

            var entity = await _mediator.Send(new GetSalesAssociateByEmailQuery { EmailAddress = emailAddress }, cancellationToken);

            _logger.LogTrace("Exit GetSalesAssociateByEmailAddress - SalesAssociateEntityKey:{SalesAssociateEntityKey}", entity.EntityKey);

            return entity;
        }
    }
}

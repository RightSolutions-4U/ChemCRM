using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemWebsite.API.Controllers.Migration
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DbMigrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DbMigrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}

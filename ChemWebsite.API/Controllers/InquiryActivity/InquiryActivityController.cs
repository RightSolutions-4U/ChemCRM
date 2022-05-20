﻿using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.InquiryActivity
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryActivityController : BaseController
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Initializes a new instance of the <see cref="InquiryNoteController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public InquiryActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the Inquiry Activities .
        /// </summary>
        /// <returns></returns>
        [HttpGet("{inquiryId}")]
        [Produces("application/json", "application/xml", Type = typeof(List<InquiryActivityDto>))]
        public async Task<IActionResult> GetInquiryActivities(Guid inquiryId)
        {
            var query = new GetInquiryActivitiesQuery { InquiryId = inquiryId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        /// <summary>
        /// Inserts the Inquiry Activity
        /// </summary>
        /// <param name="addInquiryActivityCommand">The add Inquiry Note command.</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(IndustryDto))]
        public async Task<IActionResult> AddInquiryActivity([FromBody] AddInquiryActivityCommand addInquiryActivityCommand)
        {
            var result = await _mediator.Send(addInquiryActivityCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Updates the Inquiry Activity By Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateInquiryActivityCommand">The update Inquiry By Id command.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(IndustryDto))]
        public async Task<IActionResult> UpdateInquiryActivity(Guid id, [FromBody] UpdateInquiryActivityCommand updateInquiryActivityCommand)
        {
            updateInquiryActivityCommand.Id = id;
            var result = await _mediator.Send(updateInquiryActivityCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Deletes the Inquiry Activity By Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiryActivity(Guid id)
        {
            var command = new DeleteInquiryActivityCommand() { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
    }
}

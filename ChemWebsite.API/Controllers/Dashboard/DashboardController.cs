using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Controllers.Dashboard
{
    /// <summary>
    /// DashboardController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {

        public IMediator _mediator { get; set; }
        /// <summary>
        /// DashboardController
        /// </summary>
        /// <param name="mediator"></param>
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the dashboard statistics.
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistics")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var dashboardStaticaticsQuery = new DashboardStaticaticsQuery { };
            var result = await _mediator.Send(dashboardStaticaticsQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the daily reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("dailyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetDailyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetDailyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the weekly reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("weeklyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetWeeklyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetWeeklyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the monthly reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("monthlyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetMonthlyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetMonthlyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the quarterly reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("quarterlyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetQuarterlyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetQuarterlyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the half yearly reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("halfyearlyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetHalfYearlyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetHalfYearlyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the yearly reminders.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("yearlyreminder/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetYearlyReminders(int month, int year)
        {
            var monthlyEventQuery = new GetYearlyReminderQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the monthly inquiries.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [HttpGet("monthlyinquiry/{month}/{year}")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetMonthlyInquiries(int month, int year)
        {
            var monthlyEventQuery = new GetMonthlyInquiryQuery { Month = month, Year = year };
            var result = await _mediator.Send(monthlyEventQuery);
            return Ok(result);
        }
    }
}

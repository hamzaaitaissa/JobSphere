using AutoMapper;
using JobSphere.Entities;
using JobSphere.Services.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobsAsync()
        {
            var jobs = await _jobService.GetAllJobsAsync();
            return Ok(jobs);
        }

    }
}

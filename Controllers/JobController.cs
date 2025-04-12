using AutoMapper;
using JobSphere.DTOs.Jobs;
using JobSphere.Entities;
using JobSphere.Services.Jobs;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobByIdAsync(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
                return NotFound($"Job with ID {id} not found.");
            return Ok(job);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJobAsync(CreateJobDto createJobDto)
        {
            var job = await _jobService.CreateJobAsync(createJobDto);
            return Ok(job);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobAsync(int id)
        {
            await _jobService.DeleteJobAsync(id);
            return Ok("Job deleted successfully.");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Job>> UpdateJobAsync(int id, UpdateJobDto updateJobDto)
        {
            var job = await _jobService.UpdateJobAsync(id, updateJobDto);
            return Ok(job);
        }

        [HttpGet("tags")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsByTags([FromQuery] List<string> tagsTitles)
        {
            var jobs = await _jobService.GetJobsByTagsAsync(tagsTitles);
            return Ok(jobs);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Job>>> SearchJobsByTitle([FromQuery] string title)
        {
            var jobs = await _jobService.GetJobsByNameAsync(title);
            return Ok(jobs);
        }

        [Authorize]
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleJobStatus(int id)
        {
            await _jobService.ToggleJobStatusAsync(id);
            return Ok("Job status toggled successfully.");
        }
    }
}

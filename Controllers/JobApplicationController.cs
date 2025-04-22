using JobSphere.DTOs.JobApplication;
using JobSphere.Entities;
using JobSphere.Services.JobApllication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
        [HttpPost("apply")]
        public async Task<ActionResult<JobApplicationEntity>> ApplyJobAsync([FromBody] CreateJobApplicationDto createJobApplicationDto)
        {
            try
            {
                var jobApplication = await _jobApplicationService.ApplyJobAsync(createJobApplicationDto);
                return CreatedAtAction(nameof(ApplyJobAsync), new { id = jobApplication.Id }, jobApplication);
            }
            catch (Exception ex)
            {
                //loggin the error
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

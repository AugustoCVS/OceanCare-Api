using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanCareChat.Data;
using OceanCareChat.Dtos.Reports;
using OceanCareChat.Models;
using static OceanCareChat.Dtos.Reports.ReportsDTO;

namespace OceanCareChat.Controllers
{
    [Route("/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReportsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/reports")]
        public async Task<ActionResult<IEnumerable<ReportsDto>>> GetReports()
        {
            var reports = await _context.Reports
                                        .Include(r => r.OceanUser)
                                        .ToListAsync();

            if (reports == null || reports.Count == 0)
            {
                return NotFound();
            }

            var allReports = reports.Select(r => new ReportsDto
            {
                Id = r.Id,
                TrashType = r.TrashType,
                TrashLocation = r.TrashLocation,
                TrashDescription = r.TrashDescription,
                OceanUserId = r.OceanUserId,
                OceanUserName = r.OceanUser?.Name 
            });

            return Ok(allReports);
        }

        [HttpGet("/list/{oceanUserId}")]
        public async Task<ActionResult<IEnumerable<ReportsDto>>> GetReportsByUserId(int oceanUserId)
        {
            var reports = await _context.Reports
                .Where(x => x.OceanUserId == oceanUserId)
                .Select(report => new ReportsDto
                {
                    Id = report.Id,
                    TrashType = report.TrashType,
                    TrashLocation = report.TrashLocation,
                    TrashDescription = report.TrashDescription,
                    OceanUserId = report.OceanUserId,
                    OceanUserName = report.OceanUser.Name
                })
                .ToListAsync();

            if (reports == null || reports.Count == 0)
            {
                return NotFound();
            }

            return Ok(reports);
        }

        [HttpPost("/reports")]
        public async Task<ActionResult<ReportsDto>> AddReport(RegisterReportsDTO report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.OceanUser.FirstOrDefaultAsync(x => x.Id == report.OceanUserId);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            var newReport = new Reports
            {
                TrashType = report.TrashType,
                TrashLocation = report.TrashLocation,
                TrashDescription = report.TrashDescription,
                OceanUserId = report.OceanUserId,
                OceanUser = user
            };

            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();

            user.ReportedTrash = (int.Parse(user.ReportedTrash) + 5).ToString();
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReports), new { id = newReport.Id }, new ReportsDto
            {
                Id = newReport.Id,
                TrashType = newReport.TrashType,
                TrashLocation = newReport.TrashLocation,
                TrashDescription = newReport.TrashDescription,
                OceanUserId = newReport.OceanUserId,
                OceanUserName = newReport.OceanUser.Name
            });
        }

        [HttpPut("/reports/{id}")]
        public async Task<ActionResult<ReportsDto>> UpdateReport(int id, UpdateReportsDTO report)
        {
            var reportToUpdate = await _context.Reports
                                                .Include(r => r.OceanUser)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            if (reportToUpdate == null)
            {
                return NotFound();
            }

            reportToUpdate.TrashType = report.TrashType;
            reportToUpdate.TrashLocation = report.TrashLocation;
            reportToUpdate.TrashDescription = report.TrashDescription;

            await _context.SaveChangesAsync();

            return Ok(new ReportsDto
            {
                Id = reportToUpdate.Id,
                TrashType = reportToUpdate.TrashType,
                TrashLocation = reportToUpdate.TrashLocation,
                TrashDescription = reportToUpdate.TrashDescription,
                OceanUserId = reportToUpdate.OceanUserId,
                OceanUserName = reportToUpdate.OceanUser?.Name 
            });
        }

        [HttpDelete("/reports/{id}")]
        public async Task<ActionResult> DeleteReport(int id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(x => x.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
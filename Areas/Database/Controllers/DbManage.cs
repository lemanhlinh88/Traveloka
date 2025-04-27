using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManage : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<DbManage> _logger;
        public DbManage(AppDbContext dbContext, ILogger<DbManage> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            // _logger.LogInformation(_dbContext.Database.GetConnectionString());
            return View();
        }
        
        [TempData]
        public string? StatusMessage { get; set;}

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Xóa Database thành công" : "Không xóa được Db";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cập nhật Database thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}

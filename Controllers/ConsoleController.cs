using GameBrainExplorer.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameBrainExplorer.Controllers
{
    public class ConsoleController : Controller
    {
        private readonly ConsoleService _service;

        public ConsoleController(ConsoleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string search)
        {
            var consoles = await _service.GetAllConsoles();
            return View(consoles);
        }
    }
}


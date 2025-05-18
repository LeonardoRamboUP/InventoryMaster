using Microsoft.AspNetCore.Mvc;

namespace InventoryMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Funcionando!");
    }
}
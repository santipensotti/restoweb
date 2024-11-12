using Microsoft.AspNetCore.Mvc;
using PruebaDoble.Server.Services;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace PruebaDoble.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRController : ControllerBase
    {
        private readonly QRCodeService _qrService;

        public QRController(QRCodeService qrService)
        {
            _qrService = qrService;
        }

        [HttpGet("generate/{restaurantId}/{tableId}")]
        public IActionResult GenerateQR(int restaurantId, int tableId)
        {
            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var url = $"{baseUrl}/mesa/{restaurantId}/{tableId}";
                var qrBytes = _qrService.GenerarCodigoQR(url);
                
                return File(qrBytes, "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

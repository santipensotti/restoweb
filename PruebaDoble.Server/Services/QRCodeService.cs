using System;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace PruebaDoble.Server.Services
{
    public class QRCodeService
    {
        private readonly string _secretKey = "TuCulito";  // Puede ser un valor configurable

        // M�todo que genera el QR para una mesa espec�fica
        public byte[] GenerateQRCodeForTable(string baseUrl, int restaurantId, int tableId)
        {
            try
            {
                // Construir la URL completa usando el baseUrl
                var url = $"{baseUrl}/table/{restaurantId}/{tableId}";

                // Generar el c�digo QR con la URL
                return GenerateQRCode(url);
            }
            catch (Exception ex)
            {
                // Manejo de errores si algo falla
                throw new Exception($"Error generating QR for restaurant {restaurantId}, table {tableId}: {ex.Message}");
            }
        }

        // Generaci�n del QR a partir de un contenido
        public byte[] GenerateQRCode(string content)
        {
            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        // Si necesitas generar un hash �nico para la mesa (por ejemplo, para validar o para otras razones)
        public string GenerateTableHash(int restaurantId, int tableId)
        {
            var dataToHash = $"{restaurantId}-{tableId}-{_secretKey}";
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataToHash));
            return Convert.ToBase64String(hashBytes).Replace("/", "_").Replace("+", "-").Substring(0, 10);
        }
    }
}

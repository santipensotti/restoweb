using System;
using System.Threading.Tasks;

namespace PruebaDoble.Server.Services
{
    public class NavigationService
    {
        public async Task<(double Latitude, double Longitude)> GetCurrentLocation()
        {
            // Aquí podrías implementar la lógica real para obtener la ubicación
            // Por ahora devolvemos una ubicación fija de ejemplo
            return (-34.9214, -57.9544);
        }

        public bool IsLocationWithinRange((double Latitude, double Longitude) currentLocation, 
                                        (double Latitude, double Longitude) targetLocation, 
                                        double maxDistanceInMeters)
        {
            var distance = CalculateDistance(currentLocation, targetLocation);
            return distance <= maxDistanceInMeters;
        }

        private double CalculateDistance((double Latitude, double Longitude) point1, 
                                       (double Latitude, double Longitude) point2)
        {
            var d1 = point1.Latitude * (Math.PI / 180.0);
            var d2 = point2.Latitude * (Math.PI / 180.0);
            var num1 = point1.Longitude * (Math.PI / 180.0);
            var num2 = point2.Longitude * (Math.PI / 180.0);
            var d3 = d2 - d1;
            var d4 = num2 - num1;

            var d5 = Math.Pow(Math.Sin(d3 / 2.0), 2.0) + 
                     (Math.Cos(d1) * Math.Cos(d2) * 
                      Math.Pow(Math.Sin(d4 / 2.0), 2.0));

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d5), Math.Sqrt(1.0 - d5)));
        }
    }
}

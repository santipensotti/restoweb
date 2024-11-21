using BlazorAppWithServer.Server.Data;
using BlazorAppWithServer.Shared.Models;
//using PruebaDoble.Client.Pages;

public class RestaurantService
{

    // L�gica para crear un restaurante
    public void CreateRestaurant(RestaurantRequest restaurantRequest)
    {
        // Obtener el siguiente ID desde el repositorio
        int nextId = RestaurantRepository.GetNextId();

        // Crear el restaurante con el ID asignado
        var newRestaurant = new Restaurant(
            id: nextId,
            name: restaurantRequest.Name,
            latitude: restaurantRequest.Latitude,
            longitude: restaurantRequest.Longitude,
            countTables: restaurantRequest.TableCount
        );

        // Delegar al repositorio para agregar el restaurante
        RestaurantRepository.AddRestaurant(newRestaurant);
    }

    // L�gica para actualizar un restaurante
    public void UpdateRestaurant(int id, RestaurantRequest restaurantRequest)
    {
        var restaurant = RestaurantRepository.GetRestaurant(id);

        if (restaurant != null)
        {
            restaurant.UpdateRestaurant(
                restaurantRequest.Name,
                restaurantRequest.TableCount,
                restaurantRequest.Latitude,
                restaurantRequest.Longitude
            );
        }
    }

    public List<RestaurantViewModel> GetAllRestaurants()
    {
        var restaurants = RestaurantRepository.GetAllRestaurants();
        return restaurants.Select(r => new RestaurantViewModel
        {
            Id = r.Id,
            Name = r.Name,
            TablesCount = r.getCountOfTables(),
            Latitude = r.Latitude,
            Longitude = r.Longitude,
            ShowDetails = false // Opcional: Inicializar si es requerido en el cliente
        }).ToList();
    }

    public RestaurantRequest GetRestaurantRequestById(int id)
    {
        var restaurant = RestaurantRepository.GetRestaurant(id);
        if (restaurant == null)
        {
            throw new Exception($"Restaurant with ID {id} not found.");
        }

        // Construir el objeto RestaurantRequest
        var restaurantRequest = new RestaurantRequest
        {
            Name = restaurant.Name,
            TableCount = restaurant.getCountOfTables(),
            Latitude = restaurant.Latitude,
            Longitude = restaurant.Longitude
        };

        return restaurantRequest;
    }

    public List<RestaurantMapInfo> GetInfoRestaurantForMaps()
    {
        var restaurants = RestaurantRepository.GetAllRestaurants();
        return restaurants.Select(r => new RestaurantMapInfo
        {
            Id = r.Id, // Incluye el Id del restaurante
            Lat = r.Latitude,
            Lng = r.Longitude,
            Name = r.Name,
            TablesCount = r.getCountOfTables()
        }).ToList();
    }

}

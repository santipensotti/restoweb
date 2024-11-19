using BlazorAppWithServer.Server.Data;
using BlazorAppWithServer.Shared.Models;
//using PruebaDoble.Client.Pages;

public class RestaurantService
{

    // Lógica para crear un restaurante
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

    // Lógica para actualizar un restaurante
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

    public List<Restaurant> GetAllRestaurants()
    {
        return RestaurantRepository.GetAllRestaurants();
    }

    public Restaurant GetRestaurantById(int id)
    {
        var restaurant = RestaurantRepository.GetRestaurant(id);
        return restaurant;
    }

}

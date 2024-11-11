using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAppWithServer.Shared.Models;

namespace BlazorAppWithServer.Server.Data;

public static class RestaurantRepository
{
    private static List<Restaurant> _restaurants = new()
    {
        new Restaurant(1,1),
        new Restaurant(2, 2),
        new Restaurant(3, 3)
    };

    private static readonly string filePath = "restaurants.json";

    public static int AddRestaurant(int countMesas)
    {
        int newId = _restaurants.Count;
        _restaurants.Add(new Restaurant(newId, countMesas));
        return newId;
    }

    public static void AddItemToMenu(int restaurantId, int price, string name)
    {
        if (restaurantId < 0 || restaurantId >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        var menuItem = new MenuItem { Name = name, Price = price };
        _restaurants[restaurantId].addToMenu(menuItem);
    }

    public static void DeleteItemFromMenu(int restaurantId, int price, string name)
    {
        if (restaurantId < 0 || restaurantId >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        var menuItem = new MenuItem { Name = name, Price = price };
        _restaurants[restaurantId].deleteToMenu(menuItem);
    }

    public static List<Tuple<string, int>> getMenuOfARestaurant(int id)
    {
        if (id < 0 || id >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        return _restaurants[id].getElementsOfMenu();
    }

    public static void addTable(int id)
    {
        if (id < 0 || id >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        _restaurants[id].addTable();
    }

    public static void substractTable(int id)
    {
        if (id < 0 || id >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        _restaurants[id].substractTable();
    }

    public static int getCountTableFromRestaurant(int id)
    {
        if (id < 0 || id >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        return _restaurants[id].getCountOfTables();
    }

    public static List<Restaurant> GetAllRestaurants()
    {
        return _restaurants;
    }

    public static Restaurant GetRestaurant(int id)
    {
        if (id < 0 || id >= _restaurants.Count)
            throw new ArgumentException("ID de restaurante no válido");

        return _restaurants[id];
    }

    public static int getPriceOfItemFromARestaurant(int id, string item)
    {
        return _restaurants[id].getPriceOfItem(item);
    }

    public static async Task GuardarEstados()
    {
        var estados = new List<object>();

        // Recorre cada restaurante y obtiene su estado
        foreach (var restaurant in _restaurants)
        {
            estados.Add(restaurant.getState());
        }

        // Guarda todos los estados en un único archivo JSON
        var options = new JsonSerializerOptions { WriteIndented = true };
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(stream, estados, options);
        }
    }
    public static async Task LoadStates()
    {
        if (!File.Exists(filePath))
            return;

        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            var estados = await JsonSerializer.DeserializeAsync<List<RestaurantState>>(stream);
            if (estados != null)
            {
                _restaurants.Clear();
                foreach (var estado in estados)
                {
                    Console.WriteLine(estado.Id);
                    var restaurant = new Restaurant(estado.Id, estado.Tables.Count);
                    restaurant.loadState(estado);
                    _restaurants.Add(restaurant);
                }
            }
        }
    }
}
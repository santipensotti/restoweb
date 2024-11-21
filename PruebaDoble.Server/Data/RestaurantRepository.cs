using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAppWithServer.Shared.Models;

namespace BlazorAppWithServer.Server.Data;

public static class RestaurantRepository
{
    private static List<Restaurant> _restaurants = new List<Restaurant>();

    public static int GetNextId()
    {
        return _restaurants.Count;  // El siguiente ID es el tamaño de la lista
    }

    public static void AddRestaurant(Restaurant restaurant)
    {
        _restaurants.Add(restaurant);
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

    public static int getPriceOfItemFromARestaurant(int id, string item)
    {
        return _restaurants[id].getPriceOfItem(item);
    }

}
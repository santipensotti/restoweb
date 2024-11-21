using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using BlazorAppWithServer.Shared.Models;

namespace BlazorAppWithServer.Server.Data;

public class Restaurant
{
    private int _id;
    private Menu _menu;
    private List<Table> _tables;

    public int Id => _id;

    public string Name { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public Restaurant(int id, string name, double latitude, double longitude, int countTables)
    {
        _id = id;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        _menu = new Menu();
        _tables = new List<Table>();
        this.AddTables(countTables);
    }
    public void UpdateRestaurant(string name, int countTables, double latitude, double longitude)
    {
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        AddTables(countTables);  
    }

    public void AddTables(int countTables)
    {

        _tables.Clear();
        for (int i = 0; i < countTables; i++)
        {
            _tables.Add(new Table(i));
        }
    }

    public void addToMenu(MenuItem elem)
    {
        _menu.addMenuItem(elem);
    }

    public void deleteToMenu(MenuItem elem)
    {
        _menu.deleteMenuItem(elem);
    }

    public int getPriceOfItem(string name)
    {
        return _menu.priceOfItem(name);

    }
    public List<Tuple<string, int>> getElementsOfMenu()
    {

        List<Tuple<string, int>> elementosDelMenu = new List<Tuple<string, int>>();
        elementosDelMenu = _menu.getNamesAndPrices();
        return elementosDelMenu;
    }

    public void addTable()
    {
        int tableId = _tables.Count;
        _tables.Add(new Table(tableId));
    }

    public void substractTable()
    {
        _tables.RemoveAt(_tables.Count - 1);
    }

    public int getCountOfTables()
    {
        return _tables.Count;
    }
    
    public object getState()
    {
        return new
        {
            Id = _id,
            Tables = _tables,
            Menu = _menu.getMenuItems() 
        };
    }

    public void loadState(RestaurantState state)
    {
        _id = state.Id;
        _tables = state.Tables;
        _menu.loadState(state.Menu);
    }
}
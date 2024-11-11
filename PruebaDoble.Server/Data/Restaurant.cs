using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using BlazorAppWithServer.Shared.Models;

namespace BlazorAppWithServer.Server.Data;

public class Restaurant
{

    public Restaurant(int id, int countTables)
    {
        _id = id;
        _menu = new Menu();
        _tables = new List<Table>();    
        for (int i = 0; i < countTables; i++)
        {
            this.addTable();
        }
    }

    private int _id;

    //Menu del copado
    private Menu _menu;
    
    //Mesas del copado
    private List<Table> _tables;


    // a dictionary may be better?
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
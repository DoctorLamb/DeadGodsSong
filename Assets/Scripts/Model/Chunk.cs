using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Chunk
{
    //Chunks make up the entirety of the map
    public int U; //X position of the chunk in the map
    public int V; //Y position of the chunk in the map

    public int X, Y; //X and Y Dimensions of the chunk

    public Tile[] tiles;

    public Chunk(int _U, int _V, int _X, int _Y) {
        U = _U;
        V = _V;
        X = _X;
        Y = _Y;
        tiles = new Tile[X * Y];
    }

    public void AddTile(int _i, int _j, int _type)
    {
        tiles[Index(_i, _j)] = new Tile(_i, _j, _type);
    }

    public Tile NewTile(int _i, int _j, int _type)
    {
        return tiles[Index(_i, _j)] = new Tile(_i, _j, _type);
    }

    public void AddTile(int _i, int _j, int _type, int _connection)
    {
        tiles[Index(_i, _j)] = new Tile(_i, _j, _type, _connection);
    }

    int Index(int _i, int _j)
    {
        return _i + (X * _j);
    }
}

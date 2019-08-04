using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public int X;
    public int Y;

    public Vector2Int Position {
        get {
            return new Vector2Int(X, Y);
        }
        set {
            X = value.x;
            Y = value.y;
        }
    }

    public TileType type;

    public Tile(int _X, int _Y, int _type)
    {
        X = _X;
        Y = _Y;
        type = (TileType)_type;
    }
    public Tile(int _X, int _Y, TileType _type)
    {
        X = _X;
        Y = _Y;
        type = _type;
    }
}
public enum TileType {
    Room,
    Entrance,
    Left, // The Exit from a structure that goes to another (NOT NECCESSARILY TO THE LEFT OF THE STRUCTURE IT IS JUST A NAME)
    Right,// The Entrance to a structure that comes from another (NOT NECCESSARILY TO THE RIGHT OF THE STRUCTURE IT IS JUST A NAME)
    Exit
}

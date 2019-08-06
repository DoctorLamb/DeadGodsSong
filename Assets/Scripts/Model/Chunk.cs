using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk
{
    //Chunks make up the entirety of the map
    public int I; //X position of the chunk in the map
    public int J; //Y position of the chunk in the map

    public int X, Y; //X and Y Dimensions of the chunk
    public int maxX;


    public Tile[] tiles;

    public Chunk(int _I, int _J, int _X, int _Y, int _maxX) {
        I = _I;
        J = _J;
        X = _X;
        Y = _Y;
        maxX = _maxX;
    }

    void Add(int _i, int _j, int _type)
    {
        tiles[Index(_i, _j)] = new Tile(_i, _j, _type);
    }

    int Index(int _i, int _j)
    {
        return _i + (maxX * _j);
    }
}

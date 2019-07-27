using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BoardController : MonoBehaviour
{
    Tile[] tiles;
    public Vector2Int start, end;

    public Tile GetTile(int x, int y)
    {
        return GetTile(new Vector2Int(x, y));
    }

    public Tile GetTile(Vector2Int _position)
    {
        return tiles.Where(t => t.position == _position).FirstOrDefault(); 
    }

    public List<Tile> GetPath(Tile a, Tile b)
    {
        List<Tile> temp = new List<Tile>();
        return temp;
    }
}
[Serializable]
public class Tile {
    [Range(0, 10)]
    public int tileType; //Tile types are 1 - 10, situations only for 0 & 1 now, either ground (0), or ladders (1)
    public Vector2Int position;
    public int h, g, f;
}

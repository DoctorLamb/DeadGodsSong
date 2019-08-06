using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject tilePrefab;

    public Chunk[] Map;

    Color Entrance = Color.green;
    Color Exit = Color.red;
    Color Left = Color.blue;
    Color color = Color.yellow;

    int maxY;
    int maxX;
    public int chunkX, chunkY;
    int i, j; // Current Tile - reset with each tile
    int u, v; //Current Chunk
    public int seed = 42;


    private void Start()
    {
        GenerateMap();
        VisualizeMap();
    }

    void GenerateMap() {

        Map = new Chunk[1];
        Map[0] = new Chunk(0, 0, 3, 3);
        Random.InitState(seed);
        j = 2;
        i = Random.Range(0, 3);

        bool bottom;
        bool bottom2;
        bottom = bottom2 = false;

        Map[0].Add(i, j, 1);
        maxX = Map[0].X;
        maxY = Map[0].Y;
        while (!bottom2)
        {
            chooseRoom(ref bottom, ref bottom2);
        }
    }

    void chooseRoom(ref bool a, ref bool b) {
        float r = Random.Range(0.0f, 1.0f);
        if (r > 0.67f)
        {
            bool c, d;
            c = a;
            d = b;
            checkDown(ref a, out b);
        }
        else
        {
            //Move Left or Right
            if (i == 0) //Left Wall, no left possible
            {
                if (!checkRight()){ checkDown(ref a, out b); }
            }
            else if (i == maxX - 1) //Right Wall, no right possible
            {
                if (!checkLeft()) { checkDown(ref a, out b); }
            }
            else
            {
                //In between walls so both left and right are valid options
                if (r > 0.34f) //Try left
                {
                    if (!checkLeft()) { if (!checkRight()) { checkDown(ref a, out b); } }
                }
                else
                {
                    if (!checkRight()) { if (!checkLeft()) { checkDown(ref a, out b); } }
                }
            }
        }
    }

    bool checkDown(ref bool a, out bool b) {
        //Move Down
        //Add Room below
        j--;
        if (j < 0) j = 0;

        if (j > 0)
        { //Not near bottom
            Map[0].Add(i, j, 0);
            b = false;
            return true;
        }
        else if (j <= 0)
        {
            if (a)
            { //Already on bottom, exit condition
              //Make Room Exit
                Map[0].Add(i, j, 4);
                b = true;
                return true;
            }
            else
            { //Just hit bottom
                a = true;
                b = false;
                Map[0].Add(i, j, 0);
                return true;
            }
        }
        a = false;
        b = false;
        return false;
    }

    bool checkLeft() {
        if (Map[0].tiles[Index(i - 1, j)] == null)
        {
            i--;
            Map[0].Add(i, j, 0);
            return true;
        }
        return false;
    }

    bool checkRight() {
        if (Map[0].tiles[Index(i + 1, j)] == null)
        {
            i++;
            Map[0].Add(i, j, 0);
            return true;
        }
        return false;
    }

    private void VisualizeMap() {
        Debug.Log("In Visualize");
        foreach (Chunk c in Map)
        {
            foreach (Tile t in c.tiles)
            {
                if (t != null)
                {
                    GameObject tile = Instantiate(tilePrefab, new Vector3(t.X, t.Y, 0), Quaternion.identity, this.transform);
                    tile.name = $"Tile({t.X}, {t.Y})";
                    if (t.type == TileType.Entrance) tile.GetComponent<SpriteRenderer>().color = Entrance;
                    if (t.type == TileType.Exit) tile.GetComponent<SpriteRenderer>().color = Exit;
                }
            }
        }
    }

    int Index(int _i, int _j) {
        return _i + (maxX * _j);
    }
}

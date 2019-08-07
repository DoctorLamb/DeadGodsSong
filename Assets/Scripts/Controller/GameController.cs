using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject tilePrefab;

    public List<Chunk> Chunks;
    public Chunk currentChunk;

    Color Entrance = Color.green;
    Color Exit = Color.red;
    Color Left = Color.blue;
    Color color = Color.yellow;

    int i, j; // Current Tile - reset with each tile
    int u, v;
    public int seed = 42;
    public int criticalPathLength = 1;


    private void Start()
    {
        GenerateMap();
        VisualizeMap();
    }

    void GenerateMap() {

        Random.InitState(seed);

        Chunks = new List<Chunk>();
        u = v = 0; 
        AddChunk();

        j = currentChunk.Y - 1;
        i = Random.Range(0, currentChunk.X);
        int n = 0;
        while (n < criticalPathLength)
        {
            GenerateChunk();
            //Generate Pathway
                //Choose room from current chunk's exit point. (Left)
                //Choose another room (Right)
                //Choose the I and J for a room below the (Right)
                //Generate a new chunk with that I and J as its start. <<< Take care whjen setting the chunks U and V tis will effect visualization.
                // ---- Repeat till Chunks = critical path length.
            n++;
        }
    }

    void GenerateChunk() {
        bool bottom;
        bool bottom2;
        bottom = bottom2 = false;

        currentChunk.AddTile(i, j, 1);

        while (!bottom2)
        {
            Vector2Int pos = chooseRoom(ref bottom, ref bottom2);
            i = pos.x;
            j = pos.y;
            if (bottom2) currentChunk.AddTile(i, 0, 4);
            else { currentChunk.AddTile(i, j, 0); }
        }
    }

    void GeneratePathway() {

    }

    Vector2Int chooseRoom(ref bool a, ref bool b) {
        float r = Random.Range(0.0f, 1.0f);
        if (r > 0.67f)
        {
            bool c, d;
            c = a;
            d = b;
            if (checkDown(ref a, out b))
            {
                return new Vector2Int(i, j - 1);
            }
            else {
                return new Vector2Int(i, j);
            }
        }
        else
        {
            //Move Left or Right
            if (i == 0) //Left Wall, no left possible
            {
                if (!checkRight())
                {
                    if (checkDown(ref a, out b)) {
                        return new Vector2Int(i, j - 1);
                    }
                    else
                    {
                        return new Vector2Int(i, j);
                    }
                }
                else {
                    return new Vector2Int(i + 1, j);
                }
            }
            else if (i == currentChunk.X - 1) //Right Wall, no right possible
            {
                if (!checkLeft())
                {
                    if (checkDown(ref a, out b))
                    {
                        return new Vector2Int(i, j - 1);
                    }
                    else
                    {
                        return new Vector2Int(i, j);
                    }
                }
                else {
                    return new Vector2Int(i - 1, j);
                }
            }
            else
            {
                //In between walls so both left and right are valid options
                if (r > 0.34f) //Try left
                {
                    if (!checkLeft())
                    {
                        if (!checkRight())
                        {
                            if (checkDown(ref a, out b))
                            {
                                return new Vector2Int(i, j - 1);
                            }
                            else
                            {
                                return new Vector2Int(i, j);
                            }
                        }
                        else
                        {
                            return new Vector2Int(i + 1, j);
                        }
                    }
                    else {
                        return new Vector2Int(i - 1, j);
                    }
                }
                else
                {
                    if (!checkRight())
                    {
                        if (!checkLeft())
                        {
                            if (checkDown(ref a, out b))
                            {
                                return new Vector2Int(i, j - 1);
                            }
                            else
                            {
                                return new Vector2Int(i, j);
                            }
                        }
                        else
                        {
                            return new Vector2Int(i - 1, j);
                        }
                    }
                    else {
                        return new Vector2Int(i + 1, j);
                    }
                }
            }
        }
    }

    bool checkDown(ref bool a, out bool b) {
        //Move Down
        //Add Room below
        int y = j - 1;
        if (y < 0) y = 0;

        if (y > 0)
        { //Not near bottom
            
            b = false;
            return true;
        }
        else if (y <= 0)
        {
            if (a)
            { //Already on bottom, exit condition
              //Make Room Exit
                b = true;
                return true;
            }
            else
            { //Just hit bottom
                a = true;
                b = false;

                return true;
            }
        }
        a = false;
        b = false;
        return false;
    }

    bool checkLeft() {
        if (currentChunk.tiles[Index(i - 1, j)] == null)
        {
            return true;
        }
        return false;
    }

    bool checkRight() {
        if (currentChunk.tiles[Index(i + 1, j)] == null)
        {
            return true;
        }
        return false;
    }

    void AddChunk()
    {
        Chunk temp = new Chunk(u, v, Random.Range(3, 12), Random.Range(3, 12));
        currentChunk = temp;
        Chunks.Add(temp);
    }

    void AddPathway() {
        //Need to properly set U and V which cant be done until the next room is chosen
        u = i;
        v = j;
        i = j = 0;
        Chunk temp = new Chunk(u, v, Random.Range(2, 4), Random.Range(2, 4));
        currentChunk = temp;
        Chunks.Add(temp);
    }

    private void VisualizeMap() {
        foreach (Chunk c in Chunks)
        {
            foreach (Tile t in c.tiles)
            {
                if (t != null)
                {
                    GameObject tile = Instantiate(tilePrefab, new Vector3(t.X + c.U, t.Y + c.V, 0), Quaternion.identity, this.transform);
                    tile.name = $"Tile({t.X}, {t.Y})";
                    if (t.type == TileType.Entrance) tile.GetComponent<SpriteRenderer>().color = Entrance;
                    if (t.type == TileType.Exit) tile.GetComponent<SpriteRenderer>().color = Exit;
                }
            }
        }
    }

    int Index(int _i, int _j) {
        return _i + (currentChunk.X * _j);
    }
}
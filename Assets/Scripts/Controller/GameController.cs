using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public GameObject tilePrefab;

    public Tile[] Map;

    Color Entrance = Color.green;
    Color Exit = Color.red;
    Color Left = Color.blue;
    Color color = Color.yellow;

    public int maxY;
    public int maxX;
    int i, j;
    public int seed = 42;


    bool bottom;
    bool bottom2;

    private void Start()
    {
        GenerateMap();
        VisualizeMap();
    }
    /*
        private void GenerateMap()
        {
            Debug.Log("In Generate");
            int n = 0;

            Map = new Tile[maxX * maxY];
            Random.InitState(seed);
            int j  = maxY-1;
            int i = Random.Range(0, maxX);
            Debug.Log($"Adding Entrance at {i}, {j}");
            Add(i, j, 1);
            while (1 > 0)
            { //Algorithm runs until it hits the bottom and trie to go down again.
                float r = Random.Range(0.0f, 1.0f);
                if (r > 0.67f)
                {
                    //Move Down
                    //Add Room below
                    j--;
                    if (j < 0) j = 0;

                    if (j > 0)
                    { //Not near bottom

                        Add(i, j, 0);
                    }
                    else if (j <= 0)
                    {
                        if (bottom)
                        { //Already on bottom, exit condition
                          //Make Room Exit
                            Add(i, j, 4);
                            break;
                        }
                        else
                        { //Just hit bottom
                            bottom = true;
                            Add(i, j, 0);
                        }
                    }
                }
                else
                {
                    //Move Left or Right
                    if (i == 0)
                    {
                        //Move Right
                        if (Map[Index(i + 1, j)] == null)
                        {
                            i++;
                            Add(i, j, 0);
                        }
                        else //else move down
                        {
                            //Move Down if possible
                            if (j > 0)
                            { //Not near bottom

                                Add(i, j, 0);
                            }
                            else if (j <= 0)
                            {
                                if (bottom)
                                { //Already on bottom, exit condition
                                  //Make Room Exit
                                    Add(i, j, 4);
                                    break;
                                }
                                else
                                { //Just hit bottom
                                    bottom = true;
                                    Add(i, j, 0);
                                }
                            }
                        }
                    }
                    else if (i == maxX)
                    {
                        //Move Left if empty
                        if (Map[Index(i - 1, j)] == null)
                        {
                            i--;
                            Add(i, j, 0);
                        }
                        else //else move down
                        {
                            //Move Down if possible
                            if (j > 0)
                            { //Not near bottom

                                Add(i, j, 0);
                            }
                            else if (j <= 0)
                            {
                                if (bottom)
                                { //Already on bottom, exit condition
                                  //Make Room Exit
                                    Add(i, j, 4);
                                    break;
                                }
                                else
                                { //Just hit bottom
                                    bottom = true;
                                    Add(i, j, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"{i},{j}");
                        if (r > 0.34f)
                        {
                            if(i > 0)
                            { //Move Left  if empty
                                if (Map[Index(i - 1, j)] == null)
                                {
                                    i--;
                                    Add(i, j, 0);
                                }
                                else 
                                if (Map[Index(i + 1, j)] == null) //else move right
                                {
                                    i++;
                                    Add(i, j, 0);
                                }
                                else if (j <= 0)
                                {
                                    if (bottom)
                                    { //Already on bottom, exit condition
                                      //Make Room Exit
                                        Add(i, j, 4);
                                        break;
                                    }
                                    else
                                    { //Just hit bottom
                                        bottom = true;
                                        Add(i, j, 0);
                                    }
                                }
                            }
                            else if (Map[Index(i + 1, j)] == null) //else move right
                            {
                                i++;
                                Add(i, j, 0);
                            }
                            else //else move down
                            {
                                //Move Down if possible
                                if (j > 0)
                                { //Not near bottom

                                    Add(i, j, 0);
                                }
                                else if (j <= 0)
                                {
                                    if (bottom)
                                    { //Already on bottom, exit condition
                                      //Make Room Exit
                                        Add(i, j, 4);
                                        break;
                                    }
                                    else
                                    { //Just hit bottom
                                        bottom = true;
                                        Add(i, j, 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Move Right if empty
                            if (i < maxX)
                            {
                                else if (Map[Index(i - 1, j)] == null) //else move left
                                {
                                    i--;
                                    Add(i, j, 0);
                                }
                                else //else move down
                                {
                                    //Move Down if possible
                                    if (j > 0)
                                    { //Not near bottom

                                        Add(i, j, 0);
                                    }
                                    else if (j <= 0)
                                    {
                                        if (bottom)
                                        { //Already on bottom, exit condition
                                          //Make Room Exit
                                            Add(i, j, 4);
                                            break;
                                        }
                                        else
                                        { //Just hit bottom
                                            bottom = true;
                                            Add(i, j, 0);
                                        }
                                    }
                                }
                            }
                            if (Map[Index(i + 1, j)] == null)
                            {
                                i++;
                                Add(i, j, 0);
                            }
                            else //else move down
                            {
                                //Move Down if possible
                                if (j > 0)
                                { //Not near bottom

                                    Add(i, j, 0);
                                }
                                else if (j <= 0)
                                {
                                    if (bottom)
                                    { //Already on bottom, exit condition
                                      //Make Room Exit
                                        Add(i, j, 4);
                                        break;
                                    }
                                    else
                                    { //Just hit bottom
                                        bottom = true;
                                        Add(i, j, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    */

    void GenerateMap() {

        Map = new Tile[maxX * maxY];
        Random.InitState(seed);
        j = maxY - 1;
        i = Random.Range(0, maxX);
        bottom = bottom2 = false;
        Add(i, j, 1);

        while (!bottom2)
        {
            chooseRoom();
        }
    }
    void chooseRoom() {
        float r = Random.Range(0.0f, 1.0f);
        if (r > 0.67f)
        {
            checkDown();
        }
        else
        {
            //Move Left or Right
            if (i == 0) //Left Wall, no left possible
            {
                if (!checkRight()){ checkDown(); }
            }
            else if (i == maxX - 1) //Right Wall, no right possible
            {
                if (!checkLeft()) { checkDown(); }
            }
            else
            {
                //In between walls so both left and right are valid options
                if (r > 0.34f) //Try left
                {
                    if (!checkLeft()) { if (!checkRight()) { checkDown(); } }
                }
                else
                {
                    if (!checkRight()) { if (!checkLeft()) { checkDown(); } }
                }
            }
        }
    }

    bool checkDown() {
        //Move Down
        //Add Room below
        j--;
        if (j < 0) j = 0;

        if (j > 0)
        { //Not near bottom
            Add(i, j, 0);
            return true;
        }
        else if (j <= 0)
        {
            if (bottom)
            { //Already on bottom, exit condition
              //Make Room Exit
                Add(i, j, 4);
                bottom2 = true;
                return true;
            }
            else
            { //Just hit bottom
                bottom = true;
                Add(i, j, 0);
                return true;
            }
        }
        return false;
    }

    bool checkLeft() {
        if (Map[Index(i - 1, j)] == null)
        {
            i--;
            Add(i, j, 0);
            return true;
        }
        return false;
    }

    bool checkRight() {
        if (Map[Index(i + 1, j)] == null)
        {
            i++;
            Add(i, j, 0);
            return true;
        }
        return false;
    }

    private void VisualizeMap() {
        Debug.Log("In Visualize");
        foreach (Tile t in Map) {
            if (t != null) {
                GameObject tile = Instantiate(tilePrefab, new Vector3(t.X, t.Y, 0), Quaternion.identity, this.transform);
                tile.name = $"Tile({t.X}, {t.Y})";
                if (t.type == TileType.Entrance) tile.GetComponent<SpriteRenderer>().color = Entrance;
                if (t.type == TileType.Exit) tile.GetComponent<SpriteRenderer>().color = Exit;
            }
        }
    }

    void Add(int _i, int _j, int _type) {
        Map[Index(_i, _j)] = new Tile(_i, _j, _type);
    }
    int Index(int _i, int _j) {
        return _i + (maxX * _j);
    }
}

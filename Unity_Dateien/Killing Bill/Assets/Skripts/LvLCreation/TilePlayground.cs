using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class TilePlayground : MonoBehaviour
{
    public int roomWidth;
    public int roomHeight;

    /*
     * Tile nummerierung Übersicht:
     * -1 nicht definiert
     * 0 Tür
     * 1 Wand
     * 2 Boden
     * 3 Hoher Boden
     * 
     * Reihenfolge: North, East, South, West
     */
    private Tile[] tileset;
    private int[,] room;
    private int tileTurnAngle;

    void Start()
    {
        Tile wall = new Tile(1, 2, 2, 2, "Wall1");
        Tile corner = new Tile(1, 1, 2, 2, "Corner1");
        Tile floor = new Tile(2, 2, 2, 2, "Floor1");

        tileset = new Tile[3];
        tileset[0] = wall;
        tileset[1] = corner;
        tileset[2] = floor;

        MakeRoom();
    }

    void MakeRoom()
    {

        room = new int [roomWidth,roomHeight];




        for(int x = 0; x < roomWidth; x++)
        {
            for(int y = 0; y < roomHeight; y++)
            {
                Debug.Log("x: " + x + " y: " + y);
                if(x== 0 || x== roomWidth-1 || y == 0 || y == roomHeight-1)
                {
                    int i = findFittingTile(x, y);
    
                    var loadedObject = Resources.Load("Models/LevelTiles/" + tileset[i].model);

                    Instantiate(loadedObject, new Vector3(x*2, 0, y*2), Quaternion.Euler(0, tileTurnAngle, 0));
                }
                }
        }
    }

    /// <summary>
    /// Sucht ein Tile, welches an die angegebene Stelle passt.
    /// </summary>
    /// <param name="x">X Koordinate des zu füllenden Feldes</param>
    /// <param name="y">Y Koordinate des zu füllenden Feldes</param>
    /// <returns></returns>
    int findFittingTile(int x, int y)
    {
        int[] wantedSides = new int [4];

        if (x == 0)
        {
            wantedSides[3] = 1;
            wantedSides[1] = -1;
        }
        else if (x == room.Length)
        {
            wantedSides[1] = 1;
            wantedSides[3] = -1;
        }
        else
        {
            wantedSides[3] = tileset[room[x - 1,y]].borderEast;
            wantedSides[1] = -1;
        }

        if (y == 0)
        {
            wantedSides[0] = 1;
            wantedSides[2] = -1;
        }
        else if (y == room.GetLength(1))
        {
            wantedSides[2] = 1;
            wantedSides[0] = -1;
        }
        else
        {
            wantedSides[0] = tileset[room[x,y - 1]].borderSouth;
            wantedSides[2] = -1;
        }

        bool tileFound = false;
        while (!tileFound)
        {
            int i = GetRandomTile();
            if(CheckWantedSides(i, wantedSides))
            {
                return i;
            }
        }
        throw new Exception("Tile nicht auffindbar");
    }

    /// <summary>
    /// Gibt ein Zufälliges Tile im Tileset zurück, welches die Anforderungen der Parameter erfüllt.
    /// </summary>
    /// <param name="unwantedFlags">Diese Flags dürfen nicht im Tile enthalten sein.</param>
    /// <param name="wantedSides">Diese Flags müssen im Tile enthalten sein. </param>
    /// <returns></returns>
    int GetRandomTile()
    {
        System.Random rand = new System.Random();
        int i = rand.Next(3);
        return i;

    }

    /// <summary>
    /// Prüft ob ein Tile an allen Seiten den Vorgaben entsprechen
    /// </summary>
    /// <param name="tile">The place in the tilelist of the tile</param>
    /// <param name="wantedSides">Die Vorgegenen Seiten. Muss die Länge 4 haben. für uneingeschränkte Seiten -1 einsetzen</param>
    /// <returns></returns>
    bool CheckWantedSides(int tile, int[] wantedSides)
    {
        if(wantedSides.Length != 4)
        {
            throw new ArgumentException("CheckWantedSides: wantedSides Argument muss die Länge 4 haben.");
        }

        for (int i = 0; i < 4; i++)
        {
            if ((tileset[tile].borderNorth == wantedSides[(0 + i) % 4] || wantedSides[(0 + i) % 4] == -1) &&
                (tileset[tile].borderEast == wantedSides[(1 + i) % 4] || wantedSides[(1 + i) % 4] == -1) &&
                (tileset[tile].borderSouth == wantedSides[(2 + i) % 4] || wantedSides[(2 + i) % 4] == -1) &&
                (tileset[tile].borderWest == wantedSides[(3 + i) % 4] || wantedSides[(3 + i) % 4] == -1))
            {
                tileTurnAngle = i * 90;
                return true;
            }
        }

        return false;
    }

    //abstrahierte variante von CheckWantedSide. Nicht zur Nutzung gedacht
    bool CheckWantedSidesWIP(int[] sides, int[] wantedSides)
    {
        if (wantedSides.Length != 4)
        {
            throw new ArgumentException("CheckWantedSides: wantedSides Argument muss die Länge 4 haben.");
        }

        for(int i = 0; i < 4; i++)
        {
            if ((sides[0] == wantedSides[(0 + i) % 4] || wantedSides[(0 + i) % 4] == -1) &&
                (sides[1] == wantedSides[(1 + i) % 4] || wantedSides[(1 + i) % 4] == -1) &&
                (sides[2] == wantedSides[(2 + i) % 4] || wantedSides[(2 + i) % 4] == -1) &&
                (sides[3] == wantedSides[(3 + i) % 4] || wantedSides[(3 + i) % 4] == -1))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks a tile in the Tile List for unwanted Flags
    /// </summary>
    /// <param name="tile">The place in the tilelist of the tile</param>
    /// <returns></returns>
    bool CheckUnwantedFlags(int tile, int[] unwantedFlags)
    {
        if (unwantedFlags.Contains(tileset[tile].borderNorth) ||
            unwantedFlags.Contains(tileset[tile].borderNorth) ||
            unwantedFlags.Contains(tileset[tile].borderNorth) ||
            unwantedFlags.Contains(tileset[tile].borderNorth))
        {
            return false;
        }
        return true;
    }
}

class Tile
{
    public int borderNorth, borderEast, borderSouth, borderWest;
    //int allowedNorth, allowedEast, allowedSouth, allowedWest;
    public string model;

    public Tile (int borderNorth, int borderEast, int borderSouth, int borderWest,
        //int allowedNorth, int  allowedEast, int allowedSouth, int allowedWest,
        string model)
    {
        if(borderNorth == -1 || borderEast == -1 || borderSouth == -1 || borderWest == -1)
        {
            throw new ArgumentOutOfRangeException("Ein Tile darf nicht mit der Kante -1 initialisiert werden. -1 ist für leere Kanten reserviert");
        }

        this.borderNorth = borderNorth;
        this.borderEast = borderEast;
        this.borderSouth = borderSouth;
        this.borderWest = borderWest;

        /*
        this.allowedNorth = allowedNorth;
        this.allowedEast = allowedEast;
        this.allowedSouth = allowedSouth;
        this.allowedWest = allowedWest;
        */

        this.model = model;
    }
}
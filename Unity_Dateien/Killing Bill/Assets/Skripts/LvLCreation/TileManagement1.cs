using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TileManagement1 : MonoBehaviour
{
    public int roomWidth;
    public int roomLength;

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
    private List<Tile> tileset;
    private PlacedTile[,] room;

    void Start()
    {
        tileset = new List<Tile>();

        addTile(2, 2, 1, 1, "Wall1");
        addTile(2, 3, 1, 1, "Wall2");
        addTile(3, 2, 1, 1, "Wall3");
        addTile(3, 3, 1, 1, "Wall4");
        addTile(2, 1, 1, 1, "Corner1");
        addTile(3, 1, 1, 1, "Corner2");
        addTile(2, 2, 2, 2, "Floor1");
        addTile(2, 2, 2, 2, "Floor1");
        addTile(3, 3, 3, 3, "Floor2");
        addTile(3, 3, 3, 3, "Floor2");
        addTile(2, 3, 3, 3, "Floor3");
        addTile(2, 2, 3, 3, "Floor4");
        addTile(2, 2, 3, 2, "Floor5");
        //addTile(2, 4, 3, 2, "Floor6");
        //addTile(3, 5, 2, 2, "Floor7");


        
        MakeRoom();
        //MakeTileOverview();
    }

    void addTile(int north, int east, int south, int west, String model)
    {
        Tile tile_000 = new(north, east, south, west,  model, 0);
        Tile tile_090 = new(west, north, east, south,  model, 90);
        Tile tile_180 = new(south, west, north, east,  model, 180);
        Tile tile_270 = new(east, south, west, north,  model, 270);

        Debug.Log("Making tile " + tile_000.model);

        tileset.Add(tile_000);
        tileset.Add(tile_090);
        tileset.Add(tile_180);
        tileset.Add(tile_270);
    }

    void MakeTileOverview ()
    {
        int x = 0;
        int z = 0;
        for (int i = 0; i < tileset.Count; i+=4)
        {
            
            var loadedObject = Resources.Load("Prefabs/LevelTiles/" + tileset[i].model);
            Instantiate(loadedObject, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
            if(z == 5)
            {
                z = 0;
                x += 3;
            }
            else
            {
                z+=3;
            }
            
        }
    }

    void MakeRoom()
    {

        room = new PlacedTile[roomWidth, roomLength];


        for (int x = 0; x < roomWidth; x++)
        {
            for (int z = 0; z < roomLength; z++)
            {
                //This if statement makes sure we only spawn the outer tiles
                //if (x == 0 || x == roomWidth - 1 || z == 0 || z == roomLength - 1)
                //{
                    room[x, z] = findFittingTile(x, z);

                    var loadedObject = Resources.Load("Prefabs/LevelTiles/" + room[x, z].baseTile.model);
                    GameObject currentTile = (GameObject) Instantiate(loadedObject, new Vector3(x * 2, 0, z * 2), Quaternion.Euler(0, room[x, z].baseTile.Rotation, 0));
                    
                    currentTile.GetComponent<Renderer>().material = getTileMat(room[x, z].baseTile.Rotation);

                Debug.Log("MakeRoom: " + room[x, z].baseTile.getVert(0) + ", " +
                        room[x, z].baseTile.getVert(1) + ", " +
                        room[x, z].baseTile.getVert(2) + ", " +
                        room[x, z].baseTile.getVert(3) + ", ");
                Debug.Log("------------------------------");
                //}
        }
        }
    }

    Material getTileMat(int rotation)
    {
        System.Random rand = new System.Random();

        int numb = rand.Next(10);

        string postFix = "0";

        if (rotation == 0)
        {
            postFix = "0";
        }
        else if (rotation == 90)
        {
            postFix = "270";
        }
        else if (rotation == 180)
        {
            postFix = "180";
        }
        else if (rotation == 270)
        {
            postFix = "90";
        }


        if (numb < 7)
        {
            return Resources.Load("Materials/Flesh " + postFix, typeof(Material)) as Material;
        }
        else
        {
            return Resources.Load("Materials/Bones " + postFix, typeof(Material)) as Material;
        }
    }

    /*
     * Tile nummerierung Übersicht:
     * 0 nicht definiert
     * 11 Tür
     * 1 Wand
     * 2 Boden
     * 3 Hoher Boden
     * 4 wechsel niedrig -> hoch
     * 
     * Reihenfolge: North, East, South, West
     * 0 North = blender ?
     * 1 East  = blender ?
     * 2 South = blender ?
     * 3 West  = blender ?
     */
    /// <summary>
    /// Sucht ein Tile, welches an die angegebene Stelle passt.
    /// </summary>
    /// <param name="x">X Koordinate des zu füllenden Feldes</param>
    /// <param name="z">Y Koordinate des zu füllenden Feldes</param>
    /// <returns></returns>
    PlacedTile findFittingTile(int x, int z)
    {
        Debug.Log("FindFittingTile: x: " + x + ", z: " + z);

        int[] wantedVerts = new int[4];

        if (x == 0)
        {
            wantedVerts[0] = 1;
            wantedVerts[1] = 1;
        }
        else if (x == roomWidth - 1)
        {
            wantedVerts[0] = room[x - 1, z].baseTile.getVert(3);
            wantedVerts[1] = room[x - 1, z].baseTile.getVert(2);
            wantedVerts[2] = 1;
            wantedVerts[3] = 1;
        }
        else
        {
            wantedVerts[0] = room[x - 1, z].baseTile.getVert(3);
            wantedVerts[1] = room[x - 1, z].baseTile.getVert(2);
        }

        if (z == 0)
        {
            wantedVerts[0] = 1;
            wantedVerts[3] = 1;
        }
        else if (z == roomLength - 1)
        {
            wantedVerts[0] = room[x, z - 1].baseTile.getVert(1);
            wantedVerts[1] = 1;
            wantedVerts[2] = 1;
            wantedVerts[3] = room[x, z - 1].baseTile.getVert(2);
        }
        else
        {
            wantedVerts[0] = room[x, z - 1].baseTile.getVert(1);
            wantedVerts[3] = room[x, z - 1].baseTile.getVert(2);
        }

        Debug.Log("FindFittingTile: Wanted sides are:" + wantedVerts[0] + ", " + wantedVerts[1] + ", " + wantedVerts[2] + ", " + wantedVerts[3] + ", ");

        List<PlacedTile> fittingTiles = new List<PlacedTile>();
        for (int i = 0; i < tileset.Count; i++)
        {
            int[] verts = {tileset[i].VertNorthWest, tileset[i].VertNorthEast, tileset[i].VertSouthEast, tileset[i].VertSouthWest};

            //This stops walls from spawning, except on the edge of the room
            if (((z != roomLength - 1) && (tileset[i].VertSouthEast == 1) && tileset[i].VertNorthEast == 1) ||
                ((x != roomWidth - 1) && (tileset[i].VertSouthEast == 1) && tileset[i].VertSouthWest == 1) 
                )
            {
                continue;
            }

            if (CheckWantedVerts(verts, wantedVerts))
            {
                PlacedTile fittingTile = new PlacedTile(tileset[i], x, z);
                fittingTiles.Add(fittingTile);
            }
        }
        if(fittingTiles.Count == 0)
        {
            throw new MissingComponentException("ERROR! WIE ZUR HÖLLE soll ich denn bitte ein Tile für die wanted verts\n" +
                wantedVerts[0] + "\n" + wantedVerts[1] + "\n" + wantedVerts[2] + "\n" + wantedVerts[3] + "\n finden??!" );
        }

        return GetRandomTileFromList(fittingTiles);
    }

    PlacedTile GetRandomTileFromList(List<PlacedTile> tiles)
    {
        System.Random rand = new System.Random();

        int index = rand.Next(tiles.Count);

        PlacedTile tile = tiles[index];
        return tile;
    }


    bool CheckWantedVerts(int[] verts, int[] wantedVerts)
    {


        if (wantedVerts.Length != 4)
        {
            throw new ArgumentException("CheckWantedSides: wantedSides Argument muss die Länge 4 haben.");
        }

        if (((verts[0] == wantedVerts[0]) || wantedVerts[0] == 0) &&
            ((verts[1] == wantedVerts[1]) || wantedVerts[1] == 0) &&
            ((verts[2] == wantedVerts[2]) || wantedVerts[2] == 0) &&
            ((verts[3] == wantedVerts[3]) || wantedVerts[3] == 0))
        {
            return true;
        }
        return false;
    }
}

class PlacedTile
{
    public int locX {get; set;}
    public int locZ { get; set; }
    public Tile baseTile { get; set; }

    public PlacedTile(Tile basetile, int locX, int locZ)
    {
        this.baseTile = basetile;
        this.locZ = locZ;
        this.locX = locX;
    }

    public PlacedTile(Tile basetile)
    {
        this.baseTile = basetile;
    }
}
class Tile
{
    public int VertNorthWest, VertNorthEast, VertSouthEast, VertSouthWest;
    public string model;
    public int Rotation { get; }

    public int getVert(int vert)
    {
        switch (vert)
        {
            case 0:
                return VertNorthWest;
            case 1:
                return VertNorthEast;
            case 2:
                return VertSouthEast;
            case 3:
                return VertSouthWest;
            default:
                throw new ArgumentException("TilePlayground-getVert: Vert Wert, nicht im erlaubten Bereich. Du Hurensohn");
        }
    }

    public Tile(int VertNorthWest, int VertNorthEast, int VertSouthEast, int VertSouthWest,
        string model, int rotation)


    {
        if (VertNorthWest == 0 || VertNorthEast == 0 || VertSouthEast == 0 || VertSouthWest == 0)
        {
            throw new ArgumentOutOfRangeException("Ein Tile darf nicht mit dem Vertex 0 initialisiert werden. 0 ist für leere Kanten reserviert");
        }

        this.VertNorthWest = VertNorthWest;
        this.VertNorthEast = VertNorthEast;
        this.VertSouthEast = VertSouthEast;
        this.VertSouthWest = VertSouthWest;

        this.model = model;

        Rotation = rotation;
    }
}
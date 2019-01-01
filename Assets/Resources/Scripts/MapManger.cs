using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManger : MonoBehaviour {

    //Presets
    float heightFactor = 0.1f;

    //Data
    public TileScr[,] map;
    private Texture2D mapTexture;

    //Prefabs
    private GameObject tilePrefab;

    //Materials
    private Material greenMat;
    private Material redMat;
    private Material lightBlueMat;
    private Material blueMat;
    private Material yellowMat;

    void Awake()
    {
        //Loading
        tilePrefab = Resources.Load<GameObject>("Prefabs/tilePref");
        greenMat = Resources.Load<Material>("Materials/GreenMat");
        redMat = Resources.Load<Material>("Materials/RedMat");
        blueMat = Resources.Load<Material>("Materials/BlueMat");
        lightBlueMat = Resources.Load<Material>("Materials/LightBlueMat");
        yellowMat = Resources.Load<Material>("Materials/YellowMat");
        Debug.Log(redMat.color);
    }

    public void CreateRandomMap(int _sizeX, int _sizeY)
    {
        map = new TileScr[_sizeX, _sizeY];

        //Create a map with boarder and a chance 1 to 10 to have a blocking tile

        for (int _y = 0; _y < _sizeY; _y++)
        {
            for (int _x = 0; _x < _sizeX; _x++)
            {
                if (_y == 0 || _y == _sizeY - 1 || _x == 0 || _x == _sizeX - 1)
                {
                    CreateTileScr(_x, _y, 2);
                }
                else if (Random.Range(0, 11) == 0)
                {
                    CreateTileScr(_x, _y, 2);
                }
                else
                {
                    CreateTileScr(_x, _y, 0);
                }
            }
        }

        DebugMap();
    }

    public void LoadMapFromData(string _mapname)
    {
        //Rot ist für die höhe dess terrains

        _mapname = "Maps/" +_mapname;
        mapTexture = Resources.Load<Texture2D>(_mapname);

        int _mapWidth = mapTexture.width;
        int _mapHeight = mapTexture.height;

        map = new TileScr[_mapWidth, _mapHeight];

        for (int x = 0; x < _mapWidth; x++)
        {
            for(int y = 0; y < _mapHeight; y++)
            {
                int _height = (int) (mapTexture.GetPixel(x, y).r * 255);
                //Debug.Log(_height);
                CreateTileScr(x, y, _height);
            }
        }
    }

    public void CreateMap1()
    {
        int _sizeY = 10;
        int _sizeX = 10;

        map = new TileScr[_sizeX, _sizeY];

        for (int _y = 0; _y < _sizeY; _y++)
        {
            for (int _x = 0; _x < _sizeX; _x++)
            {
                if (_y == 0 || _y == _sizeY - 1 || _x == 0 || _x == _sizeX - 1)
                {
                    CreateTileScr(_x, _y, 2);
                }
                else if ((_y == 4 && _x == 3) || (_y == 3 && _x == 4) || (_y == 4 && _x == 4) || (_y == 4 && _x == 2))
                {
                    CreateTileScr(_x, _y, 2);
                }
                else
                {
                    CreateTileScr(_x, _y, 0);
                }
            }
        }
    }

    public void CreateMap2()
    {
        int _sizeY = 10;
        int _sizeX = 10;

        map = new TileScr[_sizeX, _sizeY];

        for (int _y = 0; _y < _sizeY; _y++)
        {
            for (int _x = 0; _x < _sizeX; _x++)
            {
                if (_y == 0 || _y == _sizeY - 1 || _x == 0 || _x == _sizeX - 1)
                {
                    CreateTileScr(_x, _y, 2);
                }
                else
                {
                    CreateTileScr(_x, _y, 0);
                }
            }
        }
    }

    private void DebugMap()
    {
        for (int _x = 0; _x < map.GetLength(0); _x++)
        {
            string _string = "";
            for (int _y = 0; _y < map.GetLength(1); _y++)
            {
                _string = _string + "|" + map[_x, _y].position.z;
            }

            Debug.Log(_string);
        }
    }

    private void CreateTileScr(int _x, int _y, int _height){
        GameObject _Tile = Instantiate(tilePrefab, new Vector3(_x, _height * heightFactor, _y), Quaternion.identity);
        map[_x, _y] = new TileScr(_x, _y, _height, _Tile);

        switch (_height)
        {
            case 0:
                map[_x, _y].myTile.GetComponent<Renderer>().material = greenMat;
                break;
            case 2:
                map[_x, _y].myTile.GetComponent<Renderer>().material = redMat;
                break;
        }
    }

    public void SetColourMat(int _i, int _x, int _y)
    {
        switch (_i)
        {
            case 1:
                map[_x, _y].myTile.GetComponent<Renderer>().material = lightBlueMat;
                break;
            case 2:
                map[_x, _y].myTile.GetComponent<Renderer>().material = blueMat;
                break;
            case 3:
                map[_x, _y].myTile.GetComponent<Renderer>().material = yellowMat;
                break;
        }
    }

}

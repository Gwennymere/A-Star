using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScr
{
    public Vector3 position;    //  X/Y/Height
    public GameObject myTile;

    //Debug
    int p, q, g;
    TextMesh pMesh;
    TextMesh qMesh;
    TextMesh gMesh;

    public TileScr(int _posX, int _posY, int _height, GameObject _myTile)
    {
        position = new Vector3(_posX, _posY, _height);
        myTile = _myTile;
        pMesh = myTile.transform.Find("pText").GetComponent<TextMesh>();
        qMesh = myTile.transform.Find("qText").GetComponent<TextMesh>();
        gMesh = myTile.transform.Find("gText").GetComponent<TextMesh>();
    }

    

    public TileScr()
    {

    }

    //DEBUG ONLY, can später entfernt werden
    public void SetMyPathingValues(int _p, int _q, int _g)
    {
        p = _p;
        q = _q;
        g = _g;


        pMesh.text = p.ToString();
        qMesh.text = q.ToString();
        gMesh.text = g.ToString();

    }
}
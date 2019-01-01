using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentScr{

    MapManger mapManager;
    private List<PathingTileScr> openTiles; //NOT final values
    private PathingTileScr[,] revealableTiles;
    public Vector2 start;
    public Vector2 goal;
    private ComparerScr comparerScr;
    private List<PathingTileScr> PathFromAToB;


	public NavAgentScr(GameManagerScr _gameManagerScr)
    {
        mapManager = _gameManagerScr.mapManger;
        comparerScr = new ComparerScr();
        UpdateRevealableTiles();
        
    }

    /*NUR FÜR DEBUGGING DEAKTIVIERT
     * 
     * 
     */ 
    public void FindPath()
    {
        
        openTiles.Add(revealableTiles[(int)start.x, (int)start.y]);
        Debug.Log("Beginning search for path");
        bool _finishedSearch = false;
        float _timeout = 0f;
        while (!_finishedSearch && _timeout < 30.0f)
        {

            if (RevealeTiles(openTiles[0].position))
            {
                _finishedSearch = true;
                break;
            }
            Debug.Log("Sorting...");
            
            openTiles.Sort(comparerScr);
            Debug.Log("Done sorting...");
            //objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));

            _timeout += Time.deltaTime;
        }
        Debug.Log("FoundPath");

        CollectPath();
        HighlightPath();
        
    }
    /**/
    private void HighlightPath()
    {
        foreach(PathingTileScr _pathTileScr in PathFromAToB)
        {
            mapManager.SetColourMat(3, (int)_pathTileScr.position.x, (int)_pathTileScr.position.y);
        }
    }

    public void CollectPath()
    {
        PathFromAToB = new List<PathingTileScr>();
        PathingTileScr _lastParent = revealableTiles[(int)goal.x, (int)goal.y];
        while(_lastParent != null)
        {
            PathFromAToB.Add(_lastParent);
            if (_lastParent.parent != null)
            {
                _lastParent = _lastParent.parent;
            }
            else
            {
                _lastParent = null;
            }
        }
        Debug.Log("got the path right here sire");
    }

    //DEBUG only, kann nachher gelöscht werden
    public void TestReveal(Vector2 _position)
    {
        RevealeTiles(_position);
    }


    //Reveals the centerTile and the 8 around it
    //Returns true when end is found
    private bool RevealeTiles(Vector2 _centerTileVec)
    {

        //revealableTiles[(int)_centerTile.x, (int)_centerTile.y].RevealValus(start, goal);
        PathingTileScr _centerTile = revealableTiles[(int)_centerTileVec.x, (int)_centerTileVec.y];

        int _preP = 0;
        int _preQ = 0;
        int _LowestValue = 2000000;
        int _lowestP = 0;
        int _lowestQ = 0;

        Debug.Log("Starting to reveal Tiles. Revealing Centertile: " + _centerTile.position);

        //Von eins links neben center bis eins rechts neben center
        for (int x = (int)_centerTileVec.x - 1; x <= _centerTileVec.x + 1; x++)
        {
            //Von eins unter center bis eins über center
            for (int y = (int)_centerTileVec.y - 1; y <= _centerTileVec.y + 1; y++)
            {
                //Wenn nicht revealed und die tiles nicht zu hoher höhenunterschied
                if (!revealableTiles[x, y].revealed && Mathf.Abs(revealableTiles[x, y].position.z - _centerTile.position.z) < 2)
                {
                    //Wenn nicht das centerTile
                    if (!(x == (int)_centerTileVec.x && y == (int)_centerTileVec.y))
                    {
                        //Hinzufügen zur Liste aller revealten, aber nicht abschließend untersuchten Tiles
                        
                        if (!revealableTiles[x,y].wasAddedToOpenList) {
                            //Debug.Log(revealableTiles[x, y] == null);
                            //Debug.Log(openTiles.Count);
                            openTiles.Add(revealableTiles[x, y]);
                            revealableTiles[x, y].wasAddedToOpenList = true;
                        }
                        
                        //Wenn schräg neben dem center
                            //Werte für pathingdistance zum centertile
                        if (x != (int)_centerTileVec.x && y != (int)_centerTileVec.y)
                        {
                            _preP = 14;
                        }
                        else
                        {
                            _preP = 10;
                        }

                        //Wert bis zum ende bei optimalem verlauf
                        int _geradeSteps = (int)(Mathf.Abs(goal.x - x) - Mathf.Abs(goal.y - y));
                        int _schraegeSteps;
                        if(_geradeSteps < 0)
                        {
                            _schraegeSteps = Mathf.Abs((int)goal.y - y) + _geradeSteps;
                            _geradeSteps = -1*_geradeSteps;
                        }
                        else
                        {
                            _schraegeSteps = Mathf.Abs((int)goal.x - x) - _geradeSteps;
                        }
                        _preQ = _schraegeSteps *14 + _geradeSteps * 10;
                        //Debug.Log("@" + x + "/" + y + ":  " + _geradeSteps + " || " + _schraegeSteps);


                        //hat das centertile ein parent und muss daher dessen position einbezogen werden oder nicht
                        int _PvalueToSet;
                        int _QvalueToSet = _preQ;

                        if (_centerTile.parent != null)
                        {
                            _PvalueToSet = _preP + _centerTile.pValue;
                            //Debug.Log("pValueToSet=_preP+_cT.p.PV: " + _PvalueToSet + "=" + _preP + "+"+);
                        }
                        else
                        {
                            _PvalueToSet = _preP;
                        }

                        //falls die neu gefundenen Werte kleiner sind (also der weg kürzer) als von einem anderen Tile
                        //Setze neue values
                        if (revealableTiles[x, y].SetValuesAndParentIfSmaller(_PvalueToSet,_QvalueToSet, _centerTile))
                        {
                            
                            //Update shown Numbers
                            mapManager.map[x, y].SetMyPathingValues(revealableTiles[x, y].pValue, revealableTiles[x, y].qValue, revealableTiles[x, y].gValue);
                            mapManager.SetColourMat(1,x,y);

                        }

                        //Lege den niedrigsten gesamtwert fest für finalen wert des centertile
                        //WIRD DER LOWESTVALUE FALSCH BERECHNET ODER IST DIE ENDRECHNUNG FALSCH

                        /**
                        if (_LowestValue > _PvalueToSet + _QvalueToSet)
                        {
                            _LowestValue = _PvalueToSet + _QvalueToSet;
                            _lowestP = _PvalueToSet;
                            _lowestQ = _QvalueToSet;
                        }
                        **/

                        
                        if (_LowestValue > _preQ + _preP)
                        {
                            _LowestValue = _preP + _preQ;
                            _lowestP = _preP;
                            _lowestQ = _preQ;
                        }
                        
                    }
                }
            }
        }
        //HIER SOLLEN DIE FINALEN VALUES BESTIMMT UND EINGETRAGEN WERDEN. 
        //revealableTiles[(int)_centerTile.x, (int)_centerTile.y].SetFinalValues(_lowestP, _lowestQ);
        //Debug.Log(revealableTiles[(int)_centerTile.x, (int)_centerTile.y].gValue);
        if (_centerTile.parent != null) {
            int _positionToParentValue;
            if ((int)_centerTile.parent.position.x != (int)_centerTileVec.x && (int)_centerTile.parent.position.y != (int)_centerTileVec.y)
            {
                _positionToParentValue = 14;
            }
            else
            {
                _positionToParentValue = 10;
            }
            Debug.Log("Setting a tile final and removing it from the stack. stacksize is now " + openTiles.Count);
            _centerTile.SetFinalValues(_centerTile.parent.pValue + _positionToParentValue, _lowestQ + _preP);
            openTiles.Remove(_centerTile);
        }
        else
        {
            _centerTile.SetFinalValues(0, _lowestQ + _lowestP);
            Debug.Log("Found Centerile without parent. ID: " + _centerTile.position + "| Revealed: " + _centerTile.revealed + "| was addedToOpenlist "+ _centerTile.wasAddedToOpenList);
            openTiles.Remove(_centerTile);
        }
        //Update shown Numbers
        mapManager.map[(int)_centerTileVec.x, (int)_centerTileVec.y].SetMyPathingValues(revealableTiles[(int)_centerTileVec.x, (int)_centerTileVec.y].pValue, revealableTiles[(int)_centerTileVec.x, (int)_centerTileVec.y].qValue, revealableTiles[(int)_centerTileVec.x, (int)_centerTileVec.y].gValue);
        mapManager.SetColourMat(2,(int) _centerTileVec.x,(int) _centerTileVec.y);

        if (_centerTile.Equals(revealableTiles[(int)goal.x, (int)goal.y]))
        {
            Debug.Log("Found Tile with coordinates equal to the endtile");
            return true;

        }
        Debug.Log("ReveladeTile but end not yet found");
        return false;
    }

    private void UpdateRevealableTiles()
    {
        revealableTiles = new PathingTileScr[mapManager.map.GetLength(0),mapManager.map.GetLength(1)];
        for(int x = 0; x < mapManager.map.GetLength(0); x++)
        {
            for (int y = 0; y < mapManager.map.GetLength(1); y++)
            {
                revealableTiles[x, y] = new PathingTileScr(x,y,(int)mapManager.map[x,y].position.z);
            }
        }
    }

    public void SetStartAndGoal(Vector2 _start, Vector2 _goal)
    {
        openTiles = new List<PathingTileScr>();
        openTiles.Add(revealableTiles[(int)_start.x, (int)_start.y]);
        revealableTiles[(int)_start.x, (int)_start.y].wasAddedToOpenList = true;

        start = _start;
        goal = _goal;

        //
        //revealableTiles[(int)_start.x,(int)_start.y].wasAddedToOpenList = true;


        RevealeTiles(_start);
    }
}

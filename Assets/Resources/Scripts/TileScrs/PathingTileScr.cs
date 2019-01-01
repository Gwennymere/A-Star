using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingTileScr : TileScr//, IComparer<PathingTileScr>
{

    public int gValue; //TotalCost
    public int pValue; //CostToStart
    public int qValue; //CostToGoal

    public bool revealed = false;
    public PathingTileScr parent;
    public bool wasAddedToOpenList = false;

	public PathingTileScr(int _x, int _y, int _height)
    {
        position = new Vector3(_x, _y, _height);
        pValue = 1000000;
        qValue = 1000000;
        gValue = 2000000;
    }

    public void RevealEstimatedValues(Vector2 _start, Vector2 _goal)
    {

    }

    /*
     */

    public void SetFinalValues(int _p, int _q)
    {

        pValue = _p;
        qValue = _q;
        UpdategValue();
        revealed = true;
    }


    public void CalculateStart(Vector2 _goal)
    {
        qValue = (int)(Mathf.Abs(Mathf.Abs(_goal.x - position.x) - Mathf.Abs(_goal.y - position.y)) * 10 + Mathf.Abs(_goal.x - position.x) * 14);
        revealed = true;
        UpdategValue();
    }

    public void CalculateSurroundingTileValue(Vector2 _goal, Vector2 _start, PathingTileScr _potentialParent)
    {
        if (Mathf.Abs(position.z - _potentialParent.position.z)<2)
        {
            Debug.Log("Ist parrenting bereits korrekt?");
            if (parent == null)
            {
                Debug.Log("Muss nicht verglichen werden");
                parent = _potentialParent;
            }
            else if (_potentialParent.gValue < parent.gValue)
            {
                parent = _potentialParent;
            }

            qValue = (int)(Mathf.Abs(Mathf.Abs(_goal.x - position.x) - Mathf.Abs(_goal.y - position.y)) * 10 + Mathf.Abs(_goal.x - position.x) * 14);

            if (position.x != parent.position.x && position.y != parent.position.y)
            {
                pValue = parent.pValue + 14;
            }
            else
            {
                pValue = parent.pValue + 10;
            }
        }
        
        UpdategValue();
    }

    private void UpdategValue()
    {
        gValue = pValue + qValue;
        //SetMyPathingValues(pValue,qValue,gValue);
    }

    public bool SetValuesAndParentIfSmaller(int _p, int _q, PathingTileScr _newPotentialParent)
    {
        if (_p + _q < gValue) {
            pValue = _p;
            qValue = _q;
            UpdategValue();
            parent = _newPotentialParent;
            return true;
        }
        return false;
    }
    /*
    public void PreSetP(int _p)
    {
        pValue = _p;
    }
    
    public int Compare(PathingTileScr _firstTileScr, PathingTileScr _otherTileScr)
    {
        if (_firstTileScr.gValue == _otherTileScr.gValue)
        {
            if (_firstTileScr.qValue > _otherTileScr.qValue)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (_firstTileScr.gValue > _otherTileScr.gValue)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
    

public int Compare(PathingTileScr x, PathingTileScr y)
{
   throw new System.NotImplementedException();
}

public int Compare(int x, int y)
{
   throw new System.NotImplementedException();
}*/
}

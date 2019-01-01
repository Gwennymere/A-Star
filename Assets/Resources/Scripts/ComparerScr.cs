using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ComparerScr : IComparer<PathingTileScr> {

    int IComparer<PathingTileScr>.Compare(PathingTileScr _firstTileScr, PathingTileScr _otherTileScr)
    {
        if (_firstTileScr.gValue == _otherTileScr.gValue)
        {
            if (_firstTileScr.qValue > _otherTileScr.qValue)
            {
                return 1;
            }
            else if(_firstTileScr.qValue < _otherTileScr.qValue)
            {
                return -1;
            } else
            {
                return 0;
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
}
*/


public class ComparerScr : IComparer<PathingTileScr>
{

    int option = 2;

    int IComparer<PathingTileScr>.Compare(PathingTileScr _firstTileScr, PathingTileScr _otherTileScr)
    {
        if (option == 1)
        {
            if (_firstTileScr.qValue == _otherTileScr.qValue)
            {
                if (_firstTileScr.gValue > _otherTileScr.gValue)
                {
                    return 1;
                }
                else if (_firstTileScr.gValue < _otherTileScr.gValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
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
        }
        if (option == 6)
        {
            if (_firstTileScr.qValue == _otherTileScr.qValue)
            {
                if (_firstTileScr.gValue > _otherTileScr.gValue)
                {
                    return 1;
                }
                else if (_firstTileScr.gValue < _otherTileScr.gValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
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
        }
        else if (option == 2)
        {
            if (_firstTileScr.gValue == _otherTileScr.gValue)
            {
                if (_firstTileScr.qValue > _otherTileScr.qValue)
                {
                    return 1;
                }
                else if (_firstTileScr.qValue < _otherTileScr.qValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
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
        else if (option == 5)//NEIN, but interesting
        {
            if (_firstTileScr.gValue == _otherTileScr.gValue)
            {
                if (_firstTileScr.qValue > _otherTileScr.qValue)
                {
                    return 1;
                }
                else if (_firstTileScr.qValue < _otherTileScr.qValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (_firstTileScr.gValue < _otherTileScr.gValue)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        else if (option == 3)
        {
            if (_firstTileScr.pValue == _otherTileScr.pValue)
            {
                if (_firstTileScr.gValue > _otherTileScr.gValue)
                {
                    return 1;
                }
                else if (_firstTileScr.gValue < _otherTileScr.gValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (_firstTileScr.pValue < _otherTileScr.pValue)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        else if (option == 4)//NEIN
        {
            if (_firstTileScr.pValue == _otherTileScr.pValue)
            {
                if (_firstTileScr.gValue > _otherTileScr.gValue)
                {
                    return 1;
                }
                else if (_firstTileScr.gValue < _otherTileScr.gValue)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (_firstTileScr.pValue > _otherTileScr.pValue)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        Debug.Log("Kein such-pattern eingestellt");
        return 0;
    }
}

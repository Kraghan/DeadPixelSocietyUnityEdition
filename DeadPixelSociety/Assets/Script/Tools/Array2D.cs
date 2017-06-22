using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Array
{
    public int[] row = new int[10];

    public Array(int size)
    {
        row = new int[size];
    }

    public int GetLength()
    {
        return row.GetLength(0);
    }

    public int this[int key]
    {
        get
        {
            return row[key];
        }
        set
        {
            SetValue(key, value);
        }
    }

    private void SetValue(int key, int value)
    {
        row[key] = value;
    }

    public void Print()
    {
        string s = "";
        for(int i = 0; i < row.GetLength(0); ++i)
        {
            s += row[i] + ",";
        }
        s.Remove(s.Length - 1, 1);
        Debug.Log("[" + s + "]");
    }
}


[System.Serializable]
public class Array2D
{
    public Array[] rows = new Array[10];

    public Array2D(int row, int column)
    {
        rows = new Array[row];
        for(int i = 0; i < row; ++i)
            rows[i] = new Array(column);
    }

    public int GetLength(int deep)
    {
        return deep == 0 ? rows.GetLength(0) : rows[0].GetLength();
    }

    public int this[int key, int key2]
    {
        get
        {
            return rows[key][key2];
        }
        set
        {
            SetValue(key, key2, value);
        }
    }

    private void SetValue(int key, int key2, int value)
    {
        rows[key][key2] = value;
    }

    public void Print()
    {
        for(int i = 0; i < rows.GetLength(0); ++i)
            rows[i].Print();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : Generator
{

    public override void Generate(int rooms)
    {
        patern = new Array2D(rooms, rooms);
        patern[0, 0] = 2;
        for(int i = 1; i < patern.GetLength(0); ++i)
        {
            patern[i, 0] = 1;
        }
    }

    public override void CreateAnnexe(int roomsToAnnexe, AnnexeType type)
    {

    }
}

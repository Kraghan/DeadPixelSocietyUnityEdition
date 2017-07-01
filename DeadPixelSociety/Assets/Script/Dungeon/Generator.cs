using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator : MonoBehaviour
{
    public Array2D patern;

    public enum AnnexeType
    {
        Tresure,
        Shopkeeper
    }

    public abstract void Generate(int rooms);

    public abstract void CreateAnnexe(int roomsToAnnexe, AnnexeType type);
}

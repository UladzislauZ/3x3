using System.Collections.Generic;
using UnityEngine;

public class ZoneModel
{
    public VariantCube[,] zoneFirst;

    public VariantCube[,] zoneSecond;

    public VariantCube[,] zoneThird;

    public List<GameObject> zoneFirstCubes;

    public List<GameObject> zoneThirdCubes;

    /// <summary>
    /// Current count variants first category
    /// </summary>
    public int currentCVFC;

    /// <summary>
    /// Current count variants second category
    /// </summary>
    public int currentCVSC;

    public ZoneModel()
    {
        zoneFirst = new VariantCube[3, 3];
        zoneSecond = new VariantCube[3, 3];
        zoneThird = new VariantCube[3, 3];
        zoneFirstCubes = new List<GameObject>();
        zoneThirdCubes = new List<GameObject>();
    }
}

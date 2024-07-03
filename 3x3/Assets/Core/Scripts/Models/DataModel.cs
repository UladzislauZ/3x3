using System.Collections.Generic;
using UnityEngine;

public class DataModel
{
    public VariantCube[,] zoneFirst;
    public List<Vector3> zoneThirdCubes;
    public Vector3 positionMouseClient;
    public bool onClickMouseClient;

    public DataModel()
    {

    }

    public DataModel(VariantCube[,] zoneFirst, List<Vector3> zoneThirdCubes)
    {
        this.zoneFirst = zoneFirst;
        this.zoneThirdCubes = zoneThirdCubes;
    }

    public DataModel(Vector3 positionMouseClient, bool onClickMouseClient)
    {
        this.positionMouseClient = positionMouseClient;
        this.onClickMouseClient = onClickMouseClient;
    }
}

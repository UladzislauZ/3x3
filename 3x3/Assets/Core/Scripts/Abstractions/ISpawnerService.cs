using System.Collections.Generic;
using UnityEngine;

public interface ISpawnerService
{
    List<GameObject> SpawnCubesZone(VariantCube[,] variantCubes, Zone zone, bool onAddComponentCube);
}

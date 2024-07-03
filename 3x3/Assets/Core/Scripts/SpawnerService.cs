using System.Collections.Generic;
using UnityEngine;

public class SpawnerService : ISpawnerService
{
    private readonly IAssetProvider _assetProvider;

    public SpawnerService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public List<GameObject> SpawnCubesZone(VariantCube[,] variantCubes, Zone zone, bool onAddComponentCube)
    {
        List<GameObject> cubes = new();
        int counter = 0;
        for (var i = 0; i < variantCubes.GetLength(0); i++)
            for (var j = 0; j < variantCubes.GetLength(1); j++)
            {
                var prefab = _assetProvider.GetVariantPrefab(variantCubes[i, j]);
                cubes.Add(SpawnCube(zone.Places[counter].transform, zone.ParentCubesTransform, prefab, onAddComponentCube));
                counter++;
            }

        return cubes;
    }

    private GameObject SpawnCube(Transform placeTransform, Transform parentTransform, GameObject prefab, bool onAddComponentCube)
    {
        var cube = GameObject.Instantiate(prefab, parentTransform);
        cube.transform.position = new Vector3(placeTransform.position.x, placeTransform.position.y + 0.5f, placeTransform.position.z);
        if (onAddComponentCube)
            cube.AddComponent<Cube>();

        return cube;
    }
}

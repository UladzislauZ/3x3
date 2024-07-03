using UnityEngine;

public interface IAssetProvider
{
    GameObject GetVariantPrefab(VariantCube variant);
}

using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject GetVariantPrefab(VariantCube variant)
    {
        switch (variant)
        {
            case VariantCube.variant1:
                {
                    return Resources.Load<GameObject>("CubeV1");
                }
            case VariantCube.variant2:
                {
                    return Resources.Load<GameObject>("CubeV2");
                }
            default:
                return null;
        }
    }
}

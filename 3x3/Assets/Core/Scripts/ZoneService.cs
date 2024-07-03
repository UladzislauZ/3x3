using UnityEngine;

public class ZoneService : IZoneService
{
    private readonly ZoneModel _zoneModel;

    public ZoneService(ZoneModel zoneModel)
    {
        _zoneModel = zoneModel;
    }

    public void LoadZone1(int countVariant1, int countVariant2)
    {
        if (countVariant1 + countVariant2 != 9)
            return;

        _zoneModel.currentCVFC = countVariant1;
        _zoneModel.currentCVSC = countVariant2;

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
            {
                if (_zoneModel.currentCVFC != 0 && _zoneModel.currentCVSC != 0)
                {
                    VariantCube variantCube = VariantCube.variant2;
                    if (Random.Range(0, 2) == 1)
                    {
                        variantCube = VariantCube.variant1;
                        _zoneModel.currentCVFC--;
                    }
                    else
                    {
                        _zoneModel.currentCVSC--;
                    }

                    _zoneModel.zoneFirst[i, j] = variantCube;
                }
                else if (_zoneModel.currentCVFC == 0)
                {
                    _zoneModel.zoneFirst[i, j] = VariantCube.variant2;
                    _zoneModel.currentCVSC--;
                }
                else if (_zoneModel.currentCVSC == 0)
                {
                    _zoneModel.zoneFirst[i, j] = VariantCube.variant1;
                    _zoneModel.currentCVFC--;
                }
            }
    }

    public void LoadZone3(int countVariant1, int countVariant2)
    {
        if (countVariant1 + countVariant2 != 9)
            return;

        _zoneModel.currentCVFC = countVariant1;
        _zoneModel.currentCVSC = countVariant2;
        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
            {
                if (_zoneModel.currentCVFC != 0)
                {
                    _zoneModel.zoneThird[i, j] = VariantCube.variant1;
                    _zoneModel.currentCVFC--;
                }
                else if (_zoneModel.currentCVSC != 0)
                {
                    _zoneModel.zoneThird[i, j] = VariantCube.variant2;
                    _zoneModel.currentCVSC--;
                }
            }
    }
}
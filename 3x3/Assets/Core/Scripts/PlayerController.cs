using UnityEngine;

public class PlayerController : IPlayerController
{
    private readonly ZoneModel _zoneModel;

    private Zone _secondZonePlaces;
    private bool[,] _arrayChecks;

    public PlayerController(Zone secondZonePlaces, ZoneModel zoneModel)
    {
        _secondZonePlaces = secondZonePlaces;
        _zoneModel = zoneModel;
    }

    public bool Check()
    {
        _arrayChecks = new bool[3, 3];
        int counter = 0;
        for (var i = 0; i < _zoneModel.zoneFirst.GetLength(0); i++)
            for (var j = 0; j < _zoneModel.zoneFirst.GetLength(1); j++)
            {
                if (Physics.Raycast(
                    _secondZonePlaces.Places[counter].transform.position, 
                    _secondZonePlaces.Places[counter].transform.TransformDirection(Vector3.up), 
                    out RaycastHit hit))
                {
                    _arrayChecks[i, j] = hit.collider.gameObject.CompareTag(ConvertPlaceToTag(_zoneModel.zoneFirst[i, j]));
                }

                counter++;
            }

        return ValidateZone();
    }

    private string ConvertPlaceToTag(VariantCube variantCube)
    {
        return variantCube switch
        {
            VariantCube.variant1 => "variant1",
            VariantCube.variant2 => "variant2",
            _ => string.Empty,
        };
    }

    private bool ValidateZone()
    {
        foreach (var check in _arrayChecks)
            if (check == false)
                return false;

        return true;
    }
}

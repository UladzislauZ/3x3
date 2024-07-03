using UnityEngine;

public class PlayerController : IPlayerController
{
    private readonly ZoneModel _zoneModel;

    private Zone _secondZonePlaces;
    private bool[,] _arrayChecks;

    private PlayerModel _playerModel;
    private PlayerModel _secondPlayerModel;

    public PlayerController(Zone secondZonePlaces, ZoneModel zoneModel, PlayerModel playerModel)
    {
        _secondZonePlaces = secondZonePlaces;
        _zoneModel = zoneModel;
        _playerModel = playerModel;
        _playerModel.ClickMousePlayer += ClickMouse;
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

    public void ImplementConnectedClientData(Vector3 positionMouseClient, bool onClickMouseClient)
    {
        _secondPlayerModel = new PlayerModel() { positionMousePlayer = positionMouseClient };
        var ray = Camera.main.ScreenPointToRay(_secondPlayerModel.positionMousePlayer);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var cube = hit.collider.gameObject.GetComponent<Cube>();
            cube.FolowOn(_secondPlayerModel);
        }
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

    private void ClickMouse()
    {
        var ray = Camera.main.ScreenPointToRay(_playerModel.positionMousePlayer);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var cube = hit.collider.gameObject.GetComponent<Cube>();
            cube.FolowOn(_playerModel);
        }
    }
}

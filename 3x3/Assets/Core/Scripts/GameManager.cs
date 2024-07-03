using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ZonesPlaces _zonesPlaces;
    [SerializeField]
    private UI _ui;

    private ZoneModel _zoneModel;
    private PlayerModel _playerModel;
    private IZoneService _zoneService;
    private IAssetProvider _assetProvider;
    private ISpawnerService _spawnerService;
    private IPlayerController _playerController;

    private int countVariant1 = 4;
    private int countVariant2 = 5;

    private void Start()
    {
        BindModels();
        BindServices();
        InitZones();
        Subscribe();
    }

    private void BindModels()
    {
        _zoneModel = new ZoneModel();
        _playerModel = new PlayerModel();
    }

    private void BindServices()
    {
        _zoneService = new ZoneService(_zoneModel);
        _assetProvider = new AssetProvider();
        _spawnerService = new SpawnerService(_assetProvider);
        _playerController = new PlayerController(_zonesPlaces.SecondZonePlaces, _zoneModel, _playerModel);
    }

    private void InitZones()
    {
        _zoneService.LoadZone1(countVariant1, countVariant2);
        _zoneModel.zoneFirstCubes = _spawnerService.SpawnCubesZone(_zoneModel.zoneFirst, _zonesPlaces.FirstZonePlaces, false);
        _zoneService.LoadZone3(countVariant1, countVariant2);
        _zoneModel.zoneThirdCubes = _spawnerService.SpawnCubesZone(_zoneModel.zoneThird, _zonesPlaces.ThirdZonePlaces, true);
    }

    private void Subscribe()
    {
        _ui.ButtonCheck.onClick.AddListener(Check);
        _ui.ButtonRestart.onClick.AddListener(Restart);
    }

    private void Unsubscribe()
    {
        _ui.ButtonCheck.onClick.RemoveListener(Check);
        _ui.ButtonRestart.onClick.RemoveListener(Restart);
    }

    private void Check()
    {
        if (_playerController.Check())
        {
            //todo add start anim win
            Debug.Log("Win");
            Restart();
        }
        else
            Debug.Log("Incorrect");
    }

    private void Restart()
    {
        DestroyLastCubes();
        InitZones();
    }

    private void DestroyLastCubes()
    {
        foreach (var cube in _zoneModel.zoneFirstCubes)
            Destroy(cube);

        foreach (var cube in _zoneModel.zoneThirdCubes)
            Destroy(cube);
    }

    private void Update()
    {
        _playerModel.positionMousePlayer = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            _playerModel.ClickMousePlayer?.Invoke();
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}

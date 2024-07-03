using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ZonesPlaces _zonesPlaces;
    [SerializeField]
    private UI _ui;

    private ZoneModel _zoneModel;
    private IZoneService _zoneService;
    private IAssetProvider _assetProvider;
    private ISpawnerService _spawnerService;
    private IPlayerController _playerController;

    private int countVariant1 = 4;
    private int countVariant2 = 5;
    private List<GameObject> _zoneFirstCubes;
    private List<GameObject> _zoneThirdCubes;

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
    }

    private void BindServices()
    {
        _zoneService = new ZoneService(_zoneModel);
        _assetProvider = new AssetProvider();
        _spawnerService = new SpawnerService(_assetProvider);
        _playerController = new PlayerController(_zonesPlaces.SecondZonePlaces, _zoneModel);
    }

    private void InitZones()
    {
        _zoneService.LoadZone1(countVariant1, countVariant2);
        _zoneFirstCubes = _spawnerService.SpawnCubesZone(_zoneModel.zoneFirst, _zonesPlaces.FirstZonePlaces, false);
        _zoneService.LoadZone3(countVariant1, countVariant2);
        _zoneThirdCubes = _spawnerService.SpawnCubesZone(_zoneModel.zoneThird, _zonesPlaces.ThirdZonePlaces, true);
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
            //start anim win
            Debug.Log("Win");
            Restart();
        }
    }

    private void Restart()
    {
        DestroyLastCubes();
        InitZones();
    }

    private void DestroyLastCubes()
    {
        foreach (var cube in _zoneFirstCubes)
            Destroy(cube);

        foreach (var cube in _zoneThirdCubes)
            Destroy(cube);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}

using UnityEngine;
using Zenject;

public class GameplayTestSceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private EntityConfig _playerConfig;
    private InputHandlerOnlyForward _inputHandler;
    [SerializeField] private CameraConfig _cameraConfig;

    public override void InstallBindings()
    {
        Container.Bind<EntityConfig>().FromInstance(_playerConfig);
        Container.Bind<CameraConfig>().FromInstance(_cameraConfig);

        Player player =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity,
                null);

        Container.Bind<IMoveable>().FromInstance(player.EntityMovement);
        Container.Bind<IRotateable>().FromInstance(player.EntityMovement);
        Container.Bind<ITargetable>().FromInstance(player);
        Container.Bind<PlayerMovementDinamicTreeTwoDimensionAnimation>()
            .FromInstance(player.GetComponent<PlayerMovementDinamicTreeTwoDimensionAnimation>());

        Container.BindInterfacesAndSelfTo<InputHandlerOnlyForward>().AsSingle();
    }
}
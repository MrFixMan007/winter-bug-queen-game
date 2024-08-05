using UnityEngine;
using Zenject;

public class GameplayTestSceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    
    [SerializeField] private EntityConfig _playerConfig;
    [SerializeField] private EntityConfig _slaveCommonBeetleConfig;
    [SerializeField] private EntityConfig _cerebralCommonBeetleConfig;
    
    private InputHandlerOnlyForward _inputHandler;
    [SerializeField] private CameraConfig _cameraConfig;
    
    [SerializeField] private LayerMask _enemyLayerMask;

    public override void InstallBindings()
    {
        Container.Bind<EntityConfig>().WithId("PlayerConfig").FromInstance(_playerConfig);
        Container.Bind<EntityConfig>().WithId("SlaveCommonBeetleConfig").FromInstance(_slaveCommonBeetleConfig);
        Container.Bind<EntityConfig>().WithId("CerebralCommonBeetleConfig").FromInstance(_cerebralCommonBeetleConfig);
        Container.Bind<CameraConfig>().FromInstance(_cameraConfig);
        Container.Bind<LayerMask>().WithId("EnemyLayerMask").FromInstance(_enemyLayerMask);

        Player player =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity,
                null);

        Container.Bind<IMoveable>().FromInstance(player.EntityMovement);
        Container.Bind<IRotateable>().FromInstance(player.EntityMovement);
        Container.Bind<ITargetable>().FromInstance(player);
        Container.Bind<ICombat>().FromInstance(player.PlayerCombat);
        Container.Bind<PlayerMovementDinamicTreeTwoDimensionAnimation>()
            .FromInstance(player.GetComponent<PlayerMovementDinamicTreeTwoDimensionAnimation>());

        Container.BindInterfacesAndSelfTo<InputHandlerOnlyForward>().AsSingle();
    }
}
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayTestSceneInstaller : MonoInstaller
{
    [Header("Player install settings")] [SerializeField]
    private Player _playerPrefab;

    [SerializeField] private EntityConfig _playerConfig;
    [SerializeField] private Transform _playerSpawnPoint;
    [Space] [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField, Tooltip("Указать слой врагов")] private LayerMask _enemyLayerMask;
    [SerializeField, Tooltip("Указать слой игрока")] private LayerMask _playerLayerMask;

    [Header("Enemy install settings")] [SerializeField]
    private EntityConfig _slaveCommonBeetleConfig;

    [SerializeField] private EntityConfig _cerebralCommonBeetleConfig;

    [Space] [SerializeField] [Tooltip("Позиции, по которым ходят жуки")]
    private List<Transform> _enemyMovePoints;

    public override void InstallBindings()
    {
        PlayerBindings();
        EnemiesBindings();
    }

    private void PlayerBindings()
    {
        Container.Bind<EntityConfig>().WithId("PlayerConfig").FromInstance(_playerConfig);
        Container.Bind<CameraConfig>().FromInstance(_cameraConfig);
        Container.Bind<LayerMask>().WithId("EnemyLayerMask").FromInstance(_enemyLayerMask);

        Player player =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity,
                null);

        Container.Bind<IMoveable>().WithId("Player").FromInstance(player);
        Container.Bind<IRotateable>().WithId("Player").FromInstance(player);
        Container.Bind<ICombat>().WithId("Player").FromInstance(player.PlayerCombat);
        Container.Bind<PlayerMovementDinamicTreeTwoDimensionAnimation>().WithId("Player")
            .FromInstance(player.GetComponent<PlayerMovementDinamicTreeTwoDimensionAnimation>());
        Container.Bind<EntityMovemableGameobject>().WithId("Camera").FromInstance(player);
        Container.Bind<EntityMovemableGameobject>().WithId("Player").FromInstance(player);

        Container.BindInterfacesAndSelfTo<InputHandlerOnlyForward>().AsSingle();
    }

    private void EnemiesBindings()
    {
        Container.Bind<LayerMask>().WithId("PlayerLayerMask").FromInstance(_playerLayerMask);
        Container.Bind<EntityConfig>().WithId("SlaveCommonBeetleConfig").FromInstance(_slaveCommonBeetleConfig);
        Container.Bind<EntityConfig>().WithId("CerebralCommonBeetleConfig").FromInstance(_cerebralCommonBeetleConfig);
    }
}
using UnityEngine;
using Zenject;

public class GameplayTestSceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        Player player =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity,
                null);

        // EntityMovement playerMovement = new EntityMovement(15, 15, player.gameObject);
        // _inputHandler = new InputHandler(playerMovement, playerMovement);
        // player.SetInputHandler(_inputHandler);
        // Container.Bind<IMoveable>().To<EntityMovement>().FromInstance(playerMovement).AsTransient();
        // Container.Bind<IRotateable>().To<EntityMovement>().FromInstance(playerMovement).AsTransient();
        //
        //
        // Container.Bind<PlayerController>().FromNew().AsSingle().NonLazy();
    }
}
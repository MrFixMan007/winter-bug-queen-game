using UnityEngine;
using Zenject;

public class GameplayTestSceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private PlayerConfig _playerConfig;
    private InputHandler _inputHandler;

    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        Player player =
            Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position,
                Quaternion.identity,
                null);

        Container.Bind<IMoveable>().FromInstance(player.EntityMovement);
        Container.Bind<IRotateable>().FromInstance(player.EntityMovement);
        
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();

        // EntityMovement playerMovement = new EntityMovement(15, 15, player.gameObject);
        // _inputHandler = new InputHandler(playerMovement, playerMovement);
        // player.SetInputHandler(_inputHandler);
        //
        //
        // Container.Bind<PlayerController>().FromNew().AsSingle().NonLazy();
    }
}
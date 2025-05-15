using Fusion.Sockets;
using Fusion;
using Model.Logger;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine;

public class FusionController : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private bool connectOnAwake = false;
    [SerializeField] private int maxPlayerCount = 5;

    public static FusionController Instance;
    public NetworkRunner NetworkRunner { get; private set; }
    public string PlayerName { get; private set; }

    private const string SessionName = "SimugaSession";
    private const string LobbyName = "SimugaLobby";
    private const string GameName = "Simuga";
    private StartGameArgs DefaultConfigs { get; set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        DefaultConfigs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            // CustomLobbyName = LobbyName,
            // SessionName = SessionName,
            PlayerCount = maxPlayerCount,
            // EnableClientSessionCreation = false,
            // Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            // SessionProperties = new Dictionary<string, SessionProperty>()
            // {
            //     { "game", GameName },
            // },
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };
                
        if (connectOnAwake)
        {
            ConnectToRunner("Anonymous");
        }
    }

    public async void ConnectToRunner(string playerName)
    {
        LoggerController.Instance.Log($"Connecting as {playerName}");
        PlayerName = playerName;
        if (GetComponent<NetworkRunner>() == null)
        {
            gameObject.AddComponent<NetworkRunner>();
        }
        
        NetworkRunner = GetComponent<NetworkRunner>();
        await StartGame(NetworkRunner);
    }

    private async Task StartGame(NetworkRunner runner)
    {
        var result = await runner.StartGame(DefaultConfigs);
        if (!result.Ok) {
            LoggerController.Instance.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }
    
    private async Task JoinLobby(NetworkRunner runner) {

        var result = await runner.JoinSessionLobby(SessionLobby.Custom, LobbyName);
        if (!result.Ok) {
            LoggerController.Instance.LogError($"Failed to Start: {result.ShutdownReason}");
        }
    }

    #region Fusion Callbacks

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        LoggerController.Instance.Log($"OnPlayerJoined {player.PlayerId}, isSceneAuth={runner.IsSceneAuthority}, isMaster={runner.IsSharedModeMasterClient}, " +
                         $"isServer={runner.IsServer}, isClient={runner.IsClient}, isPlayer={runner.IsPlayer}, localPlayer={runner.LocalPlayer.PlayerId}");
    }
    
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
    
    #endregion
}

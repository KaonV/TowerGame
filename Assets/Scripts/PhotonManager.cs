using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviour, INetworkRunnerCallbacks
{
    #region - Singleton Pattern -
    public static PhotonManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }
    #endregion 

    [SerializeField] private MenuSystem     _menuSystem      = null;
    [SerializeField] private NetworkRunner  _runner          = null;
    [SerializeField] private string         _sceneNameToLoad = "GameScene"; // Replace with your scene name

    private List<PlayerRef> playersInRoom    = new List<PlayerRef>(); // Referências dos jogadores
    public int             currentTurnIndex = 0;                     // Índice do jogador com o turno atual
    private float           turnDuration     = 10f;                   // Duração do turno em segundos
    private float           turnTimer        = 0f;

    public void JoinOrCreateRoom()
    {
        _ = StartGame();
    }

    public void LeaveCurrentRoom()
    {
        _ = LeaveRoom();
    }

    private async Task LeaveRoom()
    {
        if (_runner != null && _runner.IsRunning)
        {
            await _runner.Shutdown();
            _menuSystem.OpenMenu("MainView");
            Debug.Log("Left the room.");
        }
        else
        {
            Debug.LogWarning("Cannot leave the room because you are not in one.");
        }
    }

    private void Update()
    {
        if (playersInRoom.Count >= 2) // Inicia o sistema de turnos se houver pelo menos 2 jogadores
        {
            //HandleTurnSystem();
        }
    }

    private void HandleTurnSystem()
    {
        // Decrementa o temporizador do turno
        turnTimer -= Time.deltaTime;

        if (turnTimer <= 0)
        {
            // Alterna o turno automaticamente quando o tempo acaba
            NextTurn();
        }
    }

    public void NextTurn()
    {
        // Alterna para o próximo jogador
        currentTurnIndex = (currentTurnIndex + 1) % playersInRoom.Count;
        turnTimer        = turnDuration; // Reinicia o temporizador do turno

        // Notifica todos os jogadores sobre o novo turno
        if (_runner.IsServer)
            NotifyTurnChange(playersInRoom[currentTurnIndex]);
        Debug.Log($"Turno do jogador {playersInRoom[currentTurnIndex].PlayerId} começou!");
    }

    public void ForceNextTurn()
    {
        // Método para forçar a troca de turno quando chamado
        NextTurn();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void NotifyTurnChange(PlayerRef currentPlayer)
    {
        // Este RPC é chamado em todos os clientes para notificar o jogador atual do turno
        Debug.Log($"Turno do jogador: {currentPlayer.PlayerId}");
        // Aqui você pode atualizar a UI ou outra lógica para mostrar o jogador atual
    }

    private async Task StartGame()
    {
        _menuSystem.OpenMenu("LoadingScreen");

        if (GetComponent<NetworkRunner>())
            _runner = GetComponent<NetworkRunner>();
        else 
            _runner = gameObject.AddComponent<NetworkRunner>();

        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

        // Tentar encontrar uma sala aleatória como cliente
        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "",  // Deixar em branco para procurar qualquer sala disponível
            Scene = sceneInfo,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        var result = await _runner.StartGame(startGameArgs);

        if (result.Ok)
        {
            Debug.Log("Conectado a uma sala existente como cliente!");
        }
        else
        {
            Debug.LogWarning("Nenhuma sala encontrada, criando uma nova como host...");

            startGameArgs.GameMode = GameMode.Shared;
            startGameArgs.SessionName = "Sala_" + Guid.NewGuid(); // Nome único para a nova sala

            result = await _runner.StartGame(startGameArgs);

            if (result.Ok)
            {
                Debug.Log("Nova sala criada e conectado como host!");
            }
            else
            {
                Debug.LogError("Falha ao criar e conectar a uma nova sala: " + result.ShutdownReason);
            }
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (_runner.LocalPlayer == player)
            _menuSystem?.OpenMenu("RoomView");

        Debug.Log($"Player {player.PlayerId} joined. Current players: {runner.ActivePlayers.Count()}");

        if (!playersInRoom.Contains(player))
        {
            playersInRoom.Add(player);
        }

        if (runner.ActivePlayers.Count() == 2)
            LoadGameScene();
    }

    private void LoadGameScene()
    {
        // Loads the specified scene when two players are in the room
        Debug.Log("Loading game scene...");
        _runner.LoadScene(_sceneNameToLoad);
    }

    // Implement other necessary callbacks (optional, but recommended)
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) 
    {
        if (playersInRoom.Contains(player))
        {
            playersInRoom.Remove(player);
            Debug.Log($"Jogador {player.PlayerId} saiu da sala.");
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connected to server!");
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) 
    {
        Debug.Log("Scene Loaded!");
    }

    public void OnSceneLoadStart(NetworkRunner runner) 
    { 
        Debug.Log("Scene starting loading!");
    }

    void INetworkRunnerCallbacks.OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    void INetworkRunnerCallbacks.OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }
}

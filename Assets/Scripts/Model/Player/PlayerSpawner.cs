using Fusion;
using UnityEngine;

namespace Model.Player
{
    public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private NetworkObject networkObject;
    
        public void PlayerJoined(PlayerRef player)
        {
            if (player != Runner.LocalPlayer)
            {
                return;
            }
            
            Runner.Spawn(playerPrefab, new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)), Quaternion.identity, player);
        }
    }
}
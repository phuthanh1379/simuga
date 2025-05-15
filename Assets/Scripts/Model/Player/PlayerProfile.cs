using Fusion;
using TMPro;
using UnityEngine;

namespace Model.Player
{
    public class PlayerProfile : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private TMP_Text nameLabel;

        [OnChangedRender(nameof(UpdateVisuals))]
        [Networked] public Color PlayerColor { get; set; }

        [OnChangedRender(nameof(UpdateVisuals))]
        [Networked] public NetworkString<_32> PlayerName { get; set; }
        
        private void UpdateVisuals()
        {
            nameLabel.text = PlayerName.Value;
            meshRenderer.material.color = PlayerColor;
            nameLabel.color = PlayerColor;
        }
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SetName(string playerName)
        {
            PlayerName = playerName;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SetColor(Color color)
        {
            PlayerColor = color;
        }

        private void FixedUpdate()
        {
            if (!string.IsNullOrEmpty(PlayerName.Value) && nameLabel.text != PlayerName.Value)
            {
                UpdateVisuals();
            }
        }
    }
}
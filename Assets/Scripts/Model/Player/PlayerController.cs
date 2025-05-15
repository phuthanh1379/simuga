using Fusion;
using System.Collections;
using Model.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private PlayerProfile playerProfile;

        private void Awake()
        {
            ChangeNameEvent.NameChanged += OnNameChanged;
            ChangeColorEvent.ColorChanged += OnColorChanged;
        }
        
        private void OnDestroy()
        {
            ChangeNameEvent.NameChanged -= OnNameChanged;
            ChangeColorEvent.ColorChanged -= OnColorChanged;
        }

        private void OnNameChanged(string value)
        {
            if (HasInputAuthority)
            {
                playerProfile.RPC_SetName(value);
            }
        }
        
        private void OnColorChanged(Color color)
        {
            if (HasInputAuthority)
            {
                playerProfile.RPC_SetColor(color);
            }
        }

        private IEnumerator Init()
        {
            yield return new WaitUntil(() => isActiveAndEnabled);
            yield return new WaitUntil(() => FusionController.Instance.PlayerName != null);

            if (HasInputAuthority)
            {
                playerProfile.RPC_SetName(FusionController.Instance.PlayerName);
                playerProfile.RPC_SetColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            }
        }
        
        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                StartCoroutine(Init());
            }
        }
    }
}
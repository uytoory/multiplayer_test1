using Unity.Netcode;
using UnityEngine;

public sealed class DamageTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Player player = other.attachedRigidbody.GetComponent<Player>();
            if (player)
            {
                player.TakeDamageClientRpc();
                Destroy(gameObject);
            }
        }
    }
}

using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 value)
    {
        _rigidbody.velocity = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            if (collision.rigidbody)
            {
                Player player = collision.rigidbody.GetComponent<Player>();
                if (player)
                {
                    player.TakeDamageClientRpc();
                }
            }
        }
        Destroy(gameObject);
    }
}

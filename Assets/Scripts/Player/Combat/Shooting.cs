using Unity.Netcode;
using UnityEngine;

public sealed class Shooting : NetworkBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] float _speed;
    [SerializeField] Transform _spawner;

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
            ShotServerRpc();
        }
    }

    [ServerRpc]
    private void ShotServerRpc()
    {
        ShotClientRpc();
    }

    [ClientRpc]
    private void ShotClientRpc()
    {
        if (!IsOwner)
        {
            Shot();
        }
    }

    private void Shot()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, _spawner.position, Quaternion.identity);
        newBullet.SetVelocity(_spawner.forward * _speed);
    }
}

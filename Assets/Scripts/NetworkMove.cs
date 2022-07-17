using Unity.Netcode;
using UnityEngine;

public sealed class NetworkMove : NetworkBehaviour
{
    private NetworkVariable<Vector3> _position = new NetworkVariable<Vector3>(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<short> _rotation = new NetworkVariable<short>(writePerm: NetworkVariableWritePermission.Owner);

    private void Update()
    {
        if (IsOwner)
        {
            _position.Value = transform.position;
            _rotation.Value = (short)transform.eulerAngles.y;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _position.Value, Time.deltaTime * 10f);
            Quaternion targetRotation = Quaternion.Euler(0, _rotation.Value, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}

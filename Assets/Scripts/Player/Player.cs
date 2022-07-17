using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Player : NetworkBehaviour
{
    [SerializeField] int _health;
    [SerializeField] TextMeshPro _healthText;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    [ClientRpc]
    public void TakeDamageClientRpc()
    {
        TakeDamage();
    }
    private void TakeDamage()
    {
        _health--;
        _healthText.text = _health.ToString();
    }
}

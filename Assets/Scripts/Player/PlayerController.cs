using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeedValue;

    private CharacterController _characterController;
    private bool _isAlive;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _isAlive = true;
    }
    private void Update()
    {
        if (!IsOwner) return;

        CheckPlayerInputs();
    }

    private void CheckPlayerInputs()
    {
        if (!_isAlive)
        {
            return;
        }
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _characterController.Move(moveDir * _moveSpeedValue * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPos);
        }
    }
}

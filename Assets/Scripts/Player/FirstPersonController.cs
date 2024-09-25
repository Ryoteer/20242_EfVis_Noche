using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] private Transform _headTransform;

    [Header("Inputs")]
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [Range(50.0f, 1000.0f)][SerializeField] private float _mouseSensitivity = 300.0f;

    [Header("Physics")]
    [SerializeField] private LayerMask _intMask;
    [SerializeField] private float _intRayDist = 2.0f;
    [SerializeField] private LayerMask _movMask;
    [SerializeField] private float _movRayDist = 0.75f;
    [SerializeField] private float _movSpeed = 5.0f;

    private float _xAxis, _zAxis, _inputMouseX, _inputMouseY, _mouseX;
    private Vector3 _dir;

    private Camera _camera;
    private FirstPersonCamera _camControl;
    private Rigidbody _rb;

    private Ray _intRay, _movRay;
    private RaycastHit _intHit;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;        
    }

    private void Start()
    {
        if (!GameManager.Instance)
        {
            Debug.LogError($"No GameManager found on scene.");
        }

        GameManager.Instance.Player = this;

        _camera = Camera.main;

        _camControl = Camera.main.GetComponent<FirstPersonCamera>();
        _camControl.Head = _headTransform;
    }

    private void Update()
    {
        _xAxis = Input.GetAxis($"Horizontal");
        _zAxis = Input.GetAxis($"Vertical");

        _dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;

        _inputMouseX = Input.GetAxisRaw($"Mouse X");
        _inputMouseY = Input.GetAxisRaw($"Mouse Y");

        if (_inputMouseX != 0 || _inputMouseY != 0)
        {
            Rotation(_inputMouseX, _inputMouseY);
        }

        if (Input.GetKeyDown(_interactKey))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0 && !IsBlocked(_dir))
        {
            Movement(_dir);
        }
    }

    private bool IsBlocked(Vector3 dir)
    {
        _movRay = new Ray(transform.position, dir);

        return Physics.Raycast(_movRay, _movRayDist, _movMask);
    }

    private void Interact()
    {
        _intRay = new Ray(_camera.transform.position, _camera.transform.forward);

        if(Physics.SphereCast(_intRay, 0.25f, out _intHit, _intRayDist, _intMask))
        {
            if(_intHit.collider.TryGetComponent(out IInteract intObj))
            {
                intObj.OnInteract();
            }
        }
    }

    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir * _movSpeed * Time.fixedDeltaTime);
    }    

    private void Rotation(float x, float y)
    {
        _mouseX += x * _mouseSensitivity * Time.deltaTime;

        if (_mouseX >= 360 || _mouseX <= -360)
        {
            _mouseX -= 360 * Mathf.Sign(_mouseX);
        }

        y *= _mouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);

        _camControl?.Rotation(_mouseX, y);
    }
}

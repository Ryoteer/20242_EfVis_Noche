using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private float _minRotation = -45f;
    [SerializeField] private float _maxRotation = 75f;

    private float _mouseY;

    private Transform _head;
    public Transform Head 
    {
        get { return _head; }
        set { _head = value; }
    }

    private void LateUpdate()
    {
        transform.position = _head.position;
    }

    public void Rotation(float x, float y)
    {
        _mouseY += y;
        _mouseY = Mathf.Clamp(_mouseY, _minRotation, _maxRotation);

        transform.rotation = Quaternion.Euler(-_mouseY, x, 0f);
    }
}

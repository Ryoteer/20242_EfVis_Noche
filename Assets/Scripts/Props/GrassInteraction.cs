using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInteraction : MonoBehaviour
{
    [Header("<color=green>Rendering</color>")]
    [SerializeField] private string _sqrDistName = "_SqrDistance";

    private float _sqrDist = 0.0f;

    private Renderer[] _renderers;
    private Material[] _mats;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();

        _mats = new Material[_renderers.Length];

        for(int i = 0; i < _renderers.Length; i++)
        {
            _mats[i] = _renderers[i].material;
        }
    }

    private void Update()
    {
        _sqrDist = Vector3.SqrMagnitude(transform.position - GameManager.Instance.Player.transform.position);

        for (int i = 0; i < _mats.Length; i++)
        {
            _mats[i].SetFloat(_sqrDistName, _sqrDist);
        }
    }
}

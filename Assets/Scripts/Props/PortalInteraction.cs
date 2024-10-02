using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteraction : MonoBehaviour
{
    private float _sqrDist = 0.0f;

    private void Update()
    {
        _sqrDist = Vector3.SqrMagnitude(transform.position - GameManager.Instance.Player.transform.position);
        FullScreenManager.Instance.NetherVignette.SetFloat(FullScreenManager.Instance.DistanceName, _sqrDist);
    }
}

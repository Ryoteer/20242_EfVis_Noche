using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour, IInteract
{
    [Header("<color=#7B2FBC>Animation</color>")]
    [SerializeField] private string _isActiveName = "isActive";
    [SerializeField] private string _onEnableName = "onEnabled";

    private bool _isActive = false;

    private Animator _anim;

    private void Start()
    {
        FullScreenManager.Instance.NetherVignette.SetFloat(_isActiveName, 0);

        Debug.Log(FullScreenManager.Instance.NetherVignette.GetFloat(_isActiveName));

        _anim = GetComponentInParent<Animator>();
    }

    public void OnInteract()
    {
        _isActive = !_isActive;
        _anim.SetBool(_isActiveName, _isActive);
        _anim.SetTrigger(_onEnableName);
        switch (_isActive)
        {
            case false:
                FullScreenManager.Instance.NetherVignette.SetFloat(_isActiveName, 0.0f);
                break;
            case true:
                FullScreenManager.Instance.NetherVignette.SetFloat(_isActiveName, 1.0f);
                break;
        }
        Debug.Log(FullScreenManager.Instance.NetherVignette.GetFloat(_isActiveName));
    }
}

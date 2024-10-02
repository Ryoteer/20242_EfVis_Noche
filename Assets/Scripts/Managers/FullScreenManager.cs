using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenManager : MonoBehaviour
{
    #region Singleton
    public static FullScreenManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("<color=#7B2FBC>Nether Vignette</color>")]
    [SerializeField] private Material _nvPP;
    public Material NetherVignette 
    { 
        get { return _nvPP; }
        set { _nvPP = value; }
    }
    [SerializeField] private string _distanceName = "_Distance";
    public string DistanceName 
    { 
        get { return _distanceName; }
    }
    [SerializeField] private string _isActiveName = "_IsPortalActive";
    public string IsActiveName 
    { 
        get { return _isActiveName; }
    }
}

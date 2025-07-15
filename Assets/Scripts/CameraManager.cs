using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _internalCamera;
    [SerializeField] private GameObject _externalCamera;
    [SerializeField] private GameObject _introcamera;

    private float switchDelay = 15f;
    private bool _isInternalActive = true;

    void Start()
    {
        if (_internalCamera == null || _externalCamera == null || _introcamera == null)
        {
            Debug.LogError("cameras are not assigned in the inspector!");
            return;
        }

        _introcamera.SetActive(true);
        _internalCamera.gameObject.SetActive(false);
        _externalCamera.SetActive(false);

        Invoke(nameof(SwitchToInner), switchDelay);
    }

    void SwitchToInner()
    {
        _introcamera.SetActive(false);
        SetActiveCamera(true); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isInternalActive = !_isInternalActive;
            SetActiveCamera(_isInternalActive);
        }
    }

    void SetActiveCamera(bool internalActive)
    {
        _internalCamera.gameObject.SetActive(internalActive);
        _externalCamera.SetActive(!internalActive);
    }
}
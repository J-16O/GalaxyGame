using UnityEngine;
using Cinemachine;
using UnityEditor;

public class ShipViewManager : MonoBehaviour
{
    [SerializeField] private GameObject shipModel;
    [SerializeField] private GameObject cockpit;
    [SerializeField] private GameObject glass;
    [SerializeField] private CinemachineVirtualCamera internalCamera;
    [SerializeField] private CinemachineVirtualCamera externalCamera;

    private CinemachineBrain brain;

    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        CinemachineVirtualCamera activeCam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;

        bool isInternal = activeCam == internalCamera;

        glass.SetActive(isInternal);
        cockpit.SetActive(isInternal);
        shipModel.SetActive(!isInternal);
    }
}
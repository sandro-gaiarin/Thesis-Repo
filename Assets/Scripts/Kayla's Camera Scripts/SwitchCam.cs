using UnityEngine;
using Cinemachine;

public class SwitchCam : MonoBehaviour

{
    public CinemachineVirtualCamera StraightCamera;
    public CinemachineVirtualCamera TurnCamera;
    public CinemachineVirtualCamera TopCamera;

    public CinemachineVirtualCamera R1Camera;
    public CinemachineVirtualCamera R2Camera;
    public CinemachineVirtualCamera R3Camera;

    public CinemachineVirtualCamera H1Camera;
    public CinemachineVirtualCamera H2Camera;
    public CinemachineVirtualCamera H3Camera;
    public CinemachineVirtualCamera H4Camera;
    public CinemachineVirtualCamera Store1Camera;
    public CinemachineVirtualCamera Store2Camera;
    public CinemachineVirtualCamera Store3Camera;
    public CinemachineVirtualCamera WCCamera;



    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            CameraManager.SwitchCamera(H1Camera);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraManager.SwitchCamera(WCCamera);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("H1Trigger"))
        {
            CameraManager.SwitchCamera(H1Camera);
        }
        if (other.CompareTag("H2Trigger"))
        {
            CameraManager.SwitchCamera(H2Camera);
        }
        if (other.CompareTag("H3Trigger"))
        {
            CameraManager.SwitchCamera(H3Camera);
        }
        if (other.CompareTag("H4Trigger"))
        {
            CameraManager.SwitchCamera(H4Camera);
        }
        if (other.CompareTag("Store1Trigger"))
        {
            CameraManager.SwitchCamera(Store1Camera);
        }
        if (other.CompareTag("Store2Trigger"))
        {
            CameraManager.SwitchCamera(Store2Camera);
        }
        if (other.CompareTag("Store3Trigger"))
        {
            CameraManager.SwitchCamera(Store3Camera);
        }
        if (other.CompareTag("R1Trigger"))
        {
            CameraManager.SwitchCamera(R1Camera);
        }
        if (other.CompareTag("R2Trigger"))
        {
            CameraManager.SwitchCamera(R2Camera);
        }
        if (other.CompareTag("R3Trigger"))
        {
            CameraManager.SwitchCamera(R3Camera);
        }
        if (other.CompareTag("WC"))
        {
            CameraManager.SwitchCamera(WCCamera);
        }
    }

    void OnEnable()
    {
        CombatManager.OnCombatTriggered += SwitchToCombatCam;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= SwitchToCombatCam;
    }

    void SwitchToCombatCam()
    {
        CameraManager.SwitchCamera(TopCamera);
    }
}

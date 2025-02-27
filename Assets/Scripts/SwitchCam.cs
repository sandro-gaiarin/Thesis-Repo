using UnityEngine;
using Cinemachine;

public class SwitchCam : MonoBehaviour

{
    public CinemachineVirtualCamera StraightCamera;
    public CinemachineVirtualCamera TurnCamera;
    public CinemachineVirtualCamera TopCamera;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CameraManager.SwitchCamera(TurnCamera);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraManager.SwitchCamera(StraightCamera);
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

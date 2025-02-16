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

        //if (variable that determines if player is in combat)
        // {
        //     CameraManager.SwitchCamera(TopCamera);
        // }
    }
}

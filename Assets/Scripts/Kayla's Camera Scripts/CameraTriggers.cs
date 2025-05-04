using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraTriggers : MonoBehaviour
{
    // [SerializeField] private CinemachineVirtualCamera [] _cameraSettings;
    // [SerializeField] private int _whichCamera;


    [Header("Cameras")]
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

    [Header("Walls")]

    // public Renderer Store3Walls;

    public Renderer[] Store3WallObj;
    public Renderer WC1Walls;
    public Renderer WC2Walls;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        R1Camera = GameObject.Find("R1 Cam").GetComponent<CinemachineVirtualCamera>();
        R2Camera = GameObject.Find("R2 Cam").GetComponent<CinemachineVirtualCamera>();
        R3Camera = GameObject.Find("R3 Cam").GetComponent<CinemachineVirtualCamera>();
        H1Camera = GameObject.Find("H1 Cam").GetComponent<CinemachineVirtualCamera>();
        H2Camera = GameObject.Find("H2 Cam").GetComponent<CinemachineVirtualCamera>();
        H3Camera = GameObject.Find("H3 Cam").GetComponent<CinemachineVirtualCamera>();
        H4Camera = GameObject.Find("H4 Cam").GetComponent<CinemachineVirtualCamera>();
        Store1Camera = GameObject.Find("Store 1 Cam").GetComponent<CinemachineVirtualCamera>();
        Store2Camera = GameObject.Find("Store 2 Cam").GetComponent<CinemachineVirtualCamera>();
        Store3Camera = GameObject.Find("Store 3 Cam").GetComponent<CinemachineVirtualCamera>();
        WCCamera = GameObject.Find("WC Cam").GetComponent<CinemachineVirtualCamera>();

        // Store3WallObj = Store3Array[] {
        //     GameObject.Find("Store 3 Walls").GetComponent<Renderer>(),
        //     GameObject.Find("Store3 SM1").GetComponent<Renderer>(),
        //     GameObject.Find("Store3 SM2").GetComponent<Renderer>(),
        //     GameObject.Find("Store3 SM3").GetComponent<Renderer>(),
        // };

        Store3WallObj = new Renderer[] {
            GameObject.Find("Store 3 Walls").GetComponent<Renderer>(),
            GameObject.Find("Store3 SM1").GetComponent<Renderer>(),
            GameObject.Find("Store3 SM2").GetComponent<Renderer>(),
            GameObject.Find("Store3 SM3").GetComponent<Renderer>()
        };

        WC1Walls = GameObject.Find("WC Walls 1").GetComponent<Renderer>();
        WC2Walls = GameObject.Find("WC Walls 2").GetComponent<Renderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("H1Trigger"))
        {
            CameraManager.SwitchCamera(H1Camera);
            Debug.Log("H1Trigger");
        }
        if (other.CompareTag("H2Trigger"))
        {

            Debug.Log("H2Trigger");
            CameraManager.SwitchCamera(H2Camera);
        }
        if (other.CompareTag("H3Trigger"))
        {
            CameraManager.SwitchCamera(H3Camera);

            foreach (Renderer Store3Wall in Store3WallObj)
            {
                Store3Wall.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            // Store3Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            WC1Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
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
            
            foreach (Renderer Store3Wall in Store3WallObj)
            {
                Store3Wall.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }

            // Store3Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
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
        if (other.CompareTag("WCTrigger"))
        {
            CameraManager.SwitchCamera(WCCamera);
            WC1Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            WC2Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        if (other.CompareTag("WCClose"))
        {
            WC2Walls.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
}

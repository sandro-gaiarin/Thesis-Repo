using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveWarehouse : MonoBehaviour
{
    public GameObject leaveWarehouseCanvas; // Assign in inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("Player entered trigger zone.");
            if (leaveWarehouseCanvas != null)
            {
                leaveWarehouseCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("Player exited trigger zone.");
            if (leaveWarehouseCanvas != null)
            {
                leaveWarehouseCanvas.SetActive(false);
            }
        }
    }

    // Call this when the player confirms they want to leave
    public void LeaveBasedOnDay()
    {
        int currentDay = GameManager.Instance.currentDay;

        switch (currentDay)
        {
            case 1:
                SceneManager.LoadScene("PlayerRoomA1S3");
                break;
            case 2:
                SceneManager.LoadScene("PlayerRoomA2S3");
                break;
            case 3:
                SceneManager.LoadScene("PlayerRoomA3S3");
                break;
            default:
                Debug.LogWarning("Unhandled day value in GameManager!");
                break;
        }
    }

    // Optional cancel function
    public void DontLeave()
    {
        if (leaveWarehouseCanvas != null)
        {
            leaveWarehouseCanvas.SetActive(false);
        }
    }
}

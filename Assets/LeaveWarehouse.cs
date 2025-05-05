using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveWarehouse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("Player triggered exit. Loading scene: PlayerRoomA1S3");
            SceneManager.LoadScene("PlayerRoomA1S3");
        }
    }
}

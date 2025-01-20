using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        transform.rotation =Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }
}

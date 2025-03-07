using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueTriggerOnce : MonoBehaviour
{
    public string conversationName = "NPC_Intro";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            //DialogueManager.StartConversation(conversationName);
            gameObject.SetActive(false); // Disable trigger so it doesn't repeat
        }
    }
}
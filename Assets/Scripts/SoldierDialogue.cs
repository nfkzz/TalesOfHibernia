using UnityEngine;
using Yarn.Unity;

public class SoldierDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string nodeName = "Confrontation";

    public MonoBehaviour playerController;

    private Rigidbody rb;

    private bool triggered = false;
    private bool waitingForEnd = false;

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            waitingForEnd = true;


            if (playerController != null)
                playerController.enabled = false;


            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
            dialogueRunner.StartDialogue(nodeName);
        }
    }

    void OnDialogueEnd()
    {
        if (!waitingForEnd) return;

        waitingForEnd = false;


        if (playerController != null)
            playerController.enabled = true;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueEnd);
    }
}
using UnityEngine;
using Yarn.Unity;

public class DialogueAxeGlowTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string nodeName = "Axe";

    public Renderer axeRenderer;   
    //public Behaviour axeOutline;   

    private bool triggered = false;
    private bool waitingForEnd = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            waitingForEnd = true;

            dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
            dialogueRunner.StartDialogue(nodeName);
        }
    }

    void OnDialogueEnd()
    {
        if (!waitingForEnd) return;

        waitingForEnd = false;

      
        if (axeRenderer != null)
        {
            var mat = axeRenderer.material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow * 5f);
        }

      

        
        dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueEnd);
    }
}
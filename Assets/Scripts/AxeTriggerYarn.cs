using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DialogueAxeGlowTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string nodeName = "Axe";

    public Renderer axeRenderer;

    private bool triggered = false;
    private bool waitingForEnd = false;

    private Material mat;
    private Color baseColor;

    void Start()
    {
        if (axeRenderer != null)
        {
            mat = axeRenderer.material;
            baseColor = mat.color;
        }
    }

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

        StartCoroutine(FadeGlow());

        dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueEnd);
    }

    IEnumerator FadeGlow()
    {
        float duration = 1.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float intensity = Mathf.Lerp(1f, 5f, t / duration);
            mat.color = baseColor * intensity;

            yield return null;
        }

        mat.color = baseColor * 5f;
    }
}
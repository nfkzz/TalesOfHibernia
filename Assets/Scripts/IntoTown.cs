using TMPro;
using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class ShedDoorInteract : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueRunner dialogueRunner;
    public string nodeName = "ShedDoor"; // your Yarn node

    [Header("UI")]
    public GameObject interactText;

    private TextMeshProUGUI text;

    private bool playerInRange = false;
    private bool hasInteracted = false;

    void Start()
    {
        if (interactText != null)
        {
            text = interactText.GetComponent<TextMeshProUGUI>();
            interactText.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInRange || hasInteracted) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        hasInteracted = true;

        if (interactText != null)
            interactText.SetActive(false);

        if (dialogueRunner != null)
        {
            dialogueRunner.StartDialogue(nodeName);
            Debug.Log("Started dialogue: " + nodeName);
        }
        else
        {
            Debug.LogWarning("DialogueRunner not assigned!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactText != null)
            {
                interactText.SetActive(true);
                StartCoroutine(FadeInText());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    IEnumerator FadeInText()
    {
        if (text == null) yield break;

        float t = 0f;

        Color c = text.color;
        c.a = 0f;
        text.color = c;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;

            c.a = Mathf.Lerp(0f, 1f, t);
            text.color = c;

            yield return null;
        }

        c.a = 1f;
        text.color = c;
    }
}
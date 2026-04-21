using TMPro;
using UnityEngine;
using System.Collections;

public class AxePickup : MonoBehaviour
{
    public GameObject axeVisual;
    public GameObject pickupText;

    private TextMeshProUGUI text;

    private bool playerInRange = false;
    private bool pickedUp = false;

    void Start()
    {
        if (pickupText != null)
        {
            text = pickupText.GetComponent<TextMeshProUGUI>();
            pickupText.SetActive(false);
        }
    }

    void Update()
    {
        if (pickedUp) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        pickedUp = true;

        if (axeVisual != null)
            axeVisual.SetActive(false);

        if (pickupText != null)
            pickupText.SetActive(false);

        Debug.Log("Axe picked up!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pickupText != null)
            {
                pickupText.SetActive(true);
                StartCoroutine(FadeInText());
            }

            Debug.Log("Interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pickupText != null)
                pickupText.SetActive(false);
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
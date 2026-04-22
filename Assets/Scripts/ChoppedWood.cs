using UnityEngine;
using TMPro;
using System.Collections;

public class ChopWood : MonoBehaviour
{
    public GameObject unchoppedWood;
    public GameObject choppedWood;
    public GameObject pickupText;

    private TextMeshProUGUI text;

    public bool playerInRange = false;
    public bool chopped = false;

    void Start()
    {
        choppedWood.SetActive(false);

        if (pickupText != null)
        {
            text = pickupText.GetComponent<TextMeshProUGUI>();
            pickupText.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (!chopped && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Trying to chop. Has axe: " + PlayerState.hasAxe);

            if (PlayerState.hasAxe)
                Chop();
        }
    }

    void Chop()
    {
        Debug.Log("Wood chopped!");

        unchoppedWood.SetActive(false);
        choppedWood.SetActive(true);

        if (pickupText != null)
            pickupText.SetActive(false);
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

            Debug.Log("Entered wood trigger");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pickupText != null)
                pickupText.SetActive(false);

            Debug.Log("Exited wood trigger");
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
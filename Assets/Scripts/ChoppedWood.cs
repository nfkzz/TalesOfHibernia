using UnityEngine;
using TMPro; 

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
        Debug.Log("In range: " + playerInRange + " | Chopped: " + chopped);

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

        Debug.Log("Unchopped: " + unchoppedWood.name);
        Debug.Log("Chopped: " + choppedWood.name);

        unchoppedWood.SetActive(false);
        choppedWood.SetActive(true);
        pickupText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Entered wood trigger");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Exited wood trigger");
        }
    }
}
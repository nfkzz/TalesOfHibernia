using UnityEngine;

public class AxePickup : MonoBehaviour
{
    public GameObject axeVisual;

    [Header("UI")]
    public GameObject pickupText;
    

    private bool playerInRange = false;
    private bool pickedUp = false;


    void Start()
    {
       if (pickupText != null) 
            pickupText.SetActive(false);
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
                pickupText.SetActive(true);
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
}
using UnityEngine;
using Yarn.Unity;

public class SimplePlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 150f;
    public Transform playerCamera;

    float xRotation = 0f;
    bool canControl = true;

    [Header("Dialogue")]
    public DialogueRunner dialogueRunner;

    void Start()
    {
        // Lock cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Hook into Yarn events
        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
        }
    }

    void Update()
    {
        if (!canControl) return;

        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }

    void OnDialogueStart()
    {
        canControl = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDialogueEnd()
    {
        canControl = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
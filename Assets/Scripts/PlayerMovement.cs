using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(Rigidbody))]
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

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
        }
    }

    void Update()
    {
        HandleMouseLook();
    }

    void FixedUpdate()
    {
        if (!canControl) return;
        HandleMovement();
    }

    void HandleMouseLook()
    {
        if (!canControl) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate using Rigidbody-friendly method
        Quaternion turn = Quaternion.Euler(0f, mouseX, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Vector3 velocity = move * speed;
        velocity.y = rb.linearVelocity.y; // keep gravity working

        rb.linearVelocity = velocity;
    }

    void OnDialogueStart()
    {
        canControl = false;

        rb.linearVelocity = Vector3.zero;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDialogueEnd()
    {
        canControl = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        dialogueRunner.StartDialogue("NextDialogue");
    }
}

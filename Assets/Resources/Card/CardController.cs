using UnityEngine;
using WebXR;
using System.Collections;

public class CardController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool isOpen = false;

    [Header("Input Bindings")]
    [SerializeField]
    private WebXRController.ButtonTypes[] defaultPickupButtons = new WebXRController.ButtonTypes[] {
      WebXRController.ButtonTypes.Trigger,
      WebXRController.ButtonTypes.Grip,
      WebXRController.ButtonTypes.ButtonA
    };

    private float lastClickTime;
    private const float doubleClickTime = 0.8f;

    private WebXRController leftController;
    private WebXRController rightController;

    [SerializeField] private AudioClip toggleSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float audioDelay = 0.2f;
    [SerializeField] private float audioPitch = 1.5f;

    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
            WebGLInput.captureAllKeyboardInput = false;
        #endif

        animator = GetComponent<Animator>();
        //UpdateCardState();

        // Find and store references to WebXR controllers with correct names
        leftController = GameObject.Find("handL")?.GetComponent<WebXRController>();
        rightController = GameObject.Find("handR")?.GetComponent<WebXRController>();

        // Debug logs for controller detection
        if (leftController == null)
        {
            Debug.LogWarning("handL controller not found in scene");
        }
        if (rightController == null)
        {
            Debug.LogWarning("handR controller not found in scene");
        }
    }

    void Update()
    {
        bool buttonPressed = false;

        // Check both controllers for any of the defined buttons
        foreach (WebXRController.ButtonTypes button in defaultPickupButtons)
        {
            if ((leftController != null && leftController.GetButtonDown(button)) || 
                (rightController != null && rightController.GetButtonDown(button)))
            {
                buttonPressed = true;
                break;
            }
        }

        if (buttonPressed)
        {
            Debug.Log("Controller button pressed");
            HandleClick();
        }
    }

    void OnMouseDown()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        
        if (timeSinceLastClick <= doubleClickTime)
        {
            Debug.Log("Card double-clicked/triggered");
            ToggleCard();
        }
        
        lastClickTime = Time.time;
    }

    void OnValidate()
    {
        UpdateCardState();
    }

    void ToggleCard()
    {
        isOpen = !isOpen;
        UpdateCardState();
        StartCoroutine(PlayToggleSoundWithDelay());
    }

    private IEnumerator PlayToggleSoundWithDelay()
    {
        yield return new WaitForSeconds(audioDelay);
        audioSource.pitch = audioPitch;
        audioSource.PlayOneShot(toggleSound);
    }

    void UpdateCardState()
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
            Debug.Log($"Card state updated. isOpen: {isOpen}");
        }
        else
        {
            Debug.LogWarning("Animator is null. Make sure it's attached to the GameObject.");
        }
    }
}
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdPhotoCamera : MonoBehaviour
{
    [Header("Photo Settings")]
    public Key photoKey = Key.Space;           // Key to take a photo (uses new Input System)
    public RawImage photoPreview;              // UI image that shows the last photo
    public float flashDuration = 0.15f;        // How long the white flash stays on screen
    public Canvas photoFlashCanvas;            // White flash overlay when snapping

    [Header("Verification Settings")]
    public CheckTargetVisibility visibilityChecker;     // Script to check if target is visibile
    public Transform targetObjectToCheck;
    public Camera currentActiveCamera = Camera.main;
    public TextMeshProUGUI successCounterText;

    private int successfulPhotos = 0;
    private bool isFlashing = false;
    private Texture2D lastPhoto;               // Stores the most recent screenshot

    public FearMeter fearMeter;                 // Reference to the FearMeter script

    void Update()
    {
        // Check if the player presses the photo key this frame (new Input System)
        if (Keyboard.current[photoKey].wasPressedThisFrame)
            TakePhoto();

        if (successfulPhotos >= 3)
        {
            SceneManager.LoadScene("EvidenceTestScene");
        }
    }

    void TakePhoto()
    {
        Debug.Log("Photo taken");
        if (visibilityChecker != null && targetObjectToCheck != null && currentActiveCamera != null)
        {
            bool isVisible = visibilityChecker.CheckVisibility(targetObjectToCheck, currentActiveCamera);
            Debug.Log("checking visibility");
            if (isVisible)
            {
                Debug.Log("visible");
                successfulPhotos++;
                successCounterText.text = successfulPhotos + "/3";
            }
            else
            {
                Debug.Log("not visible");
            }
        }
        // Start capturing the current screen 
        StartCoroutine(CapturePhoto());
        
        // Trigger a short flash
        if (photoFlashCanvas && !isFlashing)
            StartCoroutine(PhotoFlash());

        fearMeter.increaseFear(0.1f);

    }

    System.Collections.IEnumerator CapturePhoto()
    {
        // Wait until the frame finishes rendering before capturing
        yield return new WaitForEndOfFrame();

        // Create a new texture the size of the game window
        Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Copy current screen pixels into the texture
        screenTex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTex.Apply();

        // Remove the previous photo from memory
        if (lastPhoto != null) Destroy(lastPhoto);
        lastPhoto = screenTex;

        // Display the new photo in the bottom-left UI preview
        if (photoPreview)
            photoPreview.texture = lastPhoto;

        
        
    }

    System.Collections.IEnumerator PhotoFlash()
    {
        // Enable the flash overlay for a short moment
        isFlashing = true;
        photoFlashCanvas.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        photoFlashCanvas.enabled = false;
        isFlashing = false;
    }
}

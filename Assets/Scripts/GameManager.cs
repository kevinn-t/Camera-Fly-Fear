using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("UI Stuff")]
    public TextMeshProUGUI promptText;
    public DropSlot dropSlot;

    [Header("Images")]
    public List<DraggableImage> draggableImages;

    private List<string> availablePrompts;
    private string currentPrompt;
    private bool isGameFinished = false;

    void Start()
    {
        if (dropSlot != null)
        {
            dropSlot.OnImageDropped.AddListener(CheckAnswer);
            Debug.Log("Listening for drop");
        }
        else
        {
            Debug.LogError("GameManager: Missing DropSlot reference!");
        }

        if (draggableImages != null && draggableImages.Count > 0)
        {
            availablePrompts = draggableImages.Select(img => img.imageID).ToList();
        }
        else
        {
            Debug.LogError("GameManager: Missing DraggableImages!");
            availablePrompts = new List<string>();
        }

        GenerateNewPrompt();
    }

    void GenerateNewPrompt()
    {
        if (isGameFinished) return;

        if (availablePrompts.Count > 0)
        {
            currentPrompt = availablePrompts[Random.Range(0, availablePrompts.Count)];

            if (promptText != null)
            {
                promptText.text = "Drag the image depicting " + currentPrompt + ".";
            }
        }
        else
        {
            isGameFinished = true;
            Debug.Log("finished");
            promptText.text = "Timeline of events complete.";


            if (dropSlot != null)
            {
                dropSlot.gameObject.SetActive(false);
            }
        }
    }

    void CheckAnswer(string droppedImageID)
    {
        if (isGameFinished) return; 
        Debug.Log("Drop Heard!");
        if (droppedImageID == currentPrompt)
        {
            StartCoroutine(ConfirmEvidence(droppedImageID));
        }
        else
        {
            Debug.Log("incorrect!");

            StartCoroutine(ResetEvidence(droppedImageID));
        }
    }

    private IEnumerator ResetEvidence(string droppedImageID)
    {
        yield return new WaitForSeconds(1.5f);

        DraggableImage imageToReset = FindDraggableImageByID(droppedImageID);
        imageToReset.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        imageToReset.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        imageToReset.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        imageToReset.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        imageToReset.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        imageToReset.gameObject.SetActive(true);
        if (imageToReset != null)
        {
            imageToReset.ResetPosition();
        }
    }

    private IEnumerator ConfirmEvidence(string droppedImageID)
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("correct!");
            availablePrompts.Remove(currentPrompt);
            DraggableImage correctImage = FindDraggableImageByID(droppedImageID);
            if (correctImage != null)
            {
                correctImage.gameObject.SetActive(false);
            }

            GenerateNewPrompt();
    }

    private DraggableImage FindDraggableImageByID(string imageID)
    {
        foreach (var image in draggableImages)
        {
            if (image.imageID == imageID)
            {
                return image;
            }
        }
        return null;
    }
}

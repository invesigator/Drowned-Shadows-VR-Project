using UnityEngine;
using UnityEngine.UI;

public class PaperInteraction : MonoBehaviour
{
    [Header("Paper Settings")]
    public float pickupDistance = 2f;    // Maximum distance for interaction
    public GameObject paperModel;         // The 3D model of the paper
    public GameObject paperReadUI;        // UI panel for reading mode
    public Text paperContentText;         // Text component to display paper content
    public string paperContent;           // The actual text on the paper

    [Header("Player References")]
    public Transform playerCamera;        // Reference to the player's camera/view

    private bool canPickup = false;       // Is player looking at and close enough to paper?
    private bool isReading = false;       // Is player currently reading the paper?

    void Update()
    {
        if (!isReading)
        {
            CheckForInteraction();
        }

        // Handle interaction input
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isReading && canPickup)
            {
                PickupPaper();
            }
            else if (isReading)
            {
                PutDownPaper();
            }
        }
    }

    void CheckForInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickupDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                canPickup = true;
                // Show interaction prompt (you can add UI for this)
            }
            else
            {
                canPickup = false;
            }
        }
        else
        {
            canPickup = false;
        }
    }

    void PickupPaper()
    {
        isReading = true;
        paperModel.SetActive(false);
        paperReadUI.SetActive(true);

        // Set text content
        paperContentText.text = paperContent;
    }

    void PutDownPaper()
    {
        isReading = false;
        paperModel.SetActive(true);       // Show the 3D paper
        paperReadUI.SetActive(false);     // Hide reading UI

        // Optional: re-enable player movement
        // PlayerMovement.instance.canMove = true;
    }
}
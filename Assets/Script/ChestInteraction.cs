using UnityEngine;
using TMPro; // Remove if using UnityEngine.UI.Text

public class ChestInteraction : MonoBehaviour
{
    public GameObject promptText;       // Assign the "E to interact" text
    public GameObject collectablePrefab; // Assign your additional time prefab
    public Transform dropPoint;         // Where to spawn the collectable
    public float additionalTime = 15f;  // Example: 15 seconds extra

    private bool playerInRange = false;
    private bool opened = false;

    void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptText != null)
                promptText.SetActive(false);
        }
    }

    void Update()
    {
        if (!opened && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        opened = true;
        if (promptText != null)
            promptText.SetActive(false);
        // Optionally: play animation or change sprite

        // Drop the collectable
        if (collectablePrefab != null && dropPoint != null)
        {
            Instantiate(collectablePrefab, dropPoint.position, Quaternion.identity);
        }
    }
}
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public TextMeshProUGUI npcTextBox; // Text for orders above NPC head
    public GameObject npcPrefab; // NPC model

    private Dictionary<string, int> cropRewards = new Dictionary<string, int>
    {
        { "Carrot", 1 },
        { "Broccoli", 2 },
        { "Cauliflower", 3 },
        { "Sunflower", 4 },
        { "Corn", 5 },
        { "Mushrooms", 6 }
    };

    private List<string> requestedItems = new List<string>(); // What the NPC wants
    private List<string> deliveredItems = new List<string>(); // What we've given them so far
    private float requestDuration = 120f; // 2 minutes before NPC leaves

    void Start()
    {
        GenerateRequest();
        UpdateNPCText();
    }

    void Update()
    {
    }

    void Interact()
    {
    }

    void CompleteOrder()
    {
    }

    void GenerateRequest()
    {
        requestedItems.Clear();

        // Tags for all vegtables
        string[] availableCrops = { "Carrot", "Broccoli", "Cauliflower", "Sunflower", "Corn", "Mushroom" };

        // Check which crops are in hierarchy
        List<string> cropsInScene = new List<string>();
        foreach (string cropTag in availableCrops)
        {
            GameObject[] crops = GameObject.FindGameObjectsWithTag(cropTag);
            if (crops.Length > 0)
            {
                cropsInScene.Add(cropTag);
            }
        }

        // Checks crops are placed
        if (cropsInScene.Count > 0)
        {
            // Random number of requested items
            int requestCount = Random.Range(1, Mathf.Min(5, cropsInScene.Count + 1)); // No more than 4 items

            // Randomly picks crops
            for (int i = 0; i < requestCount; i++)
            {
                string randomCrop = cropsInScene[Random.Range(0, cropsInScene.Count)];
                requestedItems.Add(randomCrop);
            }
        }
    }

    void UpdateNPCText()
    {
        if (requestedItems.Count > 0)
        {
            // Displays request
            npcTextBox.text = "I need: " + string.Join(", ", requestedItems) + "!";
        }
        else
        {
            npcTextBox.text = "Thank you!";
        }
    }
}

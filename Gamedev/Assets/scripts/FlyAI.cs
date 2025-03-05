using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAI : MonoBehaviour
{
    public GameObject fly; // Reference to the existing fly in the scene
    public float spawnRadius = 5f;
    public float attackRange = 3f;
    public float moveSpeed = 2f;

    private Dictionary<string, int> cropValues = new Dictionary<string, int>
    {
        {"Carrot", 1}, {"Broccoli", 2}, {"Cauliflower", 3},
        {"Mushroom", 4}, {"Corn", 5}, {"Sunflower", 6}
    };
    private GameObject targetCrop;

    void Start()
    {
        // Ensure the fly is in the scene and set to inactive initially
        if (fly != null)
        {
            fly.SetActive(true); // Make sure the fly is inactive at the start
        }
    }

    void Update()
    {
        FindTargetCrop();
        MoveTowardsTarget();

        // Activate the fly only if it is not active and a crop is found
        if (targetCrop != null && fly != null && !fly.activeInHierarchy)
        {
            Debug.Log("Activating fly towards target crop: " + targetCrop.name); // Log the activation of the fly with the target crop's name

            ActivateFly();
        }
        else if (targetCrop == null && fly != null && fly.activeInHierarchy)
        {
            DeactivateFly();
        }
    }

    void FindTargetCrop()
    {
        // Gather all crops in the scene with tags that match the crop dictionary
        int highestValue = 0;
        GameObject bestCrop = null;

        foreach (var cropValue in cropValues)
        {
            GameObject[] crops = GameObject.FindGameObjectsWithTag(cropValue.Key); // Use the crop name as the tag
            foreach (GameObject crop in crops)
            {
                // If this crop has a higher value, select it as the best target
                if (cropValues[crop.tag] > highestValue)
                {
                    highestValue = cropValues[crop.tag];
                    bestCrop = crop;
                }
            }
        }

        targetCrop = bestCrop; // Set the best crop as the target
    }

    void MoveTowardsTarget()
    {
        if (targetCrop == null || fly == null) return;

        // Calculate direction to the target crop
        Vector3 directionToTarget = targetCrop.transform.position - fly.transform.position;

        // Ensure the fly does not sink below the ground level
        if (fly.transform.position.y < 4)  // Assuming ground level is at y = 0
        {
            fly.transform.position = new Vector3(fly.transform.position.x, 4, fly.transform.position.z);
        }

        // Move the fly towards the target crop, but keep the y position above the ground
        fly.transform.position = Vector3.MoveTowards(fly.transform.position, targetCrop.transform.position, moveSpeed * Time.deltaTime);

        // Optionally: Ensure fly stays above the ground by clamping the y position
        fly.transform.position = new Vector3(fly.transform.position.x, Mathf.Max(fly.transform.position.y, 0f), fly.transform.position.z);
    }

    // Activate the fly
    void ActivateFly()
    {
        if (fly != null)
        {
            fly.SetActive(true); // Set the fly active when a crop is found
        }
    }

    // Deactivate the fly
    void DeactivateFly()
    {
        if (fly != null)
        {
            fly.SetActive(false); // Set the fly inactive when no crop is found
        }
    }
}

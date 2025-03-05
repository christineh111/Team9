using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAI : MonoBehaviour
{
    public GameObject fly; 
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
        if (fly != null)
        {
            fly.SetActive(true); 
        }
    }

    void Update()
    {
        FindTargetCrop();
        MoveTowardsTarget();

        if (targetCrop != null && fly != null && !fly.activeInHierarchy)
        {
            ActivateFly();
        }
        else if (targetCrop == null && fly != null && fly.activeInHierarchy)
        {
            DeactivateFly();
        }
    }

    void FindTargetCrop()
    {
        int highestValue = 0;
        GameObject bestCrop = null;

        foreach (var cropValue in cropValues)
        {
            // Uses the crop name as the tag
            GameObject[] crops = GameObject.FindGameObjectsWithTag(cropValue.Key); 
            foreach (GameObject crop in crops)
            {
                // Targets crops that are more valuable
                if (cropValues[crop.tag] > highestValue)
                {
                    highestValue = cropValues[crop.tag];
                    bestCrop = crop;
                }

                // TODO: Target crops that have more damage
                // TODO: Target crops that are closer to being finished
            }
        }

        targetCrop = bestCrop; 
    }

    void MoveTowardsTarget()
    {
        if (targetCrop == null || fly == null) return;

        // Finds the direction to the crop
        Vector3 directionToTarget = targetCrop.transform.position - fly.transform.position;

        // Stops the fly from sinking
        if (fly.transform.position.y < 4) 
        {
            fly.transform.position = new Vector3(fly.transform.position.x, 4, fly.transform.position.z);
        }

        // Move the fly to the crop
        fly.transform.position = Vector3.MoveTowards(fly.transform.position, targetCrop.transform.position, moveSpeed * Time.deltaTime);
        fly.transform.position = new Vector3(fly.transform.position.x, Mathf.Max(fly.transform.position.y, 0f), fly.transform.position.z);
    }

    // Show fly
    void ActivateFly()
    {
        if (fly != null)
        {
            fly.SetActive(true); 
        }
    }

    // Hide fly
    void DeactivateFly()
    {
        if (fly != null)
        {
            fly.SetActive(false); 
        }
    }
}

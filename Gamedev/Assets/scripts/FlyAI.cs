using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAI : MonoBehaviour
{
    public GameObject flyPrefab; // Prefab for individual fly (if you need a visual representation)
    public float spawnRadius = 5f;
    public float attackRange = 3f;
    public float moveSpeed = 2f;
    public ParticleSystem swarmParticleSystem; // The particle system managing the flies

    private GameObject fly; // The single fly instance
    private Dictionary<string, int> cropValues = new Dictionary<string, int>
    {
        {"Carrot", 1}, {"Broccoli", 2}, {"Cauliflower", 3},
        {"Mushroom", 4}, {"Corn", 5}, {"Sunflower", 6}
    };
    private GameObject targetCrop;

    void Start()
    {
        // Spawn one fly when the game starts
        SpawnFly();
    }

    void Update()
    {
        FindTargetCrop();
        MoveTowardsTarget();
    }

    void FindTargetCrop()
    {
        GameObject[] crops = GameObject.FindGameObjectsWithTag("Broccoli"); // Example for "Broccoli"
        int highestValue = 0;
        GameObject bestCrop = null;

        foreach (GameObject crop in crops)
        {
            // Check if crop's tag matches any of the crops in the dictionary
            if (cropValues.ContainsKey(crop.tag) && cropValues[crop.tag] > highestValue)
            {
                highestValue = cropValues[crop.tag];
                bestCrop = crop;
            }
        }

        targetCrop = bestCrop;
    }

    void MoveTowardsTarget()
    {
        if (targetCrop == null || fly == null) return;

        // Move the fly towards the target crop
        Vector3 directionToTarget = targetCrop.transform.position - fly.transform.position;
        fly.transform.position = Vector3.MoveTowards(fly.transform.position, targetCrop.transform.position, moveSpeed * Time.deltaTime);
    }

    void SpawnFly()
    {
        // Spawn one fly at a random position around the spawn radius
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = transform.position.y; // Keep it at the same height level
        fly = Instantiate(flyPrefab, spawnPosition, Quaternion.identity); // Instantiate the fly
    }
}

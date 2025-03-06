using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
    public Transform spawnPoint;  // Off-screen start position

    public float walkSpeed = 20f;
    public float pauseTime = 120f; // 2 minutes

    private Animator animator; // Reference to the Animator

    void Start() {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the NPC
        StartCoroutine(CustomerRoutine());
    }

    IEnumerator CustomerRoutine() {
        // Spawn off-screen
        transform.position = spawnPoint.position;

        // Walk to ordering/waiting position 
        yield return TurnAndMove(0, 70f);

        // Wait for pause time (in seconds)
        yield return new WaitForSeconds(pauseTime);

        // Turn left (-90 degrees) and walk a few steps
        yield return TurnAndMove(-90, 10f);

        // Turn left again (-90 degrees more) and walk off-screen
        yield return TurnAndMove(-90, 70f);

        // Despawn
        Destroy(gameObject);
    }

    IEnumerator TurnAndMove(float turnAngle, float distance, Vector3? target = null) {
        transform.Rotate(0, turnAngle, 0); // Rotate NPC
        Vector3 startPos = transform.position; // current pos
        Vector3 targetPos = target ?? (startPos + transform.forward * distance);

        // Set the walking animation
        animator.SetBool("isWalking", true); // Set to walking animation

        while (Vector3.Distance(transform.position, targetPos) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }

        // Stop the walking animation once the NPC has reached the target
        animator.SetBool("isWalking", false); // Set back to idle animation
    }
}
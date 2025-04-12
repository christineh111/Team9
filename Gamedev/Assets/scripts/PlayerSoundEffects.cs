using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour {
    [Header("Sound Effects")]
    [SerializeField] private AudioClip wateringSound;
    [SerializeField] private AudioClip harvestingSound;
    [SerializeField] private AudioClip pesticideSound;
    [SerializeField] private AudioClip pickupItemSound;
    [SerializeField] private AudioClip fulfillOrderSound;
    [SerializeField] private AudioClip plantingSound;

    [SerializeField] private float wateringSoundVolume = .5f;
    [SerializeField] private float harvestingSoundVolume = .5f;
    [SerializeField] private float pesticideSoundVolume = .5f;
    [SerializeField] private float pickupItemSoundVolume = .5f;
    [SerializeField] private float fulfillOrderSoundVolume = .5f;
    [SerializeField] private float plantingSoundVolume = .5f;

    [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource component

    private void Awake() {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();  // Ensure there's an AudioSource attached to this GameObject
        }
    }

    public void PlayWateringSound() {
        if (wateringSound != null) {
            PlaySoundEffect(wateringSound, wateringSoundVolume);
        }
    }

    public void PlayHarvestingSound() {
        if (harvestingSound != null) {
            PlaySoundEffect(harvestingSound, harvestingSoundVolume);
        }
    }

    public void PlayPesticideSound() {
        if (pesticideSound != null) {
            PlaySoundEffect(pesticideSound, pesticideSoundVolume);
        }
    }

    public void PlayPickupItemSound() {
        if (pickupItemSound != null) {
            PlaySoundEffect(pickupItemSound, pickupItemSoundVolume);
        }
    }

    public void PlayFulfillOrderSound() {
        if (fulfillOrderSound != null) {
            PlaySoundEffect(fulfillOrderSound, fulfillOrderSoundVolume);
        }
    }

    public void PlayPlantingSound() {
        if (plantingSound != null) {
            PlaySoundEffect(plantingSound, plantingSoundVolume);
        }
    }

    // Private helper function to play the sound
    private void PlaySoundEffect(AudioClip clip, float volume) {
        if (clip != null) {
            audioSource.PlayOneShot(clip, volume);  // Play the sound effect once
        }
    }
}

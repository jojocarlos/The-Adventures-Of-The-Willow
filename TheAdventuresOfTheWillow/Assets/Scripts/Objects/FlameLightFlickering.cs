using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLightFlickering : MonoBehaviour
{
	[Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public UnityEngine.Rendering.Universal.Light2D lightToFlickering;
    
    [Tooltip("Minimum random light intensity")]
    public float minIntensity = 0f;
    [Tooltip("Maximum random light intensity")]
    public float maxIntensity = 1f;
    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
    [Range(1, 50)]
    public int smoothing = 5;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    Queue<float> smoothQueue;
    float lastSum = 0;


    /// <summary>
    /// Reset the randomness and start again. You usually don't need to call
    /// this, deactivating/reactivating is usually fine but if you want a strict
    /// restart you can do.
    /// </summary>
    public void Reset() {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start() {
         smoothQueue = new Queue<float>(smoothing);
         // External or internal light?
         if (lightToFlickering == null) {
            lightToFlickering = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
         }
    }

    void Update() {
        if (lightToFlickering == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        lightToFlickering.intensity = lastSum / (float)smoothQueue.Count;
    }
}

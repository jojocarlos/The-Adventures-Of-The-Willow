using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AmbientTypes : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Bloom bloom;

    [SerializeField] private float threshold = 0.5f;
    [SerializeField] private float intensity = 0.8f;
    [SerializeField] private float scatter = 1.0f;
    [SerializeField] private Color tint = Color.white;

    private void Start()
    {
        volume.profile.TryGet(out bloom);
    }
    void Update()
    {
        bloom.threshold.value = threshold;
        bloom.intensity.value = intensity;
        bloom.scatter.value = scatter;
        bloom.tint.value = tint;
    }
}

using System.Collections.Generic;
using UnityEngine;


public class LightColorSetterIntensity : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.Light2D[] lights;
    [SerializeField] private string firstTagToSearch;
    [SerializeField] private string secondTagToSearch;
    [SerializeField] private string thirdTagToSearch;
    [SerializeField] private float firstIntensity;
    [SerializeField] private float secondIntensity;
    [SerializeField] private float thirdIntensity;

    public void Refresh()
    {
        UnityEngine.Rendering.Universal.Light2D[] foundLights = FindObjectsOfType<UnityEngine.Rendering.Universal.Light2D>();
        List<UnityEngine.Rendering.Universal.Light2D> lightList = new List<UnityEngine.Rendering.Universal.Light2D>();
        foreach (var light in foundLights)
        {
            if (light.gameObject.CompareTag(firstTagToSearch) || light.gameObject.CompareTag(secondTagToSearch) || light.gameObject.CompareTag(thirdTagToSearch))
            {
                lightList.Add(light);
            }
        }
        lights = lightList.ToArray();

    }

    public void Update()
    {
        Refresh();
        foreach (var light in lights)
        {
            if (light.gameObject.CompareTag(firstTagToSearch))
            {
                light.intensity = firstIntensity;
            }
            else if (light.gameObject.CompareTag(secondTagToSearch))
            {
                light.intensity = secondIntensity;
            }
            else if (light.gameObject.CompareTag(thirdTagToSearch))
            {
                light.intensity = thirdIntensity;
            }
        }
    }
}

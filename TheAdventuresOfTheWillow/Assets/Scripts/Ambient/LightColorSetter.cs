using System.Collections.Generic;
using UnityEngine;


public class LightColorSetter : MonoBehaviour, ColorSetterInterface
{
    [SerializeField] Gradient gradient = null;

    private UnityEngine.Rendering.Universal.Light2D[] lights;
    [SerializeField] private string tagToSearch;

    public void Refresh()
    {
        //lights = GetComponentsInChildren<UnityEngine.Rendering.Universal.Light2D>();
         UnityEngine.Rendering.Universal.Light2D[] foundLights = FindObjectsOfType<UnityEngine.Rendering.Universal.Light2D>();
        List<UnityEngine.Rendering.Universal.Light2D> lightList = new List<UnityEngine.Rendering.Universal.Light2D>();
        foreach (var light in foundLights)
        {
            if (light.gameObject.CompareTag(tagToSearch))
            {
                lightList.Add(light);
            }
        }
        lights = lightList.ToArray();
    }

    public void SetColor(float time)
    {
        //foreach (var light in lights)
        //    light.color = gradient.Evaluate(time);
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = gradient.Evaluate(time);
        }
    }
}

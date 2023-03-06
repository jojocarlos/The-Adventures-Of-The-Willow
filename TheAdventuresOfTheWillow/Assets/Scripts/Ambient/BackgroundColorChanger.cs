using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BackgroundColorChanger : MonoBehaviour
{
    public string tagToFind = "Background";
    public Color color;

    public List<Material> materials = new List<Material>();

    private void Awake()
    {
        var gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tagToFind);
        var spriteShapeRenderers = new List<SpriteShapeRenderer>();

        foreach (var gameObject in gameObjectsWithTag)
        {
            var spriteShapeRenderer = gameObject.GetComponent<SpriteShapeRenderer>();
            if (spriteShapeRenderer != null)
            {
                spriteShapeRenderers.Add(spriteShapeRenderer);
            }
        }

        foreach (var spriteShapeRenderer in spriteShapeRenderers)
        {
            foreach (var material in spriteShapeRenderer.sharedMaterials)
            {
                if (!materials.Contains(material))
                {
                    materials.Add(material);
                }
            }
        }
    }

    private void Update()
    {
        var gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tagToFind);
        var spriteShapeRenderers = new List<SpriteShapeRenderer>();

        foreach (var gameObject in gameObjectsWithTag)
        {
            var spriteShapeRenderer = gameObject.GetComponent<SpriteShapeRenderer>();
            if (spriteShapeRenderer != null)
            {
                spriteShapeRenderers.Add(spriteShapeRenderer);
            }
        }

        foreach (var spriteShapeRenderer in spriteShapeRenderers)
        {
            foreach (var material in spriteShapeRenderer.sharedMaterials)
            {
                if (!materials.Contains(material))
                {
                    materials.Add(material);
                }
            }
        }
        foreach (var material in materials)
        {
            material.color = color;
        }
    }
}

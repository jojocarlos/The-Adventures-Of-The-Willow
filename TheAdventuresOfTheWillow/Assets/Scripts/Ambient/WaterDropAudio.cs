using UnityEngine;

public class WaterDropAudio : MonoBehaviour
{
    public LayerMask Layers;

    private void OnParticleCollision(GameObject other)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Layers);

        Vector2 splashLocation = hit.point;
        
        Camera MainCam = FindObjectOfType<Camera>();

        Vector3 screenPoint = MainCam.WorldToViewportPoint(splashLocation);

        bool onScreen = screenPoint.x > 0 &&
                        screenPoint.x < 1 && 
                        screenPoint.y > 0 &&
                        screenPoint.y < 1;

        if(onScreen)
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ambience/Water/WaterDrops", splashLocation);
    }
}
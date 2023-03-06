using UnityEngine;

public class WaterDropAudio : MonoBehaviour
{
    public LayerMask Layers;

    private void OnParticleCollision(GameObject other)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Layers);

        Vector2 splashLocation = hit.point;
        
        AudioManager.instance.PlayOneShot(FMODEvents.instance.WaterDrops, splashLocation);
    }
}
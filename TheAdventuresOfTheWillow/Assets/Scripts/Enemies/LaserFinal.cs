using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFinal : MonoBehaviour
{
    [SerializeField] private Color color = new Color(191/255,36/255,0);
    [SerializeField] private float colorIntensity = 4.3f;
    private float beamColorEnhance = 1;

    [SerializeField] private float maxLength = 100;
    [SerializeField] private float thickness = 9;
    [SerializeField] private float noiseScale = 3.14f;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;

    public LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.color = color * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);

        ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in particles)
        {
            Renderer r = p.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnhance));
        }
		
			Debug.Log("Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateEndPosition();   
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEndPosition();
    }
	
	public void UpdatePosition(Vector2 startPosition, Vector2 direction)
	{
		direction = direction.normalized;
		transform.position = startPosition;
		float rotationZ = Mathf.Atan2(direction.y, direction.x); //radian
		transform.rotation = Quaternion.Euler(0, 0, rotationZ*Mathf.Rad2Deg);
		
			Debug.Log("updateposition");
	}

    private void UpdateEndPosition()
    {
        Vector2 startPosition = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z;//degree
        rotationZ *= Mathf.Deg2Rad;//radian
        
			Debug.Log("updateendposition");
        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);
		Debug.DrawLine(startPosition, direction.normalized);

        float length = maxLength;
        float laserEndRotation = 180;

        if (hit)
        {
            length = (hit.point - startPosition).magnitude;

            laserEndRotation = Vector2.Angle(direction, hit.normal);
            Debug.Log(laserEndRotation);
			Debug.Log("IsHit");
        }

        lineRenderer.SetPosition(1, new Vector2(length, 0));
        
        Vector2 endPosition = startPosition + length * direction;
        startVFX.transform.position = startPosition;
        endVFX.transform.position = endPosition;
        endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }
}

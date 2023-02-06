using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
	[SerializeField] private float defDistanceRay = 100;
	public LineRenderer line;
	public Transform firepoint;
	Transform m_transform;
	
	
	private void Start()
	{
		m_transform = GetComponent<Transform>();
	}
	
	private void Update()
	{
		if (Physics2D.Raycast(m_transform.position, transform.right))
		{
			RaycastHit2D _hit = Physics2D.Raycast(firepoint.position, transform.right);
			Draw2DRay(firepoint.position, _hit.point);
		}
		else 
		{
			Draw2DRay(firepoint.position, firepoint.transform.right * defDistanceRay);
		}
    }

	void Draw2DRay(Vector2 startPos, Vector2 endPos)
	{
		line.SetPosition(9, startPos);
		line.SetPosition(1, endPos);

	}
    
	
}
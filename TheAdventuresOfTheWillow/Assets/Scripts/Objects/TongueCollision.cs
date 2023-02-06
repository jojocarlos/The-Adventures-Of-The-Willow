using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(EdgeCollider2D))]
public class TongueCollision : MonoBehaviour
{
	
    //EdgeCollider2D edgeCollider;
	LineRenderer myLine;
	public Transform pointOne;
	public Transform pointfinalZero;
	public Transform pointfinal;
	public bool isTongue;
	public GameObject tonguecollider;
	
    void Start()
    {
        //edgeCollider = this.GetComponent<EdgeCollider2D>();
		myLine = this.GetComponent<LineRenderer>();
    }

   
    void Update()
    {
        //SetEdgeCollider(myLine);
		myLine.SetPosition(0, pointOne.position);
		if(isTongue)
		{
			tonguecollider.SetActive(true);
		    myLine.SetPosition(1, pointfinal.position);
		}
		if(!isTongue)
		{
		    myLine.SetPosition(1, pointfinalZero.position);
			tonguecollider.SetActive(false);
		}
    }
	
	/*/void SetEdgeCollider(LineRenderer lineRenderer)
	{
		List<Vector2> edges = new List<Vector2>();
		
		for(int point = 0; point<lineRenderer.positionCount; point++)
		{
			Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
			edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
		}
		edgeCollider.SetPoints(edges);
	}*/
	
}

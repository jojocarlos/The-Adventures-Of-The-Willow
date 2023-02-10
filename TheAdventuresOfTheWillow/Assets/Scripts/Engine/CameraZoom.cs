using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	private GameObject CameraObj;
    private CinemachineVirtualCamera _vCam;

    float _fov;
    public Rigidbody2D playerRb;
	
	[Range(1,3)]
	public float waitTime;
	
	float waitCounter;
	
	bool zoomIn;
	bool zoomOutJump;
	public bool zoomfinish;

    public float zoomSpeed;
	public float zoomSpeedFinish;
	
	public float X, Y, Z;
	public float PosT;
    
    private void Awake()
    {
		zoomfinish = false;
    }
	
	private void Start()
	{
		CameraObj = GameObject.FindGameObjectWithTag("CinemachineCamera");
		_vCam = CameraObj.GetComponent<CinemachineVirtualCamera>();
	}
    
    public void ZoomIn()
	{
		_vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, 75, zoomSpeed);
	}
	
	public void ZoomOut()
	{
		_vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, 90, zoomSpeed);
	}

    public void ZoomOutJump()
    {
        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, 100, zoomSpeed);
    }
	
	public void ZoomOutFinish()
    {
		zoomfinish = true;
        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, 120, zoomSpeedFinish);

    }

    public void SwimmingZoomOut()
    {
        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, 90, zoomSpeed);
    }

    private void LateUpdate()
	{
		if(Mathf.Abs(playerRb.velocity.magnitude)<8 && !zoomfinish)
		{
			waitCounter += Time.deltaTime;
			if(waitCounter > waitTime)
			{
				zoomIn = true;
			}
		}
        else
        {
            zoomIn = false;
            waitCounter = 0;
        }

        if (Mathf.Abs(playerRb.velocity.y)<8 && !zoomfinish)
		{
			waitCounter += Time.deltaTime;
			if(waitCounter > waitTime)
			{
				zoomOutJump = true;
			}
		}
		else 
		{
			zoomOutJump = false;
            waitCounter = 0;
		}

		if (zoomIn)
		{
			ZoomIn();
		}
		else
		{
			ZoomOut();
		}

		if (zoomOutJump)
	    {
            ZoomIn();
        }
		else
		{
            ZoomOutJump();
        }
		
	}
   
}

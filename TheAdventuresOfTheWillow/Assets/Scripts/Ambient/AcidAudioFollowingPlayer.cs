using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class AcidAudioFollowingPlayer : MonoBehaviour
{
    private WaterArea waterArea;
    private Transform player;
    private float xPosMax;
    private float xPosMin;
    private float yPosMax;
    private float yPosMin;
    private Vector3 audioPoint;

    private FMOD.Studio.EventInstance AcidLoop;

    // Start is called before the first frame update
    void Start()
    {
        waterArea = GetComponent<WaterArea>();
        player = FindObjectOfType<PlayerMovement2D>().transform;

        float waterLength = waterArea.size.x;
        float waterHeight = waterArea.size.y;

        xPosMax = transform.position.x + (waterLength * transform.lossyScale.x / 2f);
        xPosMin = transform.position.x - (waterLength * transform.lossyScale.x / 2f);
        yPosMax = transform.position.y + (waterHeight * transform.lossyScale.y / 2f);
        yPosMin = transform.position.y - (waterHeight * transform.lossyScale.y / 2f);

        AcidLoop = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Ambience/Water/Acid");
        AcidLoop.start();
        AcidLoop.release();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.Clamp(player.position.x, xPosMin, xPosMax);
        float yPos = Mathf.Clamp(player.position.y, yPosMin, yPosMax);
        audioPoint = new Vector3(xPos, yPos, transform.position.z);

        AcidLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(audioPoint));

        if(player.position.y < yPosMin)
        {
            AcidLoop.setParameterByName("AcidAbove", yPosMin - player.position.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(audioPoint, 0.25f);
    }

    private void OnDestroy()
    {
        AcidLoop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}

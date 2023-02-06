using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Captainmushroom
{
    RED,GREEN
}

public class CaptainMushroom : MonoBehaviour
{
    RaycastHit hit;
    public float Distance;
    public LayerMask LayerM;
    public bool Right;
    public float speed;

    public Captainmushroom captainM;
    RaycastHit hit2;
    public Vector3 v3;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * Distance);
        Gizmos.DrawRay(transform.position+v3, transform.up*-1 * Distance);
    }
    // Update is called once per frame
    void Update()
    {
        switch (captainM)
        {
            case Captainmushroom.GREEN:

                ani.SetLayerWeight(0, 1);
                ani.SetLayerWeight(1, 0);

                if (Right)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if (Physics2D.Raycast(transform.position, transform.right, Distance, LayerM))
                {
                    Right = !Right;
                }
                break;
            case Captainmushroom.RED:

                ani.SetLayerWeight(0, 0);
                ani.SetLayerWeight(1, 1);

                if (Right)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                if (Physics2D.Raycast(transform.position, transform.right, Distance, LayerM))
                {
                    Right = !Right;
                }
                if (Physics2D.Raycast(transform.position+v3, transform.up*-1, Distance, LayerM))
                {

                }
                else
                {
                    Right = !Right;
                }
                break;

        }
    }
}

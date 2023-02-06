using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whompfall : MonoBehaviour
{
    public GameObject ThwompPrefab;
    public GameObject pontoA;
    private bool isMove;
    public float speedMove;

    private Animator myanimthwomp;

    void Start()
	{
        myanimthwomp = GetComponent<Animator>();
	}
    void Update()
    { 
        if(isMove == true) 
        {
            ThwompPrefab.transform.position = Vector2.MoveTowards(ThwompPrefab.transform.position, pontoA.transform.position, speedMove * Time.deltaTime);
            myanimthwomp.SetTrigger("twb");
        }
    }
    IEnumerator WaitBeforeFall()
    {
        yield return new WaitForSeconds(2f);
        ThwompPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ThwompPrefab.GetComponent<Rigidbody2D>().gravityScale = 7;
        ThwompPrefab.GetComponent<Rigidbody2D>().mass = 400;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.TwompFall, this.transform.position);
        myanimthwomp.SetTrigger("twa");

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ThwompPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ThwompPrefab.GetComponent<Rigidbody2D>().gravityScale = 7;
            ThwompPrefab.GetComponent<Rigidbody2D>().mass = 400;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.TwompFall, this.transform.position);
        }
    }
  
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            ThwompPrefab.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            ThwompPrefab.GetComponent<Rigidbody2D>().gravityScale = 0;
            ThwompPrefab.GetComponent<Rigidbody2D>().mass = 0;
            
            isMove = true;
        }
    }


}

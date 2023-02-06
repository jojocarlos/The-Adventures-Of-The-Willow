using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class shootSomething : MonoBehaviour
{

    public GameObject projectile;
    public Vector2 velocity;
    bool canShoot = true;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    public float cooldown = 1f;
    
	public Powers powers;

    public void ShootSomething(InputAction.CallbackContext context)
	{
		if (context.performed && canShoot && powers.isFirePlayer)
		{
			GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
            StartCoroutine(CanShoot());
            GetComponent<Animator>().SetTrigger("shoot");
		}
	}
     /*/old
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) && canShoot && powers.isFirePlayer)
        {

            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);


            StartCoroutine(CanShoot());

            GetComponent<Animator>().SetTrigger("shoot");

        }


    }
*/

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
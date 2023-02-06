using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour 
{
	private Animator spriteAnim;
	private GameObject child;
	public int totalCoins;
	public Sprite disabled;
	public GameObject QuestionToDisable;
	public AudioSource audioSource;
	public AudioSource audioNoCoin;

	void Start () {
		child = transform.GetChild (0).gameObject;
		spriteAnim = child.GetComponent<Animator>();
	}
	
	void OnCollisionEnter2D(Collision2D col) {

		if (col.collider.bounds.max.y < transform.position.y
			&& col.collider.bounds.min.x < transform.position.x + 0.5f
			&& col.collider.bounds.max.x > transform.position.x -0.5f
			&& col.collider.tag == "Player") {
				if (totalCoins > 0) {
					spriteAnim.Play ("BonusBlock_Hit");
					audioSource.Play();
					CoinCollect.instance.AddCoin();
					totalCoins -= 1;
					if (totalCoins == 0) {
						GetComponent<Animator> ().enabled = false;
						child.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
						child.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().sprite = disabled;
						QuestionToDisable.SetActive(false);
						audioNoCoin.Play();
					}
				}
				if (totalCoins == 0) 
				{
					audioNoCoin.Play();
				}

			}
		}
	}

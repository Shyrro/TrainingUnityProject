using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
	public SceneManager Manager;
	public PlayerController Player;
	public PlayerController Follower;

	public float Speed = 0;
	public float Jump = 0;
	public float DurationTime = 10;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			Destroy(gameObject);
			
			Manager.Apply(Player, Speed, Jump, DurationTime);
			Manager.Apply(Follower, Speed, Jump, DurationTime);
		}
	}
}

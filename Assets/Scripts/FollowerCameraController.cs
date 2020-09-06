using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCameraController : MonoBehaviour
{
	private float _leftPart;
	private float _rightPart;

	public Collider2D tilemap;
	public GameObject player;

    void Start()
    {
    	var camera = GetComponent<Camera>();

    	camera.orthographicSize = tilemap.bounds.size.y / 2.0f;
    	camera.aspect = 16.0f / 9.0f;

    	float height = camera.orthographicSize * 2.0f;
    	float width = height * camera.aspect;

        this.transform.position = new Vector3(
        	tilemap.bounds.min.x + width / 2.0f,
        	tilemap.bounds.min.y + tilemap.bounds.size.y / 2.0f,
        	this.transform.position.z
    	);

    	_leftPart = tilemap.bounds.min.x + width / 2.0f;
    	_rightPart = tilemap.bounds.max.x - width / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < _leftPart || player.transform.position.x > _rightPart)
        	return;

        this.transform.position = new Vector3(
        	player.transform.position.x,
        	this.transform.position.y,
        	this.transform.position.z
    	);
    }
}

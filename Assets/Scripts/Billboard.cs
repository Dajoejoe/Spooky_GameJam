using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    GameObject _player;
    Transform _transform;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag(Tags.Player);
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        _transform.LookAt(_player.transform, Vector3.up);
	}
}

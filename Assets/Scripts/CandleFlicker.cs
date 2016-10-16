using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour {

    public float minDelay;
    public float maxDelay;
    public float minIntensity;
    public float maxIntensity;

    Light _light;
    float timer;

	// Use this for initialization
	void Start () {
        _light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0)
        {
            _light.intensity = Random.Range(minIntensity, maxIntensity);
            timer = Random.Range(minDelay, maxDelay);
        }
        else
        {
            timer -= Time.deltaTime;
        }
	}
}

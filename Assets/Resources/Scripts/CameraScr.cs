using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScr : MonoBehaviour {

    public float cameraSpeed;
    public GameManagerScr gamemanagerScr;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement()
    {
        //Debug.Log(cameraSpeed);
        transform.Translate((new Vector3(gamemanagerScr.movement.y, 0, gamemanagerScr.movement.x).normalized) * cameraSpeed * Time.deltaTime,Space.World);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour {
// {
	[SerializeField] float rcsThrust = 230f;
	[SerializeField] float mainThrust = 80f;

	private nextLevel = 1;

	private Rigidbody rigidBody;
	private AudioSource rocketThrustSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketThrustSound = GetComponent<AudioSource>();
        
         
    }


    //collision detection
    void OnCollisionEnter(Collision collision) {
    	
    	switch(collision.gameObject.tag) {

    		case "friendly" :
    		print("launch pad");
    		break;

    		case "LandingPad" :
    		SceneManager.LoadScene(nextLevel);
    		print("LandingPad");
    		if (nextLevel == 1) {
    			nextLevel = 0;
    		} else {
    			nextLevel++;
    		}
    		
    		break;

    		case "Dead" :
    		print("Dead");
    		SceneManager.LoadScene(0);
    		break;

    		// default : print("break");
    	}

    }

    // Update is called once per frame
    void Update()
    {
    
    	ProcessInput();
        
    }


    void ProcessInput() {
    	
    	Thrust();

    	Rotate();

    }

    void Thrust() {


    	if (Input.GetKey(KeyCode.Space)) {
    	
    		rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    	
    	
    		if (!rocketThrustSound.isPlaying) {
        			rocketThrustSound.Play();
        	}
    	
    	
   		 }
   		 else {
    		rocketThrustSound.Stop();
    	}
    }


    void Rotate() {

    	rigidBody.freezeRotation = true;

    	
    	float rotationSpeedThisFrame = rcsThrust * Time.deltaTime;

    	if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(Vector3.left * rotationSpeedThisFrame);
    	} 
    	if (Input.GetKey(KeyCode.D)) {
    		transform.Rotate(Vector3.right* rotationSpeedThisFrame);
    	}

    	rigidBody.freezeRotation = false;// rocket falls off to gravity after you lose the control
    }
}

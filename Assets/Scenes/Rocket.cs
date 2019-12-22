using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rcs = 10000f;
    [SerializeField] float MainThrust = 1000f;
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrusting();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Enemy":
                print("DEAD");
                break;

            case "Fuel":
                print("FUEL");
                break;
        }
    }
        void Thrusting()
    {
        float thrustingframe = MainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustingframe);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            rigidBody.AddRelativeForce(Vector3.up * thrustingframe);
            audioSource.Stop();
        }
    }
    void Rotate()
    {
        float rotationframe;
        rotationframe = rcs * Time.deltaTime;
        rigidBody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationframe);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationframe);
        }
        rigidBody.freezeRotation = false;
    }

    
}

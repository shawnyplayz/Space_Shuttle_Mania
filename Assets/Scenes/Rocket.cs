using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rcs = 10000f;
    [SerializeField] float MainThrust = 1000f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] AudioClip destroyed;
    [SerializeField] AudioClip finsihed;
    Rigidbody rigidBody;
    AudioSource audioSource;
    enum State {alive, dying, transcending}
    State state = State.alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            RespondToThrust();
            RespondToRotate();
        }       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            
            
            case "Friendly":
                //nothing to do
                break;
            case "Obstacles":
                StartDeathSequence();
                break;

            case "Fin":
                StartEndSequence();
                break;
        }
    }

    private void StartEndSequence()
    {
        state = State.transcending;
        audioSource.PlayOneShot(finsihed);
        Invoke("LoadNextScene", 1f);
        //Loads the next scene
    }

    private void StartDeathSequence()
    {
        print("Deadly");
        state = State.dying;
        audioSource.Stop();
        audioSource.PlayOneShot(destroyed);
        Invoke("LoadSameScene", 1f);
        //Loads the scene again
    }

    void LoadSameScene()
    {
        
        
        SceneManager.LoadScene(0);

    }

    void LoadNextScene()// allow for more than two levels
    {
        SceneManager.LoadScene(1);
    }

    void RespondToThrust()
    {
        float thrustingframe = MainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustingframe);
        }
        else
            audioSource.Stop();

    }

   
        

    private void ApplyThrust(float thrustingframe)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustingframe);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainengine);
        }
    }

    void RespondToRotate()
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

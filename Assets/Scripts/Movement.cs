using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS-for tuning,typically set in the editor
    // CACHE e.g. references for readability or speed
    // STATE private instance(member)variables

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem mainEngineParticles;

    AudioSource audioSource;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }           
        else
        {
            StopThrust();
        }
    }
    void ProcessRotate()
    {
        Rotation();
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

       if (!audioSource.isPlaying)
       {
          audioSource.PlayOneShot(mainEngine);
          mainEngineParticles.Play();
       }
    }

    void StopThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopRotate()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}

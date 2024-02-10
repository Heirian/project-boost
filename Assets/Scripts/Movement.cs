using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource boostAudioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            boostAudioSource.Stop();
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!boostAudioSource.isPlaying)
            {
                boostAudioSource.PlayOneShot(mainEngine);
            }
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    private void ApplyRotation(Vector3 vector)
    {
        rb.freezeRotation = true;
        transform.Rotate(vector * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successLevelSound;
    [SerializeField] AudioClip failLevelSound;
    AudioSource boostAudioSource;
    bool isTransitioning = false;

    void Start() 
    {
        boostAudioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly");
                    break;
                case "Finish":
                    StartNextLevelSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartNextLevelSequence()
    {
        isTransitioning = true;
        boostAudioSource.Stop();
        GetComponent<Movement>().enabled = false;
        boostAudioSource.PlayOneShot(successLevelSound);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        boostAudioSource.Stop();
        GetComponent<Movement>().enabled = false;
        boostAudioSource.PlayOneShot(failLevelSound);
        Invoke("ReloadLevel", levelLoadDelay);
    }
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

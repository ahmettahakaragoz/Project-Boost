using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succesParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isTransitioning = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

           switch(other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Safe Area");
                    break;

                case "Finish":
                    StartNextSequence();
                    break;

                default:
                    StartCrashSequence();
                    break;
            }
    }

    void StartCrashSequence()
    {
        isTransitioning = true; 
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);//ReloadLevel Metodu �al��madan �nce delay kadar bekletiyoruz
    }

    void StartNextSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        succesParticles.Play();
        audioSource.PlayOneShot(succes);
        GetComponent<Movement>().enabled = false;//Movement Componentini devred��� b�rak�yoruz
        Invoke("NextLevel", delay);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //mevcut sahnemizi y�klenmesi i�in onu bir de�i�kene at�yoruz
        int loadNextLevel = currentSceneIndex + 1; //bir sonraki seviyeye ge�mek i�in mevcut sahneyi art�r�yoruz
       
        if(loadNextLevel == SceneManager.sceneCountInBuildSettings)//e�er loadNextLevel de�i�kenimiz t�m sahnelerimizin say�s�na e�itse ilk level'� y�kl�yoruz
        {
            loadNextLevel = 0;
        }
        SceneManager.LoadScene(loadNextLevel);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

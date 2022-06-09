using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }
    void OnCollisionEnter(Collision other)
    {
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
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", delay);//ReloadLevel Metodu �al��madan �nce delay kadar bekletiyoruz
    }

    void StartNextSequence()
    {
        GetComponent<Movement>().enabled = false;//Movement Componentini devred��� b�rak�yoruz
        audioSource.PlayOneShot(succes);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
       switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe Area");
                break;

            case "Fuel":
                Debug.Log("Getting Fuel");
                break;

            case "Finish":
                Debug.Log("You Win!");
                break;

            default:
                Debug.Log("Try Again");
                break;
        }
    }
}

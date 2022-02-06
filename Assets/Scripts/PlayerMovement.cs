using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float bounceForce;


    private void OnCollisionEnter(Collision collision)
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName== "Safe (Instance)")
        {
            //The ball hits the safe area
        }
        else if (materialName == "Unsafe (Instance)")
        {
            //The ball hits the unsafe area
            GameManager.gameOver = true;
        }
        else if (materialName == "LastRing (Instance)")
        {
            //Level Comlpeted
            GameManager.levelComplete = true;
        }
    }
}


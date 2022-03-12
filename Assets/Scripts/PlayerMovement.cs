using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float bounceForce;

    private AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
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
            audioManager.Play("game over");
        }
        else if (materialName == "LastRing (Instance)" && !GameManager.levelComplete) 
        {
            //Level Comlpeted
            GameManager.levelComplete = true;
            audioManager.Play("win level");
        }
    }
}


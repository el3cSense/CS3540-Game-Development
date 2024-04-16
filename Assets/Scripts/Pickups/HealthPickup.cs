using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float hoverSpeed = 1f;
    public float hoverHeight = 0.5f;
    public float rotationSpeed = 50f;
    public float respawnTimer = 5f;
    public int healthAmnt = 20;

    public AudioClip pickupSound;

    public GameObject levelManager;

    public void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }



    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position based on a sine wave to create a hovering effect
        float newY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Set the new position
        transform.position = new Vector3(transform.position.x, newY + 2, transform.position.z);

        // Rotate the cube around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ensure levelManager is not null before accessing its components
            if (levelManager != null)
            {
                var healthControl = levelManager.GetComponent<HealthControl>();
                if (healthControl != null)
                {
                    Debug.Log("Player hit health");
                    Debug.Log(levelManager);

                    //audio clip
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                    //particle effects
                    

                    //add health
                    healthControl.playerGainHealth(healthAmnt);

                    gameObject.SetActive(false);
                    Invoke("Respawn", respawnTimer);

                }
                else
                {
                    Debug.LogWarning("HealthControl component not found on the LevelManager GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("GameObject with tag 'LevelManager' not found.");
            }
        }
    }
    
    void Respawn()
    {
        // Instantiate a new object at the same position
         gameObject.SetActive(true);
    }



}
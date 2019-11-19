using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 target;
    public AudioManager audioManager;
    public float speed = 0.00001f;
    
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        transform.LookAt(target);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TestEnemyMove>().health--;
            audioManager.updateAudio("enemy");
            Debug.Log("Hit enemy");
        }
        Destroy(gameObject);
    }
}

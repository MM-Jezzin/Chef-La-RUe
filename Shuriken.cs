using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        if (this.gameObject != null) // It will destroy the shuriken 1 second after it is thrown.
        {
            Destroy(gameObject, 1f);
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // When the shuriken collides with enemy "collision"
    {
        if (collision.gameObject.tag == "Enemies")// Will destory the shuriken and the enemies.
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log(collision.gameObject.tag);
      
        }
        Debug.Log("Collided");
    }

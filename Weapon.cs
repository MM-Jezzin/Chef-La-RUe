using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject go_projectile;
    [SerializeField] private GameObject go_shot;
    [SerializeField] private GameObject go_player;
    [SerializeField] private Vector3 v3_offsetRight; // Offset when player is looking Right
    [SerializeField] private Vector3 v3_offsetLeft;  // Offset when player is looking Left
    private Rigidbody2D rb_shot;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            go_shot = (GameObject)Instantiate(go_projectile);
            rb_shot = go_shot.GetComponent<Rigidbody2D>();
            Vector3 v3_spawnPosition = go_player.transform.position + v3_offsetRight;

            if (go_player.transform.localScale.x < 0) // If player is facing left
            {
                v3_spawnPosition = go_player.transform.position + new Vector3(v3_offsetLeft.x, v3_offsetLeft.y, v3_offsetLeft.z); // Making spawn position of projectile the offset for when player is looking left
                rb_shot.velocity = Vector3.right * -10;
                Debug.Log("Firing projectile left");
            }
            else if (go_player.transform.localScale.x > 0)
            {
                v3_spawnPosition = go_player.transform.position + new Vector3(v3_offsetRight.x, v3_offsetRight.y, v3_offsetRight.z); // Making spawn position of projectile the offset for when player is looking right
                rb_shot.velocity = Vector3.right * 10;
                Debug.Log("Firing projectile right");
            }
            go_shot.transform.position = v3_spawnPosition;
        }
    }
}

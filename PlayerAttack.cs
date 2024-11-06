
// IGLORE THIS THIS AN OLD ONE Weapon.cs is the new one















using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float rangeattackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] ChocolateStars;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float rangecooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && rangecooldownTimer > rangeattackCooldown && playerMovement.canAttack())
        {
            Attack();

            rangecooldownTimer += Time.deltaTime;
        }    
    }
   
    private void Attack()
    {
        anim.SetTrigger("rangeAttack");
        rangecooldownTimer = 0;

        ChocolateStars[FindChocolateStar()].transform.position = firePoint.position;
        ChocolateStars[FindChocolateStar()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int FindChocolateStar()
    {
        for (int i = 0; i < ChocolateStars.Length; i++)
        {
            if (ChocolateStars[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

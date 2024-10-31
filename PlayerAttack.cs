using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float rangeattackCooldown;
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
    }
}

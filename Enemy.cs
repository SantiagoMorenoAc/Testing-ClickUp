using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     public Animator _animator;
    [SerializeField] private EnemySettings enemySettings;

    [SerializeField] private TransformList myTargets;
    [SerializeField] private Transform actualTarget;

    [SerializeField] private Resource drop;

    private float currentHealt;
    private float _enemyRange;

    private void Start()
    {
        _enemyRange =  enemySettings.range * enemySettings.range;
        ChangeTargetC();
    }

    private void Update()
    {
        CheckTargets();
        var sqrDistanceToTurret =
            (actualTarget.position -transform.position).sqrMagnitude; //calculate the distance between enemy and the nearest tower
        if (sqrDistanceToTurret > _enemyRange) // if the distance is greater than the range it walks
        {
            Walk();
        }
        else
        {   
            Attack(enemySettings.attackSpeed);
        }
    }


    private void Walk()
    {
        Vector3 dir = actualTarget.position - transform.position;
        transform.Translate(dir.normalized * (enemySettings.speed * Time.deltaTime));
    }

    private void Attack(float attackVelocity)
    {
        if(actualTarget != null){
            _animator.SetFloat("animationSpeed", attackVelocity );
            _animator.SetBool("isInRange", true);
            _animator.SetBool("hasTarget", true);

        }
    }

    public void TakeDamage(float dmgdone)
    {
        currentHealt = currentHealt - dmgdone;
        if (currentHealt <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this);
    }


    private void ChangeTargetC()
    {
        actualTarget = myTargets.list[myTargets.list.Count - 1];
        Debug.Log("target cambió ");
    }

   

    private void CheckTargets()
    {
        if (actualTarget == null)
        {
            if (myTargets.list[myTargets.list.Count - 1] == null)             //Delete the index of the list if is not destroyed with the object  
            {
                myTargets.list.RemoveAt(myTargets.list.Count - 1 );
            }

            ChangeTargetC();
        }
    }
}
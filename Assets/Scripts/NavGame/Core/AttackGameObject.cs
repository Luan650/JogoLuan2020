﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;
using UnityEngine.AI;

namespace NavGame.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AttackGameObject : TouchableGameObject
    {
        public OfenceStats ofenceStats;
        protected NavMeshAgent agent;
        float cooldown = 0f;

        public OnAttackHitEvent onAttackHit;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        protected virtual void Update()
        {
            DecreaseAttackCooldown();
        }
        public void AttackOnCooldown(DamageableGameObject target)
        {
            if (cooldown <= 0f)
            {
                cooldown = 1f / ofenceStats.attackSpeed;
                target.TakeDamage(ofenceStats.damage);
                if(onAttackHit != null)
                {
                    onAttackHit(target.transform.position);
                }
            }
        }

        void DecreaseAttackCooldown()
        {
            if (cooldown == 0f)
            {
                return;
            }
            cooldown -= Time.deltaTime;
            if (cooldown < 0f)
            {
                cooldown = 0f;
            }
        }
    }
}
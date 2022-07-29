using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Beehaw.Character
{
    public class Health : MonoBehaviour
    {

        [SerializeField] private int healthPoints;
        
        public int getHealthPoints()
        {
            return healthPoints;
        }

        public virtual void applyDamage(int damageToApply)
        {
            healthPoints -= damageToApply;
            if (healthPoints < 1)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
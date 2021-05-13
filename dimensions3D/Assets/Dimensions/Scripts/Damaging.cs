using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public float damageRate = 1f;
    /* 
     * Gets called every FixedUpdate, so the damage rate 
     * is robust to FPS changes
     */
    virtual public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Unity.FPS.Game.Damageable>().InflictDamage(damageRate, false, gameObject);
        }
    }
}

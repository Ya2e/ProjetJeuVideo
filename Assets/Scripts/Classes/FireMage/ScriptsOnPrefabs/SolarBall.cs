﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBall : LinearProjectile
{
<<<<<<< HEAD
=======
	[SerializeField] private Transform _particlesOnFloor;
	[SerializeField] private Transform _explosionOnFloor;
>>>>>>> refs/remotes/origin/Develop
    /// <summary>
    /// ApplyEffect method -> implementation of the abstract method in LinearProjectile mother Class
    /// </summary>
    /// <param name="col">is the collider touch by the projectile</param>
<<<<<<< HEAD
    public override void ApplyEffect(Collider col)
    {
        // TODO ...
    }
=======
	protected override void Start()
    {
		projectileSpeed = 300f;
		launcher = transform.parent;
        target = launcher.GetComponentInParent<SolarBurnSpell>().TargetOfSolarBurn;
		Debug.Log(target);
		base.Start();
    }
	
	/** ApplyEffect public override void
	 * Apply damages to the collider we hit 
	 **/
    public override void ApplyEffect(Collider col)
    {        
        if (eHit != null && eHit.gameObject.tag != "Player")
        {
            eHit.DamageFor(100);
        }
    }
		
	/** AdditionalEffects public override void
	 * No matter if we touch or not entities, when the Fireball hits the ground, we apply damages in an area of effect.
	 * We also display all animations
	 **/
	protected override void AdditionalEffects()
	{
		Instantiate(_explosionOnFloor.gameObject, transform.position, _explosionOnFloor.rotation);
		Collider[] cols = Physics.OverlapSphere(transform.position, 10f);
		foreach (Collider col in cols)
        {
			if (col.gameObject.GetComponent<EntityLivingBase>() && !col.isTrigger && col.gameObject.GetComponent<EntityLivingBase>().tag != "Player")
			{
                col.gameObject.GetComponent<EntityLivingBase>().DamageFor(100);
			}
		}
		
		for(int i = 0; i < 3; i ++)
		{
			Vector2 pointInCircle = Random.insideUnitCircle.normalized * 3;
			Vector3 v = new Vector3(transform.position.x + pointInCircle.x, transform.position.y, transform.position.z + pointInCircle.y);
			Instantiate(_particlesOnFloor.gameObject, v, _particlesOnFloor.rotation);
		}
	}
>>>>>>> refs/remotes/origin/Develop
}
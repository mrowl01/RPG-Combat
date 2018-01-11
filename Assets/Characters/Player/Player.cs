using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI; // TODO re-wire code possibly
using RPG.Core;
using RPG.Weapons ;


namespace RPG.Characters 
{
	public class Player : MonoBehaviour, IDamageable
	{
		[SerializeField] float maxHealthPoints= 100f;
		[SerializeField] float currentHealthPoints= 100f;

		[SerializeField]  const int walkableLayerNumber = 8;
		[SerializeField]  const int enemyLayerNumber = 9;

		[SerializeField] float  minTimeBetweenHits = 0.5f;
		[SerializeField] float baseDamage = 5f; 

		[SerializeField] Weapon weaponInUse;
		[SerializeField] GameObject weaponSocket;

		[SerializeField] AnimatorOverrideController animatorOverrideController ;

		// TODO Temporary serialized for dubbing 
		[SerializeField] SpecialAbilityConfig[] abilities  ; 
		AudioSource audioSource ; 
		[SerializeField] AudioClip[] playerClips; // 0 = Aughh, 1= Grr, 2= Hmph
		[SerializeField] AudioClip[] deathClips;
		[SerializeField] AnimationClip[] animationClips; 
		[Range (0.1f , 1f)] [SerializeField] float criticalHitChance 	= 0.1f;
		[SerializeField] float criticalHitMultiplier = 1.25f;
		[SerializeField] ParticleSystem criticalHitParticle =null ; 



		Animator animator;
		float lastTimeHit= 0f; 
		bool playedHitSoundRecently = false; 
		bool isPlayerDead;
		bool hasDeathClipPlayed= false; 
		Enemy enemy; 
		CameraRaycaster cameraRaycaster;


		void Start()
		{
			hasDeathClipPlayed = false; 
			isPlayerDead = false;
			SetCurrentMaxHealth ();
			RegisterMouseClick ();
			EquipWeapon(); 
			OverrideAnimatorController ();
			abilities[0].AttachComponentTo (gameObject);
			audioSource = GetComponent<AudioSource> (); 
			AttachInitialAbilities ();

		}
		void Update()
		{
			if (!isPlayerDead) 
			{
				ScanForAbilityKeyDown ();
			}
		}

		public void Heal(float amount)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0, maxHealthPoints);
		}

		void AttachInitialAbilities()
		{
			for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++) 
			{
				abilities [abilityIndex ].AttachComponentTo (gameObject);
			}
		}

		void ScanForAbilityKeyDown ()
		{
			for (int keyIndex = 1; keyIndex < abilities.Length; keyIndex ++)
			{
				if (Input.GetKeyDown (keyIndex.ToString ())) 
				{
					AttemptSpecialAbility (keyIndex);
				}
			}
		}

		bool GetIsPlayerDead()
		{
			return isPlayerDead; 
		}
		void PlayDeathSound ()
		{
				audioSource.clip = deathClips[deathClips.Length - 1];
				audioSource.Play (); 
		}
		void OverrideAnimatorController()
		{
			animator = GetComponent<Animator> ();
			animator.runtimeAnimatorController = animatorOverrideController;
			animatorOverrideController ["DEFAULT ATTACK"] = weaponInUse.GetAttackAnimClip ();
		}


		void SetCurrentMaxHealth ()
		{
			currentHealthPoints = maxHealthPoints;
		}

		void RegisterMouseClick ()
			{
				cameraRaycaster = GameObject.FindObjectOfType<CameraRaycaster> ();
					cameraRaycaster.onMouseOverEnemy += OnEnemyClicked;
			}

		public float healthAsPercentage	{get { return currentHealthPoints / maxHealthPoints;}}
		public void TakeDamage (float damage)
		{
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);

			if (!playedHitSoundRecently) 
			{
				StartCoroutine (PlayHitSoundAfterSeconds (5f)); 
			}
			if (currentHealthPoints <= 0 && ! audioSource.isPlaying  && ! hasDeathClipPlayed ) 
			{
				StartCoroutine (KillPlayer());
			}
		}


		void OnEnemyClicked(Enemy enemyToSet)
			{
				this.enemy = enemyToSet; 
				if (Input.GetMouseButton (0) && IsTargetInRange (enemy.gameObject)) 
				{
					AttackEnemy ();
				}
				else if (Input.GetMouseButtonDown (1)) 
				{
					AttemptSpecialAbility (0);//TODO remove magic number
				} 
			}

		void AttemptSpecialAbility (int abilityIndex)
		{
			EnergyBar energyComponent = GameObject.FindObjectOfType<EnergyBar> ();
			float energyCost = abilities [abilityIndex].GetEnergyCost ();
			if (energyComponent.IsEnergyAvailable(energyCost)) // TODO read from scriptable object
			{
				energyComponent.ConsumeEnergy (energyCost);
				var abilityParams = new AbilityUseParams (enemy, baseDamage); 
				abilities [abilityIndex].Use (abilityParams); 

				//TODO use the ability
			}
		}



		void AttackEnemy ()
			{
				if (Time.time - lastTimeHit > minTimeBetweenHits) 
				{
					animator.SetTrigger ("Attack");
					enemy.TakeDamage (CalculateDamage());
					lastTimeHit = Time.time;
				}
			}
		float CalculateDamage ()
		{
			bool isCriticalHit = Random.Range (0, 1f) <= criticalHitChance;
			float damageBeforeCritical = baseDamage + weaponInUse.GetWeaponDamage ();

			if (isCriticalHit)
			{
				criticalHitParticle.Play ();
				return damageBeforeCritical * criticalHitMultiplier;
			} else
			{
				return damageBeforeCritical; 
			}
		}

		bool IsTargetInRange (GameObject target)
			{
				float distanceToTarget = (target.transform.position - transform.position).magnitude;
				return distanceToTarget <= weaponInUse.MaxAttackRange (); 
			}
			
		IEnumerator PlayHitSoundAfterSeconds( float seconds)
		{
			if (!audioSource.isPlaying && ! isPlayerDead)
			{
				playedHitSoundRecently = true;
				audioSource.clip = playerClips [Random.Range (0, playerClips.Length)];
				audioSource.Play (); 
				yield return new WaitForSecondsRealtime (seconds); 
				playedHitSoundRecently = false; 
			}

		}

		IEnumerator KillPlayer()
		{
			hasDeathClipPlayed = true; 
			isPlayerDead = true; 
			PlayDeathSound (); 
			animator.SetTrigger ("Death"); 
			SceneBoss sceneBoss = GameObject.FindObjectOfType<SceneBoss> ();
			yield return new WaitForSecondsRealtime (audioSource.clip.length); // TODO  use audioclip length
			sceneBoss.ReloadCurrentScene (); 
		}

		void EquipWeapon ()
		{
			if (weaponInUse != null) 
			{
				var weaponPrefab = weaponInUse.getWeaponPrefab ();
				GameObject dominantHand = RequestDominantHand ();
				var weapon = Instantiate (weaponPrefab, weaponSocket.transform);
				weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
				weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
			}
		}
		GameObject RequestDominantHand ()
		{
			var dominantHands = GetComponentsInChildren<DominantHand> ();
			int numberOfDominantHands = dominantHands.Length;
			Assert.IsFalse (numberOfDominantHands <= 0, "No Dominant Hand please add one"); 
			Assert.IsFalse (numberOfDominantHands > 1, "You can only have one Dominant hand, please remove extras"); 
			return dominantHands [0].gameObject;
		}
	}
}

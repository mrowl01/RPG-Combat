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



		Animator animator;
		float lastTimeHit= 0f; 
		bool playedHitSoundRecently = false; 
		bool isPlayerDead;
		bool hasDeathClipPlayed= false; 
		GameObject currentTarget; 
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

		}
		void Update()
		{
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


		void OnEnemyClicked(Enemy enemy)
				{
			if (Input.GetMouseButton (0) && IsTargetInRange (enemy.gameObject)) {
				AttackEnemy (enemy);
			} else if (Input.GetMouseButtonDown (1)) {
				AttemptSpecialAbility (0, enemy);//TODO remove magic number
			} 
				}

		void AttemptSpecialAbility (int abilityIndex, Enemy enemy )
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



		void AttackEnemy (Enemy enemy)
			{
				if (Time.time - lastTimeHit > minTimeBetweenHits) 
				{
					animator.SetTrigger ("Attack");
					enemy.TakeDamage (weaponInUse.GetWeaponDamage());
					lastTimeHit = Time.time;
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

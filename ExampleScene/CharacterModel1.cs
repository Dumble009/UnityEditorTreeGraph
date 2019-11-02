using UnityEngine;

public class CharacterModel1 : MonoBehaviour
{
	private void Awake()
	{
		ai = GetComponent<EnemyBehaviour>();
		senser = GetComponentInChildren<Senser>();
		if (senser != null)
		{
			senser.DetectCallback+=() => {
				ai.IsFound = true;
			};
		}
	}

	EnemyBehaviour ai;
	Senser senser;

	[SerializeField]
	Transform player;
	[SerializeField]
	float attackableDistance = 5.0f;
	float lastAttackedTime = float.NegativeInfinity;
	[SerializeField]
	float attackCoolTime = 0.5f;
	[SerializeField]
	BulletSpitter bulletSpitter;

	[SerializeField]
	float speed = 5.0f;
	[SerializeField]
	Transform[] patrolPoints;
	int patrolIndex = 0;
	float nextPatrolDistance = 1.0f;

	bool isDamaged = false;

	public void Attack()
	{
		if (bulletSpitter)
		{
			SpitBullet((player.position - transform.position).normalized);
			lastAttackedTime = Time.time;
		}
	}

	public void SpitBullet(Vector3 direction)
	{
		if (bulletSpitter)
		{
			bulletSpitter.Spit(origin: transform.position,
										direction: direction);
		}
	}

	public void Chase()
	{
		Move(goal : player.position);
	}

	public void Patrol()
	{
		if (patrolPoints != null)
		{
			Move(goal : patrolPoints[patrolIndex].position);
			if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) <= nextPatrolDistance)
			{
				patrolIndex++;
				if (patrolIndex >= patrolPoints.Length)
				{
					patrolIndex = 0;
				}
			}
		}
	}

	public void Escape()
	{
		Vector3 direction = player.position - transform.position;
		Move(goal: transform.position - direction);
	}

	virtual public void Move(Vector3 goal)
	{
		Vector3 direction = (goal - transform.position);
		direction.Scale(new Vector3(1, 0, 1));
		transform.position += direction.normalized * speed * Time.deltaTime;

		transform.LookAt(transform.position + direction);
	}

	public void CheckAttackable()
	{
		if (Vector3.Distance(transform.position, player.position) <= attackableDistance &&
			Time.time - lastAttackedTime >= attackCoolTime)
		{
			ai.IsAttackable = true;
		}
		else
		{
			ai.IsAttackable = false;
		}
	}

	public void CheckMoveable()
	{
		if (Time.time - lastAttackedTime >= attackCoolTime)
		{
			ai.IsMoveable = true;
		}
		else
		{
			ai.IsMoveable = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player_attack")
		{
			ai.IsGotDamage = true;
		}
	}
}

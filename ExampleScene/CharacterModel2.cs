using UnityEngine;
using UnityEngine.AI;

public class CharacterModel2 : MonoBehaviour
{
	private void Awake()
	{
		ai = GetComponent<EnemyBehaviour>();
		senser = GetComponentInChildren<Senser>();
		agent = GetComponent<NavMeshAgent>();
		if (senser != null)
		{
			senser.DetectCallback += () => {
				ai.IsFound = true;
			};
		}
	}

	EnemyBehaviour ai;
	Senser senser;

	NavMeshAgent agent;

	[SerializeField]
	Transform player;
	[SerializeField]
	float attackableDistance = 5.0f;
	float lastAttackedTime = float.NegativeInfinity;
	[SerializeField]
	Sword sword;

	[SerializeField]
	float speed = 5.0f;
	[SerializeField]
	Transform[] patrolPoints;
	int patrolIndex = 0;
	float nextPatrolDistance = 1.0f;

	public void Attack()
	{
		StopAgent();
		transform.LookAt(player.position);
		if (sword)
		{
			sword.Swing();
		}
	}

	public void Chase()
	{
		Move(goal: player.position);
	}

	public void Patrol()
	{
		if (patrolPoints != null)
		{
			Move(goal: patrolPoints[patrolIndex].position);
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

	virtual public void Move(Vector3 goal)
	{
		ActivateAgent();
		agent.destination = goal;
	}

	public void CheckAttackable()
	{
		if (Vector3.Distance(transform.position, player.position) <= attackableDistance &&
			!sword.IsSwinging)
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
		if (!sword.IsSwinging)
		{
			ai.IsMoveable = true;
		}
		else
		{
			ai.IsMoveable = false;
		}
	}

	private void StopAgent()
	{
		agent.speed = 0.0f;
		agent.angularSpeed = 0.0f;
		agent.updatePosition = false;
		agent.updateRotation = false;
	}

	private void ActivateAgent()
	{
		agent.speed = 1.75f;
		agent.angularSpeed = 120.0f;
		agent.updatePosition = true;
		agent.updateRotation = true;
	}
}

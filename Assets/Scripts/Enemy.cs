using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
  Waiting,
  Chasing,
  RunningBack,
  Attaking
}

public class Enemy : MonoBehaviour
{
  public EnemyState currentState;
  public EnemyData data;
  protected Animator animator;
  protected NavMeshAgent navMeshAgent;
  protected Transform target;
  protected Transform spawnPoint;

  public void SetTaget(Transform target)
  {
    this.target = target;
    SetState(EnemyState.Chasing);
  }

  public void SetSpawnPoint(Transform spawnPoint)
  {
    this.spawnPoint = spawnPoint;
  }

  public void SetState(EnemyState newState)
  {
    currentState = newState;

    switch (currentState)
    {
      case EnemyState.Chasing:
        navMeshAgent.SetDestination(target.position);
        break;
      case EnemyState.RunningBack:
        transform.LookAt(spawnPoint);
        navMeshAgent.SetDestination(spawnPoint.position);
        EnemyManager.instance.Remove(gameObject);
        if (!animator.GetBool("HasGift")) UIManager.instance.RemoveKid();
        animator.SetBool("HasGift", true);
        animator.SetBool("Idle", false);
        break;
      case EnemyState.Attaking:
        animator.SetBool("Idle", true);
        StartCoroutine(ApplyDamage());
        break;
    }
  }

  protected IEnumerator ApplyDamage()
  {
    while (currentState == EnemyState.Attaking)
    {
      animator.SetTrigger("Attack");
      target.GetComponent<BagController>().ApplyDamage(data.attackDamage);
      yield return new WaitForSeconds(data.attackInterval);
    }
  }

  private void Awake()
  {
    animator = GetComponent<Animator>();
    navMeshAgent = GetComponent<NavMeshAgent>();
  }

  private void OnTriggerEnter(Collider other)
  {
    SetState(EnemyState.RunningBack);
    Destroy(other.gameObject);
  }

  private void FixedUpdate()
  {
    if (IsOnAttackRange())
    {
      SetState(EnemyState.Attaking);
    }

    if (IsBackToSpawn())
    {
      Destroy(gameObject);
    }
  }

  private void OnEnable()
  {
    EnemyManager.instance.Add(gameObject);
  }
  private void OnDisable()
  {
    EnemyManager.instance.Remove(gameObject);
  }

  protected bool IsOnAttackRange()
  {
    if (currentState != EnemyState.Chasing) return false;
    float distance = Vector3.Distance(transform.position, target.position);
    if (distance > 1.5f) return false;
    return true;
  }

  public bool IsBackToSpawn()
  {
    if (currentState != EnemyState.RunningBack) return false;
    float distance = Vector3.Distance(transform.position, spawnPoint.position);
    if (distance > 1.5f) return false;
    return true;
  }

}

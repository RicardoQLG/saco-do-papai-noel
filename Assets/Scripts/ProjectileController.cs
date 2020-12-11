using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
  public float speed = 1f;
  private void Awake()
  {
    StartCoroutine(DestroyAfterInterval());
  }
  private void OnEnable()
  {
    ProjectileManager.instance.AddProjectile(gameObject);
  }

  private void OnDisable()
  {
    ProjectileManager.instance.RemoveProjectile(gameObject);
  }

  IEnumerator DestroyAfterInterval()
  {
    yield return new WaitForSeconds(5f);
    Destroy(gameObject);
  }

  private void FixedUpdate()
  {
    transform.position += transform.forward * speed * Time.deltaTime;
  }
}

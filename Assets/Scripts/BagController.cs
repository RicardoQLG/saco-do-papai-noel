using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public class HealthEvent : UnityEvent<float> { }

public class BagController : MonoBehaviour
{
  public List<GameObject> gifts;
  public float turnSpeed;
  public float turnDirection;
  public float health = 100;
  public Animator animator;
  public HealthEvent onTakeDamage;
  public UnityEvent onDie;
  public AudioSource audioSource;

  private void Start()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();

    if (onTakeDamage == null) onTakeDamage = new HealthEvent();
    onTakeDamage.AddListener(UIManager.instance.ChangeHealth);

    if (onDie == null) onDie = new UnityEvent();
    onDie.AddListener(UIManager.instance.Die);
  }

  private void FixedUpdate()
  {
    Vector3 turnAngle = transform.rotation.eulerAngles + Vector3.up * turnSpeed * turnDirection;
    Vector3 clampedAngle = RotationClamp(turnAngle);
    transform.rotation = Quaternion.Euler(clampedAngle);
  }

  private Vector3 RotationClamp(Vector3 turnAngle)
  {
    if (turnAngle.y > 90f && turnAngle.y < 180f)
      turnAngle.y = 90f;
    if (turnAngle.y < 270f && turnAngle.y > 180f)
      turnAngle.y = 270f;

    return turnAngle;
  }

  public void Turn(InputAction.CallbackContext context)
  {
    turnDirection = context.ReadValue<float>();
  }

  public void Shoot(InputAction.CallbackContext context)
  {
    if (context.ReadValue<float>() == 0) return;
    animator.SetTrigger("Shoot");
  }

  public void CreateProjectile()
  {
    audioSource.Play();
    ProjectileManager.instance.InstantiateProjectile(gifts[0], transform.position, transform.rotation, 3f);
  }

  public void ApplyDamage(float damage)
  {
    health -= damage;
    onTakeDamage.Invoke(health);
    if (health <= 0) Die();
  }

  public void Die()
  {
    onDie.Invoke();
  }
}

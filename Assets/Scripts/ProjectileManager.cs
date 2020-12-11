using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
  public static ProjectileManager instance { get; private set; }
  public int maxNumberOfProjectiles;
  public float shootInterval;
  public float nextShoot;
  public float time;
  [SerializeField] protected List<GameObject> projectiles = new List<GameObject>();

  private void Awake()
  {
    instance = this;
  }

  public void AddProjectile(GameObject projectile)
  {
    projectiles.Add(projectile);
  }

  public void RemoveProjectile(GameObject projectile)
  {
    projectiles.Remove(projectile);
  }

  public void InstantiateProjectile(GameObject projectile, Vector3 position, Quaternion direction, float force)
  {
    if (CanShoot())
    {
      GameObject go = Instantiate(projectile, position, direction);
      nextShoot = Time.time + shootInterval;
    }
  }

  protected bool CanShoot()
  {
    if (nextShoot > Time.time) return false;
    if (maxNumberOfProjectiles <= projectiles.Count) return false;
    return true;
  }
}

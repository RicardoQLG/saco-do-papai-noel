using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Saco/Enemy", fileName = "New Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
  public float attackDamage;
  public float attackInterval;
}

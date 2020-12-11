using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
  public static UIManager instance { get; private set; }
  public Slider healthBar;
  public Slider waveBar;
  public GameObject dieScreen;

  private void Awake()
  {
    instance = this;
  }

  public void ChangeHealth(float health)
  {
    healthBar.value = health;
  }

  public void Die()
  {
    Time.timeScale = 0;
    dieScreen.SetActive(true);
  }

  public void SetWaveSize(int length)
  {
    waveBar.maxValue = length;
    waveBar.value = length;
  }

  public void RemoveKid()
  {
    waveBar.value -= 1;
  }
}

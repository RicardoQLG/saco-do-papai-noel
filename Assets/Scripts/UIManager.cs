using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
  public static UIManager instance { get; private set; }
  public Slider healthBar;
  public Slider waveBar;
  public TextMeshProUGUI waveCounter;
  public GameObject dieScreen;

  private void Awake()
  {
    instance = this;
  }

  public void ChangeHealth(float health)
  {
    healthBar.value = health;
  }

  public void SetWaveInfo(int total, int current)
  {
    waveCounter.text = current.ToString() + "/" + total.ToString();
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

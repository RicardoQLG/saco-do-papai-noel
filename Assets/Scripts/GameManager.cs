using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public AudioClip clickAudio;
  public GameObject soundManager;

  private void Awake()
  {
    if (SoundManager.instance == null)
    {
      Instantiate(soundManager);
    }
  }

  public void Restart()
  {
    Time.timeScale = 1f;
    SoundManager.instance.PlaySound(clickAudio);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}

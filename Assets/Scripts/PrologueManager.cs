using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
  private void NextScene()
  {
    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
  }
}

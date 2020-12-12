using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenManager : MonoBehaviour
{
  void Start()
  {
    StartCoroutine(GoBack());

  }

  IEnumerator GoBack()
  {
    yield return new WaitForSeconds(10f);
    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
  }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  public GameObject fadeLayer;

  public void StartGame()
  {
    StartCoroutine(NextScene());
  }

  public IEnumerator NextScene()
  {
    yield return new WaitForSeconds(.3f);
    SceneManager.LoadScene("Prologo", LoadSceneMode.Single);
  }

}

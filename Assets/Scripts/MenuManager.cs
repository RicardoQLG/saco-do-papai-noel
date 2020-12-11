using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
  public GameObject fadeLayer;

  public void StartGame()
  {
    SceneManager.LoadScene("Prologo", LoadSceneMode.Single);
  }

}

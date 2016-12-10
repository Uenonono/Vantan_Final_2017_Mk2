using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

  public void ChangeScene(int nextSceneIndex) {
    SceneManager.LoadScene(nextSceneIndex);
  }

}
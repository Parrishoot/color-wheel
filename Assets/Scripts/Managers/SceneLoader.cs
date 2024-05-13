using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    private AsyncOperation asyncOperation;

    private bool sceneLoaded = false;

    private bool canTransition = false;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(LoadGameScene());
    }

    public void TransitionToGameScene() {
        canTransition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneLoaded && canTransition) {
            asyncOperation.allowSceneActivation = true;
        }
    }

    
    private IEnumerator LoadGameScene() {
        asyncOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        while(asyncOperation.progress < .9f){
            yield return null;
        }
        sceneLoaded = true;
    }
}

using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionOutController : UIButtonCallbackController
{
    [SerializeField]
    private GameObject titleGameObject;

    [SerializeField]
    private GameObject buttonsContainerGameObject;

    [SerializeField]
    private float translationAmount = 1000f;

    [SerializeField]
    private float transitionDuration = 1f;

    [SerializeField]
    private float transitionElasicity = 1f;
    
    private bool transitionDone = false;

    private bool sceneLoaded = false;

    private AsyncOperation asyncOperation;

    protected override void Start() {
        base.Start();
        StartCoroutine(LoadGameScene());
    }

    protected override void OnButtonClick()
    {
         DOTween.Sequence()
            .Append(titleGameObject.transform.DOMoveY(translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Join(buttonsContainerGameObject.transform.DOMoveY(-translationAmount, transitionDuration).SetEase(Ease.InOutBack, overshoot: transitionElasicity))
            .Play()
            .OnComplete(() => transitionDone = true);
    }

    void Update() {
        if(sceneLoaded && transitionDone) {
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

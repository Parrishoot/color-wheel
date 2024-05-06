using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonController : MonoBehaviour
{
    [SerializeField]
    private UIDelegateButtonController playButton;

    [SerializeField]
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        TitleScreenTransitionController titleScreenTransitionController = TitleScreenTransitionController.Instance;

        playButton.OnButtonClick += () => {
            titleScreenTransitionController.TransitionComplete += sceneLoader.TransitionToGameScene;
            titleScreenTransitionController.Hide();
        };
    }
}

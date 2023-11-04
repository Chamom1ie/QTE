using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LobbyUI : MonoBehaviour
{
    private UIDocument _UIDocument;
    [SerializeField] private string mainScene;

    private void OnEnable()
    {
        _UIDocument = GetComponent<UIDocument>();
        VisualElement root = _UIDocument.rootVisualElement;

        VisualElement startBtn = root.Q<VisualElement>("start-btn");
        VisualElement settingBtn = root.Q<VisualElement>("setting-btn");
        VisualElement quitBtn = root.Q<VisualElement>("quit-btn");

        startBtn.RegisterCallback<ClickEvent>(e =>
        {
            print("click startBtn");
            if (mainScene != "") SceneManager.LoadScene(mainScene);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        settingBtn.RegisterCallback<ClickEvent>(e =>
        {
            print("click settingBtn");
            //SettingPanel Open
        });
        
        quitBtn.RegisterCallback<ClickEvent>(e =>
        {
            print("click quitBtn");
            Application.Quit();
        });

    }

}

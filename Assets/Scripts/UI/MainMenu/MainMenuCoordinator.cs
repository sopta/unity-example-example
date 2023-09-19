using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI
{
    public class MainMenuCoordinator : BaseScreen
    {
        [Header("UI Management")]
        [Tooltip("Layout to work with")]
        [SerializeField] private UIDocument mainMenuDocument;
        
        [Header("Screens")]
        [SerializeField] private BaseScreen playersScreen;
        [SerializeField] private BaseScreen gamesScreen;
        [SerializeField] private BaseScreen settingsScreen;
        [SerializeField] private BaseScreen creditsScreen;
        
        private BaseScreen currentActiveScreen;
        
        private void Awake()
        {
            // init coordinator
            Init(mainMenuDocument.rootVisualElement.Q<VisualElement>(UIDocumentIdentification));
            
            // init all screens
            if (playersScreen != null) playersScreen.Init(mainMenuDocument.rootVisualElement.Q<VisualElement>(playersScreen.UIDocumentIdentification));
            if (gamesScreen != null) gamesScreen.Init(mainMenuDocument.rootVisualElement.Q<VisualElement>(gamesScreen.UIDocumentIdentification));
            if (settingsScreen != null) settingsScreen.Init(mainMenuDocument.rootVisualElement.Q<VisualElement>(settingsScreen.UIDocumentIdentification));
            if (creditsScreen != null) creditsScreen.Init(mainMenuDocument.rootVisualElement.Q<VisualElement>(creditsScreen.UIDocumentIdentification));
            
            ChangeScreen(playersScreen);
        }
        
        public override void Init(VisualElement root)
        {
            base.Init(root);

            // rootVisualElement.Q<Button>("menu__button-players").Focus();
            
            rootVisualElement.Q<Button>("menu__button-players")?.RegisterCallback<ClickEvent>(ShowScreen);
            rootVisualElement.Q<Button>("menu__button-games")?.RegisterCallback<ClickEvent>(ShowScreen);
            rootVisualElement.Q<Button>("menu__button-settings")?.RegisterCallback<ClickEvent>(ShowScreen);
            rootVisualElement.Q<Button>("menu__button-credits")?.RegisterCallback<ClickEvent>(ShowScreen);
            rootVisualElement.Q<Button>("menu__button-exit")?.RegisterCallback<ClickEvent>(ExitClickCallback);
        }

        // private void Update()
        // {
        //     Debug.Log(rootVisualElement.Q<Button>("menu__button-players").focusController.focusedElement);
        // }

        public void DisableUI()
        {
            mainMenuDocument.enabled = false;
        }
        
        private void ShowScreen(ClickEvent evt)
        {
            VisualElement clickedElement = evt.target as VisualElement;
            if (clickedElement == null) return;
            
            switch (clickedElement.viewDataKey)
            {
                case "players":
                    ChangeScreen(playersScreen);
                    break;
                case "games":
                    ChangeScreen(gamesScreen);
                    break;
                case "settings":
                    ChangeScreen(settingsScreen);
                    break;
                case "credits":
                    ChangeScreen(creditsScreen);
                    break;
            }
        }

        private void ExitClickCallback(ClickEvent evt)
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
        
        private void ChangeScreen(BaseScreen screen)
        {
            currentActiveScreen?.Deactivate();
            screen.Activate();
            currentActiveScreen = screen;
        }
    }
}
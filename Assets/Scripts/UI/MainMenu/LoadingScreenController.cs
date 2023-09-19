using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using Cursor = UnityEngine.Cursor;

namespace Example.UI
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private MainMenuCoordinator mainMenuCoordinator;
        [Header("UI Management")]
        [Tooltip("Layout to work with")]
        [SerializeField] private UIDocument uiDocument;

        private VisualElement backgroundGame;
        private Label gameTitle;
        // private ProgressBar progressBar;
        
        private void Awake()
        {
            backgroundGame = uiDocument.rootVisualElement.Q<VisualElement>("background-game");
            gameTitle = uiDocument.rootVisualElement.Q<Label>("game-title");
            // progressBar = uiDocument.rootVisualElement.Q<ProgressBar>();
        }

        public void LoadGameEntryScene(GameInfoSO gameInfo)
        {
            mainMenuCoordinator.DisableUI();
            Cursor.visible = false; // todo pres RootManager?
            Cursor.lockState = CursorLockMode.Locked;
            
            // todo disable input
            
            backgroundGame.style.backgroundImage = new StyleBackground(gameInfo.Image);
            gameTitle.text = gameInfo.Title;
            
            backgroundGame?.experimental.animation.Start(new StyleValues {opacity = 0},new StyleValues {opacity = 1}, 2500);
            backgroundGame?.experimental.animation.Scale(1, 500);

            StartCoroutine(StartLoading(gameInfo));
        }

        private IEnumerator StartLoading(GameInfoSO gameInfo)
        {
            yield return new WaitForSeconds(2.5f);
            // RootGameManager.Instance.SceneManager.LoadGameEntryScene(gameInfo);
        }

        // private void Update()
        // {
        //     progressBar.value = Mathf.MoveTowards(progressBar.value, 100, 3 * Time.deltaTime);
        // }
    }
}
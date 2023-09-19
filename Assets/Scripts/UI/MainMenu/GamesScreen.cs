using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI
{
    public class GamesScreen : BaseScreen
    {
        [SerializeField] private GameDatabaseSO gameDatabase;
        [SerializeField] private VisualTreeAsset gameInfoBoxAsset;
        [SerializeField] private LoadingScreenController loadingScreenController;
        
        private VisualElement scrollViewParent;

        public override void Init(VisualElement root)
        {
            base.Init(root);
            scrollViewParent = rootVisualElement.Q<VisualElement>("ScrollView__list");
            
            // delete placeholders
            scrollViewParent.Clear();

            for (int i = 0; i < gameDatabase.Games.Length; i++)
            {
                int index = i;
                TemplateContainer template = gameInfoBoxAsset.Instantiate();

                template.Q<Label>("GameInfoBox__title").text = gameDatabase.Games[i].Title;
                template.Q<VisualElement>("GameInfoBox__game-image").style.backgroundImage = new StyleBackground(gameDatabase.Games[i].Image);
                template.Q<VisualElement>("GameInfoBox__play-button")?.RegisterCallback<ClickEvent>(_ => Click(index));
                
                // todo @example UI toolkit example
                // GearItemComponent gearItem = new GearItemComponent(gearData);
                
                // set visual element for gearItemComponent
                // gearItem.SetVisualElements(gearUIElement);
                // gearItem.SetGameData(gearUIElement);
                // gearItem.RegisterButtonCallbacks();
                
                scrollViewParent.Add(template);
            }
        }

        private void Click(int i)
        {
            loadingScreenController.LoadGameEntryScene(gameDatabase.Games[i]);
        }
    }
}
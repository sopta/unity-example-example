using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI
{
    public class PlayersScreen : BaseScreen
    {
        [SerializeField] private CharacterDatabaseSO characterDatabase;
        [SerializeField] private VisualTreeAsset playerAvatarBox;
        [SerializeField] private Placeholder[] placeholders;

        private int nextAvatarSeat;
        
        private VisualElement parent;
        
        public override void Init(VisualElement root)
        {
            base.Init(root);
            
            parent = rootVisualElement.Q<VisualElement>("players-container");
            
            // delete placeholders
            parent.Clear();
            
            // RootGameManager.Instance.OnPlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerJoined(JoinedPlayer joinedPlayer)
        {
            joinedPlayer.InputProvider.EnableMainMenuControls();
            
            var placeholder = placeholders[nextAvatarSeat];
            
            // set default visual
            joinedPlayer.CharacterVisual = characterDatabase.CharacterVisuals[0];
            
            characterDatabase.SpawnVisuals(placeholder);
            
            // create a concrete item
            TemplateContainer template = playerAvatarBox.Instantiate();
            
            AvatarItemComponent avatarItemComponent = new AvatarItemComponent(template, placeholder, joinedPlayer, characterDatabase);
            avatarItemComponent.SetVisualElements();
            
            parent.Add(template);

            // get ready for a new player to connect
            nextAvatarSeat++;
        }

        private void OnDestroy()
        {
           // RootGameManager.Instance.OnPlayerJoined -= OnPlayerJoined;
        }
    }
    
    // todo @refactoring move
    [Serializable]
    public struct Placeholder
    {
        public Transform Root;
        public RenderTexture RenderTexture;
        public List<CharacterVisual> CharacterVisuals;
    }

    // todo @refactoring move
    public class AvatarItemComponent
    {
        private TemplateContainer visualElement;
        private Placeholder placeholder;
        private JoinedPlayer joinedPlayer;
        private CharacterDatabaseSO characterDatabase;

        private int currentVisualIndex;

        public AvatarItemComponent(TemplateContainer ve, Placeholder p, JoinedPlayer jp, CharacterDatabaseSO characterDb) =>
            (visualElement, placeholder, joinedPlayer, characterDatabase) = (ve, p, jp, characterDb);

        public void SetVisualElements()
        {
            visualElement.Q<VisualElement>("PlayerAvatarBox__avatar").style.backgroundImage = new StyleBackground(Background.FromRenderTexture(placeholder.RenderTexture));
            visualElement.Q<VisualElement>("PlayerAvatarBox__prev-arrow")?.RegisterCallback<ClickEvent>(Callback);
            visualElement.Q<VisualElement>("PlayerAvatarBox__next-arrow")?.RegisterCallback<ClickEvent>(Callback);

            placeholder.CharacterVisuals[currentVisualIndex].gameObject.SetActive(true);
        }

        private void Callback(ClickEvent evt)
        {
            VisualElement clickedElement = evt.target as VisualElement;
            if (clickedElement == null) return;
                
            placeholder.CharacterVisuals[currentVisualIndex].gameObject.SetActive(false);

            if (clickedElement.viewDataKey == "prev")
            {
                currentVisualIndex--;
            }
            else
            {
                currentVisualIndex++;
            }

            currentVisualIndex = CheckRange();
            placeholder.CharacterVisuals[currentVisualIndex].gameObject.SetActive(true);

            joinedPlayer.CharacterVisual = characterDatabase.CharacterVisuals[currentVisualIndex];
        }

        private int CheckRange()
        {
            if (currentVisualIndex < 0) return placeholder.CharacterVisuals.Count - 1;
            if (currentVisualIndex == placeholder.CharacterVisuals.Count) currentVisualIndex = 0;
            return currentVisualIndex;
        }
    }
}
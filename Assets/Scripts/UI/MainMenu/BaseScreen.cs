using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [Header("BaseScreen attributes")]
        [SerializeField] protected string uiDocumentIdentification;

        protected VisualElement rootVisualElement;

        // public event Action ScreenActivated;
        // public event Action ScreenDeactivated;

        public string UIDocumentIdentification => uiDocumentIdentification;

        public virtual void Init(VisualElement root)
        {
            rootVisualElement = root;
        }

        public virtual void Activate()
        {
            rootVisualElement.style.display = DisplayStyle.Flex;
            // ScreenActivated?.Invoke();
        }

        public virtual void Deactivate()
        {
            rootVisualElement.style.display = DisplayStyle.None;
            // ScreenDeactivated?.Invoke();
        }
                
        private bool IsVisible()
        {
            return rootVisualElement.style.display == DisplayStyle.Flex;
        }
    }
}
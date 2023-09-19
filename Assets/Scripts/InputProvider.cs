using UnityEngine;
using UnityEngine.InputSystem;

namespace Example
{
    public class InputProvider : MonoBehaviour
    {
        [Tooltip("PlayerInput component for specific player´s controller")]
        [SerializeField] private PlayerInput playerInput;

        // todo @example tady je vygenerovana classa z input systemu
        // public InputActions_Player_Edited InputActions { get; private set; }
        
        public void Init()
        {
            // InputActions = new InputActions_Player_Edited(playerInput);
        }
        
        public void EnableMainMenuControls()
        {
            playerInput.SwitchCurrentActionMap("Menu Controls Primary");
            
            // InputActions.PlayerControls.Disable();
            // InputActions.MenuControlsSecondary.Disable();
            // InputActions.MenuControlsPrimary.Enable(); 
        }
        
        public void EnableGameplayControls()
        {
            playerInput.SwitchCurrentActionMap("Player Controls");
            
            // InputActions.PlayerControls.Enable();
            // InputActions.MenuControlsSecondary.Disable();
            // InputActions.MenuControlsPrimary.Disable(); 
        }
    }
}
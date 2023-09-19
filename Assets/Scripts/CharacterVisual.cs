using UnityEngine;

namespace Example
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private Avatar avatar;
        
        public void RebindAnimator(Animator animator)
        {
            animator.avatar = avatar;
            animator.Rebind();
        }
    }
}
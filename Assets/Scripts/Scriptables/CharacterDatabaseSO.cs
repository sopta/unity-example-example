using Example.UI;
using UnityEngine;

namespace Example
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Character Database", order = 1)]
    public class CharacterDatabaseSO : ScriptableObject
    {
        public CharacterVisual[] CharacterVisuals;

        public void SpawnVisuals(Placeholder placeholder)
        {
            for (int i = 0; i < CharacterVisuals.Length; i++)
            {
                var visual = Instantiate(CharacterVisuals[i], placeholder.Root);
                visual.gameObject.SetActive(false);
                
                visual.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                
                placeholder.CharacterVisuals.Add(visual);
            }
        }
    }
}
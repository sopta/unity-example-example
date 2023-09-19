using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Example
{
    [CreateAssetMenu(fileName = "000_game", menuName = "Game Info", order = 1)]
    public class GameInfoSO : ScriptableObject
    {
        public string Title;
        public Sprite Image;
        public AssetReferenceT<SceneAsset> EntryScene;
    }
}
using UnityEngine;

namespace Example
{
    [CreateAssetMenu(fileName = "GameDatabase", menuName = "Game Database", order = 0)]
    public class GameDatabaseSO : ScriptableObject
    {
        public GameInfoSO[] Games;
    }
}
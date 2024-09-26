using UnityEngine;

namespace World
{
    public class GameCharacter : MonoBehaviour
    {
        public GameCharacterType type;
    }

    public enum GameCharacterType
    {
        Neutral,
        Player,
        Enemy
    }
}
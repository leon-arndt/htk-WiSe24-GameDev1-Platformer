using World;

namespace Events
{
    public record CharacterHealthChanged(GameCharacterType type, float maxLife, float newLife, float oldLife, Damageable Damageable);
}

namespace System.Runtime.CompilerServices
{
    class IsExternalInit
    {
        // needed to make records work in older versions of Unity
    }
}
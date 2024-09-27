using World;

namespace Events
{
    public record DamageableArmorChanged(GameCharacterType type, float maxArmor, float newArmor, float oldArmor);
}
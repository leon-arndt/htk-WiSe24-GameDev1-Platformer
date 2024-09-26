using System.Collections.Generic;
using Events;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace World
{
    public interface IDamageable
    {
        public void Damage(float amount, Vector3? position = null);
        GameCharacterType GetCharacterType();
        void Heal(float amount);
        void HealArmor(float amount);
    }

    public class Damageable : MonoBehaviour, IDamageable
    {
        public GameCharacterType type;
        [SerializeField] private float armor, maxArmor, armorRegenPerSecond;
        [SerializeField] private float life, maxLife;
        [SerializeField] private float lifeRegenPerSecond;
        [SerializeField] private UnityEvent onDie, onLowHealth, onHeal;
        [SerializeField] private List<GameObject> spawnOnDamages, spawnOnDies;

        public float Life => life;
        public float MaxLife => maxLife;

        private void Start()
        {
            MessageBroker.Default.Publish(new CharacterHealthChanged(type, maxLife, life, life, this));
            MessageBroker.Default.Publish(new DamageableArmorChanged(type, maxArmor, armor, armor));
        }

        private void Update()
        {
            if (lifeRegenPerSecond > 0 && life < maxLife)
            {
                Heal(lifeRegenPerSecond * Time.deltaTime);
            }
            
            if (armorRegenPerSecond > 0 && armor < maxArmor)
            {
                HealArmor(armorRegenPerSecond * Time.deltaTime);
            }
        }

        public void Damage(float amount, Vector3? position = null)
        {
            if (life < 0)
            {
                return;
            }
            
            if (armor > 0)
            {
                var oldArmor = armor;
                armor -= amount;
                armor = Mathf.Max(0f, armor);
                MessageBroker.Default.Publish(new DamageableArmorChanged(type, maxArmor, armor, oldArmor));
                return;
            }


            var oldLife = life;
            life -= amount;
            MessageBroker.Default.Publish(new CharacterHealthChanged(type, maxLife, life, oldLife, this));

            if (spawnOnDamages != null && position.HasValue)
            {
                foreach (var spawnOnDamage in spawnOnDamages)
                {
                    Instantiate(spawnOnDamage, position.Value, Quaternion.identity);
                }
            }
            
            if (life < maxLife * 0.4f)
            {
                onLowHealth?.Invoke();
            }

            if (life < 0)
            {
                OnDie();
            }
        }

        public void Heal(float amount)
        {
            // no zombies allowed
            if (life <= 0)
            {
                return;
            }

            var oldLife = life;
            life += amount;
            MessageBroker.Default.Publish(new CharacterHealthChanged(type, maxLife, life, oldLife, this));
            
            onHeal?.Invoke();
        }
        
        public void HealArmor(float amount)
        {
            // no zombies allowed
            if (life <= 0)
            {
                return;
            }

            var oldAmor = armor;
            armor += amount;
            MessageBroker.Default.Publish(new DamageableArmorChanged(type, maxLife, life, oldAmor));
        }

        public void AddMaxHealth(float amount, bool shouldHeal)
        {
            maxLife += amount;
            if (shouldHeal)
            {
                Heal(amount);
            }
            MessageBroker.Default.Publish(new CharacterHealthChanged(type, maxLife, life, life, this));
        }

        public void AddHealthRegen(float amount)
        {
            lifeRegenPerSecond += amount;
            MessageBroker.Default.Publish(new CharacterHealthChanged(type, maxLife, life, life, this));
        }


        private void OnDie()
        {
            foreach (var spawnOnDamage in spawnOnDies)
            {
                Instantiate(spawnOnDamage, transform.position, Quaternion.identity);
            }
            
            onDie?.Invoke();
        }

        public GameCharacterType GetCharacterType()
        {
            return type;
        }
    }
}
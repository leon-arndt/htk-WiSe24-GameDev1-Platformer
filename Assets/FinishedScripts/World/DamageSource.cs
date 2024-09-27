using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private List<GameCharacterType> affectedCharacterTypes;
        [SerializeField] private float instantAmount = 1f;
        [SerializeField] private float amountPerSecond = 1f;
        [SerializeField] private bool destroySelfOnHit = true;

        public void SetAffectedTypes(List<GameCharacterType> types)
        {
            affectedCharacterTypes = types;
        }

        public void SetInstantDamageAmount(float newAmount)
        {
            instantAmount = newAmount;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                if (affectedCharacterTypes.Contains(damageable.GetCharacterType()))
                {
                    damageable.Damage(amountPerSecond * Time.fixedDeltaTime, transform.position);

                    if (destroySelfOnHit)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                if (affectedCharacterTypes.Contains(damageable.GetCharacterType()))
                {
                    damageable.Damage(instantAmount, transform.position);
                }
            }

            if (destroySelfOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
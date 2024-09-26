using Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider playerHealth;

    private void Start()
    {
        Set(1, 1);

        MessageBroker.Default.Receive<CharacterHealthChanged>().TakeUntilDestroy(gameObject)
            .Subscribe(info => Set(info.newLife, info.maxLife));
    }

    public void Set(float current, float total)
    {
        playerHealth.value = current / total;
    }
}

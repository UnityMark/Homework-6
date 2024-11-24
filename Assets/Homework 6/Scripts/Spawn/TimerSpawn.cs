using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSpawn : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    private Cooldown _cooldown;

    public void Initialize(Cooldown cooldown)
    {
        _cooldown = cooldown;
    }

    private void Update()
    {
        _label.text = $"{Mathf.RoundToInt(_cooldown.GetTime())}s";
    }
}

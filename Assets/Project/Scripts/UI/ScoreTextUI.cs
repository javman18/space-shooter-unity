using TMPro;
using UnityEngine;
using SpaceShooter.Systems;

namespace SpaceShooter.UI
{
    public sealed class ScoreTextUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private FloatingScoreService score;

        private void Awake()
        {
            if (label == null) label = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            if (score == null) return;
            score.Changed += OnChanged;
            OnChanged(score.Score);
        }

        private void OnDisable()
        {
            if (score != null) score.Changed -= OnChanged;
        }

        private void OnChanged(int value)
        {
            label.text = "SCORE: " + value.ToString();
        }
    }
}

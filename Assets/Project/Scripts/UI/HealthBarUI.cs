using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.UI
{
    public sealed class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Image pillPrefab;

        [SerializeField] private int maxPills = 10;

        [SerializeField] private Color onColor = Color.white;
        [SerializeField] private Color offColor = new Color(1f, 1f, 1f, 0.25f);

        private Image[] _pills;
        private void Awake()
        {
            Build();
            
        }

        public void Build()
        {
            if (container == null || pillPrefab == null) return;

            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);

            _pills = new Image[maxPills];

            for (int i = 0; i < maxPills; i++)
            {
                var pip = Instantiate(pillPrefab, container);
                pip.gameObject.name = $"HP_{i + 1}";
                pip.color = onColor;
                _pills[i] = pip;
            }
        }

        public void SetHealth(int current)
        {
            if (_pills == null) return;

            int clamped = Mathf.Clamp(current, 0, _pills.Length);

            for (int i = 0; i < _pills.Length; i++)
                _pills[i].color = i < clamped ? onColor : offColor;
        }

        public void SetMax(int newMax, int current)
        {
            maxPills = Mathf.Max(1, newMax);
            Build();
            SetHealth(current);
        }

    }
}


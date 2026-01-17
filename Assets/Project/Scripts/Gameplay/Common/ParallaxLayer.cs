using UnityEngine;
using SpaceShooter.Utils.Interfaces;

namespace SpaceShooter.Gameplay.Common
{
    public sealed class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] private Transform cam;
        [SerializeField] private Transform tileA;
        [SerializeField] private Transform tileB;

        [SerializeField] private float scrollSpeed = 1.5f;
        [SerializeField] private float reactionStrength = 0.15f;
        [SerializeField] private float reactionSmoothing = 12f;
        [SerializeField] private float wrapPadding = 0.5f;

        private float _tileHeight;
        private float _baseX;
        private float _reactionX;
        private IInputProvider _input;

        public void SetInput(IInputProvider input) => _input = input;

        private void Awake()
        {
            _baseX = transform.position.x;

            if (tileA == null || tileB == null)
            {
                enabled = false;
                return;
            }

            var sr = tileA.GetComponentInChildren<SpriteRenderer>();
            if (sr == null)
            {
                enabled = false;
                return;
            }

            _tileHeight = sr.bounds.size.y;
        }

        [ContextMenu("Align Tiles")]
        public void AlignTiles()
        {
            if (tileA == null || tileB == null) return;

            var srA = tileA.GetComponentInChildren<SpriteRenderer>();
            var srB = tileB.GetComponentInChildren<SpriteRenderer>();
            if (srA == null || srB == null) return;

            float heightA = srA.bounds.size.y;

            float x = tileA.position.x;
            float y = tileA.position.y + heightA;

            tileB.position = new Vector3(x, y, tileB.position.z);
        }

        private void Update()
        {
            if (cam == null) return;

            Vector2 move = _input != null ? _input.Move : Vector2.zero;

            float targetX = move.x * reactionStrength;
            _reactionX = Mathf.Lerp(_reactionX, targetX, 1f - Mathf.Exp(-reactionSmoothing * Time.deltaTime));

            float dx = _baseX + _reactionX - tileA.position.x;
            Vector3 drift = new Vector3(dx, 0f, 0f);

            float dy = scrollSpeed * Time.deltaTime;
            Vector3 step = Vector3.down * dy;

            tileA.position += step + drift;
            tileB.position += step + drift;

            WrapIfNeeded();
        }

        private void WrapIfNeeded()
        {
            var camComp = cam.GetComponent<Camera>();
            if (camComp == null) return;

            float camBottom = cam.position.y - camComp.orthographicSize;

            var srA = tileA.GetComponentInChildren<SpriteRenderer>();
            var srB = tileB.GetComponentInChildren<SpriteRenderer>();
            if (srA == null || srB == null) return;

            float topA = srA.bounds.max.y;
            float topB = srB.bounds.max.y;

            Transform topTile = topA >= topB ? tileA : tileB;
            Transform botTile = topTile == tileA ? tileB : tileA;

            var botSr = botTile.GetComponentInChildren<SpriteRenderer>();
            float botTop = botSr.bounds.max.y;

            if (botTop <= camBottom - wrapPadding)
                botTile.position = new Vector3(botTile.position.x, topTile.position.y + _tileHeight, botTile.position.z);
        }
    }
}

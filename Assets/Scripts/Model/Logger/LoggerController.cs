using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Logger
{
    public class LoggerController : MonoBehaviour
    {
        [SerializeField] private LoggerItem itemPrefab;
        [SerializeField] private RectTransform itemParent;

        private List<LoggerItem> Items { get; }= new();
        private List<LoggerItem> PooledItems { get; }= new();

        private const float PooledOffset = 100f;
        private const float DefaultSpacing = 0f;
        private const int MaxItems = 3;

        private float BaseYValue => itemPrefab.Height / 2;
        private float _lastYValue;
        
        public static LoggerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Log(string message) => Log(message, Color.white);
        public void LogError(string message) => Log(message, Color.red);
        public void LogWarning(string message) => Log(message, Color.yellow);

        private void Log(string message, Color color)
        {
            float y;
            if (Items.Count == 0)
            {
                _lastYValue = -BaseYValue;
                y = _lastYValue;
            }
            else
            {
                y = _lastYValue -= itemPrefab.Height - DefaultSpacing;
            }

            var item = AddLog();
            item.Setup(message, color, y);
            Items.Add(item);

            if (Items.Count > MaxItems)
            {
                RemoveLog();
            }
        }

        private void RemoveLog()
        {
            var item = Items[0];
            item.gameObject.SetActive(false);
            item.MoveBy(PooledOffset).Play();
            PooledItems.Add(item);
            Items.RemoveAt(0);

            OnRemoveLog();
        }

        private LoggerItem AddLog()
        {
            if (PooledItems.Count <= 0)
            {
                return Instantiate(itemPrefab, itemParent);
            }

            var item = PooledItems[0];
            item.MoveBy(-PooledOffset).Play();
            item.gameObject.SetActive(true);
            PooledItems.RemoveAt(0);
            return item;
        }

        private void OnRemoveLog()
        {
            if (Items == null || Items.Count == 0)
            {
                return;
            }

            DOTween.Kill(this);
            var sequence = DOTween.Sequence().SetTarget(this);
            for(var i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
                sequence.Join(item.MoveBy(itemPrefab.Height + DefaultSpacing));
            }
            
            _lastYValue = -BaseYValue - (itemPrefab.Height + DefaultSpacing) * (Items.Count - 1);
            sequence.Play();
        }
    }
}
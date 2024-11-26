using Managers.EventManager;
using UnityEngine;

namespace Managers.Time
{
    public class TimeManager : MonoBehaviour
    {
        public float _currentTime { get; private set; } = 0f;

        public float EndTime { get { return _endTime; } private set { _endTime = value; } }

        [SerializeField]
        public float _endTime = 30f;
        [SerializeField]
        private float _timeScale = 1f;

        [SerializeField]
        private GameEvent _tickEvent;
        [SerializeField]
        private GameEvent _endEvent;

        [SerializeField]
        private bool _started = true;
        [SerializeField]
        private bool _rewind = false;
        private bool _pause = false;

        public void StartTimer()
        {
            _currentTime = 0f;
            _started = true;
        }

        public void EndTimer()
        {
            _endEvent.TriggerEvent();
            _started = false;
            _currentTime = _endTime;
        }

        public void ResetTimerSettings()
        {
            _started = false;
            _rewind = false;
            _pause = false;

            _timeScale = 1f;
            _currentTime = 0f;
            _endTime = 10f;
        }

        public void Update()
        {
            OnTick(UnityEngine.Time.deltaTime);
        }

        private void OnTick(float deltaTime)
        {
            if (!_pause && _started)
            {
                _tickEvent.TriggerEvent();

                float direction = ((_rewind) ? -1f : 1f);
                _currentTime += direction * _timeScale * deltaTime;

                if (_currentTime >= _endTime)
                {
                    EndTimer();
                }
            }
        }

        public void SetTimeScale(float timeScale)
        {
            _timeScale = timeScale;
        }

        public void SetCurrentTime(float time)
        {
            _currentTime = time;
        }

        public void SetEndTime(float endTime)
        {
            _endTime = endTime;
        }

        public void SetEndEvent(GameEvent endEvent)
        {
            _endEvent = endEvent;
        }

        public void TogglePause()
        {
            _pause = !_pause;
        }

        public void ToggleRewind()
        {
            _rewind = !_rewind;
        }
    }
}

using Managers.EventManager;
using UnityEngine;

namespace Managers.Time
{
    public class TimeManager : MonoBehaviour
    {
        public float CurrentTime { get; private set; } = 0f;
        public float EndTime { get { return _endTime; } private set { _endTime = value; } }
        public bool Ended = false;

        [SerializeField]
        private float _endTime = 30f;
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
            CurrentTime = 0f;
            _started = true;
            Ended = false;
        }

        public void EndTimer()
        {
            _endEvent?.TriggerEvent();
            _started = false;
            Ended = true;
            CurrentTime = _endTime;
        }

        public void ResetTimerSettings()
        {
            _started = false;
            _rewind = false;
            _pause = false;

            _timeScale = 1f;
            CurrentTime = 0f;
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
                _tickEvent?.TriggerEvent();

                float direction = ((_rewind) ? -1f : 1f);
                CurrentTime += direction * _timeScale * deltaTime;

                if (CurrentTime >= _endTime)
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
            CurrentTime = time;
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

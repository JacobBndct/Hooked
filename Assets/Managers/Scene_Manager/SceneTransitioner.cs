using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.CustomSceneManager
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Canvas))]
	public class SceneTransitioner : MonoBehaviour
	{
		// private instance
		private static SceneTransitioner s_Instance;

		// public static singleton instance of scene transitioner
		public static SceneTransitioner Instance
		{
			get => s_Instance;
			private set => s_Instance = value;
		}

		// the canvas for drawing scene transitions to
		[SerializeField]
		private Canvas m_TransitionCanvas;

		// async level load operation
		private AsyncOperation m_LoadLevelOperation;

		// scriptable object for the transition
		private AbstractSceneTransitionScriptableObject m_ActiveTransition;

		// enforces the singleton pattern
		void Awake()
		{
			// if there is another instance destroy self and throw warning
			if (Instance != null)
			{
				Debug.LogWarning($"Invalid configuration. Duplicate instances found! First instance: {Instance.name}");
				Destroy(gameObject);
				return;
			}

			// register event
			SceneManager.activeSceneChanged += HandleSceneChange;

			// set the singleton instance
			s_Instance = this;

			// add object to don't destroy on load
			DontDestroyOnLoad(gameObject);
			
			// set transition canvas
			m_TransitionCanvas = GetComponent<Canvas>();
			m_TransitionCanvas.enabled = false;
		}

		// set the transition type
		public void SetSceneTransitionAnimation(AbstractSceneTransitionScriptableObject animationSO)
		{
			m_ActiveTransition = animationSO;
		}

		// load a scene by name
		public void LoadScene(string scene)
		{
			LoadSceneMode mode = LoadSceneMode.Single;
			m_LoadLevelOperation = SceneManager.LoadSceneAsync(scene, mode);

			m_LoadLevelOperation.allowSceneActivation = false;
			m_TransitionCanvas.enabled = true;
			StartCoroutine(Exit());
		}

		// load next scene in the build index
        public void LoadNextScene()
        {
            LoadSceneMode mode = LoadSceneMode.Single;
			int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
            m_LoadLevelOperation = SceneManager.LoadSceneAsync(currentBuildIndex + 1, mode);

            m_LoadLevelOperation.allowSceneActivation = false;
            m_TransitionCanvas.enabled = true;
            StartCoroutine(Exit());
        }

		// reload the currently loaded scene by build index
        public void ReloadCurrentScene()
        {
            LoadSceneMode mode = LoadSceneMode.Single;
            int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
            m_LoadLevelOperation = SceneManager.LoadSceneAsync(currentBuildIndex, mode);

            m_LoadLevelOperation.allowSceneActivation = false;
            m_TransitionCanvas.enabled = true;
            StartCoroutine(Exit());
        }

		public void QuitGame()
		{
			Application.Quit(0);
		}

		// exit coroutine
        private IEnumerator Exit()
		{
			yield return StartCoroutine(m_ActiveTransition.Exit(m_TransitionCanvas));
			m_LoadLevelOperation.allowSceneActivation = true;
		}

		// enter coroutine
		private IEnumerator Enter()
		{
			yield return StartCoroutine(m_ActiveTransition.Enter(m_TransitionCanvas));
			m_TransitionCanvas.enabled = false;
			m_LoadLevelOperation = null;
		}

		// starts a coroutine for the current scene transition
		private void HandleSceneChange(Scene oldScene, Scene newScene)
		{
			if (m_ActiveTransition != null)
			{
				StartCoroutine(Enter());
			}
		}
	}
}

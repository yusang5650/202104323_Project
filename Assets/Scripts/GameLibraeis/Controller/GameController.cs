using Miro.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Miro.Contents
{
	public class GameController : MonoBase
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		public static void CreateUiInstance()
		{
			var resource = Resources.Load<GameObject>("UI/Game UI");
			var go = Instantiate(resource);
			
			DontDestroyOnLoad(go);
		}
		
		protected override void OnGameStarted()
		{
			gameObject.SetActive(false);
			
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			SceneManager.LoadScene(0);
		}
		protected override void OnGameEnded()
		{
			gameObject.SetActive(true);
			
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public void StartButton() => GameStart();
		public void EndButton() => GameEnd();
	}
}
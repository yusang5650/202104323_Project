using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Miro.Binder
{
	public class GameBinder : MonoBehaviour, ISingleton
	{
		public bool IsGameStart { get; private set; }
		
		public delegate void onGameStarted();
		public event onGameStarted GameStarted;
		
		public delegate void onGameBegin();
		public event onGameBegin GameBegin;
		
		public delegate void onGameEnded();
		public event onGameEnded GameEnded;

		private Coroutine coroutine;

		public void GameStart()
		{
			IsGameStart = true;
			GameStarted?.Invoke();

			coroutine = StartCoroutine(UpdateAsync());
		}

		public void GameEnd()
		{
			IsGameStart = false;
			GameEnded?.Invoke();
			
			if (coroutine != null)
				StopCoroutine(coroutine);
		}

		private void OnDestroy()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
		}

		private IEnumerator UpdateAsync()
		{
			while (IsGameStart)
			{
				GameBegin?.Invoke();
				yield return null;
			}
		}
	}
}
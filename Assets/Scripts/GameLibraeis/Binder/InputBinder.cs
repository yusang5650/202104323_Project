using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Miro.Binder
{
	public class InputBinder : MonoBehaviour, ISingleton
	{
		public delegate void onInputDownDetected(KeyCode keyCode);
		public event onInputDownDetected InputDownDetected;
		
		public delegate void onInputUpDetected(KeyCode keyCode);
		public event onInputUpDetected InputUpDetected;
		
		public delegate void onInputDetected(KeyCode keyCode);
		public event onInputDetected InputDetected;

		private void Update()
		{
			if (!Input.anyKey)
				return;

			var keyCodes = Enum.GetValues(typeof(KeyCode));
			foreach (KeyCode keyCode in keyCodes)
			{
				if (Input.GetKeyDown(keyCode))
					InputDownDetected?.Invoke(keyCode);

				if (Input.GetKeyUp(keyCode))
					InputUpDetected?.Invoke(keyCode);
						
				if (Input.GetKey(keyCode))
					InputDetected?.Invoke(keyCode);
			}
		}
	}
}
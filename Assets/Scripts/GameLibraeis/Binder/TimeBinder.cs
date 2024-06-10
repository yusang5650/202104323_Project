using Unity.VisualScripting;
using UnityEngine;

namespace Miro.Binder
{
	public class TimeBinder : MonoBehaviour, ISingleton
	{
		private float m_TimeScale = 1.0f;
		public float TimeScale
		{
			get => m_TimeScale;
			set
			{
				m_TimeScale = value;
				TimeScaleChanged?.Invoke(value);
			}
		}

		public float MonoTime => Time.deltaTime * TimeScale;
		public float MonoFixedTime => Time.fixedDeltaTime * TimeScale;

		public delegate void onTimeScaleChanged(float timeScale);
		public event onTimeScaleChanged TimeScaleChanged;
	}
}
using Unity.VisualScripting;
using UnityEngine;

namespace Miro.Binder
{
	public class AiTargetBinder : MonoBehaviour, ISingleton
	{
		public Transform Target;

		public Vector3 m_TargetPosition;
		public Vector3 TargetPosition 
		{
			get => m_TargetPosition;
			set
			{
				m_TargetPosition = value;
				TargetPositionChanged?.Invoke(value);
			}
		}
		
		public delegate void onTargetPositionChanged(Vector3 target);
		public event onTargetPositionChanged TargetPositionChanged;
	}
}
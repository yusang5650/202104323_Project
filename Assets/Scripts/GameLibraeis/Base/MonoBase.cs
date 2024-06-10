using Miro.Binder;
using Miro.Scriptable;
using UnityEngine;

namespace Miro.Base
{
    // MonoBehaviour를 상속받은 MonoBase 클래스
	public class MonoBase : MonoBehaviour
	{
		// SettingScriptable 리소스를 불러와 설정 정보를 제공하는 프로퍼티
		protected SettingScriptable Setting => Resources.Load<SettingScriptable>("MiroSetting/Setting");
		// 게임 시작 여부를 반환하는 프로퍼티
		protected bool IsGameStart => BindContainer.Get<GameBinder>().IsGameStart;
		
		// TimeScale을 가져오고 설정하는 정적 프로퍼티
		protected static float TimeScale
		{
			get => BindContainer.Get<TimeBinder>().TimeScale;
			set => BindContainer.Get<TimeBinder>().TimeScale = value;
		}

		// MonoTime을 반환하는 정적 프로퍼티
		protected static float MonoTime => BindContainer.Get<TimeBinder>().MonoTime;
		// MonoFixedTime을 반환하는 정적 프로퍼티
		protected static float MonoFixedTime => BindContainer.Get<TimeBinder>().MonoFixedTime;
		
		// MonoBehaviour의 Awake 메서드를 재정의하여 초기화 작업 수행
		protected virtual void Awake()
		{
			// 다양한 이벤트에 대한 리스너를 등록
			BindContainer.Get<InputBinder>().InputDownDetected += OnInputDownDetected;
			BindContainer.Get<InputBinder>().InputUpDetected += OnIInputUpDetected;
			BindContainer.Get<InputBinder>().InputDetected += OnInputDetected;
			BindContainer.Get<GameBinder>().GameStarted += OnGameStarted;
			BindContainer.Get<GameBinder>().GameBegin += OnGameBegin;
			BindContainer.Get<GameBinder>().GameEnded += OnGameEnded;
			BindContainer.Get<TimeBinder>().TimeScaleChanged += OnTimeScaleChanged;
			BindContainer.Get<AiTargetBinder>().TargetPositionChanged += OnTargetPositionChanged;
		}

		// MonoBehaviour의 OnDestroy 메서드를 재정의하여 종료 작업 수행
		protected virtual void OnDestroy()
		{
			// 다양한 이벤트에 대한 리스너를 해제
			BindContainer.Get<InputBinder>().InputDownDetected -= OnInputDownDetected;
			BindContainer.Get<InputBinder>().InputUpDetected -= OnIInputUpDetected;
			BindContainer.Get<InputBinder>().InputDetected -= OnInputDetected;
			BindContainer.Get<GameBinder>().GameStarted -= OnGameStarted;
			BindContainer.Get<GameBinder>().GameBegin -= OnGameBegin;
			BindContainer.Get<GameBinder>().GameEnded -= OnGameEnded;
			BindContainer.Get<TimeBinder>().TimeScaleChanged -= OnTimeScaleChanged;
			BindContainer.Get<AiTargetBinder>().TargetPositionChanged -= OnTargetPositionChanged;
		}

		// 입력이 감지될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnInputDownDetected(KeyCode keycode) { }
		// 입력이 해제될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnIInputUpDetected(KeyCode keycode) { }
		// 입력이 감지될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnInputDetected(KeyCode keycode) { }
		// 게임이 시작될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnGameStarted() { }
		// 게임이 시작되기 직전에 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnGameBegin() { }
		// 게임이 종료될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnGameEnded() { }
		// TimeScale이 변경될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnTimeScaleChanged(float timescale) { }
		// 타겟 위치가 변경될 때 호출되는 가상 메서드 (하위 클래스에서 재정의 가능)
		protected virtual void OnTargetPositionChanged(Vector3 position) { }

		// 게임을 시작하는 메서드
		protected void GameStart() => BindContainer.Get<GameBinder>().GameStart();
		// 게임을 종료하는 메서드
		protected void GameEnd() => BindContainer.Get<GameBinder>().GameEnd();
		// 플레이어 Transform을 반환하는 메서드
		protected Transform GetPlayer() => BindContainer.Get<AiTargetBinder>().Target;
		// 플레이어 Transform을 설정하는 메서드
		protected void SetPlayer(Transform target) => BindContainer.Get<AiTargetBinder>().Target = target;
		// 플레이어 위치를 반환하는 메서드
		protected Vector3 GetPlayerPosition() => BindContainer.Get<AiTargetBinder>().TargetPosition;
		// 플레이어 위치를 설정하는 메서드
		protected void SendPlayerPosition(Vector3 position) => BindContainer.Get<AiTargetBinder>().TargetPosition = position;
	}	
}
using Miro.Base;
using UnityEngine;

namespace Miro.Contents
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBase
	{
		// Rigidbody 컴포넌트를 참조할 변수
		private new Rigidbody rigidbody;
		// 플레이어 이동 속도 변수
		private float Speed;

		// 초기화 함수, 객체가 생성될 때 호출됨
		protected override void Awake()
		{
			base.Awake();
			
			// 플레이어 위치 설정 함수 호출
			SetPlayer(transform);
			// 이동 속도 초기화
			Speed = Setting.MoveSpeed;
			
			// Rigidbody 컴포넌트 가져오기
			rigidbody = GetComponent<Rigidbody>();
		}

		// 매 프레임마다 호출되는 업데이트 함수
		private void Update()
		{
			// 게임이 시작되지 않았으면 함수 종료
			if (!IsGameStart)
				return;
			
			// 메인 카메라가 존재하면
			if (Camera.main)
			{
				// 플레이어의 y 축 회전을 카메라의 y 축 회전으로 설정
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
			}
			
			// 입력 값에 따라 이동 벡터 계산
			var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			// 이동 방향 계산
			var direction = transform.TransformDirection(movement);
			// Rigidbody의 속도 설정 (이동 벡터와 방향을 곱하고 시간과 속도를 반영)
			rigidbody.velocity = movement + direction * (MonoTime * Speed);
		}

		// 입력이 감지될 때 호출되는 함수 (키를 눌렀을 때)
		protected override void OnInputDownDetected(KeyCode keycode)
		{
			// LeftShift 키가 눌리면 달리기 속도로 설정
			if (keycode == KeyCode.LeftShift)
				Speed = Setting.MoveRunningSpeed;
		}

		// 입력이 해제될 때 호출되는 함수 (키를 뗐을 때)
		protected override void OnIInputUpDetected(KeyCode keycode)
		{
			// LeftShift 키를 떼면 기본 이동 속도로 설정
			if (keycode == KeyCode.LeftShift)
				Speed = Setting.MoveSpeed;
		}
	}
}
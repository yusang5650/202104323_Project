using Miro.Base;
using UnityEngine;

namespace Miro.Contents
{
    // PlayerCameraSupport 클래스는 플레이어의 카메라를 제어합니다.
	public class PlayerCameraSupport : MonoBase
	{
        // 카메라의 현재 속도를 저장하는 변수
		private Vector3 currentVelocity;
        // 마우스의 X축 회전 값을 저장하는 변수
		private float mouseX;
        // 마우스의 Y축 회전 값을 저장하는 변수
		private float mouseY;

        // MonoBehaviour의 Awake 메서드를 재정의하여 초기화 작업 수행
		protected override void Awake()
		{
            // 부모 클래스의 Awake 메서드 호출
			base.Awake();
			
            // 현재 객체의 Y축 회전 각도를 초기 마우스 X축 값으로 설정
			mouseX = transform.eulerAngles.y;
            // 현재 객체의 X축 회전 각도를 초기 마우스 Y축 값으로 설정
			mouseY = transform.eulerAngles.x;
		}
		
        // MonoBehaviour의 LateUpdate 메서드를 재정의하여 매 프레임 후반에 호출
		private void LateUpdate()
		{
            // 게임이 시작되지 않았으면 업데이트 중지
			if (!IsGameStart)
				return;
			
            // 마우스 X축 움직임을 감지하여 회전 각도 계산
			mouseX += Input.GetAxis("Mouse X") * Setting.MouseSensitivity;
            // 마우스 Y축 움직임을 감지하여 회전 각도 계산
			mouseY += Input.GetAxis("Mouse Y") * Setting.MouseSensitivity;
            // 마우스 Y축 회전 각도를 설정된 최소값과 최대값 사이로 제한
			mouseY = Mathf.Clamp(mouseY, Setting.MouseYMin, Setting.MouseYMax);
			
            // 카메라의 회전 각도 설정
			transform.eulerAngles = new Vector3(-mouseY, mouseX, GetPlayer().eulerAngles.z);
            // 카메라의 위치를 플레이어 위치로 부드럽게 이동
			transform.position = Vector3.SmoothDamp(transform.position, GetPlayer().position, ref currentVelocity, MonoTime * Setting.MoveCameraSpeed);
		}
	}
}
using UnityEngine;

namespace Miro.Scriptable
{
    // ScriptableObject를 생성할 수 있는 메뉴 옵션을 Unity 에디터에 추가합니다.
    [CreateAssetMenu(fileName = "Setting", menuName = "Miro/Setting")]
    public class SettingScriptable : ScriptableObject
    {
        // 이동 옵션 설정
        [Header("Move Option")]
        // 걷기 속도
        public float MoveSpeed = 100;
        // 뛰기 속도
        public float MoveRunningSpeed = 200;
        // 카메라 이동 속도
        public float MoveCameraSpeed = 5.0f;
        
        // 마우스 옵션 설정
        [Header("Mouse Option")]
        // 마우스 감도
        public float MouseSensitivity = 10.0f;
        // 마우스 Y축 최소 각도
        public int MouseYMin = -60;
        // 마우스 Y축 최대 각도
        public int MouseYMax = 60;
        
        // AI 탐지 옵션 설정
        [Header("Ai Detect")]
        // AI 탐지 각도
        public float AiDetectAngle = 15.0f;
        // AI 탐지 거리
        public float AiDetectDistance = 5.0f;
        // AI 탐지 범위 개수
        public int AiDetectRangeCount = 10;
        
        // AI 탐지 버프 설정
        [Header("Ai Detect Buff")]
        // AI 탐지 버프 각도
        public float AiDetectBuffAngle = 360.0f;
        // AI 탐지 버프 거리
        public float AiDetectBuffDistance = 35.0f;
        // AI 탐지 버프 범위 개수
        public int AiDetectBuffRangeCount = 200;
    }
}
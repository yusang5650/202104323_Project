using Miro.Base;
using UnityEngine;

namespace Contents.Player
{
    // PlayerOnTrigger 클래스는 플레이어가 트리거 영역에 진입했을 때 발생하는 이벤트를 처리합니다.
    public class PlayerOnTrigger : MonoBase
    {
        // 수집한 키의 개수를 저장하는 정적 변수
        private static int keyCount = 0; 
        // 승리를 위해 필요한 키의 개수를 설정하는 변수
        public int keysRequiredToWin = 3; 

        // 트리거 영역에 다른 Collider가 진입했을 때 호출되는 메서드
        private void OnTriggerEnter(Collider other)
        {
            // 진입한 객체가 'Key' 태그를 가지고 있을 때
            if (other.gameObject.CompareTag("Key"))
            {
                // 해당 객체를 삭제하고
                Destroy(other.gameObject);
                // 키의 개수를 증가시킵니다.
                keyCount++;
            }
            // 진입한 객체가 'Finish' 태그를 가지고 있을 때
            else if (other.gameObject.CompareTag("Finish"))
            {
                // 수집한 키의 개수가 승리에 필요한 개수 이상이면
                if (keyCount >= keysRequiredToWin)
                {
                    // 게임을 종료합니다.
                    GameEnd();
                }
            }
            // 진입한 객체가 'Enemy' 태그를 가지고 있을 때
            else if (other.gameObject.CompareTag("Enemy"))
            {
                // 게임을 종료합니다.
                GameEnd();
            }
        }
    }
}
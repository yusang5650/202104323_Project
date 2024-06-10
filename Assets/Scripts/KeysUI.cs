using Contents.Player;
using Miro.Base;
using UnityEngine;
using UnityEngine.UI;


public class KeyCollector : MonoBehaviour
{
    public int keysCollected = 0;
    public Text keyCountText; // UI 텍스트

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key")) // 키 태그를 가진 오브젝트와 충돌했을 때
        {
            keysCollected++; // 키 개수 증가
            Destroy(other.gameObject); // 충돌한 키 오브젝트 파괴
            UpdateUI(); // UI 갱신
        }
    }

    void UpdateUI()
    {
        if (keyCountText != null)
        {
            keyCountText.text = "Keys: " + keysCollected.ToString(); // UI 텍스트 업데이트
        }
    }
}
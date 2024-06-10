using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Miro.Base;
using UnityEngine;
using UnityEngine.AI;

namespace Miro.Support
{
	// NavMeshAgent 컴포넌트를 필수로 요구
	[RequireComponent(typeof(NavMeshAgent))]
	public class AiTargetSupport : MonoBase
	{
		// NavMeshAgent 변수 선언
		private NavMeshAgent agent;
		// 추적 코루틴을 관리하는 변수
		private Coroutine coroutine;
		
		// AI 감지 설정 변수
		private float DetectAngle;
		private float DetectDistance;
		private int DetectRangeCount;
		
		protected override void Awake()
		{
			base.Awake();
			
			// NavMeshAgent 컴포넌트 가져오기
			agent = GetComponent<NavMeshAgent>();

			// 설정값 초기화
			DetectAngle = Setting.AiDetectAngle;
			DetectDistance = Setting.AiDetectDistance;
			DetectRangeCount = Setting.AiDetectRangeCount;
		}

		// 플레이어 위치가 변경되었을 때 호출되는 메서드
		protected override void OnTargetPositionChanged(Vector3 position)
		{
			SetTracking(position);
		}

		// 게임이 시작될 때 호출되는 메서드
		protected override void OnGameBegin()
		{
			// 플레이어와의 거리가 감지 거리를 넘으면 리턴
			if (Vector3.Distance(GetPlayer().position, transform.position) > DetectDistance)
				return;
			
			// 감지 범위 내의 레이 가져오기
			var rays = GetDetectRange();
			foreach (var ray in rays)
			{
				// 레이를 쏴서 충돌체를 감지
				if (!Physics.Raycast(ray, out var hit, DetectDistance))
					continue;
				
				// 감지된 객체가 플레이어인지 확인
				if (!hit.transform.CompareTag(GetPlayer().tag))
					continue;

				// 플레이어 위치로 추적 시작
				SetTracking(hit.point);
				break;
			}
		}
		// 추적 설정
		private void SetTracking(Vector3 position)
		{
			// NavMeshAgent의 목적지 설정
			agent.SetDestination(position);
			
			// 기존 코루틴이 있으면 중지
			if (coroutine != null)
				StopCoroutine(coroutine);

			// 감지 설정을 버프 값으로 변경
			DetectAngle = Setting.AiDetectBuffAngle;
			DetectDistance = Setting.AiDetectBuffDistance;
			DetectRangeCount = Setting.AiDetectBuffRangeCount;
			
			// 새로운 코루틴 시작
			coroutine = StartCoroutine(TrackingBuff());
		}

#if UNITY_EDITOR
        // 디버깅을 위한 감지 범위 시각화
        private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			
			// 감지 범위 내의 레이 가져오기
			var rays = GetDetectRange();
			foreach (var ray in rays)
			{
				// 감지된 객체까지의 레이를 그리거나 감지 거리를 그립니다.
				if (Physics.Raycast(ray, out var hit, DetectDistance))
					Gizmos.DrawRay(ray.origin, ray.direction * hit.distance);
				else
					Gizmos.DrawRay(ray.origin, ray.direction * DetectDistance);
			}
		}
#endif

		// 감지 범위를 반환하는 메서드
		private List<Ray> GetDetectRange()
		{
			const float coneDirection = 90;

			var destination = DetectAngle * 2 / DetectRangeCount;

			var rotationList = new List<Quaternion>();
			for (var i = 1; i <= DetectRangeCount; i++)
				rotationList.Add(Quaternion.AngleAxis(DetectAngle - destination * i + coneDirection, Vector3.up));

			var vectorList = rotationList.Select(rotation => rotation * transform.right)
			                             .ToList();
			
			return vectorList.Select(vector => new Ray { origin = transform.position, direction = vector }).ToList();
		}

		// 추적 버프를 적용하는 코루틴
		private IEnumerator TrackingBuff()
		{
			// 5초 동안 버프 상태 유지
			yield return new WaitForSeconds(5.0f);
			
			// 버프가 끝난 후 감지 설정을 원래 값으로 되돌림
			DetectAngle = Setting.AiDetectAngle;
			DetectDistance = Setting.AiDetectDistance;
			DetectRangeCount = Setting.AiDetectRangeCount;
		}
	}
}
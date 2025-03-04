using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Locations
{
	SweetHome = 0,
	Library,
	LectureRoom,
	PCRoom,
	Pub
};

public class GameController : MonoBehaviour
{
	// Students의 이름 배열
	[SerializeField] private string[] arrayStudents;

	// Student 타입의 프리팹
	[SerializeField] private GameObject studentPrefab;

	// Unemployed들의 이름 배열
	[FormerlySerializedAs("arrayUnemployed")] [SerializeField]
	private string[] arrayUnemployeds;

	// Unemployed 타입의 프리팹
	[SerializeField] private GameObject unemployedPrefab;

	// 재생 제어를 위한 모든 에이전트 리스트
	private List<BaseGameEntity> entities;

	public static bool IsGameStop { set; get; } = false;

	private void Awake()
	{
		entities = new List<BaseGameEntity>();
		for (int i = 0; i < arrayStudents.Length; i++)
		{
			// 에이전트 생성, 초기화 메소드 호출
			GameObject clone = Instantiate(studentPrefab);
			Student entity = clone.GetComponent<Student>();
			entity.Setup(arrayStudents[i]);

			// 에이전트들의 재생 제어를 위해 리스트에 저장
			entities.Add(entity);
		}

		// Unemployed 에이전트 생성
		for (int i = 0; i < arrayUnemployeds.Length; ++i)
		{
			GameObject clone = Instantiate(unemployedPrefab);
			Unemployed entity = clone.GetComponent<Unemployed>();
			entity.Setup(arrayUnemployeds[i]);

			entities.Add(entity);
		}

		// 에이전트 데이터베이스 초기화 및 모든 에이전트 등록
		EntityDatabase.Instance.Setup();
		for (int i = 0; i < entities.Count; ++i)
		{
			EntityDatabase.Instance.RegisterEntity(entities[i]);
		}

		// 메시지 관리자 초기화
		MessageDispatcher.Instance.Setup();
	}

	private void Update()
	{
		if (IsGameStop == true) return;

		// 지연 발송되어야 하는 메시지 관리
		MessageDispatcher.Instance.DispatchDelayedMessages();

		// 모든 에이전트의 Updated()를 호출해 에이전트 구동
		for (int i = 0; i < entities.Count; i++)
		{
			entities[i].Updated();
		}
	}

	public static void Stop(BaseGameEntity entity)
	{
		IsGameStop = true;
		entity.PrintText("100점 획득으로 프로그램을 종료합니다.");
	}
}
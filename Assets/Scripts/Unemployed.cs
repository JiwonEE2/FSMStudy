using UnityEngine;

public enum UnemployedStates
{
	RestAndSleep = 0,
	PlayAGame,
	HitTheBottle
}

public class Unemployed : BaseGameEntity
{
	// 지루함
	private int bored;

	// 스트레스
	private int stress;

	// 피로
	private int fatigue;

	// 현재 위치
	private Locations currentLocation;

	// Unemployed가 가지고 있는 모든 상태, 상태 관리자
	private State<Unemployed>[] states;
	private StateMachine<Unemployed> stateMachine;

	public int Bored
	{
		set => bored = Mathf.Max(0, value);
		get => bored;
	}

	public int Stress
	{
		set => stress = Mathf.Max(0, value);
		get => stress;
	}

	public int Fatigue
	{
		set => fatigue = Mathf.Max(0, value);
		get => fatigue;
	}

	public Locations CurrentLocation
	{
		set => currentLocation = value;
		get => currentLocation;
	}

	public override void Setup(string name)
	{
		// 기반 클래스의 Setup 메소드 호출 (ID, 이름, 색상 설정)
		base.Setup(name);

		// 생성되는 오브젝트 이름 설정
		gameObject.name = $"{ID:D2}_Unemployed_{name}";

		// 상태를 관리하는 StateMachine에 메모리를 할당하고, 첫 상태를 설정
		stateMachine = new StateMachine<Unemployed>();

		bored = 0;
		stress = 0;
		fatigue = 0;
		currentLocation = Locations.SweetHome;
	}

	public override void Updated()
	{
		stateMachine.Execute();
	}

	public void ChangeState(UnemployedStates newState)
	{
		stateMachine.ChangeState(states[(int)newState]);
	}
}
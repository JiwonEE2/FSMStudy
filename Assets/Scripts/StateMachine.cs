public class StateMachine<T> where T : class
{
	// StateMachine의 소유주
	private T ownerEntity;

	// 현재 상태
	private State<T> currentState;

	public void Setup(T owner, State<T> entryState)
	{
		ownerEntity = owner;
		currentState = null;

		// entryState 상태로 상태 변경
		ChangeState(entryState);
	}

	public void Execute()
	{
		if (currentState != null)
		{
			currentState.Execute(ownerEntity);
		}
	}

	public void ChangeState(State<T> newState)
	{
		// 새로 바꾸려는 상태가 비어 있으면 상태를 바꾸지 않는다.
		if (newState == null) return;

		// 현재 재생 중인 상태가 있으면 Exit() 메소드 호출
		if (currentState != null)
		{
			currentState.Exit(ownerEntity);
		}

		// 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
		currentState = newState;
		currentState.Enter(ownerEntity);
	}
}
public class StateMachine<T> where T : BaseGameEntity // class
{
	// StateMachine의 소유주
	private T ownerEntity;

	// 현재 상태
	private State<T> currentState;

	// 이전 상태
	private State<T> previousState;

	// 전역 상태
	private State<T> globalState;

	public void Setup(T owner, State<T> entryState)
	{
		ownerEntity = owner;
		currentState = null;
		previousState = null;
		globalState = null;

		// entryState 상태로 상태 변경
		ChangeState(entryState);
	}

	public void Execute()
	{
		if (globalState != null)
		{
			globalState.Execute(ownerEntity);
		}

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
			// 상태가 변경되면 현재 상태는 이전 상태가 되기 때문에 previousState에 저장
			previousState = currentState;

			currentState.Exit(ownerEntity);
		}

		// 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
		currentState = newState;
		currentState.Enter(ownerEntity);
	}

	public void SetGlobalState(State<T> newState)
	{
		globalState = newState;
	}

	public void RevertToPreviousState()
	{
		ChangeState(previousState);
	}

	public bool HandleMessage(Telegram telegram)
	{
		if (globalState != null && globalState.OnMessage(ownerEntity, telegram))
		{
			return true;
		}

		if (currentState != null && currentState.OnMessage(ownerEntity, telegram))
		{
			return true;
		}

		return false;
	}
}
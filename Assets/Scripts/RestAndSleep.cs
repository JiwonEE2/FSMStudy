namespace StudentOwnedStates
{
	public class RestAndSleep : State
	{
		public override void Enter(Student entity)
		{
			// 장소를 집으로 설정하고, 집에 오면 스트레스가 0이 된다.
			entity.CurrentLocation = Locations.SweetHome;
			entity.Stress = 0;

			entity.PrintText("집에 들어온다. 스트레스가 사라졌다.");
			entity.PrintText("침대에 누워 잠을 잔다.");
		}

		public override void Execute(Student entity)
		{
			entity.PrintText("zzz...");

			// 피로가 0이 아니면
			if (entity.Fatigue > 0)
			{
				// 피로 10씩 감소
				entity.Fatigue -= 10;
			}
			// 피로가 0이면
			else
			{
				// 도서관에 가서 공부하는 "StudyHard" 상태로 변경
			}
		}

		public override void Exit(Student entity)
		{
			entity.PrintText("침대를 정리하고 집 밖으로 나간다.");
		}
	}
}
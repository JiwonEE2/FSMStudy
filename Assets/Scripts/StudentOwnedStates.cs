using UnityEngine;

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

	public class StudyHard : State
	{
		public override void Enter(Student entity)
		{
			entity.CurrentLocation = Locations.Library;
			entity.PrintText("공부를 하기 위해 도서관에 왔다.");
		}

		public override void Execute(Student entity)
		{
			entity.PrintText("공부중...");

			// 지식, 스트레스, 피로가 1씬 증가
			entity.Knowledge++;
			entity.Stress++;
			entity.Fatigue++;

			// 지식이 3~10 사이가 되면
			if (entity.Knowledge >= 3 && entity.Knowledge <= 10)
			{
				int isExit = Random.Range(0, 2);
				if (isExit == 1 || entity.Knowledge <= 10)
				{
					// 강의실에 가서 시험을 보는 TakeAExam 상태로 변경
				}
			}

			// 스트레스가 20 이상이 되면
			if (entity.Stress >= 20)
			{
				// PC방에 가서 게임을 하는 "PlayGame" 상태로 변경
			}

			// 피로가 50 이상이 되면
			if (entity.Fatigue >= 50)
			{
				// 집에 가서 쉬는 "RestAndSleep" 상태로 변경
			}
		}

		public override void Exit(Student entity)
		{
			entity.PrintText("자리를 정리하고 도서관을 나간다.");
		}
	}
}
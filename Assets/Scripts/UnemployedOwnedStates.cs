using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnemployedOwnedStates
{
	public class RestAndSleep : State<Unemployed>
	{
		public override void Enter(Unemployed entity)
		{
			// 장소를 집으로 설정하고, 집에 오면 스트레스, 피로가 0이 된다.
			entity.CurrentLocation = Locations.SweetHome;
			entity.Stress = 0;
			entity.Fatigue = 0;

			entity.PrintText("소파에 눕는다.");
		}

		public override void Execute(Unemployed entity)
		{
			string state = Random.Range(0, 2) == 0 ? "zzz..." : "TV를 본다";
			entity.PrintText(state);

			// 지루함은 70%의 확률로 증가, 30%의 확률로 감소
			entity.Bored += Random.Range(0, 100) < 70 ? 1 : -1;

			// 지루함이 4 이상이면
			if (entity.Bored >= 4)
			{
				// PC방에 가서 게임을 하는 "PlayAGame"상태로 변경
				entity.ChangeState(UnemployedStates.PlayAGame);
			}
		}

		public override void Exit(Unemployed entity)
		{
			entity.PrintText("정리를 하지 않고 그냥 나간다.");
		}
	}

	public class PlayAGame : State<Unemployed>
	{
		public override void Enter(Unemployed entity)
		{
			entity.CurrentLocation = Locations.PCRoom;
			entity.PrintText("PC방으로 들어간다.");
		}

		public override void Execute(Unemployed entity)
		{
			entity.PrintText("게임을 열정적으로 한다.");

			int randState = Random.Range(0, 10);
			if (randState == 0 || randState == 9)
			{
				entity.Stress += 20;

				// 술집에 가서 술을 마시는 "HitTheBottle" 상태로 변경
				entity.ChangeState(UnemployedStates.HitTheBottle);
			}
			else
			{
				entity.Bored--;
				entity.Fatigue += 2;

				if (entity.Bored <= 0 || entity.Fatigue >= 50)
				{
					// 집에 가서 쉬는 "RestAndSleep" 상태로 변경
					entity.ChangeState(UnemployedStates.RestAndSleep);
				}
			}
		}

		public override void Exit(Unemployed entity)
		{
			entity.PrintText("잘 즐겼다.");
		}
	}
}
using System;
using UnityEngine;

namespace uClicker {
	[CreateAssetMenu(menuName = "uClicker/BossStage")]
	public class BossStage : UnlockableComponent {
		public CurrencyTuple CostToStart;
		public CurrencyTuple CostToEnd;
		public UInt64 StageTime;

	}
}

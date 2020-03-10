using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SmokeleafIndustry
{
	public class HediffComp_HealRandomOldWound : HediffComp
	{
		private int ticksToHeal;

		public HediffCompProperties_HealRandomOldWound Props => (HediffCompProperties_HealRandomOldWound)(object)base.props;

		public override void CompPostMake()
		{
			((HediffComp)this).CompPostMake();
			ResetTicksToHeal();
		}

		private void ResetTicksToHeal()
		{
			ticksToHeal = Rand.Range(3, 8) * 60000;
		}

		public override void CompPostTick(ref float severityAdjustment)
		{
			ticksToHeal--;
			if (ticksToHeal <= 0)
			{
				TryHealRandomOldWound();
				ResetTicksToHeal();
			}
		}

		private void TryHealRandomOldWound()
		{
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			Hediff val = default(Hediff);
			if (GenCollection.TryRandomElement<Hediff>(((IEnumerable<Hediff>)((HediffComp)this).Pawn.health.hediffSet.hediffs).Where((Func<Hediff, bool>)HediffUtility.IsPermanent), out val))
			{
				val.Severity = 0f;
				if (PawnUtility.ShouldSendNotificationAbout(((HediffComp)this).Pawn))
				{
					Messages.Message(TranslatorFormattedStringExtensions.Translate("MessagePermanentWoundHealed", (((Hediff)base.parent).LabelCap), (((Entity)((HediffComp)this).Pawn).LabelShort), (val.Label), NamedArgumentUtility.Named((object)((HediffComp)this).Pawn, "PAWN")), ((Thing)(object)((HediffComp)this).Pawn), MessageTypeDefOf.PositiveEvent, true);
				}
			}
		}

		public override void CompExposeData()
		{
			Scribe_Values.Look<int>(ref ticksToHeal, "ticksToHeal", 0, false);
		}

		public override string CompDebugString()
		{
			return "ticksToHeal: " + ticksToHeal;
		}

		public HediffComp_HealRandomOldWound()
		{
		}
	}
}

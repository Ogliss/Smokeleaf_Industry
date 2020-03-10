using RimWorld;
using UnityEngine;
using Verse;
using static Verse.GenDraw;

namespace SmokeleafIndustry
{
	[StaticConstructorOnStartup]
	public class CompPowerPlantHempSolar : CompPowerPlant
	{
		private const float FullSunPower = 3000f;

		private const float NightPower = 0f;

		private static readonly Vector2 BarSize = new Vector2(4f, 0.14f);

		private static readonly Material PowerPlantHempSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f), false);

		private static readonly Material PowerPlantHempSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f), false);

		protected override float DesiredPowerOutput => Mathf.Lerp(0f, 3000f, ((Thing)base.parent).Map.skyManager.CurSkyGlow) * RoofedPowerOutputFactor;

		private float RoofedPowerOutputFactor
		{
			get
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Unknown result type (might be due to invalid IL or missing references)
				//IL_0039: Unknown result type (might be due to invalid IL or missing references)
				int num = 0;
				int num2 = 0;
				CellRect val = GenAdj.OccupiedRect((Thing)(object)base.parent);
				foreach (IntVec3 item in (CellRect)(val))
				{
					num++;
					if (((Thing)base.parent).Map.roofGrid.Roofed(item))
					{
						num2++;
					}
				}
				return (float)(num - num2) / (float)num;
			}
		}

		public override void PostDraw()
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			((CompPowerTrader)this).PostDraw();
			FillableBarRequest val = default(FillableBarRequest);
			val.center = ((Thing)base.parent).DrawPos + Vector3.up * 0.1f;
			val.size = BarSize;
			val.fillPercent = ((CompPowerTrader)this).PowerOutput / 3000f;
			val.filledMat = PowerPlantHempSolarBarFilledMat;
			val.unfilledMat = PowerPlantHempSolarBarUnfilledMat;
			val.margin = 0.15f;
			Rot4 rotation = ((Thing)base.parent).Rotation;
			((Rot4)(rotation)).Rotate((RotationDirection)1);
			val.rotation = rotation;
			GenDraw.DrawFillableBar(val);
		}

		public CompPowerPlantHempSolar()
		{
		}
	}
}

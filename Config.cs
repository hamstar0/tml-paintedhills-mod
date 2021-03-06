﻿using HamstarHelpers.Components.Config;
using PaintedHills.Colorers;
using System;
using System.Collections.Generic;
using Terraria.ID;


namespace PaintedHills {
	public class PaintedHillsConfig : ConfigurationDataBase {
		public readonly static Version CurrentVersion = new Version( 1, 0, 2 );
		public static string ConfigFileName { get { return "Painted Hills Config.json"; } }


		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;

		public bool Enabled = true;

		public float HueBlobMinimumTileRadius = 100f;
		public float HueBlobQuantityMultiplier = 2.5f;
		public float HueBlobSizeVariance = 25f;
		public float HueBlobShapeVariance = 50f;

		public IDictionary<int, ISet<Paints>> TileColorBlacklists = new Dictionary<int, ISet<Paints>>();



		////////////////

		public void SetDefaults() {
			this.TileColorBlacklists = new Dictionary<int, ISet<Paints>> {
				{ TileID.Dirt, new HashSet<Paints> {
					Paints.Red,
					Paints.Orange,
					Paints.Yellow,
					Paints.Lime,
					Paints.Green,
					Paints.Teal,
					Paints.Cyan,
					Paints.SkyBlue,
					Paints.Blue,
					Paints.Purple,
					Paints.Violet,
					Paints.Pink,
					Paints.DeepRed,
					Paints.DeepOrange,
					Paints.DeepYellow,
					Paints.DeepLime,
					Paints.DeepGreen,
					Paints.DeepTeal,
					Paints.DeepCyan,
					Paints.DeepSkyBlue,
					Paints.DeepBlue,
					Paints.DeepPurple,
					Paints.DeepViolet,
					Paints.DeepPink,
					Paints.Black,
					Paints.White,
					Paints.Gray,
					Paints.Brown,
					Paints.Shadow
				} }
			};
		}


		////////////////

		public bool UpdateToLatestVersion() {
			var new_config = new PaintedHillsConfig();
			var vers_since = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( vers_since >= PaintedHillsConfig.CurrentVersion ) {
				return false;
			}

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}
			
			this.VersionSinceUpdate = PaintedHillsConfig.CurrentVersion.ToString();

			return true;
		}
	}
}

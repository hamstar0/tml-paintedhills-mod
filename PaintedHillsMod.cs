using HamstarHelpers.MiscHelpers;
using HamstarHelpers.Utilities.Config;
using System;
using Terraria.ModLoader;


namespace PaintedHills {
    public class PaintedHillsMod : Mod {
		public JsonConfig<ConfigurationData> Config { get; private set; }
		public int DEBUGFLAGS { get; private set; } // +1: info


		public PaintedHillsMod() {
			this.Properties = new ModProperties() {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
			this.DEBUGFLAGS = 0;

			string filename = "Painted Hills Config.json";
			this.Config = new JsonConfig<ConfigurationData>( filename, "Mod Configs", new ConfigurationData() );
		}


		public override void Load() {
			this.LoadConfig();
		}

		private void LoadConfig() {
			try {
				if( !this.Config.LoadFile() ) {
					this.Config.SaveFile();
				}
			} catch( Exception e ) {
				DebugHelpers.Log( e.Message );
				this.Config.SaveFile();
			}

			if( this.Config.Data.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Painted Hills updated to " + ConfigurationData.CurrentVersion.ToString() );
				this.Config.SaveFile();
			}

			this.DEBUGFLAGS = this.Config.Data.DEBUGFLAGS;
		}
	}
}

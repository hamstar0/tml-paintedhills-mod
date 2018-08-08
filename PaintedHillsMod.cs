using HamstarHelpers.Components.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace PaintedHills {
    public class PaintedHillsMod : Mod {
		public static PaintedHillsMod Instance { get; private set; }

		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-paintedhills-mod"; } }

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + PaintedHillsConfig.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( PaintedHillsMod.Instance != null ) {
				if( !PaintedHillsMod.Instance.ConfigJson.LoadFile() ) {
					PaintedHillsMod.Instance.ConfigJson.SaveFile();
				}
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var new_config = new PaintedHillsConfig();
			//new_config.SetDefaults();

			PaintedHillsMod.Instance.ConfigJson.SetData( new_config );
			PaintedHillsMod.Instance.ConfigJson.SaveFile();
		}



		////////////////

		public JsonConfig<PaintedHillsConfig> ConfigJson { get; private set; }
		public PaintedHillsConfig Config { get { return this.ConfigJson.Data; } }


		////////////////

		public PaintedHillsMod() {
			this.Properties = new ModProperties() {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
			
			this.ConfigJson = new JsonConfig<PaintedHillsConfig>( PaintedHillsConfig.ConfigFileName, "Mod Configs", new PaintedHillsConfig() );
		}


		public override void Load() {
			PaintedHillsMod.Instance = this;

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Painted Hills updated to " + PaintedHillsConfig.CurrentVersion.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		public override void Unload() {
			PaintedHillsMod.Instance = null;
		}
	}
}

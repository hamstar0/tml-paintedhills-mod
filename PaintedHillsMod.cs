using HamstarHelpers.Utilities.Config;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace PaintedHills {
    public class PaintedHillsMod : Mod {
		public static PaintedHillsMod Instance { get; private set; }

		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-painthills-mod"; } }

		public static string ConfigFileRelativePath {
			get { return JsonConfig<PaintedHillsConfig>.RelativePath + Path.DirectorySeparatorChar + PaintedHillsConfig.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( PaintedHillsMod.Instance != null ) {
				if( !PaintedHillsMod.Instance.JsonConfig.LoadFile() ) {
					PaintedHillsMod.Instance.JsonConfig.SaveFile();
				}
			}
		}



		////////////////

		public JsonConfig<PaintedHillsConfig> JsonConfig { get; private set; }
		public PaintedHillsConfig Config { get { return this.JsonConfig.Data; } }


		////////////////

		public PaintedHillsMod() {
			this.Properties = new ModProperties() {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};

			string filename = "Painted Hills Config.json";
			this.JsonConfig = new JsonConfig<PaintedHillsConfig>( filename, "Mod Configs", new PaintedHillsConfig() );
		}


		public override void Load() {
			PaintedHillsMod.Instance = this;

			var hamhelpmod = ModLoader.GetMod( "HamstarHelpers" );
			if( hamhelpmod.Version < new Version( 1, 0, 12 ) ) {
				throw new Exception( "Hamstar's Helpers must be version " + hamhelpmod.Version.ToString() + " or greater." );
			}

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.JsonConfig.LoadFile() ) {
				this.JsonConfig.SaveFile();
			}

			if( this.Config.UpdateToLatestVersion() ) {
				ErrorLogger.Log( "Painted Hills updated to " + PaintedHillsConfig.CurrentVersion.ToString() );
				this.JsonConfig.SaveFile();
			}
		}

		public override void Unload() {
			PaintedHillsMod.Instance = null;
		}

		////////////////

		public bool IsDebugModeInfo() {
			return (this.Config.DEBUGFLAGS & 1) != 0;
		}
	}
}

using HamstarHelpers.TileHelpers;
using System;
using System.Collections.Generic;
using Terraria;


namespace PaintedHills.Colorers {
	public enum Paints : byte {
		None,
		Red,    //1073
		Orange, //1074
		Yellow, //1075
		Lime,   //1076
		Green,  //1077
		Teal,   //1078
		Cyan,   //1079
		SkyBlue,   //1080
		Blue, //1081
		Purple, //1082
		Violet, //1083
		Pink,   //1084
		DeepRed,   //1085
		DeepOrange, //1086
		DeepYellow, //1087
		DeepLime,   //1088
		DeepGreen, //1089
		DeepTeal,  //1090
		DeepCyan,  //1091
		DeepSkyBlue,  //1092
		DeepBlue,  //1093
		DeepPurple,    //1094
		DeepViolet,    //1095
		DeepPink,  //1096
		Black,  //1097
		White,  //1098
		Gray,   //1099
		Brown,  //1966
		Shadow,  //1967
		Negative,  //1966
	}



	public class HueTileMap : Dictionary<int, Dictionary<int, Paints>> {
		public bool HasHue( int tile_x, int tile_y ) {
			return this.ContainsKey( tile_x ) && this[tile_x].ContainsKey( tile_y );
		}

		public Paints GetHue( int tile_x, int tile_y ) {
			if( !this.ContainsKey( tile_x ) || !this[tile_x].ContainsKey( tile_y ) ) {
				return Paints.None;
			}
			return this[tile_x][tile_y];
		}

		public void AddHue( int tile_x, int tile_y, Paints hue ) {
			Tile tile = Framing.GetTileSafely( tile_x, tile_y );
			if( TileHelpers.IsAir( tile ) ) { return; }

			tile.color( (byte)hue );

			if( !this.ContainsKey( tile_x ) ) {
				this.Add( tile_x, new Dictionary<int, Paints>() );
			}
			this[tile_x][tile_y] = hue;
		}


		public void FindRandomTile( out int tile_x, out int tile_y ) {
			do {
				tile_x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				tile_y = WorldGen.genRand.Next( 0, Main.maxTilesY );
			} while( TileHelpers.IsAir( Framing.GetTileSafely( tile_x, tile_y ) ) );
		}
	}



	public static class ColorPicker {
		public static Paints GetRandomColor() {
			int colors = Enum.GetNames( typeof( Paints ) ).Length;
			return (Paints)Main.rand.Next( 1, colors - 2 );
		}


		public static string GetName( Paints hue ) {
			return Enum.GetNames( typeof( Paints ) )[(int)hue];
		}
	}
}

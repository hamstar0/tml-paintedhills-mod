using HamstarHelpers.TileHelpers;
using PaintedHills.Colorers;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace PaintedHills.Painters {
	public static class AreaPainter {
		public static void PeekAdjacentClearTiles( HueTileMap huemap, IDictionary<int, ISet<int>> peek, int tile_x, int tile_y ) {
			if( TileHelpers.IsAir( Framing.GetTileSafely(tile_x, tile_y) ) ) {
				return;
			}

			int min_x = Math.Max( tile_x - 1, 0 );
			int max_x = Math.Min( tile_x + 1, Main.mapMaxX - 1 );
			int min_y = Math.Max( tile_y - 1, 0 );
			int max_y = Math.Min( tile_y + 1, Main.mapMaxY - 1 );

			int x = tile_x;
			int y = tile_y - 1;
			if( y >= 0 ) {
				if( !huemap.HasHue(x, y) && !TileHelpers.IsAir( Framing.GetTileSafely(x, y) ) ) {
					if( !peek.ContainsKey( x ) ) {
						peek[x] = new HashSet<int>();
					}
					peek[x].Add( y );
				}
			}

			x -= 1;
			y += 1;
			if( x >= 0 ) {
				if( !huemap.HasHue(x, y) && !TileHelpers.IsAir( Framing.GetTileSafely(x, y) ) ) {
					if( !peek.ContainsKey( x ) ) {
						peek[x] = new HashSet<int>();
					}
					peek[x].Add( y );
				}
			}

			x += 2;
			if( x < Main.mapMaxX ) {
				if( !huemap.HasHue(x, y) && !TileHelpers.IsAir( Framing.GetTileSafely(x, y) ) ) {
					peek[x].Add( y );
				}
			}

			x -= 1;
			y += 1;
			if( y < Main.mapMaxY ) {
				if( !huemap.HasHue(x, y) && !TileHelpers.IsAir( Framing.GetTileSafely(x, y) ) ) {
					if( !peek.ContainsKey( x ) ) {
						peek[x] = new HashSet<int>();
					}
					peek[x].Add( y );
				}
			}
		}


		public static void PaintFlood( PaintedHillsMod mymod, Colorer colorer, int tile_x, int tile_y, Paints hue ) {
			var peek = new Dictionary<int, ISet<int>>();

			peek[ tile_x ] = new HashSet<int>();
			peek[ tile_x ].Add( tile_y );
			
			do {
				foreach( int x in peek.Keys ) {
					foreach( int y in peek[x] ) {
						colorer.ColorTile( mymod, x, y );
					}
				}

				int[] peek_x = peek.Keys.ToArray();
				foreach( int x in peek_x ) {
					int[] peek_y = peek[x].ToArray();

					foreach( int y in peek_y ) {
						AreaPainter.PeekAdjacentClearTiles( colorer.HueMap, peek, x, y );
						peek[x].Remove( y );
					}
					if( peek[x].Count == 0 ) {
						peek.Remove( x );
					}
				}
			} while( peek.Count > 0 );
		}
	}
}

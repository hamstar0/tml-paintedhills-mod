using Microsoft.Xna.Framework;
using Terraria;


namespace PaintedHills.Colorers {
	public class FadingColorer : Colorer {
		public FadingColorer( HueTileMap huemap, Paints hue ) : base( huemap, hue ) { }

		
		public override void ColorTileLine( PaintedHillsMod mymod, int begin_tile_x, int begin_tile_y, int end_tile_x, int end_tile_y ) {
			var beg_pos = new Vector2( begin_tile_x, begin_tile_y ) * 16f;
			var end_pos = new Vector2( end_tile_x, end_tile_y ) * 16f;
			float length = Vector2.Distance( beg_pos, end_pos );

			var paint_func = new Utils.PerLinePoint( delegate ( int tile_x_at, int tile_y_at ) {
				if( tile_x_at < 0 || tile_x_at >= Main.maxTilesX || tile_y_at < 0 || tile_y_at > Main.maxTilesY ) {
					return false;
				}

				var at_pos = new Vector2( tile_x_at, tile_y_at ) * 16f;
				float percent = Vector2.Distance( beg_pos, at_pos ) / length;

				if( Main.rand.NextFloat() > percent ) {
					if( !this.HueMap.HasHue( tile_x_at, tile_y_at ) ) {
						this.ColorTile( mymod, tile_x_at, tile_y_at );
					}
				}
				return true;
			} );

			Utils.PlotTileLine( beg_pos, end_pos, 3, paint_func );
		}


		public override void ColorTileRay( PaintedHillsMod mymod, int origin_tile_x, int origin_tile_y, float radians, float tile_length ) {
			var tile_origin = new Vector2( origin_tile_x, origin_tile_y );
			Vector2 unit_rotated = Vector2.UnitX.RotatedBy( radians );
			Vector2 beg_tile_offset = unit_rotated * 1f;
			Vector2 end_tile_offset = unit_rotated * (tile_length - 1f);
			Vector2 beg_pos = (tile_origin + beg_tile_offset) * 16;
			Vector2 end_pos = (tile_origin + end_tile_offset) * 16;

			var paint_func = new Utils.PerLinePoint( delegate ( int tile_x_at, int tile_y_at ) {
				if( tile_x_at < 0 || tile_x_at >= Main.maxTilesX || tile_y_at < 0 || tile_y_at > Main.maxTilesY ) {
					return false;
				}

				var at_pos = new Vector2( tile_x_at, tile_y_at ) * 16f;
				float percent = Vector2.Distance( beg_pos, at_pos ) / (tile_length * 16f);

				if( Main.rand.NextFloat() > percent ) {
					if( !this.HueMap.HasHue( tile_x_at, tile_y_at ) ) {
						this.ColorTile( mymod, tile_x_at, tile_y_at );
					}
				}
				return true;
			} );

			Utils.PlotTileLine( beg_pos, end_pos, 3, paint_func );
		}
	}
}

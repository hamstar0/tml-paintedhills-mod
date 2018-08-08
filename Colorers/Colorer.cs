using HamstarHelpers.Helpers.TileHelpers;
using Microsoft.Xna.Framework;
using Terraria;


namespace PaintedHills.Colorers {
	public class Colorer {
		public HueTileMap HueMap { get; private set; }
		public Paints Hue { get; private set; }


		public Colorer( HueTileMap huemap, Paints hue ) {
			this.HueMap = huemap;
			this.Hue = hue;
		}

		public void SetHue( Paints hue ) {
			this.Hue = hue;
		}


		public virtual void ColorTile( PaintedHillsMod mymod, int tile_x, int tile_y ) {
			if( TileHelpers.IsAir( Framing.GetTileSafely( tile_x, tile_y ) ) ) { return; }

			this.HueMap.AddHue( tile_x, tile_y, this.Hue );
		}

		public virtual void ColorTileLine( PaintedHillsMod mymod, int begin_tile_x, int begin_tile_y, int end_tile_x, int end_tile_y ) {
			var paint_func = new Utils.PerLinePoint( delegate ( int tile_x_at, int tile_y_at ) {
				if( tile_x_at < 0 || tile_x_at >= Main.maxTilesX || tile_y_at < 0 || tile_y_at > Main.maxTilesY ) {
					return false;
				}
				if( !this.HueMap.HasHue( tile_x_at, tile_y_at ) ) {
					this.ColorTile( mymod, tile_x_at, tile_y_at );
				}
				return true;
			} );

			var beg_pos = new Vector2( begin_tile_x * 16f, begin_tile_y * 16f );
			var end_pos = new Vector2( end_tile_x * 16f, end_tile_y * 16f );

			Utils.PlotTileLine( beg_pos, end_pos, 3, paint_func );
		}


		public virtual void ColorTileRay( PaintedHillsMod mymod, int origin_tile_x, int origin_tile_y, float radians, float tile_length ) {
			var tile_origin = new Vector2( origin_tile_x, origin_tile_y );
			var paint_func = new Utils.PerLinePoint( delegate ( int tile_x_at, int tile_y_at ) {
				if( tile_x_at < 0 || tile_x_at >= Main.maxTilesX || tile_y_at < 0 || tile_y_at > Main.maxTilesY ) {
					return false;
				}
				if( !this.HueMap.HasHue( tile_x_at, tile_y_at ) ) {
					this.ColorTile( mymod, tile_x_at, tile_y_at );
				}
				return true;
			} );

			Vector2 unit_rotated = Vector2.UnitX.RotatedBy( radians );
			Vector2 beg_tile_offset = unit_rotated * 1f;
			Vector2 end_tile_offset = unit_rotated * (tile_length - 1f);
			Vector2 beg_pos = (tile_origin + beg_tile_offset) * 16;
			Vector2 end_pos = (tile_origin + end_tile_offset) * 16;

			Utils.PlotTileLine( beg_pos, end_pos, 3, paint_func );
		}
	}
}

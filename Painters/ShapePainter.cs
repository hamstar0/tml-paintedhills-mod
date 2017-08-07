using HamstarHelpers.Utilities.Config;
using PaintedHills.Colorers;
using System.Collections.Generic;
using Terraria.ModLoader;


namespace PaintedHills.Painters {
	public static class ShapePainter {
		public static void PaintBlob( PaintedHillsMod mymod, Colorer colorer, int tile_x, int tile_y, float size, float size_variance, float shape_variance ) {
			IDictionary<float, float> rays = RayPainter.GetChunkRays( size, size_variance, shape_variance );
			
			//if( (mymod.DEBUGFLAGS & 1) != 0 ) {
			//	ErrorLogger.Log( JsonConfig<IDictionary<float, float>>.Serialize( rays ) );
			//}

			//PaintRays.PaintRadiationEdges( huemap, tile_x, tile_y, rays, hue, new Painter(PaintBasic.PaintTile) );
			//foreach( float rad in rays.Keys ) {
			//	PaintRays.PaintRay( huemap, tile_x, tile_y, rad, rays[rad], hue, new Painter( PaintArea.PaintFlood ) );
			//}
			RayPainter.PaintRadiation( mymod, colorer, tile_x, tile_y, rays );
		}
	}
}

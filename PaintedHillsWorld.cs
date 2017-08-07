using HamstarHelpers.TileHelpers;
using PaintedHills.Colorers;
using PaintedHills.Painters;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;


namespace PaintedHills {
	class PaintedHillsWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			var mymod = (PaintedHillsMod)this.mod;
			float size = mymod.Config.Data.HueBlobMinimumTileRadius;
			float size_variance = mymod.Config.Data.HueBlobSizeVariance;
			float shape_variance = mymod.Config.Data.HueBlobShapeVariance;
			int idx = tasks.FindIndex( genpass => genpass.Name.Equals( "Micro Biomes" ) );

			if( idx != -1 ) {
				tasks.Insert( idx + 1, new PassLegacy( "Painting Hills", delegate ( GenerationProgress progress ) {
					progress.Message = "Painting Hills";

					float chunk_size = 300f * 300f;
					float chunks = ((float)Main.maxTilesX * (float)Main.maxTilesY) / chunk_size;
					chunks *= mymod.Config.Data.HueBlobQuantityMultiplier;

					if( (mymod.DEBUGFLAGS & 1) != 0 ) {
						ErrorLogger.Log( "chunks: " + chunks );
					}

					for( int i=0; i<chunks; i++ ) {
						int x, y;
						var huemap = new HueTileMap();
						var colorer = new FadingColorer( huemap, Paints.None );
						Paints hue = ColorPicker.GetRandomColor();

						huemap.FindRandomTile( out x, out y );
						colorer.SetHue( hue );

						if( (mymod.DEBUGFLAGS & 1) != 0 ) {
							ErrorLogger.Log( "  painting blob ("+(i+1)+" of "+chunks+"): " + x + ", " + y + " " + ColorPicker.GetName(hue) );
						}

						ShapePainter.PaintBlob( mymod, colorer, x, y, size, size_variance, shape_variance );

						progress.Set( (float)i / chunks );
					}
				} ) );
			}
		}
	}
}

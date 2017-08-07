using Microsoft.Xna.Framework;
using PaintedHills.Colorers;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PaintedHills.Painters {
	public static class RayPainter {
		public static float GetNextRayLength( float base_length, float prev_length, float shape_variance ) {
			float offset = Main.rand.NextFloat() * shape_variance;
			float new_length = (prev_length - (shape_variance/2f)) + offset;

			float clamped_length = new_length > (base_length - (shape_variance/2f)) ? new_length : (base_length - (shape_variance/2f));
			clamped_length = clamped_length > 10f ? clamped_length : 10f;

			return (clamped_length + (base_length * 0.1f)) / 1.1f;
		}


		public static float GetRadianAt( float i_of_60 ) {
			return (i_of_60 / 60f) * (float)(Math.PI) * 2f;
		}


		public static IDictionary<float, float> GetChunkRays( float minimium_length, float size_variance, float shape_variance ) {
			var ray_rads_and_lengths = new Dictionary<float, float>();
			float base_length = minimium_length + (Main.rand.NextFloat() * size_variance);
			
			float prev_length = base_length;

			for( int i = 0; true; i++ ) {
				float rad = RayPainter.GetRadianAt( i % 60 );
				float curr_length = RayPainter.GetNextRayLength( base_length, prev_length, shape_variance );
				
				if( ray_rads_and_lengths.ContainsKey( rad ) ) {
					if( Math.Abs(ray_rads_and_lengths[rad] - curr_length) <= (shape_variance / 2f) ) {
						break;
					}
				}

				ray_rads_and_lengths[rad] = curr_length;
				prev_length = curr_length;
			}

			return ray_rads_and_lengths;
		}


		
		public static void PaintRadiation( PaintedHillsMod mymod, Colorer colorer, int tile_x, int tile_y, IDictionary<float, float> ray_rads_and_lengths ) {
			float steps = mymod.Config.Data.HueBlobMinimumTileRadius;
			var rays = new SortedSet<float>( ray_rads_and_lengths.Keys );

			for( int i=0; i < 60; i++ ) {
				float curr_rad = RayPainter.GetRadianAt( i % 60 );
				float next_rad = RayPainter.GetRadianAt( (i + 1) % 60 );

				float curr_range = ray_rads_and_lengths[ curr_rad ];
				float range_span = curr_range - ray_rads_and_lengths[ next_rad ];
//ErrorLogger.Log( "x: "+tile_x+", y: "+tile_y+", rad: "+curr_rad+", nextrad: "+next_rad+", range: "+curr_range+", span: "+range_span);

				for( float j=0; j<steps; j+=1f ) {
					float step_rad = curr_rad + RayPainter.GetRadianAt( j / steps );
					float step_range = curr_range + (range_span * (j / steps));

					colorer.ColorTileRay( mymod, tile_x, tile_y, step_rad, step_range );
				}
			}
		}


		public static void PaintRadiationEdges( PaintedHillsMod mymod, Colorer colorer, int tile_x, int tile_y, IDictionary<float, float> ray_rads_and_lengths ) {
			Vector2 tile_origin = new Vector2( tile_x, tile_y );
			Vector2 beg_pos = Vector2.Zero;
			Vector2 end_pos = Vector2.Zero;

			SortedSet<float> keys = new SortedSet<float>( ray_rads_and_lengths.Keys );

			for( int i=0; i < 60; i++ ) {
				float curr_rad = RayPainter.GetRadianAt( i % 60 );
				float next_rad = RayPainter.GetRadianAt( (i + 1) % 60 );
				
				beg_pos = tile_origin + (Vector2.UnitX.RotatedBy( curr_rad ) * ray_rads_and_lengths[curr_rad]);
				end_pos = tile_origin + (Vector2.UnitX.RotatedBy( next_rad ) * ray_rads_and_lengths[next_rad]);

				double add_rad = Math.Atan2( end_pos.Y - beg_pos.Y, end_pos.X - beg_pos.X );
				end_pos += Vector2.UnitX.RotatedBy( add_rad ) * 2f;

				colorer.ColorTileLine( mymod, (int)beg_pos.X, (int)beg_pos.Y, (int)end_pos.X, (int)end_pos.Y );
//ErrorLogger.Log( "PaintRadiationEdges x:"+tile_x+", y:"+tile_y+", rad:"+rad+", len:"+ray_rads_and_lengths[rad]+", offset:"+curr_tile_offset.ToString());
			}
		}
	}
}

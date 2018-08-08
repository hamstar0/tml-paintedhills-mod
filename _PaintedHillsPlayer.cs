/*using HamstarHelpers.PlayerHelpers;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;


namespace PaintedHills {
	class PaintedHillsPlayer : ModPlayer {
		public override void PreUpdate() {
			if( Main.mouseRight && Main.mouseRightRelease ) {
				var xs = PaintedHillsWorld.HueMap.Keys.ToArray();
				int x = xs[Main.rand.Next( 0, xs.Length - 1 )];
				var ys = PaintedHillsWorld.HueMap[x].Keys.ToArray();
				int y = xs[Main.rand.Next( 0, ys.Length - 1 )];

				Vector2 rand_pos = new Vector2( 16 * x, 16 * y );
				PlayerHelpers.Teleport( this.player, rand_pos );
Main.NewText( "teleporting to "+x+":"+y);
			}
		}
	}
}*/

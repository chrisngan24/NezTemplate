using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;


namespace NezGame
{
    public class Game1 : Nez.Core
    {
        protected override void Initialize()
        {
            base.Initialize();
            Scene = new HelloScene();
        }

    }
}

using System;

using Nez;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Nez.Systems;

namespace NezGame
{
    public class Player: Component, IUpdatable
    {
        VirtualIntegerAxis _axisInput;
        Mover mover;

        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        BoxCollider _collider;


        float speed = 100f;

        public void SetupInput()
        {
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right)
                .AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D);
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down)
                .AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S);
        }

        public override void OnAddedToEntity()
        {
            Console.Write("PLAYER");
            base.OnAddedToEntity();
            var texture = Entity.Scene.Content.Load<Texture2D>("Sprites/Characters/charactersheet");
            // frames of all the sprites are the same size... 16 x 16
            var sprites = Sprite.SpritesFromAtlas(texture, 16, 16);
            SpriteAnimator animator = this.AddComponent<SpriteAnimator>();
            animator.AddAnimation("walkDown", new[] { sprites[0], sprites[1], sprites[0], sprites[2] }, 1.0f);
            animator.Play("walkDown");
            mover = this.AddComponent(new Mover());
            this.AddComponent<CircleCollider>();
            

            //_collider = this.AddComponent(new BoxCollider());
            SetupInput();    
        }

        public void Update()
        {
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            moveDir = moveDir * speed * Time.DeltaTime;
            mover.CalculateMovement(ref moveDir, out var res);
            
                
            mover.ApplyMovement(moveDir);
        }

        //public override void Update()
        //{
        //    var kstate = Keyboard.GetState();            
        //    base.Update();

        //}
    }

    public class Robot : Component, IUpdatable
    {
        VirtualIntegerAxis _axisInput;
        Mover mover;

        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        float speed = 500f;

        public void SetupInput()
        {
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right);
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down)
                .AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S);
        }

        public override void OnAddedToEntity()
        {
            Console.Write("PLAYER");
            base.OnAddedToEntity();
            var texture = Entity.Scene.Content.Load<Texture2D>("Sprites/Characters/charactersheet");
            // frames of all the sprites are the same size... 16 x 16
            var sprites = Sprite.SpritesFromAtlas(texture, 16, 16);
            SpriteAnimator animator = this.AddComponent<SpriteAnimator>();
            animator.AddAnimation("walkDown", new[] { sprites[12], sprites[13], sprites[12], sprites[14] }, 1.0f);
            animator.Play("walkDown");
            mover = this.AddComponent(new Mover());
            this.AddComponent<CircleCollider>();
            SetupInput();
        }

        public void Update()
        {
            
        }

    }


    public class HelloScene: Scene
    {
        public HelloScene()
        {

            // setup a pixel perfect screen that fits our map
            SetDesignResolution(512, 256, SceneResolutionPolicy.ExactFit);
            Screen.SetSize(512 * 3, 256 * 3);


            var playerEntity = CreateEntity("player", new Microsoft.Xna.Framework.Vector2(Screen.Width /4, Screen.Height/4));
            playerEntity.AddComponent(new Player());
            Camera.Entity.AddComponent(new FollowCamera(playerEntity));
            Console.Write("HELLO SCENE");
            var enemyEntity= CreateEntity("enemy", new Microsoft.Xna.Framework.Vector2(Screen.Width / 4-20, Screen.Height / 4-20));
            enemyEntity.AddComponent(new Robot());
            Console.Write("HELLO SCENE");
            var manager = new NezContentManager();
            var map = manager.LoadTiledMap("Content/Tiles/tilemap.tmx");
            //Core.Content.LoadTiledMap()
            var tiledEntity = CreateEntity("tiled-map-entity");
            //var map = Content.LoadTiledMap("Content/Tiles/tilemap.tmx");
            var tiledMapRenderer = tiledEntity.AddComponent(new TiledMapRenderer(map, "collision"));
            tiledMapRenderer.SetLayersToRender(new[] { "tiles", "terrain", "details", "collision" });
            tiledMapRenderer.RenderLayer = 10;

            // render our above-details layer after the player so the player is occluded by it when walking behind things
            // render things like tops of trees
            var tiledMapDetailsComp = tiledEntity.AddComponent(new TiledMapRenderer(map));
            tiledMapDetailsComp.SetLayerToRender("above-details");
            tiledMapDetailsComp.RenderLayer = -1;
            // have the camera follow the player
         


        }



    }
}

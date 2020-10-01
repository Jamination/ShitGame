using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShitGame.Components;
using ShitGame.Levels;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

namespace ShitGame
{
    public static class Functions
    {
        public static void Draw(ref Sprite sprite, ref Transform transform)
        {
            var centerOrigin = Vector2.Zero;

            if (sprite.Centered)
                centerOrigin = sprite.Texture.Bounds.Size.ToVector2() * .5f;

            Data.SpriteBatch.Draw(
                sprite.Texture,
                new Vector2((int)transform.Position.X, (int)transform.Position.Y),
                sprite.SourceRect,
                sprite.Colour,
                transform.Rotation,
                centerOrigin + sprite.Origin,
                transform.Scale,
                sprite.Effects,
                sprite.Depth
            );
        }

        public static void Draw(ref Sprite sprite, Vector2 position, Vector2 scale, float rotation = 0f)
        {
            var centerOrigin = Vector2.Zero;

            if (sprite.Centered)
                centerOrigin = sprite.Texture.Bounds.Size.ToVector2() * .5f;

            Data.SpriteBatch.Draw(
                sprite.Texture,
                new Vector2((int)position.X, (int)position.Y),
                sprite.SourceRect,
                sprite.Colour,
                rotation,
                centerOrigin + sprite.Origin,
                scale,
                sprite.Effects,
                sprite.Depth
            );
        }

        public static void Draw(ref Sprite sprite, Body body, Vector2 scale)
            => Draw(ref sprite, Functions.FromSim(body.Position), scale, body.Rotation);

        public static void Draw(ref Text text, ref Transform transform)
        {
            var centerOrigin = Vector2.Zero;

            if (text.Centered)
                centerOrigin = text.SpriteFont.MeasureString(text.Message) * .5f;

            Data.SpriteBatch.DrawString(
                text.SpriteFont,
                text.Message,
                transform.Position,
                text.Colour,
                transform.Rotation,
                centerOrigin,
                transform.Scale,
                text.Effects,
                text.Depth
            );
        }

        public static void LoadLevel(LevelType level)
        {
            Data.CurrentLevel = level;
            Pool.Reset();
            switch (level) {
                case LevelType.Level1:
                    Level_1.Load();
                    break;
            }
        }

        public static void SetPlayerSpawnPoint(float x, float y) => Data.PlayerSpawnPoint = new Vector2(x, y);

        public static uint PlaceStaticObject(float x, float y, ObjectType type)
        {
            ref var staticObject = ref Pool.GameObjects_Static[Pool.GetInactiveGameObject_Static()];
            staticObject.Type = type;

            switch (type) {
                case ObjectType.Wall:
                    staticObject.Sprite.Texture = Data.Texture_Wall;
                    staticObject.Sprite.Centered = true;
                    staticObject.Body = Data.World.CreateRectangle(Functions.ToSim(32 * 4), Functions.ToSim(32 * 4), 1f, new Vector2(Functions.ToSim(x), Functions.ToSim(y)), 0f, BodyType.Static);
                    staticObject.Body.Enabled = true;
                    staticObject.Body.Tag = new StaticObjectTag(staticObject.ID);
                    break;
            }

            return staticObject.ID;
        }
        
        public static bool Intersects(Hitbox HB1, Vector2 pos1, Hitbox HB2, Vector2 pos2) =>
            new Rectangle((pos1 + HB1.Offset).ToPoint(), HB1.Size.ToPoint()).Intersects(
                new Rectangle((pos2 + HB2.Offset).ToPoint(), HB2.Size.ToPoint()));

        public static Rectangle Intersect(Hitbox HB1, Vector2 pos1, Hitbox HB2, Vector2 pos2) => Rectangle.Intersect(
            new Rectangle((pos1 + HB1.Offset).ToPoint(), HB1.Size.ToPoint()),
            new Rectangle((pos2 + HB2.Offset).ToPoint(), HB2.Size.ToPoint()));
        
        public static Rectangle GetBounds(ref Hitbox hitbox, ref Transform transform) => new Rectangle(
            transform.Position.ToPoint() + hitbox.Offset.ToPoint(), hitbox.Size.ToPoint() * transform.Scale.ToPoint());
        
        public static uint PlaceZombie(float x, float y, ZombieType zombieType)
        {
            uint id = Zombies.GetInactiveZombie();
            
            Zombies.Active[id] = true;
            Zombies.Types[id] = zombieType;

            Zombies.Sprites[id] = new Sprite();
            Zombies.Sprites[id].Centered = true;
            Zombies.Sprites[id].Colour = Color.White;
            Zombies.Sprites[id].Texture = Data.Texture_Zombie;

            Zombies.Bodies[id] = Data.World.CreateCircle(Functions.ToSim(16 * 4), 1f, new Vector2(Functions.ToSim(x), Functions.ToSim(y)), BodyType.Dynamic);
            Zombies.Bodies[id].LinearDamping = 2f;
            Zombies.Bodies[id].AngularDamping = 2f;
            Zombies.Bodies[id].FixedRotation = true;
            Zombies.Bodies[id].Tag = new ZombieTag(id);
            
            return id;
        }

        public static void SaveLevel(LevelType level)
        {
            Data.PlayerSpawnPoint = new Vector2((int)Functions.FromSim(Players.Bodies[Players.LocalID].Position.X),
                Functions.FromSim((int)Players.Bodies[Players.LocalID].Position.Y));
            string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string levelPath = baseDirectory.Remove(baseDirectory.Length - 41) + "Game/Levels";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("namespace ShitGame.Levels\n");
            stringBuilder.Append("{\n");
            stringBuilder.Append($"\tpublic static class Level_{(uint)level}\n");
            stringBuilder.Append("\t{\n");
            stringBuilder.Append("\t\tpublic static void Load()\n");
            stringBuilder.Append("\t\t{\n");
            stringBuilder.Append($"\t\t\tFunctions.SetPlayerSpawnPoint({Data.PlayerSpawnPoint.X}, {Data.PlayerSpawnPoint.Y});\n");
            stringBuilder.Append(CollectLevelObjects());
            stringBuilder.Append("\t\t}\n");
            stringBuilder.Append("\t}\n");
            stringBuilder.Append("}");
            File.WriteAllText($"{levelPath}\\Level{(uint)level}.cs", stringBuilder.ToString());
        }

        public static string CollectLevelObjects()
        {
            var stringBuilder = new StringBuilder();
            for (uint i = 0; i < Pool.GameObjects_Static.Length; ++i) {
                if (Pool.GameObjects_Static[i].Active && Pool.GameObjects_Static[i].Type != ObjectType.Undefined)
                    stringBuilder.Append("\t\t\tFunctions.PlaceStaticObject(" + (int)Functions.FromSim(Pool.GameObjects_Static[i].Body.Position.X) + ", " + (int)Functions.FromSim(Pool.GameObjects_Static[i].Body.Position.Y) + ", ObjectType." + Pool.GameObjects_Static[i].Type + ");\n");
            }
            for (uint i = 0; i < Zombies.MaxZombies; ++i) {
                if (Zombies.Active[i])
                    stringBuilder.Append("\t\t\tFunctions.PlaceZombie(" + (int)Functions.FromSim(Zombies.Bodies[i].Position.X) + ", " + (int)Functions.FromSim(Zombies.Bodies[i].Position.Y) + ", ZombieType." + Zombies.Types[i] + ");\n");
            }
            return stringBuilder.ToString();
        }

        public static uint ConvertBodyToStaticObject(Body body)
        {
            if (body.Tag is StaticObjectTag)
            {
                return ((StaticObjectTag) body.Tag).ID;
            }
            else if (body.Tag is ZombieTag)
            {
                return ((ZombieTag) body.Tag).ID;
            }
            throw new Exception("Could not convert body to static object.");
        }

        public static void DrawObjects()
        {
            for (uint i = 0; i < Pool.GameObjects_Static.Length; i++) {
                if (Pool.GameObjects_Static[i].Active)
                    Functions.Draw(ref Pool.GameObjects_Static[i].Sprite,
                        Functions.FromSim(Pool.GameObjects_Static[i].Body.Position), Vector2.One * 4,
                        Pool.GameObjects_Static[i].Body.Rotation);
            }
        }
        
        public static float FromSim(float simUnits) => simUnits * Data.PIXELS_PER_METER;
        public static float FromSim(double simUnits) => (float)(simUnits * Data.PIXELS_PER_METER);
        public static float FromSim(int simUnits) => simUnits * Data.PIXELS_PER_METER;
        public static Vector2 FromSim(Vector2 simUnits) => simUnits * Data.PIXELS_PER_METER;
        public static float ToSim(float displayUnits) => displayUnits * Data.SIM_UNITS_PER_PIXEL;
        public static float ToSim(double displayUnits) => (float)(displayUnits * Data.SIM_UNITS_PER_PIXEL);
        public static float ToSim(int displayUnits) => displayUnits * Data.SIM_UNITS_PER_PIXEL;
        public static Vector2 ToSim(Vector2 displayUnits) => displayUnits * Data.SIM_UNITS_PER_PIXEL;
        
        public static void LoadAssets()
        {
            Data.DebugView.LoadContent(Data.Graphics.GraphicsDevice, Data.Content, new PrimitiveBatch(Data.Graphics.GraphicsDevice));

            Data.ButtonFont = Data.Content.Load<SpriteFont>("Fonts/ButtonFont");
            Data.SmallFont = Data.Content.Load<SpriteFont>("Fonts/SmallFont");

            Data.Texture_Player = Data.Content.Load<Texture2D>("Sprites/Player");
            Data.Texture_Wall = Data.Content.Load<Texture2D>("Sprites/Wall_1x1");
            Data.Texture_Zombie = Data.Content.Load<Texture2D>("Sprites/zombie");
            Data.Texture_Background_Level1 = Data.Content.Load<Texture2D>("Sprites/Background_Level1");
        }

        public static void DrawDebug()
        {
            Data.DebugView.BeginCustomDraw(Matrix.Identity, Camera.Transform, BlendState.NonPremultiplied,
                SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullClockwise);
            for (int i = 0; i < Pool.GameObjects_Static.Length; i++)
            {
                if (Pool.GameObjects_Static[i].Active)
                {
                    Data.DebugView.DrawShape(Pool.GameObjects_Static[i].Body.FixtureList[0], Pool.GameObjects_Static[i].Body.GetTransform(), Color.White);
                }
            }
            Data.DebugView.EndCustomDraw();
        }

        public static T Choose<T>(params T[] list) => list[Data.Random.Next(0, list.ToArray().Length)];

        public static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh) =>
            (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;

        public static Vector2 ScreenToWorld(Vector2 onScreen) =>
            Vector2.Transform(onScreen, Matrix.Invert(Camera.Transform));
        
        public static float Lerp (float source, float destination, float amount) => source + (destination - source) * amount;
        
        public static float WrapAngle (float angle)
        {
            const float TwoPi = MathF.PI * 2;
            if (angle > -MathF.PI && angle <= MathF.PI)
                return angle;
            angle %= TwoPi;
            if (angle <= -MathF.PI)
                return angle + TwoPi;
            if (angle > MathF.PI)
                return angle - TwoPi;
            return angle;
        }
        
        public static float LerpAngle (float source, float destination, float amount)
        {
            const float TwoPi = MathF.PI * 2;
            if (destination < source) {
                var c = destination + TwoPi;
                return WrapAngle (c - source > source - destination ? Lerp (source, destination, amount) : Lerp (source, c, amount));
            } else if (destination > source) {
                var c = destination - TwoPi;
                return WrapAngle (destination - source > source - c ? Lerp (source, c, amount) : Lerp (source, destination, amount));
            }
            return source;
        }
    }
}
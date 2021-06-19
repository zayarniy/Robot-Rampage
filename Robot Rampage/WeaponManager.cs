using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Robot_Rampage
{
    static class WeaponManager
    {
        #region declarations
        static public List<Particle> Shots = new List<Particle>();
        static public Texture2D Texture;
        static public Rectangle shotRectangle =
        new Rectangle(0, 128, 32, 32);
        static public float WeaponSpeed = 600f;
        static private float shotTimer = 0f;
        static private float shotMinTimer = 0.15f;
        #endregion
        #region Properties
        static public float WeaponFireDelay
        {
            get
            {
                return shotMinTimer;
            }
        }
        static public bool CanFireWeapon
        {
            get
            {
                return (shotTimer >= WeaponFireDelay);
            }
        }
        #endregion

        #region Effects Management Methods
        private static void AddShot(
        Vector2 location,
        Vector2 velocity,
        int frame)
        {
            Particle shot = new Particle(
            location,
            Texture,
            shotRectangle,
            velocity,
            Vector2.Zero,
            400f,
            120,
            Color.White,
            Color.White);
            shot.AddFrame(new Rectangle(
            shotRectangle.X + shotRectangle.Width,
            shotRectangle.Y,
            shotRectangle.Width,
            shotRectangle.Height));
            shot.Animate = false;
            shot.Frame = frame;
            shot.RotateTo(velocity);
            Shots.Add(shot);
        }
        #endregion
        #region Weapons Management Methods
        public static void FireWeapon(Vector2 location, Vector2 velocity)
        {
            AddShot(location, velocity, 0);
            shotTimer = 0.0f;
        }
        #endregion

        #region Update and Draw
        static public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shotTimer += elapsed;
            for (int x = Shots.Count - 1; x >= 0; x--)
            {
                Shots[x].Update(gameTime);
                if (Shots[x].Expired)
                {
                    Shots.RemoveAt(x);
                }
            }
        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle sprite in Shots)
            {
                sprite.Draw(spriteBatch);
            }
        }
        #endregion


    }
}

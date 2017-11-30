using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;

namespace Screens
{
    class SplashScreen
    {
        Texture2D _tx;
        public bool Active { get; set; }

        public Texture2D Tx
        {
            get
            {
                return _tx;
            }

            set
            {
                _tx = value;
            }
        }
        public Song BackingTrack { get; set; }
        public SoundEffectInstance  SoundPlayer { get; set; }
        public Vector2 Position { get; set; }

        public Keys ActivationKey;


        public SplashScreen(Vector2 pos, Texture2D tx, Song sound, Keys key)
        {
            _tx = tx;
            BackingTrack = sound;
            Position = pos;
            ActivationKey = key;
        }

       

        public void Update()
        {
        if(InputEngine.IsKeyPressed(ActivationKey))
                Active = !Active;
            if (Active)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(BackingTrack);
                }
            }
         if(!Active)
                {
                    if (MediaPlayer.State == MediaState.Playing)
                    {
                        MediaPlayer.Stop();
                        // Could do resume and Pause if you want Media player state
                    }
                }
        }
        public void Draw(SpriteBatch sp)
        {
            if (Active)
            {
                sp.Draw(_tx, new Rectangle(Position.ToPoint(), new Point(
                    Helper.graphicsDevice.Viewport.Bounds.Width,
                    Helper.graphicsDevice.Viewport.Bounds.Height)),Color.White);
            }
        }

    }
}

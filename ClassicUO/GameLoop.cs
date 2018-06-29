﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ClassicUO.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClassicUO
{
    internal class GameLoop : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public GameLoop()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 144.0f);
           // IsFixedTimeStep = false;


            _graphics = new GraphicsDeviceManager(this);

            Log.Message(LogTypes.Trace, "Gameloop initialized.");
        }

        private Game.Map.Facet _facet;

        protected override void Initialize()
        {
          

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TEST


            AssetsLoader.FileManager.UoFolderPath = @"E:\Giochi\Ultima Online Classic ORION";

            _stopwatch = Stopwatch.StartNew();
            Log.Message(LogTypes.Trace, "Loading UO files...");
            AssetsLoader.FileManager.LoadFiles();
            Log.Message(LogTypes.Trace, "UO files loaded in " + _stopwatch.ElapsedMilliseconds  +" ms");


            _facet = new Game.Map.Facet(0);

            // END TEST

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }


        const double TIME_RUN_MOUNT = (2d / 20d) * 1000d;
        private DateTime _delay = DateTime.Now;

        private ushort _x = 1443, _y = 1659;
        private ushort _maxX = 1560;
        private ushort _currentX = 1443;
        private Stopwatch _stopwatch;

        protected override void Update(GameTime gameTime)
        {
            //Input.MouseManager.Update();
            //Input.KeyboardManager.Update();

            if (_stopwatch.ElapsedMilliseconds >= TIME_RUN_MOUNT)
            {
                if (_currentX + 1 > _maxX)
                    _currentX = _x;
                _currentX++;

                _facet.LoadChunks(_currentX, _y, 5 );

                //Log.Message(LogTypes.Trace, _stopwatch.ElapsedMilliseconds.ToString());
                _stopwatch.Restart();
               // _delay = DateTime.Now.AddMilliseconds(TIME_RUN_MOUNT);
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
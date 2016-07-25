﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Entity {
        protected Texture2D Texture { get; }
        protected Vector2 position;

        protected int Height { get; }
        protected int Width  { get; }

        protected static bool IsScrolling { get; private set; } = false;
        static float shiftDelta;

        private static ArrayList entities = new ArrayList();

        public Entity(Texture2D texture, double scale, Vector2 position, bool scrollable = true) {
            this.Texture  = texture;
            this.position = position;
            this.Height   = (int) (scale * texture.Height);
            this.Width    = (int) (scale * texture.Width);
            
            if (scrollable) entities.Add(this);
        }

        public Entity(Texture2D texture, int height, int width, Vector2 position, bool scrollable = true) {
            this.Texture  = texture;
            this.position = position;
            this.Height   = height;
            this.Width    = width;

            if (scrollable) entities.Add(this);
        }

        public Rectangle Hitbox() {
            return new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public static void ScrollInit(float shiftDelta) {
            IsScrolling = true;
            Entity.shiftDelta = shiftDelta;
        }


        public static bool ScrollDown() {

            float delta = (float) (Plaper.SHIFT_SPEED * Plaper.gameTime.ElapsedGameTime.TotalSeconds);

            foreach (Entity entity in entities) {
                entity.position.Y += delta;
            }

            shiftDelta -= delta;
            IsScrolling = shiftDelta > 0;

            return IsScrolling;
        }
    }
}
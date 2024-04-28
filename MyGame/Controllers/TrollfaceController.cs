using Microsoft.Xna.Framework;
using System;
using MyGame.Models;

namespace MyGame.Controllers
{
    public class TrollfaceController
    {
        private TrollfaceModel trollfaceModel;
        private Random random;

        public TrollfaceController(TrollfaceModel trollfaceModel)
        {
            this.trollfaceModel = trollfaceModel;
            random = new Random();
        }

        public void Update(GameTime gameTime)
        {
            var direction = new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 - 1);
            direction.Normalize();

            var speed = (float)random.NextDouble() * trollfaceModel.MaxSpeed;

            trollfaceModel.Velocity = direction * speed;

            trollfaceModel.Update(gameTime);
        }
    }
}
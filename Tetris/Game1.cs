using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D textureBlankBox;
        Texture2D textureBlueBox;
        Texture2D textureGreenBox;
        Texture2D textureRedBox;
        Texture2D textureWhiteBox;
        Texture2D textureYellowBox;

        string[,] boxPlane = new string[20, 10] { 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" },
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }, 
            { "n", "n", "b", "n", "b", "b", "n", "n", "b", "n" }
        };

        TimeSpan nextUpdateTime = TimeSpan.FromSeconds(0);
        Block newBlock = new Block();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 650;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            textureBlankBox = Content.Load<Texture2D>("tetris_blank");
            textureBlueBox = Content.Load<Texture2D>("tetris_blue");
            textureGreenBox = Content.Load<Texture2D>("tetris_green");
            textureRedBox = Content.Load<Texture2D>("tetris_red");
            textureWhiteBox = Content.Load<Texture2D>("tetris_white");
            textureYellowBox = Content.Load<Texture2D>("tetris_yellow");

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // Console.WriteLine(gameTime.TotalGameTime);

            if (gameTime.TotalGameTime >= nextUpdateTime) //is doing co 5 s
            {
                nextUpdateTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(1));
                if(newBlock.y <19)
                    newBlock.y += 1;
                /*for (int row = 19; row >= 0; row--)
                {
                    for (int collumn = 9; collumn >= 0; collumn--)
                    {
                        if (boxPlane[row, collumn] == "w")
                        {
                            if (row == 19 || boxPlane[row + 1, collumn] != "n")
                            {
                                boxPlane[row, collumn] = "b";
                            }
                            else
                            {
                                boxPlane[row, collumn] = "n";
                                boxPlane[row + 1, collumn] = "w";
                            }
                        }
                    }
                }    */              
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            //this test if i can show different type of box
            spriteBatch.Draw(textureBlankBox, new Vector2(605, 5), Color.White);
            spriteBatch.Draw(textureWhiteBox, new Vector2(640, 5), Color.White);
            spriteBatch.Draw(textureBlueBox, new Vector2(675, 5), Color.White);
            spriteBatch.Draw(textureGreenBox, new Vector2(605, 40), Color.White);
            spriteBatch.Draw(textureYellowBox, new Vector2(640, 40), Color.White);
            spriteBatch.Draw(textureRedBox, new Vector2(675, 40), Color.White);
            //this drow blank place to play
            /*int x = 5; //value of position Box x,y
            int y = 15;
            for (int row = 0; row < 20; row++)
            {
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    spriteBatch.Draw(textureBlankBox, new Vector2(x, y), Color.White);
                    x += 31;
                }
                y += 31;
                x = 5;
            }*/
            // n = blank, w = white, y = yellow, r = red, g = green, b = blue BOX
           
            int x; //value of position Box x,y
            int y;
            for (int row = 0; row < 20; row++)
            {
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    x = collumn * 31 + 5;
                    y = row * 31 + 15;
                    if (boxPlane[row, collumn] == "n")
                    {
                        spriteBatch.Draw(textureBlankBox, new Vector2(x, y), Color.White);
                    }
                    if (boxPlane[row,collumn] == "w")
                    {
                        spriteBatch.Draw(textureWhiteBox, new Vector2(x, y), Color.White);
                    }
                    if (boxPlane[row, collumn] == "r")
                    {
                        spriteBatch.Draw(textureRedBox, new Vector2(x, y), Color.White);
                    }
                    if (boxPlane[row, collumn] == "g")
                    {
                        spriteBatch.Draw(textureGreenBox, new Vector2(x, y), Color.White);
                    }
                    if (boxPlane[row, collumn] == "b")
                    {
                        spriteBatch.Draw(textureBlueBox, new Vector2(x, y), Color.White);
                    }
                    if (boxPlane[row, collumn] == "y")
                    {
                        spriteBatch.Draw(textureYellowBox, new Vector2(x, y), Color.White);
                    }
                }
                
            }
            
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (newBlock.x + collumn) * 31 + 5;
                    y = (newBlock.y + row) * 31 + 15;

                    if (newBlock.BlockArray[row, collumn] == "w")
                    {
                        spriteBatch.Draw(textureWhiteBox, new Vector2(x, y), Color.White);
                    }
                    if (newBlock.BlockArray[row, collumn] == "r")
                    {
                        spriteBatch.Draw(textureRedBox, new Vector2(x, y), Color.White);
                    }
                    if (newBlock.BlockArray[row, collumn] == "g")
                    {
                        spriteBatch.Draw(textureGreenBox, new Vector2(x, y), Color.White);
                    }
                    if (newBlock.BlockArray[row, collumn] == "b")
                    {
                        spriteBatch.Draw(textureBlueBox, new Vector2(x, y), Color.White);
                    }
                    if (newBlock.BlockArray[row, collumn] == "y")
                    {
                        spriteBatch.Draw(textureYellowBox, new Vector2(x, y), Color.White);
                    }
                }

            }




            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

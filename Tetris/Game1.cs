using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        Texture2D textureSquareBox;
        Texture2D textureWhiteBox;
        Texture2D textureGameOver;
        Texture2D textureBackground;
        Texture2D texturePlane;


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
            { "n", "n", "n", "n", "n", "n", "n", "n", "n", "n" }
        };

        TimeSpan nextUpdateTime = TimeSpan.FromSeconds(0);
        TimeSpan nextUpdateControlTime = TimeSpan.FromSeconds(0);
        Block currentBlock = Block.randomBlock();
        Block futureBlock = Block.randomBlock();
        double boxFallingSpeed = 0.5;                                       //base speed
        private SpriteFont font;
        private int endGame = 0;
        private int score = 0;
       // private int planeWidth = 10;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 650;
            graphics.PreferredBackBufferWidth = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //protected override void Initialize()
        //{
        //  base.Initialize();
        //}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureBlankBox = Content.Load<Texture2D>("tetris_blank");
            textureSquareBox = Content.Load<Texture2D>("tetris_blue");
            textureWhiteBox = Content.Load<Texture2D>("tetris_white");
            textureGameOver = Content.Load<Texture2D>("game_over");
            textureBackground = Content.Load<Texture2D>("background");
            texturePlane = Content.Load<Texture2D>("PLane");

            font = Content.Load<SpriteFont>("font_tetris");

            addBlockGrafic();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        //protected override void UnloadContent()
        //{
        //}

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime >= nextUpdateControlTime)
            {
                nextUpdateControlTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(0.1));
                controls();
            }

            if (gameTime.TotalGameTime >= nextUpdateTime)
            {
                nextUpdateTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(boxFallingSpeed));
                blockFallDown();

                //when the row is full i turn on gravity
                if (numberFullRow() != -1)
                {
                    score += 1;
                    gravity(numberFullRow());
                }
            }
            gameShouldBeOver();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(textureBackground, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(texturePlane, new Vector2(10, 10), Color.White);

            if (gameOver(gameShouldBeOver()) == 0)
            {
                drawBlock();
            }

            //what happen when game over
            if (gameOver(gameShouldBeOver()) == 1)
            {
                spriteBatch.Draw(textureGameOver, new Vector2(15, 15), Color.White);
                spriteBatch.DrawString(font, " Score: " + score.ToString() + "\n \n Press ENTER \n to start new game", new Vector2(20, 250), Color.White);
            }
            //Score
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(400, 15), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //There are my function
        enum direction { left, right, down, top };

        public bool collisionDetection(int choosedDirection)
        {
            for (int row = 3; row >= 0; row--)
            {
                for (int collumn = 3; collumn >= 0; collumn--)
                {
                    if (currentBlock.BlockArray[row, collumn] == "w")
                    {
                        if (choosedDirection == 0)
                        {
                            if (collumn + currentBlock.x == 0 || boxPlane[currentBlock.y + row, currentBlock.x + collumn - 1] != "n")
                            {
                                return true;
                            }
                        }
                        if (choosedDirection == 1)
                        {
                            if (collumn + currentBlock.x == 9 || boxPlane[currentBlock.y + row, currentBlock.x + collumn + 1] != "n")
                            {
                                return true;
                            }
                        }

                        if (choosedDirection == 2)
                        {
                            if (currentBlock.y + row == 19 || boxPlane[currentBlock.y + 1 + row, currentBlock.x + collumn] != "n")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private int maxCoordination(int checkedCoorditantion)
        {
            int collumnNumber = 0;
            if (checkedCoorditantion == 1)
            {
                collumnNumber = 4;
                for (int collumn = 3; collumn >= 0; collumn--)   
                {
                    collumnNumber -=1;
                    for (int row = 3; row >= 0; row--)
                    {
                        
                        if (currentBlock.BlockArray[row, collumn] == "w")
                        {

                            return 9 - collumnNumber;
                        }
                    }
                }
            }
            if (checkedCoorditantion == 0)
            {
                collumnNumber = -1;
                for (int collumn = 0; collumn < 4; collumn++)  
                {
                    collumnNumber += 1;
                    for(int row = 3; row >= 0; row--)
                    {
                        if (currentBlock.BlockArray[row, collumn] == "w")
                        {

                            return -collumnNumber;
                        }
                    }
                }
            }
            return collumnNumber;
        }

        private int numberFullRow()
        {
            int numberOfFullBox = 1;
            int fullRow = 0;
            for (int row = 0; row < 20; row++)
            {
                
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    if (boxPlane[row, collumn] == "b")
                    {
                        numberOfFullBox += 1;
                    }
                }
                if (numberOfFullBox == 10)
                {

                    return fullRow;
                }
                fullRow += 1;
                numberOfFullBox = 0;
            }
            return -1;
        }

        private void gravity(int start)
        {
            for (int row = start; row >= 0; row--)
            {
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    if (row != 0)
                        boxPlane[row, collumn] = boxPlane[row - 1, collumn];
                    else
                        boxPlane[row, collumn] = "n";
                }
            }
            
        }

        private void nextBlock()
        {
            currentBlock = futureBlock;
            futureBlock = Block.randomBlock();
            currentBlock.x = new Random().Next( maxCoordination((int)direction.left), maxCoordination((int)direction.right));
            currentBlock.y = 0;
        }
        
        private bool gameShouldBeOver()
        {
            if (currentBlock.y == 0 && collisionDetection((int)direction.down) == true)
            {
                return true;
            }
            return false;
        }

        private int gameOver(bool isItOver)
        {
            if (isItOver == true)
            {
                endGame = 1;
            }
            return endGame;

        }

        private void clearPlane()
        {
            for (int row = 0; row < 20; row++)
            {
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    boxPlane[row, collumn] = "n";
                }
            }
            currentBlock.x = 0;
            score = 0;
        }

        private void controls()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Enter) && gameOver(gameShouldBeOver()) == 1)
            {
                clearPlane();
                endGame = 0;
            }

            if (gameOver(gameShouldBeOver()) == 0)
            {
                if (kstate.IsKeyDown(Keys.Down))
                    blockFallDown();


                if (kstate.IsKeyDown(Keys.Left) && collisionDetection((int)direction.left) == false)
                    currentBlock.x -= 1;


                if (kstate.IsKeyDown(Keys.Right) && collisionDetection((int)direction.right) == false)
                    currentBlock.x += 1;
                
                if (kstate.IsKeyDown(Keys.Space) && currentBlock.x >= 0)
                {
                    currentBlock.rotateBlock();
                     if (maxCoordination((int)direction.left) > currentBlock.x)
                     {
                         currentBlock.x = maxCoordination((int)direction.left);
                     }
                     if (maxCoordination((int)direction.right) < currentBlock.x)
                     {
                         currentBlock.x = maxCoordination((int)direction.right);
                     }
                }

            }
        }

        private void blockFallDown()
        {
            if (gameOver(gameShouldBeOver()) == 0)
            {
                if (currentBlock.y < 18 && collisionDetection((int)direction.down) == false)
                    currentBlock.y += 1;

                if (collisionDetection((int)direction.down) == true)
                {
                    for (int row = 3; row >= 0; row--)
                    {
                        for (int collumn = 3; collumn >= 0; collumn--)
                        {
                            if (currentBlock.BlockArray[row, collumn] == "w")
                            {
                                boxPlane[currentBlock.y + row, currentBlock.x + collumn] = "b";
                            }
                        }
                    }
                    nextBlock();
                }
            }
           
        }

        Dictionary<string, Texture2D> blockGrafic = new Dictionary<string, Texture2D>();

        private void addBlockGrafic()
        {
            blockGrafic.Add("n", textureBlankBox);
            blockGrafic.Add("w", textureWhiteBox);
            blockGrafic.Add("b", textureSquareBox);
        }

        private void drawBlock()
        {
            int x;                                                                          //value of position Box x,y
            int y;
            //box Plane
            for (int row = 0; row < 20; row++)
            {
                for (int collumn = 0; collumn < 10; collumn++)
                {
                    x = collumn * 31 + 15;
                    y = row * 31 + 15;
                    spriteBatch.Draw(blockGrafic[boxPlane[row, collumn]], new Vector2(x, y), Color.White);

                }

            }
            //falling Block
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (currentBlock.x + collumn) * 31 + 15;
                    y = (currentBlock.y + row) * 31 + 15;
                    if (currentBlock.BlockArray[row, collumn] != "n")
                        spriteBatch.Draw(blockGrafic[currentBlock.BlockArray[row, collumn]], new Vector2(x, y), Color.White);
                }

            }

            //Side block
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (futureBlock.x + collumn) * 31 + 405;
                    y = (futureBlock.y + 1 + row) * 31 + 100;
                    spriteBatch.Draw(blockGrafic[futureBlock.BlockArray[row, collumn]], new Vector2(x, y), Color.White);

                }
            }
        }
    }
}

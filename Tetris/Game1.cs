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
        private static int planeHeight = 20, planeWidth = 10;

        string[,] boxPlane = new string[planeHeight, planeWidth];

        TimeSpan nextUpdateFallingTime = TimeSpan.FromSeconds(0);
        TimeSpan nextUpdateControlTime = TimeSpan.FromSeconds(0);
        Block currentBlock = Block.randomBlock();
        Block futureBlock = Block.randomBlock();
        double boxFallingSpeed = 0.5;                                       //base speed
        private SpriteFont font;
        private bool endGame = false;
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
            fillBasePlaneWithBlackBox(boxPlane);

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

            if (gameTime.TotalGameTime >= nextUpdateFallingTime)
            {
                nextUpdateFallingTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(boxFallingSpeed));
                blockFallDown();

                //when the row is full i turn on gravity
                if (indexFullRow() != -1)
                {
                    score += 1;
                    gravity(indexFullRow());
                }
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
            spriteBatch.Begin();
            spriteBatch.Draw(textureBackground, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(texturePlane, new Vector2(10, 10), Color.White);

            if (endGame == false)
            {
                drawBlock();
            }

            //what happen when game over
            if (endGame)
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
        private void fillBasePlaneWithBlackBox(string[,] filledArray)
        {
            for (int collumn = 0; collumn < planeWidth; collumn ++)
                for(int row = 0; row < planeHeight; row ++)
                {
                    filledArray[row, collumn] = "n";
                }

        }

        enum direction { left, right, down, top };

        public bool isMovePossible(int choosedDirection)      //check is it possible to move block in choosed direction
        {
            for (int row = 0; row < Block.width; row++)
            {
                for (int collumn = 0; collumn < Block.height; collumn++)
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

        private int maxCoordination(int checkedCoorditantion) //check and return maximum posiible coordinate for currentBlock in choosed direction
        {
            int collumnNumber = 0;
            if (checkedCoorditantion == 1)
            {
                collumnNumber = Block.width;
                for (int collumn = Block.width - 1; collumn >= 0; collumn--)   
                {
                    collumnNumber -=1;
                    for (int row = Block.height - 1; row >= 0; row--)
                    {
                        
                        if (currentBlock.BlockArray[row, collumn] == "w")
                        {

                            return planeWidth - 1 - collumnNumber;
                        }
                    }
                }
            }
            if (checkedCoorditantion == 0)
            {
                collumnNumber = -1;
                for (int collumn = 0; collumn < Block.width; collumn++)  
                {
                    collumnNumber += 1;
                    for(int row = Block.height - 1; row >= 0; row--)
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

        private int indexFullRow()                           //return index of full row
        {
            int numberOfFullBox = 1;
            int fullRow = 0;
            for (int row = 0; row < planeHeight; row++)
            {
                
                for (int collumn = 0; collumn < planeWidth; collumn++)
                {
                    if (boxPlane[row, collumn] == "b")
                    {
                        numberOfFullBox += 1;
                    }
                }
                if (numberOfFullBox == planeWidth)
                {

                    return fullRow;
                }
                fullRow += 1;
                numberOfFullBox = 0;
            }
            return -1;
        }

        private void gravity(int start)                     //unmovable box fall 1 step down
        {
            for (int row = start; row >= 0; row--)
            {
                for (int collumn = 0; collumn < planeWidth; collumn++)
                {
                    if (row != 0)
                        boxPlane[row, collumn] = boxPlane[row - 1, collumn];
                    else
                        boxPlane[row, collumn] = "n";
                }
            }
            
        }

        private void nextBlock()                            //random new array for block
        {
            currentBlock = futureBlock;
            futureBlock = Block.randomBlock();
            currentBlock.x = new Random().Next( maxCoordination((int)direction.left), maxCoordination((int)direction.right));
            currentBlock.y = 0;
            gameOver();
        }
        
       private void gameOver()
        {
            if (currentBlock.y == 0 && isMovePossible((int)direction.down))
            {
                endGame = true;
            }

        }

        private void controls()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Enter) && endGame)
            {
                fillBasePlaneWithBlackBox(boxPlane);
                currentBlock.x = 0;
                score = 0;
                endGame = false;
            }

            if (endGame == false)
            {
                if (kstate.IsKeyDown(Keys.Down))
                    blockFallDown();

                if (kstate.IsKeyDown(Keys.Left) && isMovePossible((int)direction.left) == false)
                    currentBlock.x -= 1;

                if (kstate.IsKeyDown(Keys.Right) && isMovePossible((int)direction.right) == false)
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
            if (endGame == false)
            {
                if (currentBlock.y < 18 && isMovePossible((int)direction.down) == false)
                    currentBlock.y += 1;

                else if (isMovePossible((int)direction.down))
                {
                    for (int row = 0; row < Block.height; row++)
                    {
                        for (int collumn = 0; collumn < Block.width; collumn++)
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
            int x;   //value of block position x,y
            int y;
            int boxPictureDimension = 31; //in px
            int planeMargin = 15;
            int nextBlockMarginLeft = 405;
            int nextBlockMarginTop = 100;

            for (int row = 0; row < planeHeight; row++)
            {
                for (int collumn = 0; collumn < planeWidth; collumn++)
                {
                    x = collumn * boxPictureDimension + planeMargin;
                    y = row * boxPictureDimension + planeMargin;
                    spriteBatch.Draw(blockGrafic[boxPlane[row, collumn]], new Vector2(x, y), Color.White);

                }

            }
            //falling Block
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (currentBlock.x + collumn) * boxPictureDimension + planeMargin;
                    y = (currentBlock.y + row) * boxPictureDimension + planeMargin;
                    if (currentBlock.BlockArray[row, collumn] != "n")
                        spriteBatch.Draw(blockGrafic[currentBlock.BlockArray[row, collumn]], new Vector2(x, y), Color.White);
                }

            }

            //Side block
            for (int row = 0; row < Block.height; row++)
            {
                for (int collumn = 0; collumn < Block.height; collumn++)
                {
                    x = (futureBlock.x + collumn) * boxPictureDimension + nextBlockMarginLeft; 
                    y = (futureBlock.y + 1 + row) * boxPictureDimension + nextBlockMarginTop;
                    spriteBatch.Draw(blockGrafic[futureBlock.BlockArray[row, collumn]], new Vector2(x, y), Color.White);

                }
            }
        }
    }
}

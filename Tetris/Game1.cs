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
            { "n", "b", "b", "n", "b", "b", "n", "n", "b", "n" }
        };

        TimeSpan nextUpdateTime = TimeSpan.FromSeconds(0);
        TimeSpan nextUpdateControlTime = TimeSpan.FromSeconds(0);
        Block currentBlock = Block.randomBlock();
        Block futureBlock = Block.randomBlock();
        double boxFallingSpeed = 0.5; //base speed

        int score = 0;

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
            currentBlock.printCoordinates();
            //Console.WriteLine(BlockSpecyfication[0]);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();
            // controls
            if (kstate.IsKeyDown(Keys.Down))
            {
                boxFallingSpeed = 0.01;
            }
            else
            {
                boxFallingSpeed = 0.5;
            }
            if (gameTime.TotalGameTime >= nextUpdateControlTime)
            {
                nextUpdateControlTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(0.1));

                if (kstate.IsKeyDown(Keys.Left) && collisionDetection((int)direction.left) == false)
                {
                    currentBlock.x -= 1;
                }

                if (kstate.IsKeyDown(Keys.Right) && collisionDetection((int)direction.right) == false)
                {
                    if (currentBlock.x < 8)
                    {
                        currentBlock.x += 1;
                    }
                }
            }

            //how box fall down
            if (gameTime.TotalGameTime >= nextUpdateTime)
            {
                nextUpdateTime = gameTime.TotalGameTime.Add(TimeSpan.FromSeconds(boxFallingSpeed));
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
            //when the row is full i turn on gravity

            if (numberFullRow() != -1)
            {
                gravity(numberFullRow());
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
                    if (boxPlane[row, collumn] == "w")
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
            //falling Block
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (currentBlock.x + collumn) * 31 + 5;
                    y = (currentBlock.y + row) * 31 + 15;

                    if (currentBlock.BlockArray[row, collumn] == "w")
                    {
                        spriteBatch.Draw(textureWhiteBox, new Vector2(x, y), Color.White);
                    }
                    if (currentBlock.BlockArray[row, collumn] == "r")
                    {
                        spriteBatch.Draw(textureRedBox, new Vector2(x, y), Color.White);
                    }
                    if (currentBlock.BlockArray[row, collumn] == "g")
                    {
                        spriteBatch.Draw(textureGreenBox, new Vector2(x, y), Color.White);
                    }
                    if (currentBlock.BlockArray[row, collumn] == "b")
                    {
                        spriteBatch.Draw(textureBlueBox, new Vector2(x, y), Color.White);
                    }
                    if (currentBlock.BlockArray[row, collumn] == "y")
                    {
                        spriteBatch.Draw(textureYellowBox, new Vector2(x, y), Color.White);
                    }
                }

            }

            //Side block
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    x = (futureBlock.x + collumn) * 31 + 405;
                    y = (futureBlock.y +1 + row) * 31 + 15;

                    if (futureBlock.BlockArray[row, collumn] == "n")
                    {
                        spriteBatch.Draw(textureBlankBox, new Vector2(x, y), Color.White);
                    }
                    if (futureBlock.BlockArray[row, collumn] == "w")
                    {
                        spriteBatch.Draw(textureWhiteBox, new Vector2(x, y), Color.White);
                    }

                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }



        //Her
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
                            if (collumn + currentBlock.x == 0 || boxPlane[currentBlock.y + 1 + row, currentBlock.x + collumn - 1] != "n")
                            {
                                return true;
                            }
                        }
                        if (choosedDirection == 1)
                        {
                            if (collumn + currentBlock.x == 9 || boxPlane[currentBlock.y + 1 + row, currentBlock.x + collumn + 1] != "n")
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

        public int maxCoordination(int checkedCoorditantion)
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

                            return collumnNumber;
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

                            return collumnNumber;
                        }
                    }
                }
            }
            return collumnNumber;
        }

    

        public int numberFullRow()
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
                    score += 1;
                    Console.WriteLine(score);
                    return fullRow;
                }
                fullRow += 1;
                numberOfFullBox = 0;
            }
            return -1;
        }
        public void gravity(int start)
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
        //enum BlockSpecyfication { Block1, Block2, Block3, Block4, Block5 };

        public void nextBlock()
        {
            currentBlock = futureBlock;
            futureBlock = Block.randomBlock();

            currentBlock.x = new Random().Next( -maxCoordination((int)direction.left), 10 - maxCoordination((int)direction.right));
            currentBlock.y = 0;
        }
    }
}

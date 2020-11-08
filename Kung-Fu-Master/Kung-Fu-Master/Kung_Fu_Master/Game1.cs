using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Kung_Fu_Master
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState kb;

        int enemyCount;
        int timer;
        int kickTimer;
        Player man;

        Texture2D walkL;
        Texture2D walkR;
        Texture2D kickL;
        Texture2D kickR;
        Texture2D manText;

        Texture2D enemyWalkL, enemyWalkR, enemyKickL, enemyKickR, enemyText;

        List<Enemy> enemies;

        int xcord;
        int vel;

        Texture2D startScreen;
        Boolean startGame;
        Boolean instructionScreen;
        SpriteFont Font1;
        SpriteFont Font2;
        SpriteFont Font3;
        KeyboardState oldkb;

        Boolean gameOver;
        Rectangle healthBar;
        Color background;
        Texture2D text;
        Color color;
        hp health;
        SpriteFont Font4;
        SpriteFont Font5;

        SoundEffect kick;
        SoundEffect playerDeath;
        SoundEffect enemyDeath;
        Boolean deathPlayed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
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
            xcord = 260;
            timer = 0;
            kickTimer = 0;
            vel = 4;
            enemies = new List<Enemy>();
            startGame = false;
            oldkb = Keyboard.GetState();
            instructionScreen = false;
            healthBar = new Rectangle(20, 20, 200, 20);
            health = new hp(1.0);
            color = Color.Green;
            gameOver = false;
            deathPlayed = false;
            background = Color.CornflowerBlue;
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

            walkR = this.Content.Load<Texture2D>("5.720R");
            walkL = this.Content.Load<Texture2D>("5.720");
            kickL = this.Content.Load<Texture2D>("6.720");
            kickR = this.Content.Load<Texture2D>("6.720R");
            manText = this.Content.Load<Texture2D>("5.720R");

            enemyWalkR = this.Content.Load<Texture2D>("enemy_standing");
            enemyWalkL = this.Content.Load<Texture2D>("enemy_standingLeft");
            enemyText = this.Content.Load<Texture2D>("enemy_standing");

            enemyKickL = this.Content.Load<Texture2D>("enemy_kickLeft");
            enemyKickR = this.Content.Load<Texture2D>("enemy_kick");

            man = new Player(200, new Rectangle(300, 200, 200, 200), manText);

            startScreen = this.Content.Load<Texture2D>("Start screen background");
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            Font2 = Content.Load<SpriteFont>("SpriteFont2");
            Font3 = Content.Load<SpriteFont>("SpriteFont3");

            text = this.Content.Load<Texture2D>("Bitmap1");
            Font4 = Content.Load<SpriteFont>("SpriteFont4");
            Font5 = Content.Load<SpriteFont>("SpriteFont5");

            kick = this.Content.Load<SoundEffect>("attack");
            playerDeath = this.Content.Load<SoundEffect>("playerDeath");
            enemyDeath = this.Content.Load<SoundEffect>("enemyDeath");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            kb = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();
            if (kb.IsKeyDown(Keys.S) && !oldkb.IsKeyDown(Keys.S))
            {
                startGame = true;
            }
            if (kb.IsKeyDown(Keys.I) && !oldkb.IsKeyDown(Keys.I))
            {
                startGame = false;
                instructionScreen = true;
            }
            if (kb.IsKeyDown(Keys.M) || kb.IsKeyDown(Keys.M))
            {
                startGame = false;
                instructionScreen = false;
            }
            if (kb.IsKeyDown(Keys.Space) && oldkb.IsKeyUp(Keys.Space))
            {
                kickTimer = 20; //How long the man kicks
                kick.Play();
                if (vel < 0)
                {
                    man.text = kickL;
                }
                else
                {
                    man.text = kickR;
                }
            }
            if (health.health < 0.0)
            {
                gameOver = true;
                if (deathPlayed == false)
                {
                    playerDeath.Play();
                    deathPlayed = true;
                }
            }
            healthBar.Width = (int)(200 * health.health);// Changes width of hp bar to match health
            if (health.health <= .30)// Changes healthbar to red when below threshold
            {
                color = Color.Red;
            }
            else
            {
                color = Color.Green;
            }
            if ((kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D)) && man.location < GraphicsDevice.Viewport.Width-300)
            {
                vel = 4;
                man.rect.X += vel;
                man.location += vel;

                man.text = walkR;
            }
            if ((kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A)) && man.location > -150)
            {
                vel = -4;
                man.rect.X += vel;
                man.location += vel;
                man.text = walkL;
            }

            if ((kb.IsKeyDown(Keys.R) && !oldkb.IsKeyDown(Keys.R)))
            {
                Restart();
            }

            timer++;
           
                if (enemies.Count < 2 && timer % 240 == 0)
                {

                    if (enemies.Count == 0)
                        enemies.Add(new Enemy(-150, man, enemyWalkR));
                    else if (enemies.Count == 1)
                        enemies.Add(new Enemy(950, man, enemyWalkR));
                }

                if (enemies.Count != 0)
                {
                    for (int i = 0; i < enemyCount; i++)
                    {

                        if (enemies[i].enemyX > man.location + 280)
                            enemies[i].text = enemyWalkL;
                        else if (enemies[i].enemyX < man.location)
                            enemies[i].text = enemyWalkR;
                    }
                }
                enemyCount = enemies.Count;

                for (int i = 0; i < enemyCount; i++)
                {
                    if (enemies[i].enemyX < man.location + 280 && enemies[i].enemyX > man.location + 270 && timer % 180 == 0)
                    {
                        enemies[i].kickL(enemyKickL);
                        health.health -= .05;
                    }
                    if (enemies[i].text == enemyKickL && timer % 50 == 0)
                        enemies[i].text = enemyWalkL;

                    if (enemies[i].enemyX > man.location + 70 && enemies[i].enemyX < man.location + 80 && timer % 180 == 0)
                    {
                        enemies[i].kickR(enemyKickR);
                        health.health -= .05;
                    }

                    if (enemies[i].text == enemyKickR && timer % 70 == 0)
                        enemies[i].text = enemyWalkR;

                    // if(man.location + 70 == enemies[i].enemyX)
                    //{
                    //    enemies[i].rect.X += vel;
                    //    enemies[i].enemyX += vel;
                    //}
                    //if (man.location + 70 == enemies[i].enemyX)
                    //{
                    //    enemies[i].rect.X += vel;
                    //    enemies[i].enemyX += vel;
                    //}
                    if (kb.IsKeyDown(Keys.Space) && man.text == kickL && enemies[i].enemyX > man.location + 45 && enemies[i].enemyX < man.location + 115)
                    {
                        int x = new Random().Next(-600, -100);

                        enemies[i].enemyX = x;
                        enemies[i].rect.X = x;
                    enemyDeath.Play();




                }

                if (kb.IsKeyDown(Keys.Space) && man.text == kickR && enemies[i].enemyX > man.location + 245 && enemies[i].enemyX < man.location + 315)
                    {
                        int y = new Random().Next(950, 1500);


                        enemies[i].enemyX = y;
                        enemies[i].rect.X = y;
                    enemyDeath.Play();



                }


            }

            oldkb = kb;
                base.Update(gameTime);
        }

        public void Restart()
        {
            xcord = 260;
            timer = 0;
            vel = 4;
            enemies = new List<Enemy>();
            startGame = false;
            oldkb = Keyboard.GetState();
            instructionScreen = false;
            healthBar = new Rectangle(20, 20, 200, 20);
            health = new hp(1.0);
            color = Color.Green;
            gameOver = false;
            background = Color.CornflowerBlue;
            deathPlayed = false;

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);


            spriteBatch.Begin();



            if (startGame == false && instructionScreen == false && gameOver == false)
            {
                spriteBatch.Draw(startScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.DrawString(Font1, "Kung Fu Masters", new Vector2(185, 110), new Color(219, 189, 39));
                spriteBatch.DrawString(Font2, "Press S to Start", new Vector2(265, 235), new Color(219, 189, 39));
                spriteBatch.DrawString(Font2, "Press I for Instructions", new Vector2(210, 270), new Color(219, 189, 39));
                spriteBatch.DrawString(Font2, "Press Esc to Quit", new Vector2(258, 305), new Color(219, 189, 39));
            }
            if (instructionScreen == true && gameOver == false)
            {
                spriteBatch.Draw(startScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.DrawString(Font1, "Instructions", new Vector2(230, 110), new Color(219, 189, 39));
                spriteBatch.DrawString(Font2, "--> / A         Left", new Vector2(210, 170), new Color(219, 189, 39));
                spriteBatch.Draw(walkL, new Rectangle(540, 160, 40, 40), Color.White);
                spriteBatch.DrawString(Font2, "<-- / D         Right", new Vector2(210, 220), new Color(219, 189, 39));
                spriteBatch.Draw(walkR, new Rectangle(545, 210, 40, 40), Color.White);
                spriteBatch.DrawString(Font2, "Space           Kick", new Vector2(210, 270), new Color(219, 189, 39));
                spriteBatch.DrawString(Font3, " Defeat all of the enemies in the room to win \nthe game.If your HP Bar drops to zero you lose.", new Vector2(140, 310), new Color(219, 189, 39));
                spriteBatch.Draw(kickL, new Rectangle(545, 260, 40, 40), Color.White);
                spriteBatch.DrawString(Font3, "Press M to go to Main Menu", new Vector2(20, 440), Color.White);
            }
            if (startGame == true && gameOver == false)
            {
                spriteBatch.Draw(text, healthBar, color);

                for (int i = 0; i < enemyCount; i++)
                {
                    enemies[i].update(man, enemies);

                    spriteBatch.Draw(enemies[i].text, enemies[i].rect, Color.White);

                }





                spriteBatch.Draw(man.text, man.rect, Color.White);
            }

            if (gameOver == true)
            {
                background = Color.Black;
                spriteBatch.DrawString(Font4, "Game Over", new Vector2(170, 60), Color.White);
                spriteBatch.DrawString(Font5, "Press R to Restart", new Vector2(180, 300), Color.White);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

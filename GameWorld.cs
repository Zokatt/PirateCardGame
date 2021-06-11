using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PriateCardGame.BuilderPattern;
using PriateCardGame.Cards;
using PriateCardGame.Database;
using PriateCardGame.DatabaseEnemyDiff;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PriateCardGame
{
    /// <summary>
    /// GameState is used to determine which screen the player is on
    /// </summary>
    public enum GameState { 
    CardBoard,DeckBuilding,StageSelect
    }
    /// <summary>
    /// The gameworld is what class monogame uses as a main script to run the game on
    /// </summary>
    ///<remarks>
    /// Nikolaj, Johnny,Mads
    /// </remarks>
    public class GameWorld : Game
    {
        /// <summary>
        /// What graphic setting the game runs on
        /// </summary>
        private GraphicsDeviceManager _graphics;
        /// <summary>
        /// The spritebatch is what the game uses to draw everything
        /// </summary>
        private SpriteBatch _spriteBatch;
        /// <summary>
        /// The screensize for the game, in pixels
        /// </summary>
        public static Rectangle screenBounds = new Rectangle(0, 0, 1600, 1000);
        /// <summary>
        /// A string so that we dont accidently write deck different somewhere
        /// </summary>
        public static string Deck = "Deck";
        /// <summary>
        /// A string so that we dont accidently write storage different somewhere
        /// </summary>
        public static string Storage = "Storage";
        /// <summary>
        /// the repository for all the cards
        /// </summary>
        public static CardRepository repo;
        /// <summary>
        /// the repository for the unlocked difffficulty
        /// </summary>
        public static EnemyDifficultyRepository diffRepo;
        /// <summary>
        /// The list which contains the cards in the players hand
        /// </summary>
        public static List<CardBase> playerCards;
        /// <summary>
        /// A list for all the player spaces
        /// </summary>
        public static List<CardSpace> playerSpaces;
        /// <summary>
        /// A list for all the enemy spaces
        /// </summary>
        public static List<CardSpace> enemySpaces;
        /// <summary>
        /// A list for the players deck
        /// </summary>
        public static List<CardBase> PlayerDeck;
        /// <summary>
        /// A list to check how many cards the player owns
        /// </summary>
        public static List<CardBase> AllOwnedCards;
        /// <summary>
        /// a List to check how many the player has of a specific card
        /// </summary>
        public static List<CardBase> checkCardCount;
        /// <summary>
        /// A list for all the UI elements when the player looses or wins a ga,e
        /// </summary>
        public List<UI> WinOrLoseScreenList;
        /// <summary>
        /// A bool to check whetever the player is done with a game
        /// </summary>
        public bool endScreen;
        /// <summary>
        /// a list for all the spaces in the storage, so that we can place cards
        /// </summary>
        public List<StorageSpace> storageSpaces;
        /// <summary>
        /// A list for all of the UI in the game when fighting AI
        /// </summary>
        public static List<UI> GameUI;
        /// <summary>
        /// A list for the enemys deck
        /// </summary>
        public static List<CardBase> enemyDeck;
        /// <summary>
        /// This is a card for when you click a card in your hand.
        /// </summary>
        public static CardBase refCard;
        /// <summary>
        /// The background for when fighting AI
        /// </summary>
        private Texture2D background;
        /// <summary>
        /// The background for deckbuilding
        /// </summary>
        private Texture2D deckBuildingBackground;
        /// <summary>
        /// The backgroundfor stageselect
        /// </summary>
        private Texture2D StageSelectBackground;
        /// <summary>
        /// The background for tutorial
        /// </summary>
        private Texture2D tutorialBackground;
        /// <summary>
        /// The normal sized font
        /// </summary>
        public static SpriteFont font;
        /// <summary>
        /// A font that is a little bigger than the normal one
        /// </summary>
        public static SpriteFont Bigfont;
        /// <summary>
        /// The postion of the mouse
        /// </summary>
        public static Point mousePos;
        /// <summary>
        /// To check which state the mouse is in, such as whever left click is being pressed
        /// </summary>
        public static MouseState mouseState;
        /// <summary>
        /// whever the player is hovering over a card to see it's info
        /// </summary>
        public static bool cardInfo = false;
        /// <summary>
        /// what number does the cardinfo have. used to find the card in hand or in spaces
        /// </summary>
        public static int cardInfoNumber;
        /// <summary>
        /// Used for cehcking button press. so that the player has to release left click to press again
        /// </summary>
        public static bool bPress = false;
        /// <summary>
        /// used for checking whetever the player has pressed t. Testing only
        /// </summary>
        public static bool tPress = false;
        /// <summary>
        /// A bool for whose turn it is
        /// </summary>
        public static bool playerTurn = false;
        /// <summary>
        /// The enemy when fighting the AI
        /// </summary>
        public static Enemy enemy;
        /// <summary>
        /// For showing a bigger version of a card if the player is hovering their mouse over a card
        /// </summary>
        public static CardBase infoCard;
        /// <summary>
        /// the card the player is rewarded with if they win
        /// </summary>
        public static CardBase rewardCard;
        /// <summary>
        /// Sets the starting screen to StageSelect when the player starts the game
        /// </summary>
        public static GameState gameState = GameState.StageSelect;
        /// <summary>
        /// Used for deciding which page the player is in when in deckbuilding. currently has 3 pages
        /// </summary>
        public static int pageNumber = 0;
        /// <summary>
        /// used for scrolling through ones deck in deckbuilding
        /// </summary>
        public static int ScrollValue = 0;
        /// <summary>
        /// Same thing
        /// </summary>
        public int scroll;
        /// <summary>
        /// The health of the enemy
        /// </summary>
        public static int enemyHealth = 0;
        /// <summary>
        /// thea health of the player
        /// </summary>
        public static int playerHealth = 0;
        /// <summary>
        /// how much damage the player or enemy has taken
        /// </summary>
        public static int healthDamage = 0;
        /// <summary>
        /// Used for checking whetever the player has drawn cards or not
        /// </summary>
        public bool drawnCards = false;
        /// <summary>
        /// used to check which turn it is
        /// </summary>
        public static int turn = 0;
        /// <summary>
        /// so that the enemy only takes it turn once
        /// </summary>
        public static bool endTurnOnlyOnce = true;
        /// <summary>
        /// Used for diciding which  deck the enemy should have
        /// </summary>
        public int difficulty = 1;
        /// <summary>
        /// Which tutorial needs to be shown to the player
        /// </summary>
        public int tutorial = 1;
        /// <summary>
        /// Sound effect for placing card
        /// </summary>
        public SoundEffect placeCard;
        /// <summary>
        /// sound effect for clicking a button
        /// </summary>
        public SoundEffect woodClick;
        /// <summary>
        /// sound effect for flipping through the pages in deckbuilding
        /// </summary>
        public SoundEffect bookFlip;
        /// <summary>
        /// Used to check which difficulties the player has unlocked
        /// </summary>
        public int unlockDiff { get; set; } = 1;

        //public static GameState gameState = GameState.CardBoard;
        /// <summary>
        /// the director for building the enemyes deck
        /// </summary>
        public static Director director = new Director(new EnemyBuilder());

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            //this means that the content it uses is in the content folder
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //height of the scrren
            _graphics.PreferredBackBufferHeight = screenBounds.Height;
            //width of the screen
            _graphics.PreferredBackBufferWidth = screenBounds.Width;
        }
        /// <summary>
        /// Initialize for the game which will run as the first thing
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            //A Different initialize runs depending on which screen the player is in
            if (gameState == GameState.CardBoard)
            {
                rewardCard = null;
                endScreen = false;
                playerTurn = true;
                enemyHealth = 20;
                playerHealth = 20;
                playerCards = new List<CardBase>();
                playerSpaces = new List<CardSpace>();
                enemySpaces = new List<CardSpace>();
                PlayerDeck = new List<CardBase>();
                WinOrLoseScreenList = new List<UI>();
                GameUI = new List<UI>();
                GameUI.Add(new UI("EndTurnButton", new Vector2(1050, 450)));
                //GameUI.Add(new UI("TestButton", new Vector2(450, 450)));
                enemyDeck = new List<CardBase>();
                turn = 0;

                //this makes the deck for the enemy AI
                enemy = new Enemy(difficulty);
                enemy.Deck = director.ConstructEnemyDeck(enemy.difficulty);
                //adds all the spaces for the player
                for (int i = 0; i < 8; i++)
                {
                    playerSpaces.Add(new CardSpace(i));
                }
                //adds all the spaces for the enemy
                for (int i = 0; i < 8; i++)
                {
                    enemySpaces.Add(new CardSpace(i));
                }

                
                //generates the repository for all the cards in the game
                var mapper = new CardMapper();
                var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                repo = new CardRepository(provider, mapper);

                //Finds the players deck in the repository
                repo.Open();

                PlayerDeck = repo.FindDeck();

                repo.Close();

                //makes the repostiory for the difficulty in case the player wins
                var diffMapper = new DifficultyMapper();
                var diffProvider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                diffRepo = new EnemyDifficultyRepository(diffProvider, diffMapper);


                //draws a hand for the players
                DrawHand();
                playerTurn = true;
            }
            else if (gameState == GameState.DeckBuilding)
            {
                bPress = true;

                var mapper = new CardMapper();
                var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                repo = new CardRepository(provider, mapper);
                //adds all the ui elements 
                GameUI = new List<UI>();
                GameUI.Add(new UI("MenuButton", new Vector2(800, 600)));
                GameUI.Add(new UI("LeftArrow", new Vector2(50, 600)));
                GameUI.Add(new UI("RightArrow", new Vector2(700, 600)));
                GameUI.Add(new UI("ClearDeck", new Vector2(1350, 690)));


                PlayerDeck = new List<CardBase>();
                AllOwnedCards = new List<CardBase>();
                storageSpaces = new List<StorageSpace>();
                //finds the players deck and all their owned cards
                repo.Open();
                PlayerDeck = repo.FindDeck();
                AllOwnedCards = repo.FindAllCards();
                repo.Close();
                //adds all the storage spaces and places cards where they should be
                for (int i = 0; i < 10; i++)
                {
                    storageSpaces.Add(new StorageSpace());
                    storageSpaces[i].SetCard(i);
                    storageSpaces[i].SetCardPos(i);
                    
                }
                //sets the cound of many cards the player owns of those cards
                SetDeckBuildingCardCount();
                //refreshes the lists
                RefresDeckBuildingLists();
                
            }
            else if (gameState == GameState.StageSelect)
            {
                GameUI = new List<UI>();
                //adds all the buttons to the screen
                for (int i = 1; i < 6; i++)
                {
                    if (i >=5)
                    {
                    GameUI.Add(new UI($"StageSelectButtons/Enemy{i}", new Vector2(-350 + ((i-4) * 400), 250)));
                    }
                    else
                    {
                    GameUI.Add(new UI($"StageSelectButtons/Enemy{i}", new Vector2(-350 + (i * 400), 50)));
                    }
                    
                }
                GameUI.Add(new UI("StageSelectButtons/DeckBuilder", new Vector2(1200, 850)));

                var mapper = new CardMapper();
                var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                repo = new CardRepository(provider, mapper);


                //dropRepoTable();
                //makes the player a deck if they dont have one
                //this will only run if the player owns 0 cards, which will only happen the first time they open the game
                repo.Open();
                if (repo.FindAllCards().Count <=0)
                {
                    MakePlayerDeck();
                }
                repo.Close();

                var diffMapper = new DifficultyMapper();
                var diffProvider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                diffRepo = new EnemyDifficultyRepository(diffProvider, diffMapper);

                //if there is no row in the difficulty repistory, then make one
                diffRepo.Open();
                if (diffRepo.FindDiff() == 69)
                {
                    diffRepo.AddUnlockedDifficulty(1);
                }
                //find out what the player has unlocked
                unlockDiff = diffRepo.FindDiff();
                diffRepo.Close(); 


            }

            base.Initialize();
        }
        /// <summary>
        /// Loadcontent Loads all content for the different fields
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        protected override void LoadContent()
        {
            //only make a new spritebatch if there's not one already
            if (_spriteBatch == null)
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);
            }

            font = Content.Load<SpriteFont>("Font");
            Bigfont = Content.Load<SpriteFont>("BigFont");
            placeCard = Content.Load<SoundEffect>("WoodKick");
            woodClick = Content.Load<SoundEffect>("WoodClick");
            bookFlip = Content.Load<SoundEffect>("BookFlipping");

            //Load different content based on what screen the player is on
            if (gameState == GameState.CardBoard)
            {
                background = Content.Load<Texture2D>("Background");

                if (tutorial == 4)
                {
                    tutorialBackground = Content.Load<Texture2D>("Tutorial/Tutorial3");
                }
                if (tutorial == 5)
                {
                    tutorialBackground = Content.Load<Texture2D>("Tutorial/Tutorial4");
                }
                if (tutorial == 6)
                {
                    tutorialBackground = Content.Load<Texture2D>("Tutorial/Tutorial5");
                }

                foreach (UI item in WinOrLoseScreenList)
                {
                    item.LoadContent(this.Content);
                }

                foreach (UI item in GameUI)
                {
                    item.LoadContent(this.Content);
                }


                foreach (var item in playerSpaces)
                {
                    item.LoadContent(this.Content);
                    if (item.card != null)
                    {
                        item.card.LoadContent(this.Content);
                    }
                }
                foreach (var item in enemySpaces)
                {
                    item.LoadContent(this.Content);
                    if (item.card != null)
                    {
                        item.card.LoadContent(this.Content);
                    }
                }
                foreach (var item in playerCards)
                {
                    if (item != null)
                    {
                        item.LoadContent(this.Content);
                    }
                    
                }
            }
            else if (gameState == GameState.DeckBuilding)
            {
                deckBuildingBackground = Content.LoadLocalized<Texture2D>("DeckManager");

                if (tutorial == 2)
                {
                    tutorialBackground = Content.Load<Texture2D>($"Tutorial/Tutorial2");
                }
                
                

                foreach (UI item in GameUI)
                {
                    item.LoadContent(this.Content);
                }

                foreach (CardBase item in PlayerDeck)
                {
                    item.LoadContent(this.Content);
                }

                foreach (var item in storageSpaces)
                {
                    item.LoadContent(this.Content);
                }
            }
            else if (gameState == GameState.StageSelect)
            {
                StageSelectBackground = Content.Load<Texture2D>("StageSelectBackGround");
                if (tutorial == 1)
                {
                    tutorialBackground = Content.Load<Texture2D>("Tutorial/Tutorial1");
                }
               

                foreach (UI item in GameUI)
                {
                    item.LoadContent(this.Content);
                }
            }


            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// Used to load content for a specic list so as to not call all of loadcontent
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void LoadContentForThisUIList(List<UI> refList)
        {
            foreach (var item in refList)
            {
                item.LoadContent(this.Content);
            }
        }
        /// <summary>
        /// makes a deck for the player
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void MakePlayerDeck()
        {
            for (int i = 0; i < 12; i++)
            {
                repo.AddCard("Swapper");
            }
            for (int i = 0; i < 1; i++)
            {
                repo.AddCard("Cannibal");
            }
            repo.AddCardToStorage("Cannibal");
            for (int i = 0; i < 2; i++)
            {
                repo.AddCard("Musketeer");
            }
            repo.AddCard("FatPirate");
            repo.AddCard("DavyJonesLocker");
            repo.AddCard("Captain");
        }

        /// <summary>
        /// Update constantly updates the game, meaning most of the game will happen here
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input(gameTime);


            //sets the mousestate to whatever the current mouse us
            mouseState = Mouse.GetState();
            //set the mouse position for wherever the mouse is pointing
            mousePos = new Point(mouseState.X, mouseState.Y);

            //if (mouseState.LeftButton == ButtonState.Pressed) //to check for postion placements
            //{

            //}//breakpoint here to test <--

            //run a different update depending on what screen the player is on
            if (gameState == GameState.CardBoard)
            {
                UpdateCardBoard(gameTime);
            }
            else if (gameState == GameState.DeckBuilding)
            {
                UpdateDeckBuilding(gameTime);
            }
            else if (gameState == GameState.StageSelect)
            {
                UpdateStageSelect(gameTime);
            }

            if (mouseState.LeftButton == ButtonState.Released) //so that the player can click the mouse again
            {
                bPress = false;
            }


            base.Update(gameTime);
        }
        /// <summary>
        /// Used to update all the players cards, such as updating their position in hand
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public void ListUpdate(List<CardBase> refList)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                if (refList[i] != null)
                {
                    refList[i].UpdatePlayerCardPos(i);
                }
            }
            for (int i = 0; i < refList.Count; i++)
            {
                if (refList[i]!=null)
                {
                    if (refList[i].Collision.Contains(mousePos))
                    {
                        infoCard = refList[i];
                        cardInfo = true;

                    }

                    if (refList[i].Collision.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && bPress == false)
                    {
                        refCard = refList[i];
                        //playerCards.RemoveAt(i);
                        bPress = true;
                    }
                }


            }
        }
        /// <summary>
        /// Draw is used to draw the different object in the game
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //_spriteBatch.Begin();

           //a different draw is called depending on the screen
                if (gameState == GameState.CardBoard)
                {
                    drawCardBoard(gameTime);
                }
                else if (gameState == GameState.DeckBuilding)
                {
                    drawDeckBuilding(gameTime);
                }
                else if (gameState == GameState.StageSelect)
                {
                    drawStageSelect();
                }
            
            
                _spriteBatch.End();

            
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        /// <summary>
        /// is Used to drop all the cards in the repository, only use this for testing purposes
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void dropRepoTable()
        {
            repo.Open();
            repo.DropTable();
            repo.Close();
        }
        /// <summary>
        /// Draws card for the player on their turn
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void DrawHand()
        {
            Random rnd = new Random();
            if (turn <=1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (PlayerDeck.Count >= 1 && playerCards.Count < 5)
                    {
                        int temp = rnd.Next(0, PlayerDeck.Count);

                        playerCards.Add(PlayerDeck[temp]);
                        PlayerDeck.RemoveAt(temp);
                    }


                }
            }
            else
            {

                for (int i = 0; i < 2; i++)
                {
                    if (PlayerDeck.Count >= 1 && playerCards.Count < 5)
                    {
                        int temp = rnd.Next(0, PlayerDeck.Count);

                        playerCards.Add(PlayerDeck[temp]);
                        PlayerDeck.RemoveAt(temp);
                    }
                }
            }
            //sorts the hand
            SortHand();
            LoadContent();

        }
        /// <summary>
        /// Used to check for keyboard input
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void Input(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.T) && tPress == false )
            {
                tPress = true;
                foreach (CardSpace item in playerSpaces)
                {
                    if (item.card != null)
                    {
                        item.card.CardEffect(enemySpaces, playerSpaces);
                        if (item.card.Health <= 0)
                        {
                            item.card = null;
                        }
                    }
                }

                foreach (CardSpace item in enemySpaces)
                {
                    if (item.card != null)
                    {
                       item.card.CardEffect(enemySpaces, playerSpaces);
                        if (item.card.Health <= 0)
                        {
                            item.card = null;
                        }
                    }
                }
            }

            if (state.IsKeyUp(Keys.T))
            {
                tPress = false;
            }
        }
        /// <summary>
        /// ends the turn which will start a thread, activating all the card if it's not round one
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void endTurn(GameTime gameTime)
        {
            Thread newRoundEndThread = new Thread(() => ThreadWork(gameTime))
            {
                IsBackground = true
            };
            newRoundEndThread.Start();
        }
        /// <summary>
        /// Starts a thread which sorts the players hand after name
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public void SortHand()
        {
            Thread sortHandEndThread = new Thread(() => ThreadWorkSorting())
            {
                IsBackground = true
            };
            sortHandEndThread.Start();
        }
        /// <summary>
        /// The update for deckbuilding
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void UpdateDeckBuilding(GameTime gameTime)
        {
            cardInfo = false;

            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 2 && bPress == false)
            {
                tutorial = 3;
            }

            foreach (UI item in GameUI)
            {
                item.color = Color.White;
                if (item.Collision.Contains(mousePos))
                {
                    item.color = Color.Green;
                    if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "MenuButton")
                    {
                        gameState = GameState.StageSelect;
                        Initialize();
                        LoadContent();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "ClearDeck")
                    {
                        repo.Open();
                        repo.ClearDeck();
                        repo.Close();
                        Initialize();
                        LoadContent();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "RightArrow" && bPress == false && pageNumber <=1)
                    {
                        bookFlip.Play();
                        bPress = true;
                        pageNumber += 1;
                        Initialize();
                        LoadContent();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "LeftArrow" && bPress == false && pageNumber >=1)
                    {
                        bookFlip.Play();
                        bPress = true;
                        pageNumber -= 1;
                        Initialize();
                        LoadContent();
                    }
                }
            }


            if (mouseState.ScrollWheelValue > scroll)
            {
                scrollUp();
                scroll = mouseState.ScrollWheelValue;
            }
            else if (mouseState.ScrollWheelValue < scroll)
            {
                scrollDown();
                scroll = mouseState.ScrollWheelValue;
            }



            for (int i = ScrollValue; i < PlayerDeck.Count; i++)
            {
                if (PlayerDeck[i]!= null)
                {
                    if (PlayerDeck[i].Collision.Contains(mousePos))
                    {
                        cardInfo = true;
                        infoCard = PlayerDeck[i];
                        PlayerDeck[i].color = Color.Green;

                        if (mouseState.LeftButton == ButtonState.Pressed && bPress == false)
                        {
                            bPress = true;
                            RemoveCardFromDeck(PlayerDeck[i].Name);
                            RefresDeckBuildingLists();
                        }

                    }
                    else
                    {
                        PlayerDeck[i].color = Color.White;
                    }
                }
            }
            for (int i = 0; i < storageSpaces.Count; i++)
            {
                if (storageSpaces[i].card != null)
                {
                    storageSpaces[i].card.color = Color.White;

                    if (storageSpaces[i].Collision.Contains(mousePos))
                    {
                        cardInfo = true;
                        infoCard = storageSpaces[i].card;
                        storageSpaces[i].card.color = Color.Green;
                        if (mouseState.LeftButton == ButtonState.Pressed && bPress == false && PlayerDeck.Count < 30)
                        {
                            bPress = true;
                            AddCardToDeck(storageSpaces[i].card.Name);
                            RefresDeckBuildingLists();
                        }
                    }
                }
                
            }
            for (int i = 0; i < PlayerDeck.Count; i++)
            {
                PlayerDeck[i].SetDeckBuildingPosition(i,ScrollValue);
            }

            if (mouseState.LeftButton == ButtonState.Released && bPress == true)
            {
                bPress = false;
            }
        }
        /// <summary>
        /// Update for stageSelect
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void UpdateStageSelect(GameTime gameTime)
        {
            repo.Open();
            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 1 && repo.FindAllCards().Count <=19)
            {
                tutorial = 2;
            }
            repo.Close();

            foreach (UI item in GameUI)
            {
                item.color = Color.White;
                switch (item.spritePick)
                {
                    case "StageSelectButtons/Enemy1":
                        if (unlockDiff >= 1)
                        {
                            
                        }
                        break;
                    case "StageSelectButtons/Enemy2":
                        if (unlockDiff < 2)
                        {
                            item.color = Color.SlateGray;
                        }
                        break;
                    case "StageSelectButtons/Enemy3":
                        if (unlockDiff < 3)
                        {
                            item.color = Color.SlateGray;
                        }
                        break;
                    case "StageSelectButtons/Enemy4":
                        if (unlockDiff < 4)
                        {
                            item.color = Color.SlateGray;
                        }
                        break;
                    case "StageSelectButtons/Enemy5":
                        if (unlockDiff < 5)
                        {
                            item.color = Color.SlateGray;
                        }
                        break;
                }
                if (item.Collision.Contains(mousePos))
                {
                    item.color = Color.Red;
                    switch (item.spritePick)
                    {
                        case "StageSelectButtons/Enemy1":
                            if (unlockDiff >= 1)
                            {
                                item.color = Color.Green;
                            }
                            break;
                        case "StageSelectButtons/Enemy2":
                            if (unlockDiff >= 2)
                            {
                                item.color = Color.Green;
                            }
                            break;
                        case "StageSelectButtons/Enemy3":
                            if (unlockDiff >= 3)
                            {
                                item.color = Color.Green;
                            }
                            break;
                        case "StageSelectButtons/Enemy4":
                            if (unlockDiff >= 4)
                            {
                                item.color = Color.Green;
                            }
                            break;
                        case "StageSelectButtons/Enemy5":
                            if (unlockDiff >= 5)
                            {
                                item.color = Color.Green;
                            }
                            break;
                        case "StageSelectButtons/DeckBuilder":
                            item.color = Color.Green;
                            break;
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        switch (item.spritePick)
                        {
                            case "StageSelectButtons/Enemy1":
                                if (unlockDiff >= 1)
                                {
                                    difficulty = 1;
                                    gameState = GameState.CardBoard;
                                    Initialize();
                                    LoadContent();
                                    
                                }
                                break;
                            case "StageSelectButtons/Enemy2":
                                if (unlockDiff >= 2)
                                {
                                    difficulty = 2;
                                    gameState = GameState.CardBoard;
                                    Initialize();
                                    LoadContent();
                                }
                                break;
                            case "StageSelectButtons/Enemy3":
                                if (unlockDiff >= 3)
                                {
                                    difficulty = 3;
                                    gameState = GameState.CardBoard;
                                    Initialize();
                                    LoadContent();
                                }
                                break;
                            case "StageSelectButtons/Enemy4":
                                if (unlockDiff >= 4)
                                {
                                    difficulty = 4;
                                    gameState = GameState.CardBoard;
                                    Initialize();
                                    LoadContent();
                                }
                                break;
                            case "StageSelectButtons/Enemy5":
                                if (unlockDiff >= 5)
                                {
                                    difficulty = 4;
                                    gameState = GameState.CardBoard;
                                    Initialize();
                                    LoadContent();
                                }
                                break;
                            case "StageSelectButtons/DeckBuilder":
                                gameState = GameState.DeckBuilding;
                                Initialize();
                                LoadContent();
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This refreshes the deckbuilding lists, call this after the player removes or adds a card to the deck
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void RefresDeckBuildingLists()
        {
                repo.Open();
                PlayerDeck = repo.FindDeck();
                AllOwnedCards = repo.FindAllCards();
                repo.Close();

                LoadContent();

                SetDeckBuildingCardCount();

            SortByNameAlgoByQuickSort(ref PlayerDeck);
            
        }
        /// <summary>
        /// This Removes a card from the deck
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void RemoveCardFromDeck(string cardName)
        {
            repo.Open();
            repo.removeCard(cardName);
            repo.Close();
        }
        /// <summary>
        /// This is what happens when you scroll the scroll wheel up
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void scrollDown()
        {
            if (ScrollValue <= PlayerDeck.Count - 13)
            {
                ScrollValue += 1;
            }
        }
        /// <summary>
        /// This is what happens when you scroll the scroll wheel down
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void scrollUp()
        {
            if (ScrollValue >= 1)
            {
                ScrollValue -= 1;
            }
        }
        /// <summary>
        /// this will add a card to the deck in the deckbuilding screen
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void AddCardToDeck(string cardName)
        {
            repo.Open();
            repo.AddToDeck(cardName);
            repo.Close();
        }

        public void SetCoins()
        {


            foreach (CardSpace item in playerSpaces)
            {
                item.CoinSetUp(playerSpaces);
            }
        }
        /// <summary>
        /// Will update all the coins for both playerSpaces and enemySpaces
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public static void UpdateCoinsForLists()
        {

            foreach (var item in playerSpaces)
            {
                CoinSetUp(playerSpaces, item.spaceNumber);
            }
            foreach (var item in enemySpaces)
            {
                CoinSetUp(enemySpaces, item.spaceNumber);
            }
        }
        /// <summary>
        /// This is update for combat against AI
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void UpdateCardBoard(GameTime gameTime)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 3 && bPress == false)
            {
                tutorial = 4;
                bPress = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 4 && bPress == false)
            {
                tutorial = 5;
                LoadContent();
                bPress = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 5 && bPress == false)
            {
                tutorial = 6;
                LoadContent();
                bPress = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && tutorial == 6 && bPress == false)
            {
                tutorial = 7;
            }


            UpdateCoinsForLists();
            if (enemyHealth <= 0)
            {
                endScreen = true;
            }
            if (playerHealth <=0)
            {
                endScreen = true;
            }
            if (endScreen == true && WinOrLoseScreenList.Count <=1 && enemyHealth <= 0)
            {
                WinOrLoseScreenList.Add(new UI("Win",new Vector2(400,100)));
                WinOrLoseScreenList.Add(new UI("MenuButton", new Vector2(650, 650)));
                LoadContentForThisUIList(WinOrLoseScreenList);

                CardReward();
                diffRepo.Open();
                if (difficulty >= diffRepo.FindDiff())
                {
                    diffRepo.UpdateUnlockedDifficulty(difficulty + 1);
                }
                diffRepo.Close();

            }
            if (endScreen == true && WinOrLoseScreenList.Count <= 1 && playerHealth <= 0)
            {
                WinOrLoseScreenList.Add(new UI("Lose", new Vector2(400, 100)));
                WinOrLoseScreenList.Add(new UI("MenuButton", new Vector2(650, 650)));
                LoadContentForThisUIList(WinOrLoseScreenList);


            }

            if (endScreen == true)
            {
                foreach (var item in WinOrLoseScreenList)
                {
                    item.color = Color.White;
                    if (item.Collision.Contains(mousePos)  && item.spritePick == "MenuButton")
                    {
                        item.color = Color.Green;
                    }
                    if (item.Collision.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "MenuButton")
                    {
                        rewardCard = null;
                        gameState = GameState.StageSelect;
                        Initialize();
                        LoadContent();
                    }
                }
            }

            if (endScreen == false)
            {

                if (cardInfo == true)
                {
                    cardInfo = false;
                }


                if (playerTurn == false)
                {
                    if (endTurnOnlyOnce == true)
                    {
                        enemy.DrawHand();
                        enemy.AITurn(enemySpaces);
                        EnemyCardsLoadContent();
                        endTurn(gameTime);
                        endTurnOnlyOnce = false;
                        turn += 1;
                    }
                }
                for (int i = 0; i < playerSpaces.Count; i++)
                {
                    if (playerSpaces[i].Collision.Contains(mousePos) && playerSpaces[i].card != null)
                    {
                        cardInfo = true;
                        infoCard = playerSpaces[i].card;
                    }
                    playerSpaces[i].setPos(i);
                    playerSpaces[i].CanPlace = false;
                    playerSpaces[i].canPlaceButNotEnoughCoins = false;
                }
                for (int i = 0; i < enemySpaces.Count; i++)
                {
                    if (enemySpaces[i].Collision.Contains(mousePos) && enemySpaces[i].card != null)
                    {
                        cardInfo = true;
                        infoCard = enemySpaces[i].card;
                    }
                    enemySpaces[i].setEnemyPos(i);
                }
                //for (int i = 0; i < enemyDeck.Count; i++)
                //{
                //    enemyDeck[i].setEnemyPos(i);
                //}

                // TODO: Add your update logic here


                foreach (UI item in GameUI)
                {
                    if (item.spritePick == "EndTurnButton" && playerTurn == false && item.clicked == true)
                    {
                        item.clicked = false;
                    }
                    if (item.Collision.Contains(mousePos))
                    {
                        item.color = Color.Goldenrod;
                        if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "EndTurnButton" && bPress == false && playerTurn == true && item.clicked == false)
                        {
                            bPress = true;
                            endTurn(gameTime);
                            turn += 1;
                            item.clicked = true;
                        }
                        if (mouseState.LeftButton == ButtonState.Pressed && item.spritePick == "TestButton" && bPress == false)
                        {
                            gameState = GameState.StageSelect;
                            Initialize();
                            LoadContent();
                            bPress = true;
                        }
                    }
                    else if (item.color != Color.White)
                    {
                        item.color = Color.White;
                    }
                }
                foreach (CardSpace item in playerSpaces)
                {
                    if (item.Collision.Contains(mousePos) && refCard != null && item.card == null)
                    {
                        if (refCard.Coin > item.spaceCoin)
                        {
                            item.canPlaceButNotEnoughCoins = true;
                        }
                        item.CanPlace = true;
                    }
                    if (item.Collision.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && bPress == false && refCard != null && item.card == null && playerTurn == true)
                    {

                        if (item.spaceCoin >= refCard.Coin)
                        {
                            item.setCard(refCard);
                            playerCards.Remove(refCard);
                            refCard = null;
                            //Place soundeffect
                            placeCard.Play();
                        }
                        bPress = true;
                        //CoinSetUp(playerSpaces, item.spaceNumber);
                    }
                }
                ListUpdate(playerCards);
                if (refCard != null)
                {
                    refCard.position.X = mousePos.X;
                    refCard.position.Y = mousePos.Y;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && bPress == false && refCard!=null)
                {
                    refCard = null;
                    bPress = true;
                }

                if (playerTurn == true && drawnCards == false)
                {
                    DrawHand();
                    drawnCards = true;
                }

            }

            
        }
        /// <summary>
        /// this is draw for deckbuilding
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void drawDeckBuilding(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(deckBuildingBackground, new Vector2(0, 0), Color.White);

            _spriteBatch.DrawString(font, $"Deck: {PlayerDeck.Count}/30", new Vector2(980 , 750), Color.White);
            _spriteBatch.DrawString(font, $"Use Scrollwheel to scroll through the deck", new Vector2(980, 770), Color.White);
            _spriteBatch.DrawString(font, $"Page: {pageNumber+1}/3", new Vector2(375, 650), Color.White);

            var max = 12 + ScrollValue;
            if (max >=PlayerDeck.Count)
            {
                max = PlayerDeck.Count;
            }
            for (int i = ScrollValue; i < max; i++)
            {
                PlayerDeck[i].Draw(this._spriteBatch);
            }

            foreach (UI item in GameUI)
            {
                item.Draw(this._spriteBatch);
            }

            foreach (var item in storageSpaces)
            {
                item.Draw(this._spriteBatch);
            }

            if (cardInfo == true)
            {
                _spriteBatch.Draw(infoCard.sprite, new Vector2(1200, 150), null, Color.White, 0f,
                Vector2.Zero, 1f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(GameWorld.font, $"{infoCard.Damage}", new Vector2(1237, 480), Color.Black);
                _spriteBatch.DrawString(GameWorld.font, $"{infoCard.Health}", new Vector2(1410, 477), Color.Goldenrod);

            }

            if (tutorial == 2)
            {
                _spriteBatch.Draw(tutorialBackground, new Vector2(0, 0), Color.White);
            }
           

        }
        /// <summary>
        /// this is draw for combat against AI
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void drawCardBoard(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(-150, 0), Color.White);
            _spriteBatch.DrawString(Bigfont, $"{enemyHealth}", new Vector2(165, 75), Color.White);
            if (playerTurn == false && healthDamage >=1)
            {
                _spriteBatch.DrawString(Bigfont, $"-{healthDamage}", new Vector2(220, 880), Color.Red);
            }
            _spriteBatch.DrawString(Bigfont, $"{playerHealth}", new Vector2(170, 880), Color.White);
            if (playerTurn == true)
            {
                _spriteBatch.DrawString(Bigfont, $"Player", new Vector2(0, 450), Color.Green);
            }
            if (playerTurn == true && healthDamage >= 1)
            {
                _spriteBatch.DrawString(Bigfont, $"-{healthDamage}", new Vector2(220, 75), Color.Red);
            }
            if (playerTurn == false)
            {
                _spriteBatch.DrawString(Bigfont, $"AI", new Vector2(0, 370), Color.Red);
            }
            //_spriteBatch.DrawString(Bigfont, $"Turn : {turn}", new Vector2(0, 200), Color.Black);

            foreach (var item in enemySpaces)
            {
                if (item.card != null)
                {
                    item.card.Draw(this._spriteBatch);
                }
                item.DrawCoin(this._spriteBatch);
            }
            foreach (UI item in GameUI)
            {
                item.Draw(this._spriteBatch);
            }
            foreach (var item in playerSpaces)
            {
                item.DrawCanPlaceHere(this._spriteBatch);
                if (item.card != null)
                {
                    item.card.Draw(this._spriteBatch);
                }
                item.DrawCoin(this._spriteBatch);
            }
            foreach (CardBase item in playerCards)
            {
                if (item.sprite!=null)
                {
                    item.Draw(this._spriteBatch);
                }
            }
            if (cardInfo == true)
            {
                _spriteBatch.Draw(infoCard.sprite, new Vector2(1300, 400), null, Color.White, 0f,
                Vector2.Zero, 1f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(GameWorld.font, $"{infoCard.Damage}", new Vector2(1337, 730), Color.Black);
                _spriteBatch.DrawString(GameWorld.font, $"{infoCard.Health}", new Vector2(1505, 727), Color.Goldenrod);

            }

            if (endScreen == true)
            {
                foreach (var item in WinOrLoseScreenList)
                {
                    item.Draw(this._spriteBatch);
                }
                if (playerHealth <= 0)
                {
                    _spriteBatch.DrawString(Bigfont, $"You lose!", new Vector2(740, 350), Color.Black);
                }
                else if (enemyHealth <=0)
                {
                    _spriteBatch.DrawString(Bigfont, $"You WIN!", new Vector2(715, 125), Color.Black);
                    _spriteBatch.DrawString(Bigfont, $"Reward", new Vector2(725, 250), Color.Black);
                }

                if (rewardCard!=null)
                {
                    rewardCard.Draw2(this._spriteBatch);
                }

            }

            if (tutorial == 4 || tutorial == 5 || tutorial == 6)
            {
                _spriteBatch.Draw(tutorialBackground, new Vector2(0, 0), Color.White);
            }
        }
        /// <summary>
        /// this is draw for stageSelect
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void drawStageSelect()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(StageSelectBackground, new Vector2(0, 0), Color.White);


            repo.Open();
            if (tutorial == 1 && repo.FindAllCards().Count <=19)
            {
                _spriteBatch.Draw(tutorialBackground, new Vector2(0, 0), Color.White);
            }
            repo.Close();

            foreach (UI item in GameUI)
            {
                item.Draw(this._spriteBatch);
            }
        }
        /// <summary>
        /// call this to set the numbers in deckbuilding
        /// <para>the numbers of how many is in the storage, and how many is in the deck</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void SetDeckBuildingCardCount()
        {
            var storage = 0;
            var deck = 0;
            foreach (var item in storageSpaces)
            {
                if (item.card != null)
                {
                    repo.Open();
                    storage = repo.FindAllCardsInStorageWithThisName(item.card.Name).Count;
                    deck = repo.FindAllCardsInDeckWithThisName(item.card.Name).Count;
                    repo.Close();

                    item.SetCardCount(storage, deck);
                }
            }
        }
        /// <summary>
        /// What work the thread for sorting the hand should do
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public void ThreadWorkSorting()
        {
            SortByNameAlgoByQuickSort(ref playerCards);
        }

        /// <summary>
        /// What work the endturn thread should do
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void ThreadWork(GameTime gameTime)
        {
            if (turn >=3)
            {
                var WhileBool = true;
                while (WhileBool == true && endScreen == false)
                {
                    if (playerTurn == true)
                    {
                        foreach (CardSpace item in playerSpaces)
                        {
                            if (item.card != null)
                            {
                                if (item.card.Health <= 0)
                                {
                                    if (item.card.Name == "DavyJonesLocker")
                                    {
                                        item.card.AdditionalCardEffect(enemySpaces, playerSpaces);
                                        LoadContent();
                                    }
                                    item.card = null;
                                }
                            }


                            if (item.card != null)
                            {
                                item.card.color = Color.LawnGreen;
                                item.card.CardEffect(enemySpaces, playerSpaces);
                            }

                            foreach (CardSpace enemyItem in enemySpaces)
                            {
                                if (enemyItem.card != null)
                                {
                                    if (enemyItem.card.damageTaken > 0)
                                    {
                                        enemyItem.card.tookDamage = true;
                                    }
                                }
                            }
                            foreach (CardSpace Item in playerSpaces)
                            {
                                if (Item.card != null)
                                {
                                    if (Item.card.damageTaken > 0)
                                    {
                                        Item.card.tookDamage = true;
                                    }
                                }
                            }
                            if (item.card != null)
                            {
                                Thread.Sleep(500);
                            }

                            foreach (CardSpace enemyItem in enemySpaces)
                            {
                                if (enemyItem.card != null)
                                {
                                    enemyItem.card.tookDamage = false;
                                    enemyItem.card.damageTaken = 0;

                                    if (enemyItem.card.Health <= 0)
                                    {
                                        if (enemyItem.card.Name == "DavyJonesLocker")
                                        {
                                            enemyItem.card.AdditionalCardEffect(enemySpaces, playerSpaces);
                                            LoadContent();
                                        }
                                        enemyItem.card = null;

                                    }
                                }
                            }
                            if (item.card != null)
                            {
                                Thread.Sleep(200);
                            }
                            if (item.card != null)
                            {
                                item.card.color = Color.White;
                            }


                        }
                        foreach (CardSpace item in enemySpaces)
                        {
                            if (item.card != null)
                            {
                                if (item.card.Health <= 0)
                                {
                                    if (item.card.Name == "DavyJonesLocker")
                                    {
                                        item.card.AdditionalCardEffect(enemySpaces, playerSpaces);
                                        LoadContent();
                                    }
                                    item.card = null;
                                }
                            }

                        }
                        WhileBool = false;
                    }
                    else
                    {
                        Thread.Sleep(499);
                        for (int i = 0; i < 8; i++)
                        {
                            var tmp = 0;

                            if (i <= 3)
                            {

                                tmp = (i + 4);
                            }
                            else
                            {
                                tmp = (i - 4);
                            }

                            if (enemySpaces[tmp].card != null)
                            {
                                enemySpaces[tmp].card.color = Color.LawnGreen;
                                enemySpaces[tmp].card.CardEffect(enemySpaces, playerSpaces);
                            }

                            if (playerHealth <= 0)
                            {
                                WhileBool = false;
                                break;
                            }

                            foreach (CardSpace playerItem in playerSpaces)
                            {
                                if (playerItem.card != null)
                                {
                                    if (playerItem.card.damageTaken > 0)
                                    {
                                        playerItem.card.tookDamage = true;
                                    }
                                }

                            }
                            foreach (CardSpace enemyItem in enemySpaces)
                            {
                                if (enemyItem.card != null)
                                {
                                    if (enemyItem.card.damageTaken > 0)
                                    {
                                        enemyItem.card.tookDamage = true;
                                    }
                                }
                            }

                            if (enemySpaces[tmp].card != null)
                            {
                                Thread.Sleep(500);
                            }

                            foreach (CardSpace playerItem in playerSpaces)
                            {
                                if (playerItem.card != null)
                                {
                                    playerItem.card.tookDamage = false;
                                    playerItem.card.damageTaken = 0;
                                    if (playerItem.card.Health <= 0)
                                    {
                                        if (playerItem.card.Name == "DavyJonesLocker")
                                        {
                                            playerItem.card.AdditionalCardEffect(enemySpaces, playerSpaces);
                                            LoadContent();
                                        }
                                        playerItem.card = null;
                                    }

                                }
                            }
                            if (enemySpaces[tmp].card != null)
                            {
                                Thread.Sleep(200);
                            }
                            if (enemySpaces[tmp].card != null)
                            {
                                enemySpaces[tmp].card.color = Color.White;
                            }

                        }
                        foreach (CardSpace item in playerSpaces)
                        {
                            if (item.card != null)
                            {
                                if (item.card.Health <= 0)
                                {
                                    if (item.card.Name == "DavyJonesLocker")
                                    {
                                        item.card.AdditionalCardEffect(enemySpaces, playerSpaces);
                                        LoadContent();
                                    }
                                    item.card = null;
                                }
                            }

                        }




                    }
                    WhileBool = false;
                    playerTurn = !playerTurn;
                    endTurnOnlyOnce = true;
                    drawnCards = false;
                    healthDamage = 0;

                }
            }
            else
            {
                healthDamage = 0;
                playerTurn = !playerTurn;
                endTurnOnlyOnce = true;
                drawnCards = false;
            }
            

        }
        /// <summary>
        /// loads the content for all enemy cards in cardboard
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void EnemyCardsLoadContent()
        {
            foreach (var item in enemySpaces)
            {
                item.LoadContent(this.Content);
                if (item.card != null)
                {
                    item.card.LoadContent(this.Content);
                }
            }
        }
        /// <summary>
        /// Sets up all the coins for the spaces in cardboard / against the AI
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public static void CoinSetUp(List<CardSpace> refList,int cSPace)
        {
            switch (cSPace)
            {
                case 0:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 4].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace + 1].checkCard().Coin + refList[cSPace + 4].checkCard().Coin)+1;
                    }
                    break;
                case 1:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 4].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 1].checkCard().Coin + 
                            refList[cSPace + 1].checkCard().Coin + refList[cSPace+4].checkCard().Coin)+1;

                    }
                    break;
                case 2:
                    if (refList[cSPace].card == null)
                    {
                    //    refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;
                    //    refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;
                    //    refList[cSPace + 4].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 1].checkCard().Coin +
                           refList[cSPace + 1].checkCard().Coin + refList[cSPace + 4].checkCard().Coin)+1;
                    }
                    break;
                case 3:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 4].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 1].checkCard().Coin + refList[cSPace + 4].checkCard().Coin)+1;
                    }
                    break;
                case 4:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 4].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 4].checkCard().Coin +
                           refList[cSPace + 1].checkCard().Coin)+1;
                    }
                    break;
                case 5:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 4].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 4].checkCard().Coin +
                           refList[cSPace - 1].checkCard().Coin + refList[cSPace + 1].checkCard().Coin)+1;
                    }
                    break;
                case 6:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 4].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace + 1].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 4].checkCard().Coin +
                           refList[cSPace - 1].checkCard().Coin + refList[cSPace + 1].checkCard().Coin)+1;
                    }
                    break;
                case 7:
                    if (refList[cSPace].card == null)
                    {
                        //refList[cSPace - 4].spaceCoin += refList[cSPace].spaceCoin;
                        //refList[cSPace - 1].spaceCoin += refList[cSPace].spaceCoin;

                        refList[cSPace].spaceCoin = (refList[cSPace - 4].checkCard().Coin +
                           refList[cSPace - 1].checkCard().Coin)+1;
                    }
                    break;

            }
        }
        /// <summary>
        /// Decides which reward the player should get if they win a game
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void CardReward()
        {
            Random rnd = new Random();

            var tmp = rnd.Next(0,104);

            switch (difficulty)
            {
                case 1:
                    if (tmp <=30)
                    {
                        rewardCard = new Swapper();
                    }
                    if (tmp >=31 && tmp <=50)
                    {
                        rewardCard = new SmallSlime();
                    }
                    if (tmp >= 51 && tmp <=70)
                    {
                        rewardCard = new Cannibal();
                    }
                    if (tmp >= 71 && tmp <= 91)
                    {
                        rewardCard = new Musketeer();
                    }
                    if (tmp >= 92 && tmp <= 102)
                    {
                        rewardCard = new Thief();
                    }
                    if (tmp >= 103)
                    {
                        rewardCard = new Captain();
                    }
                    break;
                case 2:
                    if (tmp <= 26)
                    {
                        rewardCard = new FatPirate();
                    }
                    if (tmp <=27 && tmp <= 51)
                    {
                        rewardCard = new BigSlime();
                    }
                    if (tmp >= 52 && tmp <= 76)
                    {
                        rewardCard = new SniperParrot();
                    }
                    if (tmp >= 77 && tmp <= 90)
                    {
                        rewardCard = new Whale();
                    }
                    if (tmp>=91 && tmp<=102)
                    {
                        rewardCard = new DavyJonesLocker();
                    }
                    if (tmp >= 103)
                    {
                        rewardCard = new Captain();
                    }
                    break;
                case 3:
                    if (tmp <= 25)
                    {
                        rewardCard = new DavyJonesLocker();
                    }
                    if (tmp >= 26 && tmp <= 36)
                    {
                        rewardCard = new Gambler();
                    }
                    if (tmp >= 37 && tmp <= 50)
                    {
                        rewardCard = new Mimic();
                    }
                    if (tmp >= 51 && tmp <= 75)
                    {
                        rewardCard = new Whale();
                    }
                    if (tmp >=76 && tmp <=102)
                    {
                        rewardCard = new FatPirate();
                    }
                    if (tmp >= 103)
                    {
                        rewardCard = new Captain();
                    }
                    break;
                case 4:
                    if (tmp <= 50)
                    {
                        rewardCard = new Cannibal();
                    }
                    if (tmp >= 51 && tmp <= 75)
                    {
                        rewardCard = new Cannon();
                    }
                    if (tmp >= 76 && tmp <= 102)
                    {
                        rewardCard = new Whale();
                    }
                    if (tmp >= 103)
                    {
                        rewardCard = new Captain();
                    }
                    break;
                case 5:
                    rewardCard = new Mimic();
                    break;
            }




            rewardCard.position = new Vector2(700,300);
            rewardCard.LoadContent(this.Content);

            repo.Open();
            repo.AddCardToStorage(rewardCard.Name);
            repo.Close();
        }

        //A Quicksort method which takes in a list of cards of the CardBase class
        /// <summary>
        /// Sorts a liste by names
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public List<CardBase> SortByNameAlgoByQuickSort(ref List<CardBase> ListOfCards)
        {
            //If there is 1 or less amount of cards in the list, it returns the card.
            if (ListOfCards.Count <= 1)
            {
                return ListOfCards;
            }
            //Sets pivot to be the first card in the list. 
            CardBase pivot = ListOfCards[0];

            //Makes a "before" and an "after" list for the cards that comes before and after in terms of the first letter in the name
            List<CardBase> before = new List<CardBase>();
            List<CardBase> after = new List<CardBase>();

            for (int i = 1; i < ListOfCards.Count; i++)
            {
                //Checks if the first letter of the Name of pivot (the first card in the list) comes after 
                //the cards in the list and if so, adds it to the before list. Otherwise if the first letter comes before
                //it adds the card to the after list.
                if (ListOfCards[i].Name[0] < pivot.Name[0])
                {
                    before.Add(ListOfCards[i]);
                }
                else
                {
                    after.Add(ListOfCards[i]);
                }
            }
            //A result list has been made to add all the cards.
            List<CardBase> result = new List<CardBase>();
            //First adds all the cards with the Names that comes before pivot's Name. 
            result.AddRange(SortByNameAlgoByQuickSort(ref before));
            //Adds the card that is pivot that is the first card that is originally the first card in the list, but now comes
            //after the cards in the before list.
            result.Add(pivot);
            //Adds all the cards in the after list, which is is all the cards with the Name that comes after pivot and the before list.
            result.AddRange(SortByNameAlgoByQuickSort(ref after));
            //After the cards have been sorted and added to the result list, it sets the original list "ListOfCards" equal to the
            //result list which has been sorted and returns the sorted list. 
            ListOfCards = result;
            return ListOfCards;
        }
    }
}

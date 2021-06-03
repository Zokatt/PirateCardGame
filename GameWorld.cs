﻿using Microsoft.Xna.Framework;
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
    public enum GameState { 
    CardBoard,DeckBuilding,StageSelect
    }
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Rectangle screenBounds = new Rectangle(0, 0, 1600, 1000);
        public static string Deck = "Deck";
        public static string Storage = "Storage";
        public static CardRepository repo;
        public static EnemyDifficultyRepository diffRepo;
        public static List<CardBase> playerCards;
        public static List<CardSpace> playerSpaces;
        public static List<CardSpace> enemySpaces;
        public static List<CardBase> PlayerDeck;
        public static List<CardBase> AllOwnedCards;
        public static List<CardBase> checkCardCount;
        public List<UI> WinOrLoseScreenList;
        public bool endScreen;
        public List<StorageSpace> storageSpaces;
        public int owned;
        public static List<UI> GameUI;
        public static List<CardBase> enemyDeck;
        public static CardBase refCard;
        private Texture2D background;
        private Texture2D deckBuildingBackground;
        private Texture2D StageSelectBackground;
        private Texture2D tutorialBackground;
        public static SpriteFont font;
        public static SpriteFont Bigfont;
        public static Point mousePos;
        public static MouseState mouseState;
        public static bool cardInfo = false;
        public static int cardInfoNumber;
        public static bool bPress = false;
        public static bool tPress = false;
        public static bool playerTurn = false;
        public static Enemy enemy;
        public static CardBase infoCard;
        public static CardBase rewardCard;
        public static GameState gameState = GameState.StageSelect;
        public static int pageNumber = 0;
        public static int ScrollValue = 0;
        public int scroll;
        public static int enemyHealth = 0;
        public static int playerHealth = 0;
        public static int healthDamage = 0;
        public bool drawnCards = false;
        public static int turn = 0;
        public static bool endTurnOnlyOnce = true;
        public int difficulty = 1;
        public int tutorial = 1;
        public SoundEffect placeCard;
        public SoundEffect woodClick;
        public SoundEffect bookFlip;
        public int unlockDiff { get; set; } = 1;

        //public static GameState gameState = GameState.CardBoard;
        public static Director director = new Director(new EnemyBuilder());

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = screenBounds.Height;
            _graphics.PreferredBackBufferWidth = screenBounds.Width;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
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
                GameUI.Add(new UI("TestButton", new Vector2(450, 450)));
                enemyDeck = new List<CardBase>();
                turn = 0;

                enemy = new Enemy(difficulty);
                enemy.Deck = director.ConstructEnemyDeck(enemy.difficulty);

                for (int i = 0; i < 8; i++)
                {
                    playerSpaces.Add(new CardSpace(i));
                }

                for (int i = 0; i < 8; i++)
                {
                    enemySpaces.Add(new CardSpace(i));
                }

                //for (int i = 0; i < 8; i++)
                //{
                //    enemySpaces[i].setCard(new Captain());
                //}

                var mapper = new CardMapper();
                var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                repo = new CardRepository(provider, mapper);

                //dropRepoTable();

                repo.Open();

                PlayerDeck = repo.FindDeck();

                repo.Close();

                //Random rnd = new Random();

                //for (int i = 0; i < 5; i++)
                //{
                //    int temp = rnd.Next(0, PlayerDeck.Count);

                //    playerCards.Add(PlayerDeck[temp]);
                //    PlayerDeck.RemoveAt(temp);
                //}

                var diffMapper = new DifficultyMapper();
                var diffProvider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                diffRepo = new EnemyDifficultyRepository(diffProvider, diffMapper);



                DrawHand();
                playerTurn = true;
            }
            else if (gameState == GameState.DeckBuilding)
            {
                bPress = true;

                var mapper = new CardMapper();
                var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                repo = new CardRepository(provider, mapper);

                GameUI = new List<UI>();
                GameUI.Add(new UI("MenuButton", new Vector2(800, 600)));
                GameUI.Add(new UI("LeftArrow", new Vector2(50, 600)));
                GameUI.Add(new UI("RightArrow", new Vector2(700, 600)));


                PlayerDeck = new List<CardBase>();
                AllOwnedCards = new List<CardBase>();
                storageSpaces = new List<StorageSpace>();

                repo.Open();
                PlayerDeck = repo.FindDeck();
                AllOwnedCards = repo.FindAllCards();
                repo.Close();

                for (int i = 0; i < 10; i++)
                {
                    storageSpaces.Add(new StorageSpace());
                    storageSpaces[i].SetCard(i);
                    storageSpaces[i].SetCardPos(i);
                    
                }
                SetDeckBuildingCardCount();

                RefresDeckBuildingLists();
                
            }
            else if (gameState == GameState.StageSelect)
            {
                GameUI = new List<UI>();

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
                repo.Open();
                if (repo.FindAllCards().Count <=0)
                {
                    MakePlayerDeck();
                }
                repo.Close();

                var diffMapper = new DifficultyMapper();
                var diffProvider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
                diffRepo = new EnemyDifficultyRepository(diffProvider, diffMapper);


                diffRepo.Open();
                if (diffRepo.FindDiff() == 69)
                {
                    diffRepo.AddUnlockedDifficulty(1);
                }
                unlockDiff = diffRepo.FindDiff();
                diffRepo.Close(); 


            }

            base.Initialize();
        }
        protected override void LoadContent()
        {
            if (_spriteBatch == null)
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);
            }

            font = Content.Load<SpriteFont>("Font");
            Bigfont = Content.Load<SpriteFont>("BigFont");
            placeCard = Content.Load<SoundEffect>("WoodKick");
            woodClick = Content.Load<SoundEffect>("WoodClick");
            bookFlip = Content.Load<SoundEffect>("BookFlipping");

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

        public void LoadContentForThisUIList(List<UI> refList)
        {
            foreach (var item in refList)
            {
                item.LoadContent(this.Content);
            }
        }

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


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input(gameTime);



            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            //if (mouseState.LeftButton == ButtonState.Pressed) //to check for postion placements
            //{

            //}//breakpoint here to test <--

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //_spriteBatch.Begin();

           
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

        public void dropRepoTable()
        {
            repo.Open();
            repo.DropTable();
            repo.Close();
        }

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
            SortHand();
            LoadContent();

        }

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

        public void endTurn(GameTime gameTime)
        {
            Thread newRoundEndThread = new Thread(() => ThreadWork(gameTime))
            {
                IsBackground = true
            };
            newRoundEndThread.Start();
        }
        public void SortHand()
        {
            Thread sortHandEndThread = new Thread(() => ThreadWorkSorting())
            {
                IsBackground = true
            };
            sortHandEndThread.Start();
        }

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

        public void RemoveCardFromDeck(string cardName)
        {
            repo.Open();
            repo.removeCard(cardName);
            repo.Close();
        }
        public void scrollDown()
        {
            if (ScrollValue <= PlayerDeck.Count - 13)
            {
                ScrollValue += 1;
            }
        }
        public void scrollUp()
        {
            if (ScrollValue >= 1)
            {
                ScrollValue -= 1;
            }
        }
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
                ListUpdate(playerCards);
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


                if (refCard != null)
                {
                    refCard.position.X = mousePos.X;
                    refCard.position.Y = mousePos.Y;
                }
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

        public void drawDeckBuilding(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(deckBuildingBackground, new Vector2(0, 0), Color.White);

            _spriteBatch.DrawString(font, $"Deck: {PlayerDeck.Count}/30", new Vector2(980 , 750), Color.White);
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
                if (item !=null)
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

            if (tutorial == 4 || tutorial == 5)
            {
                _spriteBatch.Draw(tutorialBackground, new Vector2(0, 0), Color.White);
            }
        }

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

        public void ThreadWorkSorting()
        {
            SortByNameAlgoByQuickSort(ref playerCards);
        }


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
        public List<CardBase> SortByNameAlgoByQuickSort(ref List<CardBase> ListOfCards)
        {
            if (ListOfCards.Count <= 1)
            {
                return ListOfCards;
            }
            CardBase pivot = ListOfCards[0];


            List<CardBase> before = new List<CardBase>();
            List<CardBase> after = new List<CardBase>();

            for (int i = 1; i < ListOfCards.Count; i++)
            {
                if (ListOfCards[i].Name[0] < pivot.Name[0])
                {
                    before.Add(ListOfCards[i]);
                }
                else
                {
                    after.Add(ListOfCards[i]);
                }
            }
            List<CardBase> result = new List<CardBase>();
            result.AddRange(SortByNameAlgoByQuickSort(ref before));
            result.Add(pivot);
            result.AddRange(SortByNameAlgoByQuickSort(ref after));
            ListOfCards = result;
            return ListOfCards;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Explore
{
    public static class WaveManager
    {
        private static int waveNumber;
        public static int WaveNumber {
            get {
                return waveNumber;
            }
        }

        private static bool firstWave;

        private static List<GameObject> enemies;

        private static Random rand;

        private static float initalBombShipCooldown = 10f;
        private static float bombShipCooldown = 5f;

        public static void Init() {
            waveNumber = 0;
            firstWave = true;
            enemies = new List<GameObject>();
            rand = new Random();
        }

        public static void Update() {

            if (firstWave) {
                for (int i = 0; i < 3; i++) {
                    NewShip();
                }
                firstWave = false;
            }

            for (int i = 0; i < enemies.Count; i++) {
                if (enemies[i].isDead) {
                    enemies.RemoveAt(i);
                } else {
                    enemies[i].Update();
                }
            }

            UpdateWaves();
        }

        private static void UpdateWaves() {
            if (enemies.Count == 0) {
                waveNumber++;
                for (int i = 0; i < 3 + rand.Next(waveNumber); i++) {
                    NewShip();
                }
                DropManager.EndOfWaveDrop();
            }

            if (bombShipCooldown <= 0) {
                NewBombShip();
                bombShipCooldown = initalBombShipCooldown;
            } else {
                bombShipCooldown -= GameManager.DeltaTime;
            }
        }

        public static void Draw(SpriteBatch spriteBatch) {

            for (int i = 0; i < enemies.Count; i++) {
                enemies[i].Draw(spriteBatch);
            }
        }

        public static void AddBaseEnemy(BaseEnemy e) {
            enemies.Add(e);
        }
        
        private static void NewShip() {
            BaseShip s = new BaseShip(new Vector2(rand.Next(-300, 300), 0));
            s.SetTexture();
            enemies.Add(s);
        }

        private static void NewBombShip() {
            BombShip b = new BombShip(new Vector2(rand.Next(-300, 300), 0));
            b.SetTexture();
            enemies.Add(b);
        }
    }
}
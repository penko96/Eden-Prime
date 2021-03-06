﻿using System;
using GameShadow.GameData;

namespace GameShadow.GameHelpers
{
    public static class GameInitializer
    {
        public const int ReducedFieldLength = 12;
        public const int ReducedFieldWidth = 12;
        public const int MovableTerrainOdds = 97;
        public const int NoSpawnRadius = 5;
        public const int EmoticonCount = 5;

        private static readonly int MaxFieldPosition = ReducedFieldWidth * ReducedFieldLength - 1;

        public static void GenerateObstacles(Game game)
        {
            int playerPositionX = game.Player.PositionX;
            int playerPositionY = game.Player.PositionY;
            Random rnd = new Random();

            for (int x = 0; x < ReducedFieldLength; x++)
            {
                for (int y = 0; y < ReducedFieldWidth; y++)
                {
                    if (x == playerPositionX && y == playerPositionY)
                        continue;

                    int r = rnd.Next(0, 100);
                    if (r > MovableTerrainOdds)
                    {
                        int position = (y * ReducedFieldWidth + x);
                        game.ObstaclesByPosition.Add(position, true);
                    }
                }
            }
        }

        public static void GenerateEmoticions(Game game)
        {
            Random rnd = new Random();

            while (game.Emoticons.Count != EmoticonCount)
            {
                int position = rnd.Next(0, MaxFieldPosition);

                if (!game.ObstaclesByPosition.ContainsKey(position))
                {
                    int positionX = position % ReducedFieldLength;
                    int positionY = position / ReducedFieldWidth;
                    int distanceToPlayer =
                        (positionX - game.Player.PositionX)
                        * (positionX - game.Player.PositionX)
                        + (positionY - game.Player.PositionY)
                        * (positionY - game.Player.PositionY);

                    if (distanceToPlayer > NoSpawnRadius * NoSpawnRadius)
                    {
                        Emoticon emoticon = new Emoticon(positionX, positionY);
                        emoticon.Type = EmoticonType.EmoticonAngry;
                        if (game.Emoticons.Count >= 3)
                            emoticon.Type = (EmoticonType)rnd.Next(0, 4);
                        game.Emoticons.Add(emoticon);
                    }
                }
            }
        }
    }
}
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmojiHunter.GameAnimation
{
    internal enum EmoticonType
    {
        SmileEmoticon,
        CheekyEmoticon,
        GrinEmoticon,
        LoveEmoticon,
        SadEmoticon,
        ShoutingEmoticon,
        ShyEmoticon,
        CryEmoticon,
        AngryEmoticon,
        OnfireEmoticon,
        Length
    }

    internal enum HeroType
    {
        LightHero,
        DarkHero
    }

    internal static class SpriteInitializer
    {
        private const float HeroFrameDuration = 250f;
        private const float EmoticonFrameDuration = 250f;
        private const float DeadEmoticonFrameDuration = 250f;
        private const float FreezeEmoticonFrameDuration = 250f;
        private const float CrazyEmoticonFrameDuration = 250f;
        private const int HeroFrameCount = 3;
        private const int EmoticonFrameCount = 10;
        private const int DeadEmoticonFrameCount = 3;
        private const int FreezeEmoticonFrameCount = 3;
        private const int CrazyEmoticonFrameCount = 3;

        public static void InitializeSprites(SpriteData spriteData, ContentManager content)
        {
            InitializeHeroSprite(spriteData, content);
            InitializeEmoticonSprite(spriteData, content);
        }

        private static void InitializeHeroSprite(SpriteData spriteData, ContentManager content)
        {
            var texture = content.Load<Texture2D>(@"Content\Heroes");
            var sprite = new AnimatedSprite(texture, new Rectangle(0, 128, 32, 32),
                HeroFrameDuration, HeroFrameCount);

            sprite.AddAnimation(texture, new Rectangle(0, 160, 32, 32),
                HeroFrameDuration, HeroFrameCount);
            sprite.AddAnimation(texture, new Rectangle(0, 192, 32, 32),
                HeroFrameDuration, HeroFrameCount);
            sprite.AddAnimation(texture, new Rectangle(0, 224, 32, 32),
                HeroFrameDuration, HeroFrameCount);

            sprite.Name = $"{HeroType.LightHero}";

            UpdateSpriteData(spriteData, sprite);
        }


        private static void InitializeEmoticonSprite(SpriteData spriteData, ContentManager content)
        {
            var texture = content.Load<Texture2D>(@"Content\Emoticons");
            for (EmoticonType emoticon = 0; emoticon < EmoticonType.Length; emoticon++)
            {
                int index = (int)emoticon;
                var sprite = new AnimatedSprite(texture, new Rectangle(0, index * 50, 50, 50),
                    EmoticonFrameDuration, EmoticonFrameCount);

                sprite.AddAnimation(texture, new Rectangle(500, index * 50, 50, 50),
                    DeadEmoticonFrameDuration, DeadEmoticonFrameCount);
                sprite.AddAnimation(texture, new Rectangle(650, index * 50, 50, 50),
                    FreezeEmoticonFrameDuration, FreezeEmoticonFrameCount);
                sprite.AddAnimation(texture, new Rectangle(800, index * 50, 50, 50),
                    CrazyEmoticonFrameDuration, CrazyEmoticonFrameCount);

                sprite.Name = $"{emoticon}";

                UpdateSpriteData(spriteData, sprite);
            }
        }

        private static void UpdateSpriteData(SpriteData spriteData, AnimatedSprite sprite)
        {
            if (spriteData.SpriteByName.ContainsKey(sprite.Name))
                spriteData.SpriteByName[sprite.Name] = sprite;
            else
                spriteData.SpriteByName.Add(sprite.Name, sprite);
        }
    }
}

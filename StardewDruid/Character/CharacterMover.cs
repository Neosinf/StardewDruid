using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Character
{

    public class CharacterMover
    {

        public CharacterHandle.characters character;

        public enum moveType
        {

            from,
            to,
            remove,

        }

        public moveType type;

        public string locale;

        public Vector2 position;

        public bool animate;

        public CharacterMover(CharacterHandle.characters CharacterType)
        {

            character = CharacterType;

        }

        public void Update()
        {

            if (character == CharacterHandle.characters.Dragon)
            {

                RemoveDragons();

                return;

            }

            Character entity = Mod.instance.characters[character];

            GameLocation target;

            if (Mod.instance.locations.ContainsKey(locale))
            {

                target = Mod.instance.locations[locale];

            }
            else
            {

                target = Game1.getLocationFromName(locale);

            }

            switch (type)
            {

                case moveType.from:

                    target.characters.Remove(entity);

                    break;

                case moveType.to:

                    Warp(target, entity, position);

                    break;

                case moveType.remove:

                    RemoveAll(entity);

                    break;

            }


        }

        public void WarpSet(string Target, Vector2 Position, bool Animate = true)
        {

            type = moveType.to;

            locale = Target;

            position = Position;

            animate = Animate;

        }

        public static void Warp(GameLocation target, Character entity, Vector2 position, bool animate = true)
        {

            if (entity.currentLocation != null)
            {

                if (animate)
                {

                    Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position, true);

                }

                entity.currentLocation.characters.Remove(entity);

            }

            entity.ResetActives(true);

            target.characters.Add(entity);

            entity.currentLocation = target;

            entity.Position = position;

            entity.SettleOccupied();

            if (animate)
            {

                Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position);

            }

        }

        public void RemovalSet(string From)
        {

            type = moveType.from;

            locale = From;

        }

        public void RemoveAll(Character entity)
        {

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    if (location.characters.Contains(entity))
                    {

                        location.characters.Remove(entity);

                    }

                }

            }

            if (!Context.IsMainPlayer)
            {

                Mod.instance.characters.Remove(character);

            }

        }

        public void RemoveDragons(bool avatars = true)
        {

            List<Cast.Ether.Dragon> dragons = new();

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    for (int c = location.characters.Count - 1; c >= 0; c--)
                    {

                        if (location.characters[c] is Cast.Ether.Dragon dragonCharacter)
                        {

                            if (avatars)
                            {

                                if (!dragonCharacter.avatar)
                                {

                                    continue;

                                }

                            }

                            location.characters.Remove(dragonCharacter);

                        }

                    }

                }

            }

        }

    }

}

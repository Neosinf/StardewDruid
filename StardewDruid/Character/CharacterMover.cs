using Microsoft.Xna.Framework;
using StardewDruid.Data;
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

        public GameLocation location = null;

        public Character character = null;

        public CharacterHandle.characters characterisation = CharacterHandle.characters.none;

        public string localisation = null;

        public enum moveType
        {

            move,
            remove,
            wipe,
            purge,

        }

        public moveType type;

        public IconData.warps warp = IconData.warps.portal;

        public Vector2 position;

        public bool animate;

        public CharacterMover(CharacterHandle.characters Characterisation, moveType Type = moveType.purge)
        {

            characterisation = Characterisation;

            type = Type;

        }

        public CharacterMover(CharacterHandle.characters Characterisation, string Localisation, moveType Type = moveType.wipe)
        {

            characterisation = Characterisation;

            localisation = Localisation;

            type = Type;

        }

        public CharacterMover(Character Entity, string Localisation, Vector2 Position, bool Animate, moveType Type = moveType.move)
        {

            character = Entity;

            localisation = Localisation;

            position = Position;

            animate = Animate;

            type = Type;

        }

        public CharacterMover(Character Entity, GameLocation Location, Vector2 Position, bool Animate, moveType Type = moveType.move)
        {

            character = Entity;

            location = Location;

            position = Position;

            animate = Animate;

            type = Type;

        }

        public CharacterMover(CharacterHandle.characters Characterisation, GameLocation Location, moveType Type = moveType.remove)
        {

            characterisation = Characterisation;

            location = Location;

            type = Type;

        }

        public void Update()
        {

            if (characterisation == CharacterHandle.characters.Dragon)
            {

                RemoveDragons();

                return;

            }

            switch (type)
            {

                case moveType.move:

                    TryMove();

                    break;

                case moveType.remove:

                    TryRemove();

                    break;

                case moveType.wipe:

                    TryWipe();

                    break;

                case moveType.purge:

                    TryPurge();

                    break;

            }


        }

        public GameLocation ReferenceLocation()
        {

            if(localisation is not null)
            {

                if (Mod.instance.locations.ContainsKey(localisation))
                {

                    return Mod.instance.locations[localisation];

                }
                else
                {

                    return Game1.getLocationFromName(localisation);

                }

            }

            return null;

        }

        public Character ReferenceCharacter()
        {

            if(characterisation is not CharacterHandle.characters.none)
            {

                if (Mod.instance.characters.ContainsKey(characterisation))
                {

                    return Mod.instance.characters[characterisation];

                }

            }

            return null;

        }

        public void TryMove()
        {

            if(location is null)
            {

                location = ReferenceLocation();

            }

            if (location is null)
            {

                return;

            }

            if (character is null)
            {

                character = ReferenceCharacter();

            }

            if (character is null)
            {

                return;

            }

            Warp(location, character, position, animate, warp);

        }

        public static void Warp(GameLocation target, Character entity, Vector2 position, bool animate = true, IconData.warps Warp = IconData.warps.portal)
        {

            if (entity.currentLocation != null)
            {

                if (animate)
                {

                    Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position, true, Warp);

                }

                entity.currentLocation.characters.Remove(entity);

            }

            entity.ResetActives(true);

            target.characters.Add(entity);

            entity.currentLocation = target;

            entity.Position = position;

            entity.SettleOccupied();

            if (Mod.instance.trackers.ContainsKey(entity.characterType))
            {

               entity.attentionTimer = 180;

            }

            if (animate)
            {

                Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position, false, Warp);

            }

        }

        public void TryRemove()
        {

            if (location is null)
            {

                location = ReferenceLocation();

            }

            if (location is null)
            {

                return;

            }

            if (character is null)
            {

                character = ReferenceCharacter();

            }

            if (character is null)
            {

                return;

            }

            location.characters.Remove(character);

        }


        public void TryWipe()
        {

            if (location is null)
            {

                location = ReferenceLocation();

            }

            if (location is null)
            {

                return;

            }

            if (character is not null)
            {

                characterisation = character.characterType;

            }

            if (characterisation is CharacterHandle.characters.none)
            {

                return;

            }

            for (int c = location.characters.Count-1; c >= 0; c--)
            {

                NPC npc = location.characters.ElementAt(c);

                if (npc is StardewDruid.Character.Character entity)
                {

                    if (entity.characterType == characterisation)
                    {

                        location.characters.RemoveAt(c);

                    }

                }

            }


        }

        public void TryPurge()
        {
            
            if (character is not null)
            {

                characterisation = character.characterType;

            }

            if (characterisation is CharacterHandle.characters.none)
            {

                return;

            }

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    for (int c = location.characters.Count - 1; c >= 0; c--)
                    {

                        NPC npc = location.characters.ElementAt(c);

                        if (npc is StardewDruid.Character.Character entity)
                        {

                            if (entity.characterType == characterisation)
                            {

                                location.characters.RemoveAt(c);

                            }

                        }

                    }

                }

            }

            if (!Context.IsMainPlayer)
            {

                Mod.instance.characters.Remove(characterisation);

                if (Mod.instance.trackers.ContainsKey(characterisation))
                {

                    Mod.instance.trackers.Remove(characterisation);

                }

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

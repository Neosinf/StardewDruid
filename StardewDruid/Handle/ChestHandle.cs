using StardewDruid.Journal;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StardewDruid.Handle
{
    public class ChestHandle
    {

        public enum chests
        {

            Companions,
            Anvil,
            Distillery,

        }

        public static void CleanInventory(chests character)
        {

            if (!Mod.instance.chests.ContainsKey(character))
            {

                return;

            }

            Chest chest = Mod.instance.chests[character];

            for (int i = chest.Items.Count - 1; i >= 0; i--)
            {

                if (chest.Items.ElementAt(i) != null)
                {

                    if (chest.Items.ElementAt(i).Stack <= 0)
                    {

                        chest.Items.RemoveAt(i);

                    }

                }

            }

        }

        public static void OpenInventory(chests character)
        {

            RetrieveInventory(character);

            Mod.instance.chests[character].ShowMenu();

        }

        public static void RetrieveInventory(chests character)
        {

            if (Mod.instance.chests.ContainsKey(character))
            {

                return;

            }

            Chest newChest = new();

            newChest.SpecialChestType = Chest.SpecialChestTypes.BigChest;

            Mod.instance.chests[character] = newChest;

            if (Mod.instance.save.chests.ContainsKey(character))
            {

                XmlSerializer serializer = new(typeof(Chest));

                StringReader stringReader;

                stringReader = new StringReader(Mod.instance.save.chests[character]);

                System.Xml.XmlTextReader xmlReader;

                xmlReader = new System.Xml.XmlTextReader(stringReader);

                Chest serialchest = (Chest)serializer.Deserialize(xmlReader);

                xmlReader.Close();

                stringReader.Close();

                Mod.instance.chests[character] = serialchest;

                return;

            }

        }

        public static void OpenInventory(chests chest, DruidJournal.journalTypes journal)
        {

            ChestHandle.RetrieveInventory(chest);

            Chest distilleryChest = Mod.instance.chests[chest];

            ItemGrabMenu itemGrabMenu =
                new ItemGrabMenu(
                    distilleryChest.GetItemsForPlayer(),
                    reverseGrab: false,
                    showReceivingMenu: true,
                    InventoryMenu.highlightAllItems,
                    distilleryChest.grabItemFromInventory,
                    null,
                    distilleryChest.grabItemFromChest,
                    snapToBottom: false,
                    canBeExitedWithKey: true,
                    playRightClickSound: true,
                    allowRightClick: true,
                    showOrganizeButton: true,
                    1,
                    distilleryChest,
                    -1,
                    distilleryChest
              );

            itemGrabMenu.exitFunction = (IClickableMenu.onExit)Delegate.Combine(itemGrabMenu.exitFunction, (IClickableMenu.onExit)delegate
            {

                DruidJournal.openJournal(journal);

            });

            Game1.activeClickableMenu = itemGrabMenu;

            if (itemGrabMenu != null && Game1.activeClickableMenu is ItemGrabMenu itemGrabMenu3)
            {
                itemGrabMenu3.inventory.moveItemSound = itemGrabMenu.inventory.moveItemSound;
                itemGrabMenu3.inventory.highlightMethod = itemGrabMenu.inventory.highlightMethod;
            }

        }

    }

}

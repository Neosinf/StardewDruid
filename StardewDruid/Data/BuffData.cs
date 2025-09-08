using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{

    public class BuffDetail
    {

        public BuffHandle.buffTypes type;

        public string name;

        public string description;

    }

    public class BuffData
    {

        public static Dictionary<BuffHandle.buffTypes, BuffDetail> BuffList()
        {

            Dictionary<BuffHandle.buffTypes, BuffDetail> list = new();

            list[BuffHandle.buffTypes.alignment] = new()
            {
                type = BuffHandle.buffTypes.alignment,
                name = Mod.instance.Helper.Translation.Get("HerbalData.1266"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.2"),

            };

            list[BuffHandle.buffTypes.vigor] = new()
            {
                type = BuffHandle.buffTypes.vigor,
                name = Mod.instance.Helper.Translation.Get("HerbalData.1274"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.4"),

            };

            list[BuffHandle.buffTypes.celerity] = new()
            {
                type = BuffHandle.buffTypes.celerity,
                name = Mod.instance.Helper.Translation.Get("HerbalData.1282"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.6"),

            };

            list[BuffHandle.buffTypes.imbuement] = new()
            {
                type = BuffHandle.buffTypes.imbuement,
                name = Mod.instance.Helper.Translation.Get("HerbalData.361.29"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.500.buff.7"),

            };

            list[BuffHandle.buffTypes.amorous] = new()
            {
                type = BuffHandle.buffTypes.amorous,
                name = Mod.instance.Helper.Translation.Get("HerbalData.361.30"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.373.2"),

            };

            list[BuffHandle.buffTypes.macerari] = new()
            {
                type = BuffHandle.buffTypes.macerari,
                name = Mod.instance.Helper.Translation.Get("HerbalData.500.buff.6"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.500.buff.8"),

            };

            list[BuffHandle.buffTypes.rapidfire] = new()
            {
                type = BuffHandle.buffTypes.rapidfire,
                name = Mod.instance.Helper.Translation.Get("HerbalData.386.29"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.386.30"),

            };

            list[BuffHandle.buffTypes.concussion] = new()
            {
                type = BuffHandle.buffTypes.concussion,
                name = Mod.instance.Helper.Translation.Get("HerbalData.361.32"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.373.4"),

            };

            list[BuffHandle.buffTypes.jumper] = new()
            {
                type = BuffHandle.buffTypes.jumper,
                name = Mod.instance.Helper.Translation.Get("HerbalData.361.33"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.373.5"),

            };

            list[BuffHandle.buffTypes.feline] = new()
            {
                type = BuffHandle.buffTypes.feline,
                name = Mod.instance.Helper.Translation.Get("HerbalData.361.34"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.373.6"),

            };

            list[BuffHandle.buffTypes.sanctified] = new()
            {
                type = BuffHandle.buffTypes.sanctified,
                name = Mod.instance.Helper.Translation.Get("HerbalData.386.31"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.386.32"),

            };

            list[BuffHandle.buffTypes.capture] = new()
            {
                type = BuffHandle.buffTypes.capture,
                name = Mod.instance.Helper.Translation.Get("HerbalData.386.33"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.386.34"),

            };

            list[BuffHandle.buffTypes.spellcatch] = new()
            {
                type = BuffHandle.buffTypes.spellcatch,
                name = Mod.instance.Helper.Translation.Get("HerbalData.399.5"),
                description = Mod.instance.Helper.Translation.Get("HerbalData.399.6"),

            };

            return list;

        }

    }

}

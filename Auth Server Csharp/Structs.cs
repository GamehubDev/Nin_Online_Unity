using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AuthServer
{




    enum Equipment { Weapon, Shirt, Hat, Vest, Facemask, Pants, Shoe, Accessory, counter };
    enum Elements { Fire, Wind, Lightning, Earth, Water, counter };
    enum Vitals { HP, MP, counter };
    enum Stats { Strength, Fortitude, Intellect, Agility, Chakra, counter };

    public struct Player // TODO: switch to Class
    {
        //Marshalling
        //http://stackoverflow.com/a/3278956


        private string playerName;
        public byte sex;
        public int playerClass;
        public int sprite;
        public byte level;
        public int exp;
        public byte access;
        public byte isPK;
        public int hair;
        public int eyes;

        //Currency
        public int ryo;

        //Vitals
        public int[] vitals;

        //Stats
        public short[] stats;
        public int points;

        //Worn equipment
        public int[] equipment;

        //Inventory
        public Items[] inventory; // Inv(1 To MAX_INV) As PlayerInvRec

        public int[] spells; //Spell(1 To MAX_PLAYER_SPELLS) As Long

        //Hotbar
        public Hotbar[] hotbar;//(1 To MAX_HOTBAR) As HotbarRec

        //Position
        public int currentMap;
        public byte x;
        public byte y;
        public byte dir;

        public byte[] switches;
        public int[] variables;//(0 To MAX_VARIABLES) As Long

        //Quests
        public PlayerQuest[] quests;//(1 To MAX_PLAYER_QUESTS) As PlayerQuest

        //Completed Quest
        public int[] completedQuest;//(1 To MAX_QUESTS) As Long

        //Guild ID
        public int guild;

        //Played
        public int minutes;
        public int hours;

        public int attackAnimation;

        //Friends List
        public Friends[] friends;

        //Elements
        public byte[] elements;//(1 To MAX_ELEMENTS) As Byte
        public byte isMuted;
        public byte IsDonor;
        public bool HasChar;
        public Player(int test = 0)
        {

             this.playerName = null;
             this.sex = 0;
             this.playerClass = 0;
             this.sprite = 0;
             this.level = 0;
             this.exp = 0;
             this.access = 0;
             this.isPK = 0;
             this.hair = 0;
             this.eyes = 0;

            this.ryo = 0;

            this.vitals = new int[(int)Vitals.counter];

            this.stats = new short[(int)Stats.counter];
            this.points = 0;

            //Worn equipment
            this.equipment = new int[(int)Equipment.counter];
            this.inventory = new Items[Constants.MAX_INV]; //    Inv(1 To MAX_INV) As PlayerInvRec
            this.spells = new int[Constants.MAX_PLAYER_SPELLS];
            //Hotbar
            this.hotbar = new Hotbar[Constants.MAX_HOTBAR];//(1 To MAX_HOTBAR) As HotbarRec

            //Position
            this.currentMap = 0;
            this.x = 0;
            this.y = 0;
            this.dir = 0;

            this.switches = new byte[Constants.MAX_SWITCHES];
            this.variables = new int[Constants.MAX_VARIABLES];
            //Quests
            this.quests = new PlayerQuest[Constants.MAX_PLAYER_QUESTS];//(1 To MAX_PLAYER_QUESTS) As PlayerQuest
            for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
            {
                quests[i] = new PlayerQuest(0);
            }
            this.completedQuest = new int[Constants.MAX_QUESTS];
            //Guild ID
            this.guild = 0;

            //Play time
            this.minutes = 0;
            this.hours = 0;

            this.attackAnimation = 0;

            //Friends List
            this.friends = new Friends[Constants.MAX_PLAYER_FRIENDS];
            for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
            {
                friends[i].name = "";
            }
            //Element
            this.elements = new byte[Constants.MAX_ELEMENTS];

            this.isMuted = 0;
            this.IsDonor = 0;
            this.HasChar = false;
        }

        public string PlayerName{
            get{return this.playerName;}
            set
            {
                int size = value.Length;
                if (size > 20)
                {
                    size = 20;
                    playerName = value.Substring(0, 20);
                }
                else playerName = value;

                int fillerSize = Constants.PLAYER_NAME_LENGTH - size;
                if (fillerSize > 0)
                {
                    playerName += new string(' ', fillerSize);
                }
            }
        }
    }
    public struct Bank
    {
        public Items[] items;//(1 To MAX_BANK) As PlayerInvRec
        public Bank(int test = 0)
        {
            this.items = new Items[Constants.MAX_BANK];

        }
    }
    public struct Items
    {
        public int id;
        public int amount;
    }
    public struct Hotbar
    {
        public int slot;
        public byte sType;
    }
    public struct PlayerQuest
    {
        public byte currentObjective;
        public byte[] completed;//(1 To 3) As Byte
        public int id;
        public int[] counter;//(1 To 3) As Long
        public PlayerQuest(int test = 0)
        {
            this.currentObjective = 0;
            this.completed = new byte[3];
            this.id = 0;
            this.counter = new int[3];
        }
    }
    public struct Friends
    {
        public string name;
    }

    public struct Fragmentation
    {
        public byte[] buffer; // there can be only one fragment
        public int missingBytes; // Missing bytes amount in the fragmented packet.
        public int alreadyFilledBytes;
        public bool pendingFragment;
        //Extreme case - size header splitted bteween packets
        public bool sizeSplitted; // If packet get splitted between bytes responsible for the packet size, it will all fail.
        public int sizeMissingBytes;
        public int sizeAlreadyFilledBytes;
        public byte[] sizeBuffer;

        public Fragmentation(int maxSize)
        {
            this.buffer = new byte[maxSize];
            this.missingBytes = 0;
            this.alreadyFilledBytes = 0;
            this.pendingFragment = false;

            //Extreme case
            this.sizeSplitted = false;
            this.sizeMissingBytes = 0;
            this.sizeAlreadyFilledBytes = 0;
            this.sizeBuffer = new byte[4];
        }
    }
    public struct PlayerDBInfo
    {
        public string salt;
        public string hash;
        public int group;
        public string charName;
        public PlayerDBInfo(string salt, string hash, string group, string charName)
        {
            this.salt = salt;
            this.hash = hash;
            this.group = int.Parse(group);
            this.charName = charName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public class AccountsManager
    {
        AuthenticationServer form;
        public AccountsManager(AuthenticationServer form)
        {
            this.form = form;
        }
        public void SavePlayer(string loginName, ref Player playerToSave)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "data\\accounts\\" + loginName + ".bin";

            while (IsFileLocked(path))
            {
                System.Threading.Thread.Sleep(100);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
            {
                writer.Write(Encoding.UTF8.GetBytes(playerToSave.PlayerName));
                writer.Write(playerToSave.sex) ;
                writer.Write(playerToSave.playerClass);
                writer.Write(playerToSave.sprite);
                writer.Write(playerToSave.level);
                writer.Write(playerToSave.exp);
                writer.Write(playerToSave.access);
                writer.Write(playerToSave.isPK);
                writer.Write(playerToSave.hair);
                writer.Write(playerToSave.eyes);
                //Currency
                writer.Write(playerToSave.ryo);

                //Vitals
                writer.Write(playerToSave.vitals[(int)Vitals.HP]);
                writer.Write(playerToSave.vitals[(int)Vitals.MP]);

                //Stats
                for (int i = 0; i < (int)Stats.counter; i++)
                {
                    writer.Write(BitConverter.GetBytes(playerToSave.stats[i]));
                }
                writer.Write(playerToSave.points);

                //Worn equipment
                for (int i = 0; i < (int)Equipment.counter; i++)
                {
                    writer.Write(playerToSave.equipment[i]);
                }

                //Inventory
                for (int i = 0; i < Constants.MAX_INV; i++)
                {
                    writer.Write(playerToSave.inventory[i].id);
                    writer.Write(playerToSave.inventory[i].amount);
                }

                //Spells
                for (int i = 0; i < Constants.MAX_PLAYER_SPELLS; i++)
                {
                    writer.Write(playerToSave.spells[i]);
                }

                //Hotbar
                for (int i = 0; i < Constants.MAX_HOTBAR; i++)
                {
                    writer.Write(playerToSave.hotbar[i].slot);
                    writer.Write(playerToSave.hotbar[i].sType);
                }
                //Position
                writer.Write(playerToSave.currentMap);
                writer.Write(playerToSave.x);
                writer.Write(playerToSave.y);

                writer.Write(playerToSave.dir);

                //Switches
                for (int i = 0; i < Constants.MAX_SWITCHES; i++)
                {
                    writer.Write(playerToSave.switches[i]);
                }

                //Variables
                for (int i = 0; i < Constants.MAX_VARIABLES; i++)
                {
                    writer.Write(playerToSave.variables[i]);
                }

                //Quests
                for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
                {
                    writer.Write(playerToSave.quests[i].currentObjective);
                    for (int j = 0; j < 3; j++)
                    {
                        writer.Write(playerToSave.quests[i].completed[j]);
                    }
                    writer.Write(playerToSave.quests[i].id);

                    for (int j = 0; j < 3; j++)
                    {
                        writer.Write(playerToSave.quests[i].counter[j]);
                    }
                }
                //Completed Quest
                for (int i = 0; i < Constants.MAX_QUESTS; i++)
                {
                    writer.Write(playerToSave.completedQuest[i]);
                }

                //Guild ID
                writer.Write(playerToSave.guild);

                //Play time
                writer.Write(playerToSave.minutes);
                writer.Write(playerToSave.hours);

                writer.Write(playerToSave.attackAnimation);

                //Friends List
                for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
                {
                    //Its only true fro VB6, it stores size of string that are next to it in 16 vyte variable
                    //Int16 friendNameSize = (Int16)playerToSave.friends[i].name.Length;
                    //writer.Write(friendNameSize);
                    byte[] nameBytes = Encoding.UTF8.GetBytes(playerToSave.friends[i].name);
                    if (nameBytes.Length > 0)
                    {
                        writer.Write(nameBytes);
                    }
                }

                //Elements
                for (int i = 0; i < Constants.MAX_ELEMENTS; i++)
                {
                    writer.Write(playerToSave.elements[i]);

                }

                writer.Write(playerToSave.isMuted);
                writer.Write(playerToSave.IsDonor);

                Int16 hasChar = playerToSave.HasChar ? (Int16)(-1) : (Int16)0;
                writer.Write(hasChar);
            }
        }

        public void AddLog(string text)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            
            using (FileStream f = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter s = new StreamWriter(f))
                {
                    s.Write(text);
                }

            }

        }

        public bool LoadPlayer(string loginName, ref Player playerToLoad)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "data\\accounts\\" + loginName + ".bin";

            if (File.Exists(path))
            {
                while (IsFileLocked(path))
                {
                    System.Threading.Thread.Sleep(100);
                }

                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    playerToLoad.PlayerName = Encoding.Default.GetString(reader.ReadBytes(Constants.PLAYER_NAME_LENGTH));
                    playerToLoad.sex = reader.ReadByte();
                    playerToLoad.playerClass = reader.ReadInt32();
                    playerToLoad.sprite = reader.ReadInt32();
                    playerToLoad.level = reader.ReadByte();
                    playerToLoad.exp = reader.ReadInt32();
                    playerToLoad.access = reader.ReadByte();
                    playerToLoad.isPK = reader.ReadByte();
                    playerToLoad.hair = reader.ReadInt32();
                    playerToLoad.eyes = reader.ReadInt32();
                    //Currency
                    playerToLoad.ryo = reader.ReadInt32();

                    //Vitals
                    int test = (int)Vitals.HP;
                    playerToLoad.vitals[test] = reader.ReadInt32();
                    playerToLoad.vitals[(int)Vitals.MP] = reader.ReadInt32();

                    //Stats
                    for (int i = 0; i < (int)Stats.counter; i++)
                    {
                        playerToLoad.stats[i] = reader.ReadInt16();
                    }
                    playerToLoad.points = reader.ReadInt32();

                    //Worn equipment
                    for (int i = 0; i < (int)Equipment.counter; i++)
                    {
                        playerToLoad.equipment[i] = reader.ReadInt32();
                    }

                    //Inventory
                    for (int i = 0; i < Constants.MAX_INV; i++)
                    {
                        playerToLoad.inventory[i].id = reader.ReadInt32();
                        playerToLoad.inventory[i].amount = reader.ReadInt32();
                    }

                    //Spells
                    for (int i = 0; i < Constants.MAX_PLAYER_SPELLS; i++)
                    {
                        playerToLoad.spells[i] = reader.ReadInt32();
                    }

                    //Hotbar
                    for (int i = 0; i < Constants.MAX_HOTBAR; i++)
                    {
                        playerToLoad.hotbar[i].slot = reader.ReadInt32();
                        playerToLoad.hotbar[i].sType = reader.ReadByte();
                    }
                    //Position
                    playerToLoad.currentMap = reader.ReadInt32();
                    playerToLoad.x = reader.ReadByte();
                    playerToLoad.y = reader.ReadByte();
                    playerToLoad.dir = reader.ReadByte();

                    //Switches
                    for (int i = 0; i < Constants.MAX_SWITCHES; i++)
                    {
                        playerToLoad.switches[i] = reader.ReadByte();
                    }

                    //Variables
                    for (int i = 0; i < Constants.MAX_VARIABLES; i++)
                    {
                        playerToLoad.variables[i] = reader.ReadInt32();
                    }

                    //Quests
                    for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
                    {
                        playerToLoad.quests[i].currentObjective = reader.ReadByte();
                        for (int j = 0; j < 3; j++)
                        {
                            playerToLoad.quests[i].completed[j] = reader.ReadByte();
                        }
                        playerToLoad.quests[i].id = reader.ReadInt32();

                        for (int j = 0; j < 3; j++)
                        {
                            playerToLoad.quests[i].counter[j] = reader.ReadInt32();
                        }
                    }
                    //Completed Quest
                    for (int i = 0; i < Constants.MAX_QUESTS; i++)
                    {
                        playerToLoad.completedQuest[i] = reader.ReadInt32();
                    }

                    //Guild ID
                    playerToLoad.guild = reader.ReadInt32();

                    //Play time
                    playerToLoad.minutes = reader.ReadInt32();
                    playerToLoad.hours = reader.ReadInt32();

                    playerToLoad.attackAnimation = reader.ReadInt32();

                    //Friends List
                    for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
                    {
                        //Its only true fro VB6, it stores size of string that are next to it in 16 vyte variable
                        playerToLoad.friends[i].name = Encoding.Default.GetString(reader.ReadBytes(Constants.PLAYER_NAME_LENGTH));
                    }

                    //Elements
                    for (int i = 0; i < Constants.MAX_ELEMENTS; i++)
                    {
                        playerToLoad.elements[i] = reader.ReadByte();

                    }

                    playerToLoad.isMuted = reader.ReadByte();
                    playerToLoad.IsDonor = reader.ReadByte();

                    playerToLoad.HasChar = reader.ReadInt16() == -1 ? true : false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool LoadPlayerBank(string loginName, ref Bank bankToLoad)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "data\\banks\\" + loginName + ".bin";

            if (File.Exists(path))
            {
                while (IsFileLocked(path))
                {
                    System.Threading.Thread.Sleep(100);
                }

                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    for (int i = 0; i < Constants.MAX_BANK; i++)
                    {
                        bankToLoad.items[i].id = reader.ReadInt32();
                        bankToLoad.items[i].amount = reader.ReadInt32();
                    }
                }
                return true;
            }
            return false;
        }
        public void SavePlayerBank(string loginName, ref Bank bankToSave)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "data\\banks\\" + loginName + ".bin";

            while (IsFileLocked(path))
            {
                System.Threading.Thread.Sleep(100);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < Constants.MAX_BANK; i++)
                {
                    writer.Write(bankToSave.items[i].id);
                    writer.Write(bankToSave.items[i].amount);
                }
            }
            return;
        }

        public void SavePacket(string loginName, ref byte[] bytes)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "data\\accountz\\" + loginName + ".bin";

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.Write(bytes);
            }
        }

        protected virtual bool IsFileLocked(string path)
        {
            FileStream stream = null;

            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                stream = File.Open(path, FileMode.Open);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}

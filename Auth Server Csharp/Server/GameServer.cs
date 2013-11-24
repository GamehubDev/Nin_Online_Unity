using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using AuthServer.Client;

namespace AuthServer
{
    public enum GameRecvPackets { RegisterServer = 1, UpdatePlayer, PlayerAcknowledged, PlayerLogout, BeginPlayerUpdate, FinishPlayerUpdate };
    public enum GamePlayerAdmission { Accepted = 1, Rejected_Multilogin, Rejected_TooManyPlayers };
    public enum GameSendPackets { RegisterNewPlayer = 1, MassKick = 2, ShutDown = 3, restart = 4, SaveConfirmation = 5 };    
    

    public class GameServer
    {
        private static AuthenticationServer _form;
        private static bool updatingPlayers = false;
        private static string updatedPlayerList = "";
        public static void Received(AuthenticationServer form, PacketContent pc, ServerDetails serverDetails)
        {
            _form = form;
            GameRecvPackets msgType = (GameRecvPackets)pc.GetMsgType();
            switch (msgType)
            {
                case GameRecvPackets.RegisterServer:
                    RegisterServer(pc, serverDetails);
                    break;
                case GameRecvPackets.UpdatePlayer:
                    PreSavePlayer(pc);
                    break;
                case GameRecvPackets.PlayerAcknowledged:
                    CheckAdmission(pc);
                    break;
                case GameRecvPackets.BeginPlayerUpdate:
                    updatingPlayers = true;
                    break;
                case GameRecvPackets.FinishPlayerUpdate:
                    if (updatedPlayerList.Length == 0) return;
                    updatedPlayerList = updatedPlayerList.Remove(updatedPlayerList.Length - 2, 2);
                    appendLog("Saved accounts & banks for:\n" + updatedPlayerList);
                    updatingPlayers = false;
                    updatedPlayerList = "";
                    SendServer_SaveConfirmation(serverDetails);
                    break;
                default:
                    break;
            }

        }

        private static void RegisterServer(PacketContent pc, ServerDetails serverDetails)
        {
            appendLog(".::Received server connection::. ");
            serverDetails.serverIndex = pc.ReadLong();
            serverDetails.name = pc.ReadString();
            serverDetails.ip = pc.ReadString();
            serverDetails.port = pc.ReadLong();
            serverDetails.location = pc.ReadString();
            serverDetails.donorOnly = pc.ReadFlag();
            serverDetails.registered = true;
            appendLog(".::Name: " + serverDetails.name);
            appendLog(".::IP: " + serverDetails.ip);
            appendLog(".::Port: " + serverDetails.port);

        }

        public static bool RegisterNewPlayer(int serverIndex, string loginName, string loginToken, ref Player player, ref Bank playerBank)
        {
            ServerDetails server = AuthenticationServer.GetServerByIndex(serverIndex);
            if (server == null) return false;
            return SendServer_RegisterPlayer(server, loginName, loginToken, ref player, ref playerBank);

        }
        public static bool SendServer_RegisterPlayer(ServerDetails server, string loginName, string loginToken, ref Player player, ref Bank playerBank)
        {
            PacketsProcessor sendBuffer = new PacketsProcessor((int)GameSendPackets.RegisterNewPlayer);
            sendBuffer.WriteString(loginToken);
            sendBuffer.WriteString(loginName);
            sendBuffer.BufferPlayer(ref player);
            sendBuffer.BufferPlayerBank(ref playerBank);
            sendBuffer.FinishPacking();
            //Testing packet
            //byte[] temp = sendBuffer.GetSendBytes();
            //AccountsManager.SavePacket("davemax", ref temp);

            return SendPacket(sendBuffer, server.socket);
        }
        public static void SendServer_SaveConfirmation(ServerDetails server)
        {
            PacketsProcessor sendBuffer = new PacketsProcessor((int)GameSendPackets.SaveConfirmation);
            sendBuffer.FinishPacking();

            SendPacket(sendBuffer, server.socket);
        }
        private static bool SendPacket(PacketsProcessor sendBuffer, Socket s)
        {
            // Begin sending the data to the remote device.
            //byte[] test = sendBuffer.GetSendBytes();
            //AccountsManager.SavePacket("davemax", ref test);
            try
            {
                s.BeginSend(sendBuffer.GetSendBytes(), 0, sendBuffer.GetSendBytes().Length, 0, new AsyncCallback(SendCallback), s);
                return true;
            }
            catch (Exception ex)
            {
                appendLog("Exception in: GameServer - SendPacket ->" + ex.Message);
                return false;
                //MessageBox.Show(ex.Message, "GameServer - SendCallback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket s = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = s.EndSend(ar);
                //appendLog(String.Format("Sent {0} bytes to server.", bytesSent));

            }
            catch (Exception ex)
            {
                appendLog("Exception in: GameServer - SendCallback ->" + ex.Message);
                //MessageBox.Show(ex.Message, "GameServer - SendCallback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AllowPlayerToJoinTheServer(string loginName)
        {
            appendLog("Server has accepted new player. " + loginName + " joined the game.");
            PlayerClient.SendClient_JoinGame(loginName);
        }
        private static void appendLog(string msg)
        {
            _form.appendLog("[Server] " + msg);
        }
        private static void PreSavePlayer(PacketContent pc)
        {
            //byte[] temp = data.getContent();
            //AccountsManager.SavePacket("davemax", ref temp);
            AccountsManager file = new AccountsManager(_form);
            string playerLoginName = pc.ReadString();
            Player playerToSave = new Player(0);
            DebuffPlayer(pc, ref playerToSave);
            file.SavePlayer(playerLoginName, ref playerToSave);

            Bank bankToSave = new Bank(0);
            DebuffBank(pc, ref bankToSave);
            file.SavePlayerBank(playerLoginName, ref bankToSave);

            if (!updatingPlayers) appendLog(playerLoginName + "'s account & bank saved.");
            else updatedPlayerList += playerLoginName + ", ";
        }

        public static void DebuffPlayer(PacketContent pc, ref Player playerToFill)
        {

            playerToFill.PlayerName = pc.ReadUnicode(Constants.PLAYER_NAME_LENGTH);
            playerToFill.sex = (byte)pc.ReadLong();
            playerToFill.playerClass = pc.ReadLong();

            playerToFill.sprite = pc.ReadLong();
            playerToFill.level = (byte)pc.ReadLong();
            playerToFill.exp = pc.ReadLong();
            playerToFill.access = pc.ReadByte();
            playerToFill.isPK = pc.ReadByte();
            pc.readOffset += 2;

            playerToFill.hair = pc.ReadLong();
            playerToFill.eyes = pc.ReadLong();

            playerToFill.ryo = pc.ReadLong();
            playerToFill.vitals[(int)Vitals.HP] = pc.ReadLong();
            playerToFill.vitals[(int)Vitals.MP] = pc.ReadLong();

            for (int i = 0; i < (int)Stats.counter; i++)
            {
                playerToFill.stats[i] = pc.ReadInteger();
            }
            pc.readOffset += 2;

            playerToFill.points = pc.ReadLong();
            //Inventory
            for (int i = 0; i < (int)Equipment.counter; i++)
            {
                playerToFill.equipment[i] = pc.ReadLong();
            }

            for (int i = 0; i < Constants.MAX_INV; i++)
            {
                playerToFill.inventory[i].id = pc.ReadLong();
                playerToFill.inventory[i].amount = pc.ReadLong();
            }
            //Spells
            for (int i = 0; i < Constants.MAX_PLAYER_SPELLS; i++)
            {
                playerToFill.spells[i] = pc.ReadLong();
            }

            //Hotbar
            for (int i = 0; i < Constants.MAX_HOTBAR; i++)
            {
                playerToFill.hotbar[i].slot = pc.ReadLong();
                playerToFill.hotbar[i].sType = (byte)pc.ReadLong();
            }

            //Map
            playerToFill.currentMap = pc.ReadLong();
            playerToFill.x = pc.ReadByte();
            playerToFill.y = pc.ReadByte();
            playerToFill.dir = pc.ReadByte();

            //Switches
            for (int i = 0; i < Constants.MAX_SWITCHES; i++)
            {
                playerToFill.switches[i] = pc.ReadByte();
            }

            //Variables
            for (int i = 0; i < Constants.MAX_VARIABLES; i++)
            {
                playerToFill.variables[i] = pc.ReadLong();
            }
            //PlayerQuests
            for (int i = 0; i < Constants.MAX_PLAYER_QUESTS; i++)
            {
                playerToFill.quests[i].currentObjective = pc.ReadByte();

                playerToFill.quests[i].completed[0] = pc.ReadByte();
                playerToFill.quests[i].completed[1] = pc.ReadByte();
                playerToFill.quests[i].completed[2] = pc.ReadByte();

                playerToFill.quests[i].id = pc.ReadLong();

                playerToFill.quests[i].counter[0] = pc.ReadLong();
                playerToFill.quests[i].counter[1] = pc.ReadLong();
                playerToFill.quests[i].counter[2] = pc.ReadLong();
            }
            //Completed Quest
            for (int i = 0; i < Constants.MAX_QUESTS; i++)
            {
                playerToFill.completedQuest[i] = pc.ReadLong();
            }
            //Guild
            playerToFill.guild = pc.ReadLong();

            playerToFill.minutes = pc.ReadLong();
            playerToFill.hours = pc.ReadLong();
            playerToFill.attackAnimation = pc.ReadLong();

            //Friends
            for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
            {
                playerToFill.friends[i].name = pc.ReadUnicode(Constants.PLAYER_NAME_LENGTH);
            }
            //Elements
            for (int i = 0; i < Constants.MAX_ELEMENTS; i++)
            {
                playerToFill.elements[i] = pc.ReadByte();
            }

            playerToFill.isMuted = pc.ReadByte();
            playerToFill.IsDonor = pc.ReadByte();

            pc.readOffset += 1;

            //HasChar
            Int16 hasChar = pc.ReadInteger();
            playerToFill.HasChar = (hasChar == (-1) ? true : false);

        }
        public static void DebuffBank(PacketContent pc, ref Bank bankToFill)
        {
            for (int i = 0; i < Constants.MAX_BANK; i++)
            {
                bankToFill.items[i].id = pc.ReadLong();
                bankToFill.items[i].amount = pc.ReadLong();
            }
        }
        private static void CheckAdmission(PacketContent data)
        {
            string loginName = data.ReadString();
            GamePlayerAdmission status = (GamePlayerAdmission)data.ReadLong();
            switch (status)
            {
                case GamePlayerAdmission.Accepted:
                    AllowPlayerToJoinTheServer(loginName);
                    break;
                case GamePlayerAdmission.Rejected_Multilogin:
                    appendLog("Server has rejected " + loginName + " due to multilogin attempt!");
                    PlayerClient.PlayerRejected(loginName, "Multiple logins are not authorized.");
                    break;
                case GamePlayerAdmission.Rejected_TooManyPlayers:
                    appendLog("Server has rejected " + loginName + " due to max players limit!");
                    PlayerClient.PlayerRejected(loginName, "Theres too many players in game. Please try again later.");
                    break;
                default:
                    break;
            }


        }

    }
}

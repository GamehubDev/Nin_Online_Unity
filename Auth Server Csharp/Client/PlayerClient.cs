using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Client
{
    public enum PlayerRecvPackets { Login = 1, ServerSelect };
    public enum PlayerSendPackets { AlertMsg = 1, ServerDetails, JoinGame };
    public enum Donors { Silver = 8, Gold };
    public class PlayerClient
    {
        private static AuthenticationServer _form;

        public static bool Received(AuthenticationServer form, PacketContent pc, Candidate c)
        {
            _form = form;
            PlayerRecvPackets msgType = (PlayerRecvPackets)pc.GetMsgType();
            bool ret = true;
            switch (msgType)
            {
                case PlayerRecvPackets.Login:
                    ret = CheckCredentials(pc, c);
                    break;
                case PlayerRecvPackets.ServerSelect:
                    ret = PassNewPlayerToServer(pc, c);
                    break;
            }
            return ret;
        }

        private static bool CheckCredentials(PacketContent pc, Candidate c)
        {
            if (c.socket == null) return false;
            IPEndPoint clientInfo = (IPEndPoint)c.socket.RemoteEndPoint;
            AppendLog(clientInfo.Address.ToString() + " is trying to login...");
            c.loginName = pc.ReadString();
            c.password = pc.ReadString();
            int appMajor = pc.ReadLong();
            int appMinor = pc.ReadLong();
            int appRev = pc.ReadLong();
            DecryptPassword(c);
            if (appMajor != _form.getMajor() || appMinor != _form.getMinor() || appRev != _form.getRevision())
            {
                AppendLog(clientInfo.Address.ToString() + " has an outdated version of the client (" + appMajor + "." + appMinor + "." + appRev + "). Rejecting...");
                SendClient_AlertMsg(c.socket, "Old version detected, run the updater and try again!", true);
                KillClientSocket(c);
                return false;
            }
            return CheckPlayerStatus(c);
        }

        private static void AppendLog(string msg)
        {
            _form.appendLog("[Client] " + msg);
        }

        private static bool CheckPlayerStatus(Candidate c)
        {
            List<string>[] playerInfo;
            string ipAddress = ((IPEndPoint)c.socket.RemoteEndPoint).Address.ToString();
            if (!Database.Instance.SelectPlayerData(out playerInfo, c.loginName))
            {
                AppendLog("Couldn't connect with database for: " + ipAddress + ". Rejecting...");
                SendClient_AlertMsg(c.socket, "Unexpected loss of connection, try again in a minute!", true);
                KillClientSocket(c);
                return false;
            }
            if (playerInfo[0].Count == 0)
            {
                AppendLog("->" + c.loginName + "<-(" + ipAddress + ") doesn't exist in the database. Rejecting...");
                SendClient_AlertMsg(c.socket, "Sorry, but it seems that you've entered an incorrect login or password. Please try again!", true);
                KillClientSocket(c);
                return false;
            }
            PlayerDBInfo pInfo = new PlayerDBInfo(playerInfo[0][0], playerInfo[1][0], playerInfo[2][0], playerInfo[3][0]);
            if (!CheckPassword(pInfo, c.password))
            {
                AppendLog("Wrong password for: " + c.loginName + "/" + ipAddress + ". Rejecting...");
                SendClient_AlertMsg(c.socket, "Sorry, you've entered incorrect login or password. Please try again!", true);
                KillClientSocket(c);
                return false;
            }
            if (!CheckValidating(pInfo))
            {
                AppendLog("Account is not verified for: " + c.loginName + "/" + ipAddress + ". Rejecting...");
                SendClient_AlertMsg(c.socket, "Please validate your account, by confirming your email address!", true);
                KillClientSocket(c);
                return false;
            }
            //Load Player, to synchronize his status - gold silver admin.
            PreloadPlayer(c, pInfo);
            if (!CheckAdmin(pInfo, c))
            {
                SendClient_AlertMsg(c.socket, "Sorry, but the server is only available to staff members at the moment.", true);
                AppendLog(c.loginName + "/" + ipAddress + " isn't an Admin. Rejecting...");
                KillClientSocket(c);
                return false;
            }
            if (!CheckSubscription(pInfo, c))
            {
                SendClient_AlertMsg(c.socket, "Sorry, but the server is only available to Gold or Silver Shinobi at the moment.", true);
                AppendLog(c.loginName + "/" + ipAddress + " isn't a Donor" +
                          "" +
                          ". Rejecting...");
                KillClientSocket(c);
                return false;
            }

            //No more checks, it's seems player can join the server.
            AccountsManager file = new AccountsManager(_form);
            file.SavePlayer(c.loginName, ref c.player);
            file.SavePlayerBank(c.loginName, ref c.playerBank);

            if (!SendClient_ServerDetails(c))
            {
                SendClient_AlertMsg(c.socket, "Server is down, try again in a minute!", true);
                AppendLog(c.loginName + "/" + ipAddress + " rejected -> no available servers");
                KillClientSocket(c);
            }

            return true;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static bool CheckPassword(PlayerDBInfo playerinfo, string pass)
        {
            string ultimateHash = playerinfo.hash;
            string salt = playerinfo.salt;
            using (MD5 md5Hash = MD5.Create())
            {
                string firstIteration = GetMd5Hash(md5Hash, salt) + GetMd5Hash(md5Hash, pass);
                string seconditeration = GetMd5Hash(md5Hash, firstIteration);
                if (seconditeration == ultimateHash)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CheckSubscription(PlayerDBInfo playerinfo, Candidate c)
        {
            if (playerinfo.group == (int)Donors.Silver || playerinfo.group == (int)Donors.Gold)
            {
                Donors don = (Donors)playerinfo.group;
                string donorType = "";
                switch (don)
                {
                    case Donors.Silver:
                        donorType = " (Silver)";
                        c.player.IsDonor = 2;
                        break;
                    case Donors.Gold:
                        donorType = " (Gold)";
                        c.player.IsDonor = 1;
                        break;
                }
                _form.appendLog(c.loginName + " is a donor" + donorType + ".");
            }
            else
            {
                c.player.IsDonor = 0;
                if (c.player.access == 0) _form.appendLog(c.loginName + " is a regular player.");
            }

            if (_form.DonorOnly && c.player.access < Constants.ACCESS_DONOR) return false;

            return true;
        }

        private static bool CheckAdmin(PlayerDBInfo playerinfo, Candidate c)
        {
            switch (playerinfo.group)
            {
                case 4:
                    c.player.access = Constants.ACCESS_ADMIN;
                    break;
                case 6:
                    c.player.access = Constants.ACCESS_DONOR;
                    break;
                case 7:
                    c.player.access = Constants.ACCESS_MAPPER_DEVELOPER;
                    break;
                case 8:
                    c.player.access = Constants.ACCESS_DONOR;
                    break;
                case 9:
                    c.player.access = Constants.ACCESS_DONOR;
                    break;
                case 10:
                    c.player.access = Constants.ACCESS_DONOR;
                    break;
                case 12:
                    c.player.access = Constants.ACCESS_GAMEMASTER;
                    break;
                default:
                    c.player.access = Constants.ACCESS_FREE;
                    break;
            }
            if (c.player.access < Constants.ACCESS_MAPPER_DEVELOPER)
            {
                if (_form.AdminOnly) return false;
            }
            else AppendLog(c.loginName + " is an admin.");

            return true;
        }

        private static bool SendClient_ServerDetails(Candidate c)
        {
            PacketsProcessor sendBuffer = new PacketsProcessor((int)PlayerSendPackets.ServerDetails);
            //TODO: Add multi server support - > loop
            int serversCount = AuthenticationServer.GetServerCount();
            sendBuffer.WriteLong(serversCount);//Server Count
            for (int i = 0; i < serversCount; i++)
            {
                ServerDetails server = AuthenticationServer.GetServer(i);
                if (server == null) break;
                sendBuffer.WriteLong(server.serverIndex);
                sendBuffer.WriteString(server.name);
                sendBuffer.WriteString(server.ip);
                sendBuffer.WriteLong(server.port);
                sendBuffer.WriteString(server.location);
                sendBuffer.WriteFlag(server.donorOnly); 
            }

            sendBuffer.FinishPacking();
            SendPacket(sendBuffer, c.socket);
            return true;
        }
        public static void SendClient_AlertMsg(Socket s, string msg, bool direct = false)
        {
            PacketsProcessor sendBuffer = new PacketsProcessor((int)PlayerSendPackets.AlertMsg);
            sendBuffer.WriteString(msg);
            sendBuffer.FinishPacking();
            SendPacket(sendBuffer, s, direct);
        }
        private static void SendPacket(PacketsProcessor sendBuffer, Socket s, bool direct = false)
        {
            // Begin sending the data to the remote device.
            try
            {
                if (s == null) return;
                if (direct) s.BeginSend(sendBuffer.GetSendBytes(), 0, sendBuffer.GetSendBytes().Length, 0, null, s);
                else s.BeginSend(sendBuffer.GetSendBytes(), 0, sendBuffer.GetSendBytes().Length, 0, new AsyncCallback(SendCallback), s);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "PlayerClient - SendPacket", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendLog("Exception in: PlayerClient - SendPacket ->" + ex.Message);
            }

        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket s = (Socket)ar.AsyncState;
                if (s == null) return;
                // Complete sending the data to the remote device.
                s.EndSend(ar);
                //appendLog(String.Format("Sent {0} bytes to client.", bytesSent));

            }
            catch (Exception ex)
            {
                AppendLog("Exception in: PlayerClient - SendCallback ->" + ex.Message);
            }
        }
        private static void KillClientSocket(Candidate c)
        {
            try
            {
                _form.RemoveClient(c);
            }
            catch (Exception ex)
            {
                AppendLog("Exception in: PlayerClient - KillconnectionWithClient ->" + ex.Message);
            }
        }
        //http://stackoverflow.com/a/8996788
        private static string RandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            try
            {
                if (length < 0) throw new ArgumentOutOfRangeException("length", @"length cannot be less than zero.");
                if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

                const int byteSize = 0x100;
                var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
                if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

                // Guid.NewGuid and System.Random are not particularly random. By using a
                // cryptographically-secure random number generator, the caller is always
                // protected, regardless of use.
                using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    var result = new StringBuilder();
                    var buf = new byte[128];
                    while (result.Length < length)
                    {
                        rng.GetBytes(buf);
                        for (var i = 0; i < buf.Length && result.Length < length; ++i)
                        {
                            // Divide the byte into allowedCharSet-sized groups. If the
                            // random value falls into the last group and the last group is
                            // too small to choose from the entire allowedCharSet, ignore
                            // the value in order to avoid biasing the result.
                            var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                            if (outOfRangeStart <= buf[i]) continue;
                            result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                        }
                    }
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                AppendLog("Exception in: PlayerClient - RandomString ->" + ex.Message);
                return "";
            }

        }

        private static bool PassNewPlayerToServer(PacketContent pc, Candidate c)
        {
            bool ret = true;
            if (c.socket == null) return false;
            c.loginToken = RandomString(16);
            c.selectedServerIndex = pc.ReadLong();
            ret = GameServer.RegisterNewPlayer(c.selectedServerIndex, c.loginName, c.loginToken, ref c.player, ref c.playerBank);
            if (!ret)
            {
                string ipAddress = ((IPEndPoint)c.socket.RemoteEndPoint).Address.ToString();
                AppendLog(c.loginName + "/" + ipAddress +  "is trying to login, but there's no available servers for him to join.");
                SendClient_AlertMsg(c.socket, "Server is down, try again in a minute!", true);
                KillClientSocket(c);
            }
            return ret;
        }

        private static void PreloadPlayer(Candidate c, PlayerDBInfo pInfo)
        {
            c.player = new Player(0);
            var file = new AccountsManager(_form);
            if (!file.LoadPlayer(c.loginName, ref c.player))
            {
                for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
                {
                    c.player.friends[i].name = new string(' ', Constants.PLAYER_NAME_LENGTH); //<--- max display name length on the forum.
                }
            }
            c.player.PlayerName = pInfo.charName;
            c.playerBank = new Bank(0);
            file.LoadPlayerBank(c.loginName, ref c.playerBank);
        }

        public static void SendClient_JoinGame(string loginName)
        {
            Candidate c = AuthenticationServer.getCandidateByLoginName(loginName);
            var send = new PacketsProcessor((int)PlayerSendPackets.JoinGame);
            send.WriteLong(c.selectedServerIndex);
            send.WriteString(c.loginToken);
            send.FinishPacking();

            SendPacket(send, c.socket);
        }
        public static void PlayerRejected(string loginName, string reason)
        {
            Candidate c = AuthenticationServer.getCandidateByLoginName(loginName);
            SendClient_AlertMsg(c.socket, reason);
            KillClientSocket(c);
        }
        private static void DecryptPassword(Candidate c)
        {
            c.password = StrXor(c.password, Constants.salt.Substring(0, c.password.Length));
        }
        private static string StrXor(string encryptedPassword, string salt)
        {
            if (encryptedPassword.Length != salt.Length) return "no";
            string decryptedPassword = "";
            for (var i = 0; i < encryptedPassword.Length; i++)
            {
                decryptedPassword += (char)(encryptedPassword[i] ^ salt[i]);
            }
            return decryptedPassword;
        }
        private static bool CheckValidating(PlayerDBInfo pInfo)
        {
            if (pInfo.group != 1) return true; // 1 -> Group = 1 -> accounts awaiting validation
            return false;
        }


    }
}

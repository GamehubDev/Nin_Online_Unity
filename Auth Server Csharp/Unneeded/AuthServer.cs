using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using AuthServer.Client;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Collections.Concurrent;

namespace AuthServer
{

    public partial class AuthenticationServer : Form
    {
        //Servers
        private Socket _serversListener;
        private static List<ServerDetails> servers = new List<ServerDetails>();
        static ConcurrentQueue<PacketContainer> _queuedServerPackets;
        private Thread _serverPacketsProcesor;
        private volatile static bool _continueServerProcessing = true;

        //Clients
        private Socket _clientsListener;
        private static List<Candidate> clients = new List<Candidate>();
        static ConcurrentQueue<PacketContainer> _queuedClientPackets;
        private Thread _clientPacketsProcesor;
        private volatile static bool _continueClientProcessing = true;
        private long clientsCustomID = 0;

        //Updating log
        private delegate void updateLogDelegate(String textToAppend);
        private updateLogDelegate updateLog;
        void updateLogger(string str)
        {
            DateTime src = DateTime.Now;
            richLog.Text += String.Format("{0:T}", src) + "| " + str + "\n";
        }
        public void appendLog(string text) { this.Invoke(this.updateLog, text); }

        public AuthenticationServer()
        {
            InitializeComponent();
            _queuedServerPackets = new ConcurrentQueue<PacketContainer>();
            _serverPacketsProcesor = new Thread(() => ServerPacketsProcessor(this));

            _queuedClientPackets = new ConcurrentQueue<PacketContainer>();
            _clientPacketsProcesor = new Thread(() => ClientPacketsProcessor(this));
            updateLog = new updateLogDelegate(updateLogger);

            Database.Instance.SetUI(this);

            txtMajor.setName = "AppMajor";
            txtMinor.setName = "AppMinor";
            txtRevision.setName = "AppRevision";
            txtMajor.Load();
            txtMinor.Load();
            txtRevision.Load();
            SetVersionStatus(true);
        }
        //START======================SERVER==============================
        private void Start_ServerListener()
        {
            try
            {
                appendLog("Started listening for servers on port 7001...");

                _serverPacketsProcesor.Start();

                _serversListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _serversListener.Bind(new IPEndPoint(IPAddress.Any, 7001));
                _serversListener.Listen(1);
                _serversListener.BeginAccept(new AsyncCallback(AcceptServer), null);
            }
            catch (System.Exception ex)
            {
                appendLog("Exception in: Server Listener - Start_ServerListener() ->" + ex.Message);
                //MessageBox.Show(ex.Message, "Server Listener", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AcceptServer(IAsyncResult ar)
        {
            bool restarted = false;
            try
            {
                Socket tempServer = _serversListener.EndAccept(ar);
                _serversListener.BeginAccept(new AsyncCallback(AcceptServer), null);
                restarted = true;
                if (servers.Count < Constants.MAX_SERVERS)
                {
                    ServerDetails newServer = new ServerDetails();
                    servers.Add(newServer);

                    newServer.socket = tempServer;

                    
                    newServer.buffer = new byte[newServer.socket.ReceiveBufferSize];
                    newServer.socket.BeginReceive(newServer.buffer, 0, newServer.buffer.Length, SocketFlags.None, new AsyncCallback(OnServerReceive), newServer);
                }
                else RejectConnection("Rejecting Server -> too many servers registered...", tempServer);
 

            }
            catch (Exception ex)
            {
                if (!restarted) _serversListener.BeginAccept(new AsyncCallback(AcceptServer), null);
                appendLog("Exception in: Server Listener - AcceptServer() ->" + ex.Message);
                //MessageBox.Show(ex.Message, "Server Listener - AcceptServer()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OnServerReceive(IAsyncResult ar)
        {
            ServerDetails server = (ServerDetails)ar.AsyncState;
            try
            {
                int size = server.socket.EndReceive(ar);

                if (size > 0)
                {
                    _queuedServerPackets.Enqueue(new PacketContainer(server.buffer, size, server.serverIndex));
                    server.socket.BeginReceive(server.buffer, 0, server.buffer.Length, SocketFlags.None, new AsyncCallback(OnServerReceive), server);
                }
                else RemoveServer(server);

            }
            catch (Exception ex)
            {
                if (ex is SocketException)
                {
                    IPEndPoint serverInfo = (IPEndPoint)server.socket.RemoteEndPoint;
                    switch (((SocketException)ex).ErrorCode)
                    {
                        case 10054:
                            this.appendLog("Server["+server.index+"] - "+serverInfo.Address.ToString() + " abruptly closed the connection!");
                            break;

                        default:
                            this.appendLog("Server[" + server.index + "] - SocketException" + " \n" + ex.Message);
                            break;
                    }
                }
                RemoveServer(server);
            }
        }
        private static void ServerPacketsProcessor(AuthenticationServer ui)
        {
            //TODO: make it work for multiple servers, we need a list holding fragments for different servers and logic to manage it.
            Fragmentation fragment = new Fragmentation(8192);

            while (_continueServerProcessing)
            {
                PacketContainer cont;
                if (_queuedServerPackets.TryDequeue(out cont))
                {

                    PacketsProcessor dataProcessor = new PacketsProcessor();
                    dataProcessor.ExtractPackets(cont, ref fragment);
                    foreach (PacketContent p in cont.extractedPackets)
                    {
                        GameServer.Received(ui, p, GetServerByIndex(cont.serverID));
                    }
                }
                else
                {
                    Thread.Sleep(5);
                }
            }
        }
        //END========================SERVER==============================

        //START======================CLIENT==============================
        private void Start_ClientsListener()
        {
            try
            {
                _clientPacketsProcesor.Start();

                appendLog("Started listening for clients on port 4001...");
                _clientsListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientsListener.Bind(new IPEndPoint(IPAddress.Any, 4001));
                _clientsListener.Listen(Constants.MAX_CLIENTS_AT_ONCE);
                _clientsListener.BeginAccept(new AsyncCallback(AcceptClient), null);

            }
            catch (System.Exception ex)
            {
                appendLog("Exception in: Client Listener - Start_ClientsListener() ->" + ex.Message);
                //MessageBox.Show(ex.Message, "Server Listener", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AcceptClient(IAsyncResult ar)
        {
            bool restarted = false;
            try
            {
                Socket tempClient = _clientsListener.EndAccept(ar);
                _clientsListener.BeginAccept(new AsyncCallback(AcceptClient), null);
                restarted = true; // do not touch
                if (servers.Count == 0)
                {
                    PlayerClient.SendClient_AlertMsg(tempClient, "Server is down, try again in a minute!", true);
                    IPEndPoint clientInfo = (IPEndPoint)tempClient.RemoteEndPoint;
                    RejectConnection(clientInfo.Address.ToString() + " is trying to login, but there's no available servers for him to join.", tempClient);
                }
                else if (clients.Count < Constants.MAX_CLIENTS_AT_ONCE)
                {
                    Candidate c = new Candidate();
                    clients.Add(c);
                    c.socket = tempClient;

                    c.buffer = new byte[c.socket.ReceiveBufferSize];
                    c.id = clientsCustomID;
                    c.connectionTime = DateTime.Now.Ticks;
                    clientsCustomID++;
                    c.socket.BeginReceive(c.buffer, 0, c.buffer.Length, SocketFlags.None, new AsyncCallback(OnClientReceive), c);
                }
                else RejectConnection("Rejecting Client -> too many clients attempting to log in...(Limit set to " + Constants.MAX_CLIENTS_AT_ONCE + ")", tempClient);// TODO: Make a queue and send it to client?
            }
            catch (Exception ex)
            {
                if (!restarted) _clientsListener.BeginAccept(new AsyncCallback(AcceptClient), null);
                appendLog("Exception in: Client Listener - AcceptClient() ->" + ex.Message);
                //MessageBox.Show(ex.Message, "Client Listener - AcceptClient()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        private void OnClientReceive(IAsyncResult ar)
        {
            Candidate c = (Candidate)ar.AsyncState;
            try
            {
                int size = c.socket.EndReceive(ar);
                if (CountRegisteredServers() < 1)
                {
                    PlayerClient.SendClient_AlertMsg(c.socket, "Server is down, try again in a minute!", true);
                    IPEndPoint clientInfo = (IPEndPoint)c.socket.RemoteEndPoint;
                    this.appendLog(clientInfo.Address.ToString() + " is trying to login, but there's no available servers for him to join.");
                    RemoveClient(c);
                    return;
                }
                if (size > 0)
                {
                    _queuedClientPackets.Enqueue(new PacketContainer(c.buffer, size, c.id));
                    c.socket.BeginReceive(c.buffer, 0, c.buffer.Length, SocketFlags.None, new AsyncCallback(OnClientReceive), c);
                }
                else RemoveClient(c);
            }
            catch (Exception ex)
            {
                if (ex is SocketException)
                {
                    IPEndPoint clientInfo = (IPEndPoint)c.socket.RemoteEndPoint;
                    switch (((SocketException)ex).ErrorCode)
                    {
                        case 10054:
                            this.appendLog(clientInfo.Address.ToString() + " abruptly closed the connection!");
                            break;

                        default:
                            this.appendLog(clientInfo.Address.ToString() + "SocketException" + " \n" + ex.Message);
                            break;
                    }
                }
                RemoveClient(c);

            }
        }
        private static void ClientPacketsProcessor(AuthenticationServer ui)
        {
            //TODO: make it work for multiple servers, we need a list holding fragments for different servers and logic to manage it.
            Fragmentation fragment = new Fragmentation(8192);

            while (_continueClientProcessing)
            {
                PacketContainer cont;
                if (_queuedClientPackets.TryDequeue(out cont))
                {

                    PacketsProcessor dataProcessor = new PacketsProcessor();
                    dataProcessor.ExtractPackets(cont, ref fragment);
                    foreach (PacketContent p in cont.extractedPackets)
                    {
                        PlayerClient.Received(ui, p, getCandidateByID(cont.clientID));
                    }
                }
                else
                {
                    Thread.Sleep(5);
                }
            }
        }
        //END========================CLIENT==============================

        public bool AdminOnly
        {
            get { return checkAdmins.Checked & !checkDonors.Checked & !checkEveryone.Checked; }
        }
        public bool DonorOnly
        {
            get { return checkDonors.Checked & !checkEveryone.Checked; }
        }
        public bool PublicOpen
        {
            get { return checkEveryone.Checked; }
        }
        public static ServerDetails GetServerByIndex(int serverIndex)
        {
            foreach (ServerDetails server in servers)
            {
                if (server.serverIndex == serverIndex)
                {
                    return server;
                }
            }
            return null;
        }
        public static ServerDetails GetServer(int serverIndex)
        {
            if (serverIndex > servers.Count) return null;
            return servers[serverIndex];
        }
        public static int GetServerCount(){
            return servers.Count;
        }
        public static Candidate getCandidateByLoginName(string loginName)
        {
            foreach (Candidate c in clients)
            {
                if (c != null)
                {
                    if (c.loginName == loginName)
                    {
                        return c;
                    }
                }
            }
            return new Candidate();
        }
        public static Candidate getCandidateByID(long id)
        {
            foreach (Candidate c in clients)
            {
                if (c != null)
                {
                    if (c.id == id)
                    {
                        return c;
                    }
                }
            }
            return new Candidate();
        }
        private void AuthServer_Load(object sender, EventArgs e)
        {
            //Player test = new Player(0);
            //string charName = "Dawid R";
            //int size = charName.Length;
            //test.playerName = charName;

            //int fillerSize = Constants.PLAYER_NAME_LENGTH - size;
            //if (fillerSize > 0)
            //{
            //    test.playerName += new string(' ', fillerSize);
            //}

            //for (int i = 0; i < Constants.MAX_PLAYER_FRIENDS; i++)
            //{
            //    test.friends[i].name = new string(' ', Constants.PLAYER_NAME_LENGTH); //<--- max display name length on the forum.
            //    test.friends[i].online = 0;
            //}

            ////AccountsManager.LoadPlayer("davemax", ref test);
            //Bank temp = new Bank(0);
            //ServerDetails temp2 = new ServerDetails();
            //GameServer.SendServer_RegisterPlayer(temp2, "davemax", "1234567891234567", ref test, ref temp);

            //AccountsManager.SavePlayer("davemax", ref test);,

            //Packet Tester

            //Application.Exit();

            if (Database.Instance.CheckConnection())
            {
                appendLog("Database is available!");
                tmCleaner.Start();
                Start_ServerListener();
                Start_ClientsListener();
            }
            else
            {
                appendLog("Can't establish database connection!");
            }
        }
        private void RemoveServer(ServerDetails server)
        {
            if (server.socket != null && SocketConnected(server.socket))
            {
                server.socket.Shutdown(SocketShutdown.Both);
                server.socket.Close();
            }
            servers.Remove(server);
            DisconnectClientsForThisServer(server);
        }
        public void RemoveClient(Candidate c)
        {
            if (c.socket != null && SocketConnected(c.socket))
            {
                c.socket.Shutdown(SocketShutdown.Both);
                c.socket.Close();
            }
            clients.Remove(c);
        }
        private void RejectConnection(String msg, Socket s)
        {
            appendLog(msg);
            if (s != null && SocketConnected(s))
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }

        }
        bool SocketConnected(Socket s)//http://stackoverflow.com/a/2661876
        {
            try
            {
                bool part1 = s.Poll(1000, SelectMode.SelectRead);
                bool part2 = (s.Available == 0);
                if (part1 & part2)
                    return false;
                else
                    return true;
            }
            catch (ObjectDisposedException ex)
            {
                return false;
            }

        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            richLog.Clear();
            richLog.Refresh();
        }
        public long getMajor()
        {
            return int.Parse(txtMajor.Text);
        }
        public long getMinor()
        {
            return int.Parse(txtMinor.Text);
        }
        public long getRevision()
        {
            return int.Parse(txtRevision.Text);
        }
        private void AuthServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _continueClientProcessing = false;
                _continueServerProcessing = false;
                tmCleaner.Stop();
                List<ServerDetails> temp_servers = servers.ToList();
                foreach (ServerDetails server in temp_servers)
                {
                    RemoveServer(server);
                }
                List<Candidate> temp_clients = clients.ToList();
                foreach (Candidate c in temp_clients)
                {
                    if (c != null)
                    {
                        RemoveClient(c);
                    }
                }
            }
            catch (System.Exception ex) { };

        }

        private void tmCleaner_Tick(object sender, EventArgs e)
        {
            long elapsedTicks;
            TimeSpan elapsedSpan;
            List<Candidate> temp_clients = clients.ToList();
            foreach (Candidate c in temp_clients)
            {
                if (c != null)
                {
                    elapsedTicks = DateTime.Now.Ticks - c.connectionTime;
                    elapsedSpan = new TimeSpan(elapsedTicks);
                    if (elapsedSpan.TotalSeconds > 20)
                    {
                        RemoveClient(c);
                    }
                }
            }

        }

        private void checkAdmins_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAdmins.Checked == false)
            {
                checkDonors.CheckedChanged -= checkDonors_CheckedChanged;
                checkEveryone.CheckedChanged -= checkEveryone_CheckedChanged;

                checkDonors.Checked = false;
                checkEveryone.Checked = false;

                checkDonors.CheckedChanged += checkDonors_CheckedChanged;
                checkEveryone.CheckedChanged += checkEveryone_CheckedChanged;
            }
        }

        private void checkDonors_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDonors.Checked == false)
            {
                checkEveryone.CheckedChanged -= checkEveryone_CheckedChanged;

                checkEveryone.Checked = false;

                checkEveryone.CheckedChanged += checkEveryone_CheckedChanged;
            }
            checkAdmins.CheckedChanged -= checkAdmins_CheckedChanged;
            checkAdmins.Checked = true;
            checkAdmins.CheckedChanged += checkAdmins_CheckedChanged;
        }

        private void checkEveryone_CheckedChanged(object sender, EventArgs e)
        {
            checkDonors.CheckedChanged -= checkDonors_CheckedChanged;
            checkAdmins.CheckedChanged -= checkAdmins_CheckedChanged;

            checkDonors.Checked = true;
            checkAdmins.Checked = true;

            checkDonors.CheckedChanged += checkDonors_CheckedChanged;
            checkAdmins.CheckedChanged += checkAdmins_CheckedChanged;
        }

        public void SetVersionStatus(bool saved)
        {
            if (saved)
            {
                lblMajorOut.Text = txtMajor.Text;
                lblMinorOut.Text = txtMinor.Text;
                lblRevisionOut.Text = txtRevision.Text;

                lblVersionStatus.Text = "Saved";
                lblVersionStatus.BackColor = Color.FromArgb(0, 192, 0);
            }
            else
            {
                lblVersionStatus.Text = "Unsaved";
                lblVersionStatus.BackColor = Color.FromArgb(192, 0, 0); 

            }
        }

        private void Version_TextChanged(object sender, EventArgs e)
        {
            
            SetVersionStatus(false);
        }

        private void lblVersionStatus_Click(object sender, EventArgs e)
        {
            SetVersionStatus(true);
            txtMajor.SetColor(true);
            txtMinor.SetColor(true);
            txtRevision.SetColor(true);

            txtMajor.Save();
            txtMinor.Save();
            txtRevision.Save();
        }

        private static int CountRegisteredServers()
        {
            int counter = 0;
            foreach (ServerDetails server in servers)
            {
                if (server.registered) counter++;
            }
            return counter;
        }

        public void DisconnectClientsForThisServer(ServerDetails server)
        {
            foreach (Candidate c in clients)
            {
                if (c != null)
                {
                    if (c.selectedServerIndex == server.serverIndex)
                    {
                        RemoveClient(c);
                    }
                }
            }
        }

    }
}

using System;
using GameClient.Managers;
using SFML.Graphics;
using SFML.Window;

namespace GameClient
{
    public class Game
    {
        #region Field Region

        RenderWindow RenderWindow;

        #endregion

        #region Property Region

        /// <summary>
        /// Returns true as long as the <see cref="RenderWindow"/> is active.
        /// </summary>
        public bool IsOpen
        {
            get { return RenderWindow.IsOpen(); }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class in debug mode.
        /// </summary>
        /// <param name="path">Path to the project directory.</param>
        /// <param name="x">X co-ordinate of the window</param>
        /// <param name="y">Y co-ordinate of the window</param>
        /// <param name="width">Width of the window</param>
        /// <param name="height">Height of the window</param>
        public Game(string path, uint x, uint y, uint width, uint height)
        {
            if (!ProjectManager.Instance.LoadProject(path)) return;

            RenderWindow = new RenderWindow(new VideoMode(width, height), ProjectManager.Instance.Project.Name);
            RenderWindow.SetFramerateLimit(60);

            if (x != 0 && y != 0)
                RenderWindow.Position = new Vector2i((int)x, (int)y);

            RenderWindow.Closed += OnClose;
        }

        #endregion

        #region Event Handler Region

        /// <summary>
        /// Handles the Close event of the <see cref="RenderWindow"/> object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClose(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        #endregion

        #region Method Region

        /// <summary>
        /// Forces the game to close.
        /// </summary>
        public void Close()
        {            
            RenderWindow.Close();
        }

        #endregion

        #region SFML Region

        /// <summary>
        /// Renders the game.
        /// </summary>
        public void Render()
        {
            RenderWindow.DispatchEvents();

            RenderWindow.Clear(new Color(200, 200, 200));

            RenderWindow.Display();
        }

        #endregion
    }
}
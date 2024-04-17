using System;
using UnityEngine;
using TwitchLib.Unity;

namespace Twitchmata {

    public class FeatureManager : MonoBehaviour {
        public ConnectionManager Connection;
        public TwitchManager Manager;

        #region Initialization
        /// <summary>
        /// Override this in a subclass to perform any initialisation
        /// </summary>
        /// <remarks>
        /// This is where you would setup things such as chat commands and channel point rewards
        /// </remarks>
        public virtual void InitializeFeatureManager() {}
        #endregion


        #region Convenience Properties & Methods
        /// <summary>
        /// The current channel ID
        /// </summary>
        public string ChannelID {
            get { return this.Connection.ChannelID; }
        }

        /// <summary>
        /// The current twitch API
        /// </summary>
        public TwitchLib.Api.Helix.Helix HelixAPI {
            get { return this.Connection.API.Helix; }
        }

        internal UserManager UserManager {
            get { return this.Connection.UserManager; }
        }

        /// <summary>
        /// Sends a Message to Chat
        /// </summary>
        /// <param name="message">Message to Send</param>
        public void SendChatMessage(string message) {
            this.Connection.Client.SendMessage(this.Connection.ConnectionConfig.ChannelName, message);
        }

        /// <summary>
        /// Uses Twitch Feature to Reply to a Message
        /// </summary>
        /// <param name="messageId">Message Id you reply to</param>
        /// <param name="message">Message to Reply</param>
        public void ReplyToChatMessage(string messageId, string message) {
            this.Connection.Client.SendReply(this.Connection.ConnectionConfig.ChannelName, messageId, message);
        }
        #endregion



        /**************************************************
         * INTERNAL CODE. NO NEED TO READ BELOW THIS LINE *
         **************************************************/

        #region Internal Initialization
        internal void InitializeWithAPIManager(ConnectionManager manager) {
            this.Connection = manager;
            this.InitializeFeatureManager();
            this.InitializePubSub(manager.PubSub);
            this.InitializeClient(manager.Client);
        }

        internal virtual void InitializePubSub(PubSub pubSub) { }

        internal virtual void InitializeClient(Client client) { }
        
        //All feature managers set up by user are guaranteed to exist when this is called
        internal virtual void PerformPostDiscoverySetup() { }
        #endregion
    }

}
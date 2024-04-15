using System;
using System.Collections.Generic;
using System.Linq;

namespace Twitchmata.Models {
    /// <summary>
    /// Class to hold multiple rewards to make it easier to mass enable/disable rewards
    /// </summary>
    public class ManagedRewardGroup
    {
        /// <summary>
        /// The Name of the Group
        /// </summary>
        public string GroupName;
        
        /// <summary>
        /// The Guid of the Group
        /// </summary>
        public Guid GroupId;
        
        /// <summary>
        /// The rewards in the group
        /// </summary>
        public List<ManagedReward> Rewards { get; private set; } = new List<ManagedReward>() { };

        /// <summary>
        /// Add a reward to the Group
        /// </summary>
        /// <param name="reward">The reward to add</param>
        public void AddReward(ManagedReward reward) {
            if (this.Rewards.Contains(reward)) {
                return;
            }
            this.Rewards.Add(reward);
        }

        /// <summary>
        /// Removes a Reward with matching Id from the Group
        /// </summary>
        /// <param name="id">Guid of the Reward</param>
        public void RemoveReward(Guid id)
        {
            var reward = this.Rewards.FirstOrDefault(x => x.Id == id.ToString());
            if (reward is not null)
                RemoveReward(reward);
        }
        
        /// <summary>
        /// Removes a Reward with matching Title from the Group
        /// </summary>
        /// <param name="title">Title from the Reward (Case Sensitive!)</param>
        public void RemoveReward(string title)
        {
            var reward = this.Rewards.FirstOrDefault(x => x.Title == title);
            if (reward is not null)
                RemoveReward(reward);
        }
        
        /// <summary>
        /// Removes a reward from the Group
        /// </summary>
        /// <param name="reward">Reward to Remove</param>
        public void RemoveReward(ManagedReward reward)
        {
            if (this.Rewards.Contains(reward))
                this.Rewards.Remove(reward);
        }

        /// <summary>
        /// Create a new Group from an Array of rewards
        /// </summary>
        /// <param name="rewards">The List of rewards to create the Group with</param>
        public ManagedRewardGroup(List<ManagedReward> rewards) {
            this.Rewards = rewards;
            this.GroupId = new Guid();
        }
        
        /// <summary>
        /// Create a new group from an array of rewards
        /// </summary>
        /// <param name="rewards">The array of rewards to create the Group with</param>
        public ManagedRewardGroup(ManagedReward[] rewards) {
            this.Rewards = new List<ManagedReward>(rewards);
            this.GroupId = new Guid();
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <param name="name">Name of the Group</param>
        /// <param name="guid">Guid of the Group (Optional parameter to provide and set own Guid if needed)</param>
        public ManagedRewardGroup(string name = "", Guid guid = new Guid())
        {
            this.GroupName = name;
            this.GroupId = guid;
        }
    }
}


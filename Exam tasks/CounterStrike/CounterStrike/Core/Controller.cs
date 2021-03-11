using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;

        public Controller()
        {
            guns = new GunRepository();
            players = new PlayerRepository();
            map = new Map();
        }
        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gunToAdd;
            if (type == "Rifle")
            {
                gunToAdd = new Rifle(name, bulletsCount);
            }
            else if (type == "Pistol")
            {
                gunToAdd = new Pistol(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            guns.Add(gunToAdd);

            return string.Format(OutputMessages.SuccessfullyAddedGun, name);
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {

            IGun gunToAdd = guns.FindByName(gunName);
            if (gunToAdd == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            IPlayer playerToAdd;

            if (type == "Terrorist")
            {
                playerToAdd = new Terrorist(username, health, armor, gunToAdd);
            }
            else if (type == "CounterTerrorist")
            {
                playerToAdd = new CounterTerrorist(username, health, armor, gunToAdd);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            players.Add(playerToAdd);

            return string.Format(OutputMessages.SuccessfullyAddedPlayer, username);
        }

        public string Report()
        {
            var sortedPlayers = players
                 .Models
                 .OrderBy(p => p.GetType().Name)
                 .ThenByDescending(p => p.Health)
                 .ThenBy(p => p.Username);

            StringBuilder result = new StringBuilder();
            foreach (var player in sortedPlayers)
            {
                result.AppendLine(player.ToString());
            }

            return result.ToString().TrimEnd();
        }

        public string StartGame()
        {
            return map.Start(players.Models.ToList());
        }
    }
}

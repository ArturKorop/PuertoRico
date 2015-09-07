using System;
using System.Runtime.Remoting.Channels;

namespace Core.Entities
{
    public class RoleCard
    {
        public Roles Role { get; }

        public bool IsUsed { get; private set; }

        public int Doubloons { get; private set; }

        public RoleCard(Roles role)
        {
            Role = role;
            IsUsed = false;
            Doubloons = 0;
        }

        public Tuple<Roles, int> Take()
        {
            if (IsUsed)
            {
                throw new InvalidOperationException("Role card was used");
            }

            IsUsed = true;
            var doubloons = Doubloons;
            Doubloons = 0;

            return new Tuple<Roles, int>(Role, doubloons);
        }
    }

    public static class RoleCardsExtensions
    {
        public static bool IsRequiredAllPlayerActions(this RoleCard card)
        {
            switch (card.Role)
            {
                case Roles.Builder:
                case Roles.Captain:
                case Roles.Mayor:
                case Roles.Settler:
                case Roles.Trader:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsRequiredCurrentPlayerAction(this RoleCard card)
        {
            switch (card.Role)
            {
                    case Roles.Craftsman:
                    return true;
                default:
                    return false;
            }
        }
    }
}
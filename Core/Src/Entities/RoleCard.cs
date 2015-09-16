using System;

namespace Core.Entities
{

    public class RoleCardStatus
    {
        public bool IsUsed { get; protected set; }

        public Roles Role { get; }

        public int Doubloons { get; protected set; }

        public RoleCardStatus(Roles role)
        {
            Role = role;
            Doubloons = 0;
            IsUsed = false;
        }
    }

    public class RoleCard : RoleCardStatus
    {
        public RoleCard(Roles role) : base(role)
        {
            IsUsed = false;
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

        public void NextRound()
        {
            if (!IsUsed)
            {
                Doubloons++;
            }
            else
            {
                IsUsed = false;
            }
        }
    }

    public static class RoleCardsExtensions
    {
        public static bool IsRequiredAllPlayerActions(this RoleCardStatus card)
        {
            switch (card.Role)
            {
                case Roles.Builder:
                case Roles.Mayor:
                case Roles.Settler:
                case Roles.Trader:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsRequiredCurrentPlayerAction(this RoleCardStatus card)
        {
            switch (card.Role)
            {
                case Roles.Craftsman:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsRequiredAllPlayersActionSeveralTimes(this RoleCardStatus card)
        {
            switch (card.Role)
            {
                case Roles.Captain:
                    return true;
                default:
                    return false;
            }
        }
    }
}
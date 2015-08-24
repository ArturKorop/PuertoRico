using System;

namespace Core.Entities
{
    public class RoleCard
    {
        public Roles Role { get; private set; }

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
}
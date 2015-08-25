using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Core
{
    public class CommandManager
    {
        public PlayerController CurrentPlayer { get; set; }

        public void RunCommand(Roles role, BuilderActionParameter builderActionParameter = null,
            SettlerActionParameter settlerActionParameter = null, MayorActionParameter mayorActionParameter = null)
        {
            switch (role)
            {
                case Roles.Builder:
                    DoBuilderAction(builderActionParameter);
                    break;
                case Roles.Settler:
                    DoSettlerAction(settlerActionParameter);
                    break;
                case Roles.Mayor:
                    DoMayorAction(mayorActionParameter);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void DoMayorAction(MayorActionParameter mayorActionParameter)
        {
        }

        private void DoSettlerAction(SettlerActionParameter settlerActionParameter)
        {
        }

        private void DoBuilderAction(BuilderActionParameter parameter)
        {
        }
    }

    public abstract class RoleOwner
    {
        public bool IsRoleOwner { get; set; }
    }

    public class MayorActionParameter : RoleOwner
    {
        public List<ColonistMoving> ColonistsMoving;
    }

    public class ColonistMoving
    {
    }

    public class SettlerActionParameter
    {
    }

    public class BuilderActionParameter
    {
        private IBuilding BuildingToBuild { get; set; }
    }
}
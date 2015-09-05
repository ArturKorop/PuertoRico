using System;
using Core.Entities.Interfaces;

namespace Core.Entities.Base
{
    public abstract class BuildingBase<T> : ColonistsHolderBase, IBuilding
    {
        public int Cost { get; protected set; }

        public int Vp { get; protected set; }

        public int Size { get; protected set; }

        public int Discount { get; protected set; }

        protected BuildingBase(int cost, int vp, int size, int discount, int maxColonists) : base(maxColonists)
        {
            Cost = cost;
            Vp = vp;
            Size = size;
            Discount = discount;
        }

        public virtual void DoAction(ref T parameters)
        {
            if (ActivePoints == 0)
            {
                throw new InvalidOperationException("Too less colonists");
            }

            DoActionImpl(ref parameters);
        }

        protected abstract void DoActionImpl(ref T parameters);
    }
}
namespace Core.Entities
{
    public abstract class BuildingBase<T> : IBuilding
    {
        public int Cost { get; protected set; }

        public int Vp { get; protected set; }

        public int Size { get; protected set; }

        public int Discount { get; protected set; }

        public int MaxColonistsCount { get; protected set; }

        public int CurrentColonistsCount { get; protected set; }

        public virtual bool CanDoAction
        {
            get { return CurrentColonistsCount > 0; }
        }

        protected BuildingBase(int cost, int vp, int size, int discount, int maxColonists)
        {
            Cost = cost;
            Vp = vp;
            Size = size;
            Discount = discount;
            MaxColonistsCount = maxColonists;
        }

        public bool AddColonist()
        {
            if (CurrentColonistsCount < MaxColonistsCount)
            {
                CurrentColonistsCount++;

                return true;
            }

            return false;
        }

        public bool RemoveColonist()
        {
            if (CurrentColonistsCount > 0)
            {
                CurrentColonistsCount--;

                return true;
            }

            return false;
        }

        public virtual void DoAction(ref T parameters)
        {
            if (CanDoAction)
            {
                DoActionImpl(ref parameters);
            }
        }

        protected abstract void DoActionImpl(ref T parameters);
    }
}

namespace Core.Entities
{
    public class Plantation
    {
        public Goods Type { get; private set; }

        public bool HasWorker { get; private set; }

        public Plantation(Goods type)
        {
            Type = type;
            HasWorker = false;
        }

        public bool AddWorker()
        {
            if (HasWorker) return false;
            HasWorker = true;

            return true;
        }

        public bool RemoveWorker()
        {
            if (!HasWorker) return false;
            HasWorker = false;

            return true;
        }
    }
}

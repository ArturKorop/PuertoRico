namespace Core.Entities
{
    public class Player
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Victory points
        /// </summary>
        public int Vp { get; set; }

        public int Doubloons { get; set; }

        public int Colonists { get; set; }

        public PlayerBoard Board { get; set; }

        public Warehouse Warehouse { get; set; }

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
            Board = new PlayerBoard();
            Warehouse = new Warehouse();
        }

        public void ReceiveDoubloons(int doubloons)
        {
            Doubloons += doubloons;
        }
    }
}

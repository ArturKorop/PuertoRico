using System.Text;

namespace Core.Entities
{
    public class PlayerStatus
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Vp { get; set; }

        public int Doubloons { get; set; }

        public PlayerBoard Board { get; set; }

        public Warehouse Warehouse { get; set; }

        public PlayerStatus(int id, string name)
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

        public string Status()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Id: {0}; Name: {1}\n", Id, Name);
            builder.AppendFormat("Vp: {0}; Doubloons: {1}\n", Vp, Doubloons);
            builder.AppendFormat("Colonists: {0};\n", Board.Colonists);
            builder.AppendLine(Warehouse.Status());
            builder.AppendLine(Board.Status());

            return builder.ToString();
        }
    }
}

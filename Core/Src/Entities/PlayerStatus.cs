using System;
using System.Text;

namespace Core.Entities
{
    public class PlayerStatus
    {
        public int Id { get; }

        public string Name { get; }

        public int Vp { get; }

        public int Doubloons { get; private set; }

        public PlayerBoard Board { get; }

        public Warehouse Warehouse { get; }

        public PlayerStatus(int id, string name)
        {
            Id = id;
            Name = name;
            Vp = 0;
            Doubloons = 0;
            Board = new PlayerBoard();
            Warehouse = new Warehouse();
        }

        public void ReceiveDoubloons(int doubloons)
        {
            Doubloons += doubloons;
        }

        public int PayDoubloons(int doubloons)
        {
            if (doubloons > Doubloons)
            {
                throw new InvalidOperationException("Too less doubloons");
            }

            Doubloons -= doubloons;

            return doubloons;
        }

        public string Status()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Id: {0}; Name: {1}\n", Id, Name);
            builder.AppendFormat("Vp: {0}; Doubloons: {1}\n", Vp, Doubloons);
            builder.AppendFormat("Colonists: {0};\n", Board.TotalColonists);
            builder.AppendLine(Warehouse.Status());
            builder.AppendLine(Board.Status());

            return builder.ToString();
        }
    }
}

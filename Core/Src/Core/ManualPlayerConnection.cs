namespace Core.Core
{
    public class ManualPlayerConnection : IPlayerConnection
    {
        private PlayerController _controller;

        public string Name { get; private set; }

        public int Id { get { return _controller.PlayerStatus.Id; } }
        public Manager Manager { get; private set; }

        public ManualPlayerConnection(string name)
        {
            Name = name;
        }

        public void Init(PlayerController playerController)
        {
            _controller = playerController;
            Manager = new Manager(_controller);
        }

        public string Status()
        {
            return _controller.PlayerStatus.Status();
        }
    }
}
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boll an egg", Line="Boll water", Plataform="Ketlle & Pan"},
                new Command{Id=1, HowTo="Cut Bread", Line="Get a Knife", Plataform="Knige & chopping board"},
                new Command{Id=2, HowTo="Boll an egg", Line="Boll water", Plataform="Ketlle & Pan"}
            };
            return commands;
        }

        public Command GetCommandByID(int id)
        {
            return new Command{Id=0, HowTo="Boll an egg", Line="Boll water", Plataform="Ketlle & Pan"};
        }

        public bool saveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
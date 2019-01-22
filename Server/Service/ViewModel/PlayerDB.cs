using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerDb : UserDb
    {
        protected override BaseEntity NewEntity()
        {
            return new Player();
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Player p = entity as Player;

            base.CreateModel(p);
            p.Id = (int) Reader["ID"];
            p.UserId = (int) Reader["user_id"];
            p.TempScore = (int) Reader["temp_score"];

            return p;
        }

        public new PlayerList SelectAll()
        {
            Command.CommandText =
                "SELECT *, Player_Table.Id AS ID FROM(User_Table INNER JOIN Player_Table ON User_Table.ID = Player_Table.user_id)";
            PlayerList temp = new PlayerList(Select());
            return temp;
        }

        public PlayerList GetPlayersByUserId(int userId)
        {
            Command.CommandText =
                "SELECT * FROM Player_Table WHERE [user_id] = "+userId+"";
            PlayerList temp = new PlayerList(Select());
            return temp;
        }

        //get the last <index> players inserted into the table
        public Player GetLastPlayer()
        {
            Command.CommandText = "SELECT User_Table.ID, User_Table.username, User_Table.[password], User_Table.first_name, User_Table.last_name, User_Table.score, User_Table.[level], User_Table.admin, User_Table.wins, User_Table.losses, Player_Table.ID AS Expr1, Player_Table.user_id, Player_Table.temp_score, Player_Table.ID AS Expr2 FROM(User_Table INNER JOIN Player_Table ON User_Table.ID = Player_Table.user_id) WHERE(User_Table.ID = (SELECT MAX(ID) AS Expr1 FROM Player_Table))";
            PlayerList temp = new PlayerList(Select());
            return temp.Count > 0 ? temp[0] : new Player(0);
        }

        public int GetLastPlayerID()
        {
            Command.CommandText = "SELECT MAX(ID) FROM Player_Table";
            GameList temp = new GameList(Select());
            return temp.Count > 0 ? temp[0].Id : new Game(0).Id;
        }


        public UserList SelectByName(string firstName, string lastName)
        {
            Command.CommandText = "SELECT *, Player_Table.Id AS ID FROM(User_Table INNER JOIN Player_Table ON User_Table.ID = Player_Table.user_id) WHERE [first_name] = @fName AND [last_name] = @lName";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@fName", firstName));
            Command.Parameters.Add(new OleDbParameter("@lName", lastName));


            UserList temp = new UserList(Select());
            return temp;
        }


        public Player GetPlayerById(int id)
        {
            Command.Parameters.Clear();
            Command.CommandText = "SELECT *, Player_Table.ID AS ID FROM" +
                    "(Player_Table INNER JOIN" +
                    " User_Table ON User_Table.ID = Player_Table.user_id)"+
                    "WHERE Player_Table.ID = @Id";

            //parameters
            Command.Parameters.Add(new OleDbParameter("@Id", id));

            PlayerList temp = new PlayerList(Select());

            if (temp.Count >= 1) return temp[0];

            return null;
        }

        public override void Insert(BaseEntity entity)
        {
            Player p = entity as Player;
            if (p != null) Inserted.Add(new ChangeEntity(CreateInsertSql, entity));
        }

        public void InsertTable(Game game)
        {
           Inserted.Add(new ChangeEntity(CreateInsertTableSql, game));
        }

        public void InsertList(PlayerList entity)
        {
            PlayerList pl = entity;
            foreach (var player in pl)
                if (player != null)
                    Inserted.Add(new ChangeEntity(CreateInsertSql, player));
        }

        public override void Delete(BaseEntity entity)
        {
            Player p = entity as Player;
            PlayerCardDb pCdb = new PlayerCardDb();
            PlayerGameDb pGdb = new PlayerGameDb();

            ConnectionList pc = pCdb.SelectByPlayerId(p.Id);
            ConnectionList pg = pGdb.SelectByPlayerId(p.Id);


            if (p != null)
            {
                foreach (Connection c in pc) //delete all cards connections to this player using PlayerCardDB
                    Updated.Add(new ChangeEntity(pCdb.CreateDeleteSql, c));

                foreach (Connection c in pg) //delete all games connections to this player using PlayerGameDB
                    Updated.Add(new ChangeEntity(pGdb.CreateDeleteSql, c));

                Updated.Add(new ChangeEntity(CreateDeleteSql, entity)); //delete the player itself
            }
        }


        public override void Update(BaseEntity entity)
        {
            Player player = entity as Player;
            if (player != null) Updated.Add(new ChangeEntity(CreateUpdateSql, entity));
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            command.Parameters.Clear();

            Player p = entity as Player;

            command.CommandText = "INSERT INTO Player_Table ([user_id], [temp_score]) VALUES (@user_id,  0)";

            //parameters

           // command.Parameters.Add(new OleDbParameter("@Id", p.Id));
            command.Parameters.Add(new OleDbParameter("@user_id", p.UserId));
        }

        public void CreateInsertTableSql(BaseEntity entity, OleDbCommand command)
        {
            command.Parameters.Clear();

            Game g = entity as Game;

            command.CommandText = "INSERT INTO Player_Table ([ID]) VALUES (@Id)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@Id", -g.Id));
           
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            Player p = entity as Player;

            command.CommandText = "INSERT INTO User_Table WHERE ID = @id";

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", p.Id));
        }


        //only used to update the score
        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            Player p = entity as Player;

            command.CommandText = "UPDATE Player_Table WHERE ID = @id SET temp_score = @score";

            //parameters

            command.Parameters.Add(new OleDbParameter("@score", p.TempScore));
            command.Parameters.Add(new OleDbParameter("@id", p.Id));
        }
    }
}
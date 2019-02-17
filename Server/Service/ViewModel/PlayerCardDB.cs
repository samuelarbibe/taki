using System;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class PlayerCardDb : BaseDb
    {
        protected override BaseEntity NewEntity()
        {
            return new PlayerCardConnection();
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            PlayerDb playerDb = new PlayerDb();
            CardDb cardDb = new CardDb();

            PlayerCardConnection con = entity as PlayerCardConnection;
            con.Id = (int) Reader["ID"];
            con.Player = playerDb.GetPlayerById((int) Reader["player_id"]);
            con.Card = cardDb.SelectById((int) Reader["card_id"]);
            return con;
        }

        public ConnectionList SelectByPlayer(Player player)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE [player_id] = @id";


            //parameters
            Command.Parameters.Add(new OleDbParameter("@id", player.Id));


            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public PlayerCardConnection GetConnectionByPlayerIdAndCardId(Player player, Card card)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE [player_id] = @playerId AND [card_id] = @cardId";
            
            // parameters
            Command.Parameters.Add(new OleDbParameter("@playerId", player.Id));
            Command.Parameters.Add(new OleDbParameter("@cardId", card.Id));



            ConnectionList conList = new ConnectionList(Select());
            return (PlayerCardConnection)conList[0];
        }

        public ConnectionList GetConnectionsByPlayerId(Player player)
        {
            Command.CommandText = "SELECT * FROM Player_Card_Table WHERE [player_id] = @playerId";

            // parameters
            Command.Parameters.Add(new OleDbParameter("@playerId", player.Id));

            ConnectionList conList = new ConnectionList(Select());
            return conList;
        }

        public void SwitchConnectionsByPlayersId(Player player1, Player player2)
        {
            ConnectionList hand1 = GetConnectionsByPlayerId(player1);
            ConnectionList hand2 = GetConnectionsByPlayerId(player2);

            foreach(PlayerCardConnection c in hand1)
            {
                c.Player = player1; // switch the owner of the cards to the other player
                Update(c); // update
            }

            foreach(PlayerCardConnection c in hand2)
            {
                c.Player = player2;// switch the owner of the cards to the other player
                Update(c); // update
            }

        }

        public void InsertList(ConnectionList entity)
        {
            ConnectionList cl = entity;
            foreach (var PlayerCardConnection in cl)
                if (PlayerCardConnection != null)
                    Inserted.Add(new ChangeEntity(CreateInsertSql, PlayerCardConnection));
        }

        public void Insert(Game game)
        {
            foreach (Player p in game.Players)
            {
                foreach (Card c in p.Hand)
                {
                    Inserted.Add(new ChangeEntity(CreateInsertSql, new PlayerCardConnection()
                    {
                        Player = p,
                        Card = c
                    }));
                }
            }

        }

        public override void Delete(BaseEntity entity)
        {
            if(entity != null)
            {
                Updated.Add(new ChangeEntity(CreateDeleteSql, entity));
            }
        }


        public override void CreateInsertSql(BaseEntity entity, OleDbCommand command)
        {
            PlayerCardConnection con = entity as PlayerCardConnection;

            command.CommandText = "INSERT INTO Player_Card_Table (player_id, card_id) VALUES (@player_id, @card_id)";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.Player.Id));
            command.Parameters.Add(new OleDbParameter("@card_id", con.Card.Id));

            Console.WriteLine("PlayerCardConnection between player [" + con.Player.Id + "] and card [" + con.Card.Id +
                              "] INSERTED");
        }

        public override void CreateDeleteSql(BaseEntity entity, OleDbCommand command)
        {
            PlayerCardConnection con = entity as PlayerCardConnection;

            command.CommandText = "DELETE FROM Player_Card_Table WHERE [ID] = @id ";

            //parameters

            command.Parameters.Add(new OleDbParameter("@id", con.Id));

            Console.WriteLine("PlayerCardConnection between player [" + con.Player.Id + "] and card [" + con.Card.Id + "] DELETED");
        }

        public override void CreateUpdateSql(BaseEntity entity, OleDbCommand command)
        {
            PlayerCardConnection con = entity as PlayerCardConnection;

            command.CommandText = "UPDATE Player_Card_Table SET [player_id] = @player_id WHERE [ID] = @id";

            //parameters

            command.Parameters.Add(new OleDbParameter("@player_id", con.Player.Id));
            command.Parameters.Add(new OleDbParameter("@id", con.Id));

            Console.WriteLine("PlayerCardConnection between player [" + con.Player.Id + "] and card [" + con.Card.Id + "] UPDATED");
        }
    }
}
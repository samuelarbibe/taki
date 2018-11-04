﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Form.TakiService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="CardList", Namespace="http://schemas.datacontract.org/2004/07/Model", ItemName="Card")]
    [System.SerializableAttribute()]
    public class CardList : System.Collections.Generic.List<Form.TakiService.Card> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Card : Form.TakiService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ColorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SpecialField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Color {
            get {
                return this.ColorField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorField, value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Special {
            get {
                return this.SpecialField;
            }
            set {
                if ((this.SpecialField.Equals(value) != true)) {
                    this.SpecialField = value;
                    this.RaisePropertyChanged("Special");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.User))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Player))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Game))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Message))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Card))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Player))]
    public partial class User : Form.TakiService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AdminField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LossesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WinsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Admin {
            get {
                return this.AdminField;
            }
            set {
                if ((this.AdminField.Equals(value) != true)) {
                    this.AdminField = value;
                    this.RaisePropertyChanged("Admin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Level {
            get {
                return this.LevelField;
            }
            set {
                if ((this.LevelField.Equals(value) != true)) {
                    this.LevelField = value;
                    this.RaisePropertyChanged("Level");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Losses {
            get {
                return this.LossesField;
            }
            set {
                if ((this.LossesField.Equals(value) != true)) {
                    this.LossesField = value;
                    this.RaisePropertyChanged("Losses");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Score {
            get {
                return this.ScoreField;
            }
            set {
                if ((this.ScoreField.Equals(value) != true)) {
                    this.ScoreField = value;
                    this.RaisePropertyChanged("Score");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Wins {
            get {
                return this.WinsField;
            }
            set {
                if ((this.WinsField.Equals(value) != true)) {
                    this.WinsField = value;
                    this.RaisePropertyChanged("Wins");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Player : Form.TakiService.User {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Form.TakiService.CardList HandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TempScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Form.TakiService.CardList Hand {
            get {
                return this.HandField;
            }
            set {
                if ((object.ReferenceEquals(this.HandField, value) != true)) {
                    this.HandField = value;
                    this.RaisePropertyChanged("Hand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TempScore {
            get {
                return this.TempScoreField;
            }
            set {
                if ((this.TempScoreField.Equals(value) != true)) {
                    this.TempScoreField = value;
                    this.RaisePropertyChanged("TempScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Game", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Game : Form.TakiService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime EndTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Form.TakiService.PlayerList PlayersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WinnerField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime EndTime {
            get {
                return this.EndTimeField;
            }
            set {
                if ((this.EndTimeField.Equals(value) != true)) {
                    this.EndTimeField = value;
                    this.RaisePropertyChanged("EndTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Form.TakiService.PlayerList Players {
            get {
                return this.PlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayersField, value) != true)) {
                    this.PlayersField = value;
                    this.RaisePropertyChanged("Players");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Winner {
            get {
                return this.WinnerField;
            }
            set {
                if ((this.WinnerField.Equals(value) != true)) {
                    this.WinnerField = value;
                    this.RaisePropertyChanged("Winner");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Message : Form.TakiService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ActionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Form.TakiService.Card CardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GameIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RecieverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TargetField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Action {
            get {
                return this.ActionField;
            }
            set {
                if ((object.ReferenceEquals(this.ActionField, value) != true)) {
                    this.ActionField = value;
                    this.RaisePropertyChanged("Action");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Form.TakiService.Card Card {
            get {
                return this.CardField;
            }
            set {
                if ((object.ReferenceEquals(this.CardField, value) != true)) {
                    this.CardField = value;
                    this.RaisePropertyChanged("Card");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GameId {
            get {
                return this.GameIdField;
            }
            set {
                if ((this.GameIdField.Equals(value) != true)) {
                    this.GameIdField = value;
                    this.RaisePropertyChanged("GameId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Reciever {
            get {
                return this.RecieverField;
            }
            set {
                if ((this.RecieverField.Equals(value) != true)) {
                    this.RecieverField = value;
                    this.RaisePropertyChanged("Reciever");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Target {
            get {
                return this.TargetField;
            }
            set {
                if ((this.TargetField.Equals(value) != true)) {
                    this.TargetField = value;
                    this.RaisePropertyChanged("Target");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="PlayerList", Namespace="http://schemas.datacontract.org/2004/07/Model", ItemName="Player")]
    [System.SerializableAttribute()]
    public class PlayerList : System.Collections.Generic.List<Form.TakiService.Player> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="MessageList", Namespace="http://schemas.datacontract.org/2004/07/Model", ItemName="Message")]
    [System.SerializableAttribute()]
    public class MessageList : System.Collections.Generic.List<Form.TakiService.Message> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="UserList", Namespace="http://schemas.datacontract.org/2004/07/Model", ItemName="User")]
    [System.SerializableAttribute()]
    public class UserList : System.Collections.Generic.List<Form.TakiService.User> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TakiService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetCardList", ReplyAction="http://tempuri.org/IService/GetCardListResponse")]
        Form.TakiService.CardList GetCardList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetCardList", ReplyAction="http://tempuri.org/IService/GetCardListResponse")]
        System.Threading.Tasks.Task<Form.TakiService.CardList> GetCardListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/BuildDeck", ReplyAction="http://tempuri.org/IService/BuildDeckResponse")]
        Form.TakiService.Card BuildDeck();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/BuildDeck", ReplyAction="http://tempuri.org/IService/BuildDeckResponse")]
        System.Threading.Tasks.Task<Form.TakiService.Card> BuildDeckAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StartGame", ReplyAction="http://tempuri.org/IService/StartGameResponse")]
        Form.TakiService.Game StartGame(Form.TakiService.Player p, int playerCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StartGame", ReplyAction="http://tempuri.org/IService/StartGameResponse")]
        System.Threading.Tasks.Task<Form.TakiService.Game> StartGameAsync(Form.TakiService.Player p, int playerCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPlayerList", ReplyAction="http://tempuri.org/IService/GetPlayerListResponse")]
        Form.TakiService.PlayerList GetPlayerList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPlayerList", ReplyAction="http://tempuri.org/IService/GetPlayerListResponse")]
        System.Threading.Tasks.Task<Form.TakiService.PlayerList> GetPlayerListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddAction", ReplyAction="http://tempuri.org/IService/AddActionResponse")]
        void AddAction(Form.TakiService.Message m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddAction", ReplyAction="http://tempuri.org/IService/AddActionResponse")]
        System.Threading.Tasks.Task AddActionAsync(Form.TakiService.Message m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddActions", ReplyAction="http://tempuri.org/IService/AddActionsResponse")]
        void AddActions(Form.TakiService.MessageList ml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddActions", ReplyAction="http://tempuri.org/IService/AddActionsResponse")]
        System.Threading.Tasks.Task AddActionsAsync(Form.TakiService.MessageList ml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/DoAction", ReplyAction="http://tempuri.org/IService/DoActionResponse")]
        Form.TakiService.MessageList DoAction(int gameId, int playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/DoAction", ReplyAction="http://tempuri.org/IService/DoActionResponse")]
        System.Threading.Tasks.Task<Form.TakiService.MessageList> DoActionAsync(int gameId, int playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Login", ReplyAction="http://tempuri.org/IService/LoginResponse")]
        Form.TakiService.User Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Login", ReplyAction="http://tempuri.org/IService/LoginResponse")]
        System.Threading.Tasks.Task<Form.TakiService.User> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Register", ReplyAction="http://tempuri.org/IService/RegisterResponse")]
        bool Register(string firstName, string lastName, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Register", ReplyAction="http://tempuri.org/IService/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(string firstName, string lastName, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PasswordAvailable", ReplyAction="http://tempuri.org/IService/PasswordAvailableResponse")]
        bool PasswordAvailable(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PasswordAvailable", ReplyAction="http://tempuri.org/IService/PasswordAvailableResponse")]
        System.Threading.Tasks.Task<bool> PasswordAvailableAsync(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/UsernameAvailable", ReplyAction="http://tempuri.org/IService/UsernameAvailableResponse")]
        bool UsernameAvailable(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/UsernameAvailable", ReplyAction="http://tempuri.org/IService/UsernameAvailableResponse")]
        System.Threading.Tasks.Task<bool> UsernameAvailableAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StopSearchingForGame", ReplyAction="http://tempuri.org/IService/StopSearchingForGameResponse")]
        bool StopSearchingForGame(Form.TakiService.Player p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StopSearchingForGame", ReplyAction="http://tempuri.org/IService/StopSearchingForGameResponse")]
        System.Threading.Tasks.Task<bool> StopSearchingForGameAsync(Form.TakiService.Player p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PlayerQuit", ReplyAction="http://tempuri.org/IService/PlayerQuitResponse")]
        bool PlayerQuit(Form.TakiService.Player p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PlayerQuit", ReplyAction="http://tempuri.org/IService/PlayerQuitResponse")]
        System.Threading.Tasks.Task<bool> PlayerQuitAsync(Form.TakiService.Player p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetAllUsers", ReplyAction="http://tempuri.org/IService/GetAllUsersResponse")]
        Form.TakiService.UserList GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetAllUsers", ReplyAction="http://tempuri.org/IService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<Form.TakiService.UserList> GetAllUsersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : Form.TakiService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<Form.TakiService.IService>, Form.TakiService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Form.TakiService.CardList GetCardList() {
            return base.Channel.GetCardList();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.CardList> GetCardListAsync() {
            return base.Channel.GetCardListAsync();
        }
        
        public Form.TakiService.Card BuildDeck() {
            return base.Channel.BuildDeck();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.Card> BuildDeckAsync() {
            return base.Channel.BuildDeckAsync();
        }
        
        public Form.TakiService.Game StartGame(Form.TakiService.Player p, int playerCount) {
            return base.Channel.StartGame(p, playerCount);
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.Game> StartGameAsync(Form.TakiService.Player p, int playerCount) {
            return base.Channel.StartGameAsync(p, playerCount);
        }
        
        public Form.TakiService.PlayerList GetPlayerList() {
            return base.Channel.GetPlayerList();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.PlayerList> GetPlayerListAsync() {
            return base.Channel.GetPlayerListAsync();
        }
        
        public void AddAction(Form.TakiService.Message m) {
            base.Channel.AddAction(m);
        }
        
        public System.Threading.Tasks.Task AddActionAsync(Form.TakiService.Message m) {
            return base.Channel.AddActionAsync(m);
        }
        
        public void AddActions(Form.TakiService.MessageList ml) {
            base.Channel.AddActions(ml);
        }
        
        public System.Threading.Tasks.Task AddActionsAsync(Form.TakiService.MessageList ml) {
            return base.Channel.AddActionsAsync(ml);
        }
        
        public Form.TakiService.MessageList DoAction(int gameId, int playerId) {
            return base.Channel.DoAction(gameId, playerId);
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.MessageList> DoActionAsync(int gameId, int playerId) {
            return base.Channel.DoActionAsync(gameId, playerId);
        }
        
        public Form.TakiService.User Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.User> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public bool Register(string firstName, string lastName, string username, string password) {
            return base.Channel.Register(firstName, lastName, username, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(string firstName, string lastName, string username, string password) {
            return base.Channel.RegisterAsync(firstName, lastName, username, password);
        }
        
        public bool PasswordAvailable(string password) {
            return base.Channel.PasswordAvailable(password);
        }
        
        public System.Threading.Tasks.Task<bool> PasswordAvailableAsync(string password) {
            return base.Channel.PasswordAvailableAsync(password);
        }
        
        public bool UsernameAvailable(string username) {
            return base.Channel.UsernameAvailable(username);
        }
        
        public System.Threading.Tasks.Task<bool> UsernameAvailableAsync(string username) {
            return base.Channel.UsernameAvailableAsync(username);
        }
        
        public bool StopSearchingForGame(Form.TakiService.Player p) {
            return base.Channel.StopSearchingForGame(p);
        }
        
        public System.Threading.Tasks.Task<bool> StopSearchingForGameAsync(Form.TakiService.Player p) {
            return base.Channel.StopSearchingForGameAsync(p);
        }
        
        public bool PlayerQuit(Form.TakiService.Player p) {
            return base.Channel.PlayerQuit(p);
        }
        
        public System.Threading.Tasks.Task<bool> PlayerQuitAsync(Form.TakiService.Player p) {
            return base.Channel.PlayerQuitAsync(p);
        }
        
        public Form.TakiService.UserList GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.UserList> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
    }
}

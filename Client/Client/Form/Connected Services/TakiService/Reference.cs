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
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Game))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.User))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Player))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Message))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Card))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Game", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Game : Form.TakiService.BaseEntity {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Form.TakiService.Player))]
    public partial class User : Form.TakiService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
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
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Player : Form.TakiService.User {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Message : Form.TakiService.BaseEntity {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Card : Form.TakiService.BaseEntity {
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TakiService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/BuildDeck", ReplyAction="http://tempuri.org/IService/BuildDeckResponse")]
        Form.TakiService.Card BuildDeck();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/BuildDeck", ReplyAction="http://tempuri.org/IService/BuildDeckResponse")]
        System.Threading.Tasks.Task<Form.TakiService.Card> BuildDeckAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StartGame", ReplyAction="http://tempuri.org/IService/StartGameResponse")]
        Form.TakiService.Game StartGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StartGame", ReplyAction="http://tempuri.org/IService/StartGameResponse")]
        System.Threading.Tasks.Task<Form.TakiService.Game> StartGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPlayerList", ReplyAction="http://tempuri.org/IService/GetPlayerListResponse")]
        Form.TakiService.PlayerList GetPlayerList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetPlayerList", ReplyAction="http://tempuri.org/IService/GetPlayerListResponse")]
        System.Threading.Tasks.Task<Form.TakiService.PlayerList> GetPlayerListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Action", ReplyAction="http://tempuri.org/IService/ActionResponse")]
        Form.TakiService.MessageList Action(Form.TakiService.Message m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Action", ReplyAction="http://tempuri.org/IService/ActionResponse")]
        System.Threading.Tasks.Task<Form.TakiService.MessageList> ActionAsync(Form.TakiService.Message m);
        
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
        
        public Form.TakiService.Card BuildDeck() {
            return base.Channel.BuildDeck();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.Card> BuildDeckAsync() {
            return base.Channel.BuildDeckAsync();
        }
        
        public Form.TakiService.Game StartGame() {
            return base.Channel.StartGame();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.Game> StartGameAsync() {
            return base.Channel.StartGameAsync();
        }
        
        public Form.TakiService.PlayerList GetPlayerList() {
            return base.Channel.GetPlayerList();
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.PlayerList> GetPlayerListAsync() {
            return base.Channel.GetPlayerListAsync();
        }
        
        public Form.TakiService.MessageList Action(Form.TakiService.Message m) {
            return base.Channel.Action(m);
        }
        
        public System.Threading.Tasks.Task<Form.TakiService.MessageList> ActionAsync(Form.TakiService.Message m) {
            return base.Channel.ActionAsync(m);
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
    }
}

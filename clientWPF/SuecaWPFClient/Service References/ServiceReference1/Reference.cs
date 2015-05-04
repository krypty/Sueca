﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace suecaWPFClient.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Room", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [System.SerializableAttribute()]
    public partial class Room : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Room.StateRoom RoomStateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Player[] listPlayersField;
        
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
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Room.StateRoom RoomState {
            get {
                return this.RoomStateField;
            }
            set {
                if ((this.RoomStateField.Equals(value) != true)) {
                    this.RoomStateField = value;
                    this.RaisePropertyChanged("RoomState");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Player[] listPlayers {
            get {
                return this.listPlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.listPlayersField, value) != true)) {
                    this.listPlayersField = value;
                    this.RaisePropertyChanged("listPlayers");
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
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="Room.StateRoom", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
        public enum StateRoom : int {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            WAITING_READY = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            GAME_IN_PROGRESS = 1,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            END_GAME = 2,
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [System.SerializableAttribute()]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HoldingCardsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberTurnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TakenCardsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool isReadyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tokenField;
        
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
        public int HoldingCards {
            get {
                return this.HoldingCardsField;
            }
            set {
                if ((this.HoldingCardsField.Equals(value) != true)) {
                    this.HoldingCardsField = value;
                    this.RaisePropertyChanged("HoldingCards");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberTurn {
            get {
                return this.NumberTurnField;
            }
            set {
                if ((this.NumberTurnField.Equals(value) != true)) {
                    this.NumberTurnField = value;
                    this.RaisePropertyChanged("NumberTurn");
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
        public int TakenCards {
            get {
                return this.TakenCardsField;
            }
            set {
                if ((this.TakenCardsField.Equals(value) != true)) {
                    this.TakenCardsField = value;
                    this.RaisePropertyChanged("TakenCards");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool isReady {
            get {
                return this.isReadyField;
            }
            set {
                if ((this.isReadyField.Equals(value) != true)) {
                    this.isReadyField = value;
                    this.RaisePropertyChanged("isReady");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string token {
            get {
                return this.tokenField;
            }
            set {
                if ((object.ReferenceEquals(this.tokenField, value) != true)) {
                    this.tokenField = value;
                    this.RaisePropertyChanged("token");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CardColor", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    public enum CardColor : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Spades = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Diamonds = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Clubs = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hearts = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CardValue", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    public enum CardValue : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Two = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Three = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Four = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Five = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Six = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Seven = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Jack = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Queen = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        King = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ace = 10,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameInfo", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [System.SerializableAttribute()]
    public partial class GameInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Card FirstCardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FirstPlayerNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsMyTurnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Card[] ListCardsPlayedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Card[] ListCardsPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Player[] ListPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.Player PlayerField;
        
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
        public suecaWPFClient.ServiceReference1.Card FirstCard {
            get {
                return this.FirstCardField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstCardField, value) != true)) {
                    this.FirstCardField = value;
                    this.RaisePropertyChanged("FirstCard");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FirstPlayerNumber {
            get {
                return this.FirstPlayerNumberField;
            }
            set {
                if ((this.FirstPlayerNumberField.Equals(value) != true)) {
                    this.FirstPlayerNumberField = value;
                    this.RaisePropertyChanged("FirstPlayerNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsMyTurn {
            get {
                return this.IsMyTurnField;
            }
            set {
                if ((this.IsMyTurnField.Equals(value) != true)) {
                    this.IsMyTurnField = value;
                    this.RaisePropertyChanged("IsMyTurn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Card[] ListCardsPlayed {
            get {
                return this.ListCardsPlayedField;
            }
            set {
                if ((object.ReferenceEquals(this.ListCardsPlayedField, value) != true)) {
                    this.ListCardsPlayedField = value;
                    this.RaisePropertyChanged("ListCardsPlayed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Card[] ListCardsPlayer {
            get {
                return this.ListCardsPlayerField;
            }
            set {
                if ((object.ReferenceEquals(this.ListCardsPlayerField, value) != true)) {
                    this.ListCardsPlayerField = value;
                    this.RaisePropertyChanged("ListCardsPlayer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Player[] ListPlayer {
            get {
                return this.ListPlayerField;
            }
            set {
                if ((object.ReferenceEquals(this.ListPlayerField, value) != true)) {
                    this.ListPlayerField = value;
                    this.RaisePropertyChanged("ListPlayer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.Player Player {
            get {
                return this.PlayerField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayerField, value) != true)) {
                    this.PlayerField = value;
                    this.RaisePropertyChanged("Player");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [System.SerializableAttribute()]
    public partial class Card : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.CardColor ColorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private suecaWPFClient.ServiceReference1.CardValue ValueField;
        
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
        public suecaWPFClient.ServiceReference1.CardColor Color {
            get {
                return this.ColorField;
            }
            set {
                if ((this.ColorField.Equals(value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public suecaWPFClient.ServiceReference1.CardValue Value {
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
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.Sueca", CallbackContract=typeof(suecaWPFClient.ServiceReference1.SuecaCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface Sueca {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/CreateRoom", ReplyAction="http://tempuri.org/Sueca/CreateRoomResponse")]
        string CreateRoom(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/CreateRoom", ReplyAction="http://tempuri.org/Sueca/CreateRoomResponse")]
        System.Threading.Tasks.Task<string> CreateRoomAsync(string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/JoinRoom", ReplyAction="http://tempuri.org/Sueca/JoinRoomResponse")]
        string JoinRoom(string roomName, string password, bool isUsingCallback);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/JoinRoom", ReplyAction="http://tempuri.org/Sueca/JoinRoomResponse")]
        System.Threading.Tasks.Task<string> JoinRoomAsync(string roomName, string password, bool isUsingCallback);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/SendReady", ReplyAction="http://tempuri.org/Sueca/SendReadyResponse")]
        void SendReady(string playerToken, bool isReady);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/SendReady", ReplyAction="http://tempuri.org/Sueca/SendReadyResponse")]
        System.Threading.Tasks.Task SendReadyAsync(string playerToken, bool isReady);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/ListRoom", ReplyAction="http://tempuri.org/Sueca/ListRoomResponse")]
        suecaWPFClient.ServiceReference1.Room[] ListRoom();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/ListRoom", ReplyAction="http://tempuri.org/Sueca/ListRoomResponse")]
        System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.Room[]> ListRoomAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/PlayCard", ReplyAction="http://tempuri.org/Sueca/PlayCardResponse")]
        bool PlayCard(string playerToken, suecaWPFClient.ServiceReference1.CardColor color, suecaWPFClient.ServiceReference1.CardValue value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/PlayCard", ReplyAction="http://tempuri.org/Sueca/PlayCardResponse")]
        System.Threading.Tasks.Task<bool> PlayCardAsync(string playerToken, suecaWPFClient.ServiceReference1.CardColor color, suecaWPFClient.ServiceReference1.CardValue value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/GetRoom", ReplyAction="http://tempuri.org/Sueca/GetRoomResponse")]
        suecaWPFClient.ServiceReference1.Room GetRoom(string playerToken, string roomName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/GetRoom", ReplyAction="http://tempuri.org/Sueca/GetRoomResponse")]
        System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.Room> GetRoomAsync(string playerToken, string roomName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/GetGameInfo", ReplyAction="http://tempuri.org/Sueca/GetGameInfoResponse")]
        suecaWPFClient.ServiceReference1.GameInfo GetGameInfo(string playerToken, string roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Sueca/GetGameInfo", ReplyAction="http://tempuri.org/Sueca/GetGameInfoResponse")]
        System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.GameInfo> GetGameInfoAsync(string playerToken, string roomId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SuecaCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/Sueca/GameStarted")]
        void GameStarted(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/Sueca/RoomUpdated")]
        void RoomUpdated(suecaWPFClient.ServiceReference1.Room room);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/Sueca/GameInfoUpdated")]
        void GameInfoUpdated(suecaWPFClient.ServiceReference1.GameInfo gameInfo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SuecaChannel : suecaWPFClient.ServiceReference1.Sueca, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SuecaClient : System.ServiceModel.DuplexClientBase<suecaWPFClient.ServiceReference1.Sueca>, suecaWPFClient.ServiceReference1.Sueca {
        
        public SuecaClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public SuecaClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public SuecaClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SuecaClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SuecaClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string CreateRoom(string password) {
            return base.Channel.CreateRoom(password);
        }
        
        public System.Threading.Tasks.Task<string> CreateRoomAsync(string password) {
            return base.Channel.CreateRoomAsync(password);
        }
        
        public string JoinRoom(string roomName, string password, bool isUsingCallback) {
            return base.Channel.JoinRoom(roomName, password, isUsingCallback);
        }
        
        public System.Threading.Tasks.Task<string> JoinRoomAsync(string roomName, string password, bool isUsingCallback) {
            return base.Channel.JoinRoomAsync(roomName, password, isUsingCallback);
        }
        
        public void SendReady(string playerToken, bool isReady) {
            base.Channel.SendReady(playerToken, isReady);
        }
        
        public System.Threading.Tasks.Task SendReadyAsync(string playerToken, bool isReady) {
            return base.Channel.SendReadyAsync(playerToken, isReady);
        }
        
        public suecaWPFClient.ServiceReference1.Room[] ListRoom() {
            return base.Channel.ListRoom();
        }
        
        public System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.Room[]> ListRoomAsync() {
            return base.Channel.ListRoomAsync();
        }
        
        public bool PlayCard(string playerToken, suecaWPFClient.ServiceReference1.CardColor color, suecaWPFClient.ServiceReference1.CardValue value) {
            return base.Channel.PlayCard(playerToken, color, value);
        }
        
        public System.Threading.Tasks.Task<bool> PlayCardAsync(string playerToken, suecaWPFClient.ServiceReference1.CardColor color, suecaWPFClient.ServiceReference1.CardValue value) {
            return base.Channel.PlayCardAsync(playerToken, color, value);
        }
        
        public suecaWPFClient.ServiceReference1.Room GetRoom(string playerToken, string roomName) {
            return base.Channel.GetRoom(playerToken, roomName);
        }
        
        public System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.Room> GetRoomAsync(string playerToken, string roomName) {
            return base.Channel.GetRoomAsync(playerToken, roomName);
        }
        
        public suecaWPFClient.ServiceReference1.GameInfo GetGameInfo(string playerToken, string roomId) {
            return base.Channel.GetGameInfo(playerToken, roomId);
        }
        
        public System.Threading.Tasks.Task<suecaWPFClient.ServiceReference1.GameInfo> GetGameInfoAsync(string playerToken, string roomId) {
            return base.Channel.GetGameInfoAsync(playerToken, roomId);
        }
    }
}

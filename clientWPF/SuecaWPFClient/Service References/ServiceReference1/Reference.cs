﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace suecaWPFClient.ServiceReference1 {
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="Room", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [Serializable()]
    public partial class Room : object, IExtensibleDataObject, INotifyPropertyChanged {
        
        [NonSerialized()]
        private ExtensionDataObject extensionDataField;
        
        [OptionalField()]
        private string NameField;
        
        [OptionalField()]
        private string PasswordField;
        
        [OptionalField()]
        private Player[] listPlayersField;
        
        [Browsable(false)]
        public ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [DataMember()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [DataMember()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [DataMember()]
        public Player[] listPlayers {
            get {
                return this.listPlayersField;
            }
            set {
                if ((ReferenceEquals(this.listPlayersField, value) != true)) {
                    this.listPlayersField = value;
                    this.RaisePropertyChanged("listPlayers");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
    [DataContract(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/SuecaContracts")]
    [Serializable()]
    public partial class Player : object, IExtensibleDataObject, INotifyPropertyChanged {
        
        [NonSerialized()]
        private ExtensionDataObject extensionDataField;
        
        [OptionalField()]
        private int holdingCardsField;
        
        [OptionalField()]
        private bool isReadyField;
        
        [OptionalField()]
        private int takenCardsField;
        
        [OptionalField()]
        private string tokenField;
        
        [Browsable(false)]
        public ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [DataMember()]
        public int holdingCards {
            get {
                return this.holdingCardsField;
            }
            set {
                if ((this.holdingCardsField.Equals(value) != true)) {
                    this.holdingCardsField = value;
                    this.RaisePropertyChanged("holdingCards");
                }
            }
        }
        
        [DataMember()]
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
        
        [DataMember()]
        public int takenCards {
            get {
                return this.takenCardsField;
            }
            set {
                if ((this.takenCardsField.Equals(value) != true)) {
                    this.takenCardsField = value;
                    this.RaisePropertyChanged("takenCards");
                }
            }
        }
        
        [DataMember()]
        public string token {
            get {
                return this.tokenField;
            }
            set {
                if ((ReferenceEquals(this.tokenField, value) != true)) {
                    this.tokenField = value;
                    this.RaisePropertyChanged("token");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName="ServiceReference1.Sueca", CallbackContract=typeof(SuecaCallback), SessionMode=SessionMode.Required)]
    public interface Sueca {
        
        [OperationContract(Action="http://tempuri.org/Sueca/CreateRoom", ReplyAction="http://tempuri.org/Sueca/CreateRoomResponse")]
        string CreateRoom(string password);
        
        [OperationContract(Action="http://tempuri.org/Sueca/CreateRoom", ReplyAction="http://tempuri.org/Sueca/CreateRoomResponse")]
        Task<string> CreateRoomAsync(string password);
        
        [OperationContract(Action="http://tempuri.org/Sueca/JoinRoom", ReplyAction="http://tempuri.org/Sueca/JoinRoomResponse")]
        string JoinRoom(string roomName, string password);
        
        [OperationContract(Action="http://tempuri.org/Sueca/JoinRoom", ReplyAction="http://tempuri.org/Sueca/JoinRoomResponse")]
        Task<string> JoinRoomAsync(string roomName, string password);
        
        [OperationContract(Action="http://tempuri.org/Sueca/SendReady", ReplyAction="http://tempuri.org/Sueca/SendReadyResponse")]
        void SendReady(string playerToken, bool isReady);
        
        [OperationContract(Action="http://tempuri.org/Sueca/SendReady", ReplyAction="http://tempuri.org/Sueca/SendReadyResponse")]
        Task SendReadyAsync(string playerToken, bool isReady);
        
        [OperationContract(Action="http://tempuri.org/Sueca/ListRoom", ReplyAction="http://tempuri.org/Sueca/ListRoomResponse")]
        Room[] ListRoom();
        
        [OperationContract(Action="http://tempuri.org/Sueca/ListRoom", ReplyAction="http://tempuri.org/Sueca/ListRoomResponse")]
        Task<Room[]> ListRoomAsync();
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface SuecaCallback {
        
        [OperationContract(IsOneWay=true, Action="http://tempuri.org/Sueca/GameStarted")]
        void GameStarted(string message);
    }
    
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public interface SuecaChannel : Sueca, IClientChannel {
    }
    
    [DebuggerStepThrough()]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public partial class SuecaClient : DuplexClientBase<Sueca>, Sueca {
        
        public SuecaClient(InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public SuecaClient(InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public SuecaClient(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SuecaClient(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public SuecaClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string CreateRoom(string password) {
            return base.Channel.CreateRoom(password);
        }
        
        public Task<string> CreateRoomAsync(string password) {
            return base.Channel.CreateRoomAsync(password);
        }
        
        public string JoinRoom(string roomName, string password) {
            return base.Channel.JoinRoom(roomName, password);
        }
        
        public Task<string> JoinRoomAsync(string roomName, string password) {
            return base.Channel.JoinRoomAsync(roomName, password);
        }
        
        public void SendReady(string playerToken, bool isReady) {
            base.Channel.SendReady(playerToken, isReady);
        }
        
        public Task SendReadyAsync(string playerToken, bool isReady) {
            return base.Channel.SendReadyAsync(playerToken, isReady);
        }
        
        public Room[] ListRoom() {
            return base.Channel.ListRoom();
        }
        
        public Task<Room[]> ListRoomAsync() {
            return base.Channel.ListRoomAsync();
        }
    }
}

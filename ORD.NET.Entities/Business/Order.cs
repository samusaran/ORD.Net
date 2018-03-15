using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ORD.NET.Entities.Business
{
    [Serializable]
    public class Order : INotifyPropertyChanged
    {
        private DateTime _data = DateTime.Today;
        public DateTime Data
        {
            get { return _data; }
            set { SetField(ref _data, value); }
        }

        [Browsable(false)]
        private TimeSpan _oraordinazione = DateTime.Now.TimeOfDay;
        public TimeSpan OraOrdinazione
        {
            get { return _oraordinazione; }
            set { SetField(ref _oraordinazione, value); }
        }


        private User _utente;
        public User Utente
        {
            get { return _utente; }
            set { SetField(ref _utente, value); }
        }


        private Zeppelin _zeppelin;
        public Zeppelin Zeppelin
        {
            get { return _zeppelin; }
            set { SetField(ref _zeppelin, value); }
        }


        private DishType _tipopiatto = DishType.Altro;
        public DishType TipoPiatto
        {
            get { return _tipopiatto; }
            set { SetField(ref _tipopiatto, value); }
        }


        private string _piatto;
        public string Piatto
        {
            get { return _piatto; }
            set { SetField(ref _piatto, value); }
        }

        private bool _shottini;
        public bool Shottini
        {
            get { return _shottini; }
            set { SetField(ref _shottini, value); }
        }

        public int IdOrdinazione
        {
            get; private set;
        }

        [JsonIgnore]
        /// <summary>
        /// Serve solo e unicamente per mappare la foto profilo dell'utente sulla griglia.
        /// </summary>
        public byte[] ImageData => Utente?.Image;

        public Order() { }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //C# 6 null-safe operator. No need to check for event listeners
            //If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // C# 5 - CallMemberName means we don't need to pass the property's name
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}

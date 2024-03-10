using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Todo.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        private string _title;
        private bool _isDone;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                if (_isDone != value)
                {
                    _isDone = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

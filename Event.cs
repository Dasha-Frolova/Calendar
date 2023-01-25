using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar {
    [Serializable]
    public class Event {
        public enum Type {
            Napominanie,
            Prazdnik,
        }
        public DateTime Date;
        public string Name;
        public Type EventType;



        public Event(DateTime dateTime, string name, Type type) {
            Date = dateTime;
            Name = name;
            EventType = type;
        }



        public override string ToString() {
            switch (EventType) {
                case Type.Napominanie:
                return Date.ToString() + " " + Name;
                case Type.Prazdnik:
                return Date.ToShortDateString() + " " + Name;
                default:
                throw new Exception();
            }
        }

    }
}

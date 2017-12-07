using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfEnumBindingExample {
    public enum ShirtType {
        [Display(Name ="Dress Shirt")]
        DressShirt,
        [Display(Name ="T-Shirt")]
        TShirt,
        CampShirt,
        Polo,
        Jersey,
    }

    public class Shirt : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private ShirtType stype = ShirtType.DressShirt;
        public ShirtType SType {
            get { return this.stype; }
            set { SetProperty(ref this.stype, value); }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            if(Object.Equals(storage, value)) { return false; }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

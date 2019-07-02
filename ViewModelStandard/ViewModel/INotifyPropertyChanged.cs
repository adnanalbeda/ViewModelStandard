namespace ViewModelStandard
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

    public abstract partial class ViewModelBase : Interfaces.ViewModel.IViewModelBase
    {
        
        #region Implementing Interfaces

        /*-*-*-*-*-*-*-*/

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /*-*-*-*-*-*-*-*/
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke
                (this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        protected virtual void changeProp<T>(ref T foo, T bar, [CallerMemberName] string propertyName = "")
        {
            foo = bar;
            OnPropertyChanged(propertyName);
        }

        /*-*-*-*-*-*-*-*/
        /// <summary>
        /// To Change property and raising property changed events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns>Returns bool: property has changed its value or not. </returns>
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            // not necessarily needed but to reduce code for extended.
            changeProp(ref storage, value, propertyName);
            return true;
        }

        /*-*-*-*-*-*-*-*/
    }
}
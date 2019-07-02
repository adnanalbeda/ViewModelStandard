namespace ViewModelStandard
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;

    /// <summary>
    /// Implements INotifyPropertyChanging
    /// </summary>
    public abstract partial class ViewModelBasing : ViewModelBase, Interfaces.ViewModel.IViewModelBasing, INotifyPropertyChanging
    {
        
        #region Implementing Interfaces

        /*-*-*-*-*-*-*-*/

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        /*-*-*-*-*-*-*-*/
        // Source: MSDN
        public void OnPropertyChanging([CallerMemberName] String propertyName = "")
        {
            PropertyChanging?.Invoke
                (this, new PropertyChangingEventArgs(propertyName));
        }
        #endregion


        protected sealed override void changeProp<T>(ref T foo, T bar, [CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
            base.changeProp(ref foo, bar, propertyName);
        }

    }
}
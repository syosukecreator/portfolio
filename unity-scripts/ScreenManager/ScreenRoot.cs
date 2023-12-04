using UnityEngine;

namespace HkUniGameBase
{
    public class ScreenRoot : MonoBehaviour
    {
        #region Member Datas

        public static readonly string ScreenRootTag = "ScreenRoot";
        protected IScreenData _screenData;

        #endregion

        #region Public Functions

        public void StartScreen(IScreenData screenData)
        {
            _screenData = screenData;
            OnStartScreen();
        }

        public void EndScreen()
        {
            OnEndScreen();
        }

        #endregion

        #region Non Public Functions

        protected virtual void OnStartScreen()
        {
            Debug.Log($"OnStartScreen(): {_screenData.ScreenName}");
        }

        protected virtual void OnEndScreen()
        {
            Debug.Log($"OnEndScreen(): {_screenData.ScreenName}");
        }

        #endregion
    }
}

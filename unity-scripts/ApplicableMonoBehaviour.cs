using UnityEngine;

namespace HkUniBase
{
    /// <summary>
    /// Apply可能なクラス
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class ApplicableMonoBehaviour<T> : MonoBehaviour
    {
        /// <summary>
        /// true: Setupされている
        /// false: Setupされていない
        /// </summary>
        bool isSetup;

        /// <summary>
        /// 最初の1回だけ実行される
        /// </summary>
        /// <param name="parameter">Setupする値</param>
        public void Setup(T parameter)
        {
            if (isSetup)
            {
                return;
            }

            isSetup = true;
            OnSetup(parameter);
        }

        /// <summary>
        /// 値を適用する
        /// </summary>
        /// <param name="parameter">Applyする値</param>
        public void Apply(T parameter)
        {
            if (!isSetup)
            {
                Setup(parameter);
            }

            OnApply(parameter);
        }

        /// <summary>
        /// 継承先のクラスでoverrideするとSetup時に呼び出される
        /// </summary>
        /// <param name="parameter">Setupする値</param>
        protected virtual void OnSetup(T parameter)
        {
        }

        /// <summary>
        /// 継承先のクラスでoverrideするとApply時に呼び出される
        /// </summary>
        /// <param name="parameter">Applyする値</param>
        protected virtual void OnApply(T parameter)
        {
        }
    }
}

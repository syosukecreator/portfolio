using UnityEngine;

namespace HkUniBase
{
    /// <summary>
    /// Apply�\�ȃN���X
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class ApplicableMonoBehaviour<T> : MonoBehaviour
    {
        /// <summary>
        /// true: Setup����Ă���
        /// false: Setup����Ă��Ȃ�
        /// </summary>
        bool isSetup;

        /// <summary>
        /// �ŏ���1�񂾂����s�����
        /// </summary>
        /// <param name="parameter">Setup����l</param>
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
        /// �l��K�p����
        /// </summary>
        /// <param name="parameter">Apply����l</param>
        public void Apply(T parameter)
        {
            if (!isSetup)
            {
                Setup(parameter);
            }

            OnApply(parameter);
        }

        /// <summary>
        /// �p����̃N���X��override�����Setup���ɌĂяo�����
        /// </summary>
        /// <param name="parameter">Setup����l</param>
        protected virtual void OnSetup(T parameter)
        {
        }

        /// <summary>
        /// �p����̃N���X��override�����Apply���ɌĂяo�����
        /// </summary>
        /// <param name="parameter">Apply����l</param>
        protected virtual void OnApply(T parameter)
        {
        }
    }
}

using System;
using UniRx;

namespace HkUniBase
{
    /// <summary>
    /// �͈͂�\���N���X
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class Range<T> where T : IComparable<T>
    {
        #region Member Datas

        public ReactiveProperty<T> MinReactProp
        {
            get;
            private set;
        } = new();

        public ReactiveProperty<T> MaxReactProp
        {
            get;
            private set;
        } = new();

        public ReactiveProperty<T> CurrentReactProp
        {
            get;
            private set;
        } = new();

        /// <summary>�ŏ��l</summary>
        public T Min
        {
            get
            {
                return MinReactProp.Value;
            }

            set
            {
                var val = value;

                // val > Max
                if (_genericOperation.GreaterThan(val, Max))
                {
                    val = Max;
                }

                MinReactProp.Value = val;
            }
        }

        /// <summary>�ő�l</summary>
        public T Max
        {
            get
            {
                return MaxReactProp.Value;
            }

            set
            {
                var val = value;

                // val < Min
                if (_genericOperation.LessThan(val, Min))
                {
                    val = Min;
                }

                MaxReactProp.Value = val;
            }
        }

        /// <summary>���ݒl</summary>
        public T Current
        {
            get
            {
                return CurrentReactProp.Value;
            }

            set
            {
                var val = value;

                // val < Min
                if (_genericOperation.LessThan(val, Min))
                {
                    val = Min;
                }
                // val > Max
                else if (_genericOperation.GreaterThan(val, Max))
                {
                    val = Max;
                }

                CurrentReactProp.Value = val;
            }
        }

        public bool IsMin
        {
            get
            {
                return _genericOperation.Equal(Current, Min);
            }
        }

        public bool IsMax
        {
            get
            {
                return _genericOperation.Equal(Current, Max);
            }
        }

        GenericOperation<T> _genericOperation = new();

        #endregion

        #region Public Functions

        public Range(T min, T max, T current)
        {
            Min = min;
            Max = max;
            Current = current;
        }

        /// <summary>
        /// �f�B�[�v�R�s�[
        /// </summary>
        /// <param name="range">Range</param>
        public Range(Range<T> range)
        {
            Min = range.Min;
            Max = range.Max;
            Current = range.Current;
        }

        #endregion
    }
}

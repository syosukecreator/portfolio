using HkUniBase;
using System.Collections.Generic;
using UnityEngine;

namespace HkUniGameBase
{
    /// <summary>
    /// ��ʃ��[�_�[
    /// </summary>
    public class ScreenLoader : SingletonMonoBehaviour<ScreenLoader>
    {
        #region Member Datas

        SceneStructureIdol _sceneStructureIdol;
        List<IScreenData> _historyScreenDatas = new();

        #endregion

        #region Public Functions

        public void Initialize(SceneStructureIdol sceneStructureIdol)
        {
            _sceneStructureIdol = sceneStructureIdol;
        }

        public void ChangeScreen(IScreenData screenData)
        {
            _historyScreenDatas.Add(screenData);
            var previousScreenData = GetScreenData(1);
            var currentScreenData = GetScreenData(0);
            _sceneStructureIdol.ChangeScreen(previousScreenData, currentScreenData).Forget();
        }

        public IScreenData GetScreenData(int index)
        {
            var targetIndex = _historyScreenDatas.Count - (1 + index);

            if (targetIndex < 0 || _historyScreenDatas.Count <= targetIndex)
            {
                return null;
            }

            return _historyScreenDatas[targetIndex];
        }

        public void BackScreen()
        {
            var currentScreenData = GetScreenData(0);
            var previousScreenData = GetScreenData(1);

            if (previousScreenData == null)
            {
                Debug.LogError("�߂��ʂ�����܂���B�������X�L�b�v���܂��B");
                return;
            }

            _sceneStructureIdol.ChangeScreen(currentScreenData, previousScreenData).Forget();
            _historyScreenDatas.RemoveAt(_historyScreenDatas.Count - 1);
        }

        #endregion
    }
}

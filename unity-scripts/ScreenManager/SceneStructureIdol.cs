using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HkUniGameBase
{
    /// <summary>
    /// �V�[���\���̋���
    /// </summary>
    public class SceneStructureIdol : MonoBehaviour
    {
        #region Public Functions

        public async UniTaskVoid ChangeScreen(IScreenData fromScreenData, IScreenData toScreenData)
        {
            var screenRootGmObjs = GameObject.FindGameObjectsWithTag(ScreenRoot.ScreenRootTag);

            if (screenRootGmObjs.Length <= 0)
            {
                Debug.LogError($"{ScreenRoot.ScreenRootTag}�^�O�̂��Ă���GameObject������܂���B" +
                    $"Tag���m�F���Ă��������B");
                return;
            }

            if (fromScreenData != null)
            {
                var fromScreenTf = transform.Find($"{fromScreenData.GameStateName}/" +
                    $"{fromScreenData.ScreenName}");

                if (fromScreenTf == null || fromScreenTf.childCount <= 0)
                {
                    Debug.LogError($"Idol�ȉ��̍\�����Ԉ���Ă��܂��B�\�����m�F���Ă��������BPath: " +
                        $"{fromScreenData.GameStateName}/{fromScreenData.ScreenName}");
                    return;
                }

                foreach (Transform sceneTf in fromScreenTf)
                {
                    var screenRootGmObj = Array.Find(screenRootGmObjs, gmObj => gmObj.scene.name ==
                    sceneTf.name);
                    var screenRoot = screenRootGmObj.GetComponent<ScreenRoot>();

                    if (screenRoot == null)
                    {
                        Debug.LogError($"ScreenRoot��������܂���ł����BPath: " +
                            $"{fromScreenData.GameStateName}/{fromScreenData.ScreenName}/{sceneTf.name}");
                        continue;
                    }

                    screenRoot.EndScreen();
                }

                foreach (Transform sceneTf in fromScreenTf)
                {
                    SceneManager.UnloadSceneAsync(sceneTf.name);
                }
            }

            var toScreenTf = transform.Find($"{toScreenData.GameStateName}/" +
                $"{toScreenData.ScreenName}");

            if (toScreenTf == null || toScreenTf.childCount <= 0)
            {
                Debug.LogError($"Idol�ȉ��̍\�����Ԉ���Ă��܂��B�\�����m�F���Ă��������BPath: " +
                    $"{toScreenData.GameStateName}/{toScreenData.ScreenName}");
                return;
            }

            foreach (Transform sceneTf in toScreenTf)
            {
                SceneManager.LoadScene(sceneTf.name, LoadSceneMode.Additive);
            }

            await UniTask.DelayFrame(1);

            screenRootGmObjs = GameObject.FindGameObjectsWithTag(ScreenRoot.ScreenRootTag);

            foreach (Transform sceneTf in toScreenTf)
            {
                var screenRoot = Array.Find(screenRootGmObjs, gmObj => gmObj.scene.name ==
                sceneTf.name)?.GetComponent<ScreenRoot>();

                if (screenRoot == null)
                {
                    Debug.LogError($"ScreenRoot��������܂���ł����BPath: " +
                        $"{toScreenData.GameStateName}/{toScreenData.ScreenName}/{sceneTf.name}");
                    continue;
                }

                screenRoot.StartScreen(toScreenData);
            }
        }

        #endregion
    }
}

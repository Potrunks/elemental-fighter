using Assets.Script.Business.Interface;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    internal class CursorBusiness : ICursorBusiness
    {
        public void SetAsLastSiblingAllCursor(GameObject[] cursorArray)
        {
            foreach (GameObject cursor in cursorArray)
            {
                cursor.transform.SetAsLastSibling();
            }
        }
    }
}

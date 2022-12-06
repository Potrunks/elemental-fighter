using UnityEngine;

namespace Assets.Script.Business
{
    public class CursorBusiness : ICursorBusiness
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

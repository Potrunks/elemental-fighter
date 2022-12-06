using UnityEngine;

namespace Assets.Script.Business
{
    public class TokenBusiness : ITokenBusiness
    {
        public void FollowCursor(bool cursorHasToken, GameObject token, GameObject cursor)
        {
            if (cursorHasToken == true)
            {
                token.transform.position = cursor.transform.position + new Vector3(-.2f, .2f);
            }
        }
    }
}

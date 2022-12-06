using UnityEngine;

namespace Assets.Script.Business
{
    public interface ITokenBusiness
    {
        /// <summary>
        /// The token of the player follow the cursor if the bool is true
        /// </summary>
        /// <param name="_cursorHasToken">Boolean if the cursor has the token or not</param>
        void FollowCursor(bool cursorHasToken, GameObject token, GameObject cursor);
    }
}

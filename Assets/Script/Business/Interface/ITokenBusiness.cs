using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Business.Interface
{
    internal interface ITokenBusiness
    {
        /// <summary>
        /// The token of the player follow the cursor if the bool is true
        /// </summary>
        /// <param name="_cursorHasToken">Boolean if the cursor has the token or not</param>
        void FollowCursor(bool cursorHasToken, GameObject token, GameObject cursor);
    }
}

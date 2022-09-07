using Assets.Script.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    internal class TokenBusiness : ITokenBusiness
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
